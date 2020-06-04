namespace webSocketClient2Multiconnection
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
            this.PortNumberBox = new System.Windows.Forms.TextBox();
            this.IpAddressBox = new System.Windows.Forms.TextBox();
            this.PiggybackMessageBox = new System.Windows.Forms.TextBox();
            this.TextMessageBox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.ConnectToServer = new System.Windows.Forms.Button();
            this.DisconnectFromServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PortNumberBox
            // 
            this.PortNumberBox.Location = new System.Drawing.Point(340, 45);
            this.PortNumberBox.Name = "PortNumberBox";
            this.PortNumberBox.Size = new System.Drawing.Size(209, 20);
            this.PortNumberBox.TabIndex = 0;
            this.PortNumberBox.TextChanged += new System.EventHandler(this.PortNumberBox_TextChanged);
            // 
            // IpAddressBox
            // 
            this.IpAddressBox.Location = new System.Drawing.Point(31, 45);
            this.IpAddressBox.Name = "IpAddressBox";
            this.IpAddressBox.Size = new System.Drawing.Size(268, 20);
            this.IpAddressBox.TabIndex = 1;
            this.IpAddressBox.TextChanged += new System.EventHandler(this.IpAddressBox_TextChanged);
            // 
            // PiggybackMessageBox
            // 
            this.PiggybackMessageBox.Location = new System.Drawing.Point(31, 233);
            this.PiggybackMessageBox.Name = "PiggybackMessageBox";
            this.PiggybackMessageBox.Size = new System.Drawing.Size(518, 20);
            this.PiggybackMessageBox.TabIndex = 2;
            // 
            // TextMessageBox
            // 
            this.TextMessageBox.Location = new System.Drawing.Point(31, 113);
            this.TextMessageBox.Name = "TextMessageBox";
            this.TextMessageBox.Size = new System.Drawing.Size(518, 20);
            this.TextMessageBox.TabIndex = 3;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(31, 153);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(156, 37);
            this.SendButton.TabIndex = 4;
            this.SendButton.Text = "Send Message";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ConnectToServer
            // 
            this.ConnectToServer.Location = new System.Drawing.Point(224, 153);
            this.ConnectToServer.Name = "ConnectToServer";
            this.ConnectToServer.Size = new System.Drawing.Size(158, 37);
            this.ConnectToServer.TabIndex = 5;
            this.ConnectToServer.Text = "Connect to server";
            this.ConnectToServer.UseVisualStyleBackColor = true;
            this.ConnectToServer.Click += new System.EventHandler(this.ConnectToServer_Click);
            // 
            // DisconnectFromServer
            // 
            this.DisconnectFromServer.Location = new System.Drawing.Point(418, 153);
            this.DisconnectFromServer.Name = "DisconnectFromServer";
            this.DisconnectFromServer.Size = new System.Drawing.Size(131, 37);
            this.DisconnectFromServer.TabIndex = 6;
            this.DisconnectFromServer.Text = "Disconnect from server";
            this.DisconnectFromServer.UseVisualStyleBackColor = true;
            this.DisconnectFromServer.Click += new System.EventHandler(this.DisconnectFromServer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Port Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Piggyback Data Text";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Text Mesage";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ip Address";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 273);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DisconnectFromServer);
            this.Controls.Add(this.ConnectToServer);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.TextMessageBox);
            this.Controls.Add(this.PiggybackMessageBox);
            this.Controls.Add(this.IpAddressBox);
            this.Controls.Add(this.PortNumberBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PortNumberBox;
        private System.Windows.Forms.TextBox IpAddressBox;
        private System.Windows.Forms.TextBox PiggybackMessageBox;
        private System.Windows.Forms.TextBox TextMessageBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Button ConnectToServer;
        private System.Windows.Forms.Button DisconnectFromServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

