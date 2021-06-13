
namespace SpyW4r3
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTextBox = new System.Windows.Forms.RichTextBox();
            this.sendBox = new System.Windows.Forms.TextBox();
            this.sendText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ipSend = new System.Windows.Forms.TextBox();
            this.portSend = new System.Windows.Forms.TextBox();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.requestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainTextBox
            // 
            this.mainTextBox.Location = new System.Drawing.Point(138, 10);
            this.mainTextBox.Name = "mainTextBox";
            this.mainTextBox.ReadOnly = true;
            this.mainTextBox.Size = new System.Drawing.Size(650, 373);
            this.mainTextBox.TabIndex = 0;
            this.mainTextBox.Text = "";
            // 
            // sendBox
            // 
            this.sendBox.Location = new System.Drawing.Point(183, 404);
            this.sendBox.Name = "sendBox";
            this.sendBox.Size = new System.Drawing.Size(452, 23);
            this.sendBox.TabIndex = 1;
            // 
            // sendText
            // 
            this.sendText.AutoSize = true;
            this.sendText.Location = new System.Drawing.Point(138, 407);
            this.sendText.Name = "sendText";
            this.sendText.Size = new System.Drawing.Size(39, 15);
            this.sendText.TabIndex = 2;
            this.sendText.Text = "Send :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(12, 46);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(86, 23);
            this.portTextBox.TabIndex = 6;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 75);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(118, 23);
            this.connectButton.TabIndex = 7;
            this.connectButton.Text = "Start Listener";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(649, 403);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 8;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "IP Send Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Send Port";
            // 
            // ipSend
            // 
            this.ipSend.Location = new System.Drawing.Point(13, 167);
            this.ipSend.Name = "ipSend";
            this.ipSend.Size = new System.Drawing.Size(100, 23);
            this.ipSend.TabIndex = 11;
            // 
            // portSend
            // 
            this.portSend.Location = new System.Drawing.Point(13, 225);
            this.portSend.Name = "portSend";
            this.portSend.Size = new System.Drawing.Size(100, 23);
            this.portSend.TabIndex = 12;
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(13, 345);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(100, 23);
            this.usernameBox.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 324);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "Username";
            // 
            // requestButton
            // 
            this.requestButton.Location = new System.Drawing.Point(12, 255);
            this.requestButton.Name = "requestButton";
            this.requestButton.Size = new System.Drawing.Size(75, 23);
            this.requestButton.TabIndex = 16;
            this.requestButton.Text = "Connect";
            this.requestButton.UseVisualStyleBackColor = true;
            this.requestButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.requestButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.portSend);
            this.Controls.Add(this.ipSend);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sendText);
            this.Controls.Add(this.sendBox);
            this.Controls.Add(this.mainTextBox);
            this.Name = "FrmMain";
            this.Text = "SpyW4r3 - Messaging";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox mainTextBox;
        private System.Windows.Forms.Label sendText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox sendBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ipSend;
        private System.Windows.Forms.TextBox portSend;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button requestButton;
    }
}

