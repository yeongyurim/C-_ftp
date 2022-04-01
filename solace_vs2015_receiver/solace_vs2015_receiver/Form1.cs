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

namespace solace_vs2015_receiver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static EventWaitHandle waitHandle = new AutoResetEvent(false);

        FolderBrowserDialog dialog = new FolderBrowserDialog();

        string VPNName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }

        const int DefaultReconnectRetries = 3;

        int receiveC;
        int msgC;

        private ISession Session = null;
        private IQueue Queue = null;
        private IFlow Flow = null;
        private EventWaitHandle WaitEventWaitHandle = new AutoResetEvent(false);
        void Run(IContext context, string host)
        {
            receiveC = 0;
            msgC = 0;

            // 파라미터 검증
            if (context == null)
            {
                throw new ArgumentException("Solace Systems API context Router must be not null.", "context");
            }
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new ArgumentException("Solace Messaging Router host name must be non-empty.", "host");
            }
            if (string.IsNullOrWhiteSpace(VPNName))
            {
                throw new InvalidOperationException("VPN name must be non-empty.");
            }
            if (string.IsNullOrWhiteSpace(UserName))
            {
                throw new InvalidOperationException("Client username must be non-empty.");
            }

            //세션 속성 생성
            SessionProperties sessionProps = new SessionProperties()
            {
                Host = host,
                VPNName = VPNName,
                UserName = UserName,
                Password = Password,
                ReconnectRetries = DefaultReconnectRetries
            };

            // 솔라스 라우터 연결
            Log_Box.Invoke(new logcall(logAppend), "연결을 시도합니다 " + UserName + "@" + VPNName + " on " + host + "...\n");
            Session = context.CreateSession(sessionProps, null, null);
            ReturnCode returnCode = Session.Connect();
            if (returnCode == ReturnCode.SOLCLIENT_OK)
            {
                Log_Box.Invoke(new logcall(logAppend), "세션 연결 성공..!\n");

                // 큐 이름 설정
                string queueName = "YeonQueue";
                Log_Box.Invoke(new logcall(logAppend), queueName + "큐 생성 시도 중...\n");

                // 큐 권한 설정
                EndpointProperties endpointProps = new EndpointProperties()
                {
                    Permission = EndpointProperties.EndpointPermission.Delete,
                    AccessType = EndpointProperties.EndpointAccessType.Exclusive
                };
                // 큐 생성
                Queue = ContextFactory.Instance.CreateQueue(queueName);

                // 큐 세션에 공급
                Session.Provision(Queue, endpointProps,
                     ProvisionFlag.IgnoreErrorIfEndpointAlreadyExists | ProvisionFlag.WaitForConfirm, null);
                Log_Box.Invoke(new logcall(logAppend), queueName + "큐 생성 및 공급완료..!\n");

                // Flow 생성 및 큐 연결 및 message,flow event handler 연결  
                Flow = Session.CreateFlow(new FlowProperties()
                {
                    AckMode = MessageAckMode.ClientAck
                },
                Queue, null, HandleMessageEvent, HandleFlowEvent);
                Flow.Start();
                Log_Box.Invoke(new logcall(logAppend), queueName + "큐에서 메시지 수신 대기 중...\n");
                waitHandle.WaitOne();
            }
            else
            {
                Log_Box.Invoke(new logcall(logAppend), "문제 발생, 오류 코드는 " + returnCode + "입니다\n");
            }
        }

        //API로 부터 이벤트 도착시마다 실행됌
        private void HandleMessageEvent(object source, MessageEventArgs args)
        {
            // 메시지 수신   
            using (IMessage message = args.Message)
            {
                string FileName = message.UserPropertyMap.GetString("name");
                string destPath = destinePath.Text+"\\"+message.UserPropertyMap.GetString("prefix");
                destPath = destPath.Replace("\\\\", "\\");
                if (!Directory.Exists(destPath))
                {
                    Directory.CreateDirectory(destPath);
                    Log_Box.Invoke(new logcall(logAppend), destPath + "폴더 생성\n");
                }
                    string destineFileName = System.IO.Path.Combine(destPath, FileName);
                byte[] buffer = new byte[1 * 1024 * 1024];
                buffer = message.BinaryAttachment;
                FileStream outStream = new FileStream(destineFileName, FileMode.Create, FileAccess.Write);
                if(buffer != null)
                {
                    outStream.Write(buffer, 0, buffer.Length);
                    outStream.Close();
                }
                Log_Box.Invoke(new logcall(logAppend),destineFileName+"파일 생성\n");

                Flow.Ack(message.ADMessageId);
                if (message.UserPropertyMap.GetBool("frag")) 
                {
                    string baseFileName = FileName.Substring(0, FileName.LastIndexOf(Convert.ToChar("@")));

                    if (Directory.GetFiles(destPath, baseFileName + "*.tmp").Length > message.UserPropertyMap.GetInt32("TM"))
                    {
                        assemble(baseFileName,destPath);
                    }
                }
                if (!message.UserPropertyMap.GetBool("frag"))
                {
                    receiveC++;
                }
                msgC++;
            }
        }



        public void HandleFlowEvent(object sender, FlowEventArgs args)
        {
            // Received a flow event
            Log_Box.Invoke(new logcall(logAppend), "Received Flow Event "+args.Event+"Type: "+args.ResponseCode.ToString()+ " Text: "+ args.Info +"\n");
        }

        void assemble(string bsm,string destPath)
        {
            // tmp 파일 분류

            string tmpname = bsm;
            string[] tmpfiles = Directory.GetFiles(destPath, tmpname + "*.tmp");
            FileStream outputFile = null;
            string PrevFileName = "";
            foreach (string tmpfile in tmpfiles)
            {
                string FileName = tmpfile.Substring(0, tmpfile.LastIndexOf(Convert.ToChar("@")));
                string baseFileName = Path.GetFileNameWithoutExtension(FileName);
                string extension = Path.GetExtension(FileName);

                if (!PrevFileName.Equals(baseFileName))
                {
                    if (outputFile != null)
                    {
                        outputFile.Flush();
                        outputFile.Close();
                    }
                    string destineFileName = System.IO.Path.Combine(destPath, baseFileName + extension);
                    outputFile = new FileStream(destineFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    Log_Box.Invoke(new logcall(logAppend), destineFileName + "파일 생성\n");
                }

                int bytesRead = 0;
                byte[] buffer = new byte[1024];
                FileStream inputTempFile = new FileStream(tmpfile, FileMode.OpenOrCreate, FileAccess.Read);
                while ((bytesRead = inputTempFile.Read(buffer, 0, buffer.Length)) > 0) outputFile.Write(buffer, 0, bytesRead);
                inputTempFile.Close();
                File.Delete(tmpfile);
                Log_Box.Invoke(new logcall(logAppend),tmpfile+ "파일 삭제\n");
                PrevFileName = baseFileName;
            }
            if (outputFile != null)
            {
                outputFile.Close();
            }
            receiveC++;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion


        private void Receive()
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
                Log_Box.Invoke(new logcall(logAppend), "메시지\'" + msgC + "\'개 파일 \'" + receiveC + "\'개 수신 완료.....\n");
            }
        }

        private void destine_click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                destinePath.Text = dialog.SelectedPath;
            }
        }

        void logAppend(string log)
        {
            Log_Box.AppendText(log);
            Log_Box.ScrollToCaret();
        }

        delegate void logcall(string log);

        private void btnreceive(object sender, EventArgs e)
        {
            if (destinePath.Text == "")
            {
                MessageBox.Show("경로를 지정해주세요");
                return;
            }
            Thread receive = new Thread(Receive);
            receive.Start();
        }

        private void btnStop(object sender, EventArgs e)
        {
            waitHandle.Set();
        }
    }
}
