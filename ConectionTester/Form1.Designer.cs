namespace ConectionTester
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
            this.button1 = new System.Windows.Forms.Button();
            this.Message = new System.Windows.Forms.TextBox();
            this.disconnect = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.ipAddressBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.responseFromServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "Send text";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Message
            // 
            this.Message.Location = new System.Drawing.Point(50, 97);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(443, 20);
            this.Message.TabIndex = 2;
            // 
            // disconnect
            // 
            this.disconnect.Location = new System.Drawing.Point(340, 133);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(153, 34);
            this.disconnect.TabIndex = 4;
            this.disconnect.Text = "Disconnect from server";
            this.disconnect.UseVisualStyleBackColor = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(178, 133);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(145, 34);
            this.Connect.TabIndex = 5;
            this.Connect.Text = "Connect to server";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "text message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Port number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ip Address";
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(303, 32);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(190, 20);
            this.PortBox.TabIndex = 9;
            this.PortBox.TextChanged += new System.EventHandler(this.PortBox_TextChanged);
            // 
            // ipAddressBox
            // 
            this.ipAddressBox.Location = new System.Drawing.Point(53, 31);
            this.ipAddressBox.Name = "ipAddressBox";
            this.ipAddressBox.Size = new System.Drawing.Size(228, 20);
            this.ipAddressBox.TabIndex = 10;
            this.ipAddressBox.TextChanged += new System.EventHandler(this.ipAddressBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = ":";
            // 
            // responseFromServer
            // 
            this.responseFromServer.Location = new System.Drawing.Point(50, 203);
            this.responseFromServer.Name = "responseFromServer";
            this.responseFromServer.Size = new System.Drawing.Size(443, 20);
            this.responseFromServer.TabIndex = 12;
            this.responseFromServer.TextChanged += new System.EventHandler(this.responseFromServer_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Respons from server";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 235);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.responseFromServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ipAddressBox);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.disconnect);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.Button disconnect;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.TextBox ipAddressBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox responseFromServer;
        private System.Windows.Forms.Label label5;
    }
}

