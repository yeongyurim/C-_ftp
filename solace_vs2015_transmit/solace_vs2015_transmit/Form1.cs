using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SolaceSystems.Solclient.Messaging;

namespace solace_vs2015_transmit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        string VPNName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }

        const int DefaultReconnectRetries = 3;
        static EventWaitHandle waitHandle = new AutoResetEvent(false);
        static int TM;
        bool WaitHandleBool = false;
        void Run(IContext context, string host)
        {
            TM = 0;

            // 파라미터 검증
            if (context == null)
            {
                MessageBox.Show("컨텍스트가 올바르지 않습니다!", "오류상자", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(host))
            {
                MessageBox.Show("host가 올바르지 않습니다!", "오류상자", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(VPNName))
            {
                MessageBox.Show("VPN name이 올바르지 않습니다!", "오류상자", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(UserName))
            {
                MessageBox.Show("USER name이 올바르지 않습니다!", "오류상자", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(sourcePath.Text)){
                MessageBox.Show("파일 경로가 올바르지 않습니다!","오류상자", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SessionProperties sessionProps = new SessionProperties()
            {
                Host = host,
                VPNName = VPNName,
                UserName = UserName,
                Password = Password,
                ReconnectRetries = DefaultReconnectRetries
            };

            Log_Box.Invoke(new logcall(logAppend), "연결을 시도합니다 " + UserName + "@" + VPNName + " on " + host + "...\n");
            using (ISession session = context.CreateSession(sessionProps, null, null))
            {
                ReturnCode returnCode = session.Connect();
                if (returnCode == ReturnCode.SOLCLIENT_OK)
                {
                    Log_Box.Invoke(new logcall(logAppend), "세션 연결 성공..!\n");
                    MessagePreprocess(sourcePath.Text, session);
                    Log_Box.Invoke(new logcall(logAppend), TM + "개 전송 완료되었습니다.\n");
                }
                else
                {
                    Log_Box.Invoke(new logcall(logAppend), "문제 발생, 오류 코드는 " + returnCode + "입니다\n");
                }
            }
            WaitHandleBool = true;
            waitHandle.WaitOne();
        }
        void MessagePreprocess(string sdir,ISession session)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(sdir);
                if (dirs.Length > 0)
                {
                    foreach (string dir in dirs)
                    {
                        MessagePreprocess(dir,session);
                    }
                }
                string prefix = sdir.Replace(sourcePath.Text, "");
                PublicMessage(prefix,session);
            }
            catch
            {
                MessageBox.Show("폴더 전처리 작업중 문제가 발생했습니다!");
            }
        }
        private void PublicMessage(string prefix,ISession session)
        {
            //메시지 만들기
            using (IMessage message = ContextFactory.Instance.CreateMessage())
            {
                // 메시지 목적지 및 전송 모드 설정
                message.Destination = ContextFactory.Instance.CreateTopic("queue/topic");
                message.DeliveryMode = MessageDeliveryMode.Persistent;
                string sourPath = sourcePath.Text + prefix;
                string[] files = Directory.GetFiles(sourPath);
                for (int i = 0; i < files.Length; i++)
                {
                    string file = files[i];
                    var info = new FileInfo(file);

                    // 파일 크기가 일정 초과 일때
                    if (info.Length > 1 * 1024 * 1024)
                    {
                        Stream inStream = new FileStream(file, FileMode.Open);
                        int fIleNum = (int)info.Length / (1 * 1024 * 1024);
                        for (int j = 0; j <= fIleNum; j++)
                        {
                            int bytesRead = 0;
                            MemoryStream ms = new MemoryStream();
                            byte[] buffer = new byte[1 * 1024 * 1024];
                            string fileName = info.Name +"@"+ j + ".tmp";

                            if ((bytesRead = inStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, bytesRead);
                                message.CreateUserPropertyMap();
                                message.UserPropertyMap.AddString("prefix", prefix);
                                message.UserPropertyMap.AddString("name", fileName);
                                message.UserPropertyMap.AddInt32("TM", fIleNum);
                                message.UserPropertyMap.AddBool("frag", true);
                                message.BinaryAttachment = ms.ToArray();
                                ms.Close();
                                ms.Dispose();

                                // 파일 전송
                                ReturnCode returnCode = session.Send(message);
                                if (returnCode == ReturnCode.SOLCLIENT_OK)
                                {
                                    Log_Box.Invoke(new logcall(logAppend), fileName + "파일 전송 완료.\n");
                                    TM++;
                                }
                                else
                                {
                                    Log_Box.Invoke(new logcall(logAppend), "메시지 전송에 실패하였습니다 오류 코드는 " + returnCode + "입니다.\n");
                                }
                            }
                        }
                        inStream.Close();
                        inStream.Dispose();
                    }
                    // 파일 크기가 일정 이하 일때
                    else
                    {
                        Stream inStream = new FileStream(file, FileMode.Open);
                        MemoryStream ms = new MemoryStream();
                        string filename = info.Name;
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while ((bytesRead = inStream.Read(buffer, 0, buffer.Length)) > 0) ms.Write(buffer, 0, bytesRead);
                        inStream.Close();
                        inStream.Dispose();

                        message.CreateUserPropertyMap();
                        message.UserPropertyMap.AddString("prefix",prefix);
                        message.UserPropertyMap.AddString("name", filename);
                        message.UserPropertyMap.AddBool("frag",false);
                        message.BinaryAttachment = ms.ToArray();
                        ms.Close();
                        ms.Dispose();

                        ReturnCode returnCode = session.Send(message);
                        if (returnCode == ReturnCode.SOLCLIENT_OK)
                        {
                            Log_Box.Invoke(new logcall(logAppend), filename + "파일 전송 완료.\n");
                            TM++;
                        }
                        else
                        {
                            Log_Box.Invoke(new logcall(logAppend), "메시지 전송에 실패하였습니다 오류 코드는 " + returnCode + "입니다.\n");
                        }
                    }
                }
            }
        }
        void source_click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                sourcePath.Text = dialog.SelectedPath;
            }
        }
        void Send()
        {
            string host = host_.Text;
            UserName = user_name.Text;
            VPNName = vpn_name.Text;
            Password = password_.Text;

            ContextFactoryProperties cfp = new ContextFactoryProperties()
            {
                SolClientLogLevel = SolLogLevel.Warning
            };
            cfp.LogToConsoleError();
            ContextFactory.Instance.Init(cfp);
            try
            {
                using (IContext context = ContextFactory.Instance.CreateContext(new ContextProperties(), null))
                {
                    Run(context, host);
                }
            }
            catch (Exception ex)
            {
                Log_Box.Invoke(new logcall(logAppend), "예외 발생 :" + ex.Message + "\n");
            }
            finally
            {
                // Dispose Solace Systems Messaging API
                ContextFactory.Instance.Cleanup();
            }
        }
        void logAppend(string log)
        {
            Log_Box.AppendText(log);
            Log_Box.ScrollToCaret();
        }
        delegate void logcall(string log);
        private void btntransmit(object sender, EventArgs e)
        {
            if (WaitHandleBool)
            {
                waitHandle.Set();
            }
            if (sourcePath.Text == "")
            {
                MessageBox.Show("경로를 지정해주세요");
                return;
            }
            Thread send = new Thread(Send);
            send.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            waitHandle.Set();
        }
    }
}
