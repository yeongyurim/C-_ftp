namespace solace_vs2015_transmit
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
            this.source = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lable = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.transmit = new System.Windows.Forms.Button();
            this.host_ = new System.Windows.Forms.TextBox();
            this.user_name = new System.Windows.Forms.TextBox();
            this.password_ = new System.Windows.Forms.TextBox();
            this.sourcePath = new System.Windows.Forms.TextBox();
            this.vpn_name = new System.Windows.Forms.TextBox();
            this.Log_Box = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // source
            // 
            this.source.Location = new System.Drawing.Point(21, 12);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(75, 23);
            this.source.TabIndex = 0;
            this.source.Text = "소스폴더";
            this.source.UseVisualStyleBackColor = true;
            this.source.Click += new System.EventHandler(this.source_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Host";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "User name";
            // 
            // lable
            // 
            this.lable.AutoSize = true;
            this.lable.Location = new System.Drawing.Point(19, 80);
            this.lable.Name = "lable";
            this.lable.Size = new System.Drawing.Size(66, 12);
            this.lable.TabIndex = 3;
            this.lable.Text = "VPN name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(285, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password";
            // 
            // transmit
            // 
            this.transmit.Location = new System.Drawing.Point(552, 12);
            this.transmit.Name = "transmit";
            this.transmit.Size = new System.Drawing.Size(75, 86);
            this.transmit.TabIndex = 5;
            this.transmit.Text = "보내기";
            this.transmit.UseVisualStyleBackColor = true;
            this.transmit.Click += new System.EventHandler(this.btntransmit);
            // 
            // host_
            // 
            this.host_.Location = new System.Drawing.Point(91, 50);
            this.host_.Name = "host_";
            this.host_.Size = new System.Drawing.Size(188, 21);
            this.host_.TabIndex = 8;
            this.host_.Text = "192.168.10.123:55554";
            // 
            // user_name
            // 
            this.user_name.Location = new System.Drawing.Point(358, 50);
            this.user_name.Name = "user_name";
            this.user_name.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.user_name.Size = new System.Drawing.Size(188, 21);
            this.user_name.TabIndex = 10;
            this.user_name.Text = "yeonpub";
            // 
            // password_
            // 
            this.password_.Location = new System.Drawing.Point(358, 77);
            this.password_.Name = "password_";
            this.password_.Size = new System.Drawing.Size(188, 21);
            this.password_.TabIndex = 11;
            this.password_.Text = "edcore";
            // 
            // sourcePath
            // 
            this.sourcePath.Location = new System.Drawing.Point(102, 12);
            this.sourcePath.Name = "sourcePath";
            this.sourcePath.Size = new System.Drawing.Size(444, 21);
            this.sourcePath.TabIndex = 12;
            // 
            // vpn_name
            // 
            this.vpn_name.Location = new System.Drawing.Point(91, 77);
            this.vpn_name.Name = "vpn_name";
            this.vpn_name.Size = new System.Drawing.Size(188, 21);
            this.vpn_name.TabIndex = 14;
            this.vpn_name.Text = "yeon";
            // 
            // Log_Box
            // 
            this.Log_Box.Location = new System.Drawing.Point(21, 104);
            this.Log_Box.Name = "Log_Box";
            this.Log_Box.Size = new System.Drawing.Size(606, 158);
            this.Log_Box.TabIndex = 15;
            this.Log_Box.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 274);
            this.Controls.Add(this.Log_Box);
            this.Controls.Add(this.vpn_name);
            this.Controls.Add(this.sourcePath);
            this.Controls.Add(this.password_);
            this.Controls.Add(this.user_name);
            this.Controls.Add(this.host_);
            this.Controls.Add(this.transmit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.source);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button source;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button transmit;
        private System.Windows.Forms.TextBox host_;
        private System.Windows.Forms.TextBox user_name;
        private System.Windows.Forms.TextBox password_;
        private System.Windows.Forms.TextBox sourcePath;
        private System.Windows.Forms.TextBox vpn_name;
        private System.Windows.Forms.RichTextBox Log_Box;
    }
}

