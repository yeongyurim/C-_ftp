namespace solace_vs2015_receiver
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.destine = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.host_ = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.vpn_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.user_name = new System.Windows.Forms.TextBox();
            this.password_ = new System.Windows.Forms.TextBox();
            this.destinePath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Log_Box = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // destine
            // 
            this.destine.Location = new System.Drawing.Point(12, 18);
            this.destine.Name = "destine";
            this.destine.Size = new System.Drawing.Size(75, 23);
            this.destine.TabIndex = 0;
            this.destine.Text = "목적버튼";
            this.destine.UseVisualStyleBackColor = true;
            this.destine.Click += new System.EventHandler(this.destine_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Host";
            // 
            // host_
            // 
            this.host_.Location = new System.Drawing.Point(96, 45);
            this.host_.Name = "host_";
            this.host_.Size = new System.Drawing.Size(183, 21);
            this.host_.TabIndex = 2;
            this.host_.Text = "192.168.10.10:55555";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "VPN name";
            // 
            // vpn_name
            // 
            this.vpn_name.Location = new System.Drawing.Point(96, 72);
            this.vpn_name.Name = "vpn_name";
            this.vpn_name.Size = new System.Drawing.Size(183, 21);
            this.vpn_name.TabIndex = 4;
            this.vpn_name.Text = "yeon";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(285, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "User name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(285, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // user_name
            // 
            this.user_name.Location = new System.Drawing.Point(371, 45);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(178, 21);
            this.user_name.TabIndex = 7;
            this.user_name.Text = "yeonconsume";
            // 
            // password_
            // 
            this.password_.Location = new System.Drawing.Point(371, 72);
            this.password_.Name = "password_";
            this.password_.Size = new System.Drawing.Size(178, 21);
            this.password_.TabIndex = 8;
            this.password_.Text = "dusrbfla1";
            // 
            // destinePath
            // 
            this.destinePath.Location = new System.Drawing.Point(96, 18);
            this.destinePath.Name = "destinePath";
            this.destinePath.Size = new System.Drawing.Size(453, 21);
            this.destinePath.TabIndex = 9;
            this.destinePath.Text = "C:\\Users\\EDCORE\\Desktop\\연규림_작업폴더\\테스트\\C-\\목적폴더";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(567, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 36);
            this.button2.TabIndex = 10;
            this.button2.Text = "받기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnreceive);
            // 
            // Log_Box
            // 
            this.Log_Box.Location = new System.Drawing.Point(12, 99);
            this.Log_Box.Name = "Log_Box";
            this.Log_Box.Size = new System.Drawing.Size(630, 150);
            this.Log_Box.TabIndex = 11;
            this.Log_Box.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(567, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 12;
            this.button1.Text = "중지";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnStop);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Log_Box);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.destinePath);
            this.Controls.Add(this.password_);
            this.Controls.Add(this.user_name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vpn_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.host_);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.destine);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button destine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox host_;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox vpn_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox user_name;
        private System.Windows.Forms.TextBox password_;
        private System.Windows.Forms.TextBox destinePath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox Log_Box;
        private System.Windows.Forms.Button button1;
    }
}

