namespace MudBot
{
    partial class MudBot
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
            this.servertextBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.PorttextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.terminalControl = new WalburySoftware.TerminalControl();
            this.DebugButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(224, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // servertextBox1
            // 
            this.servertextBox1.Location = new System.Drawing.Point(12, 35);
            this.servertextBox1.Name = "servertextBox1";
            this.servertextBox1.Size = new System.Drawing.Size(100, 20);
            this.servertextBox1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Server";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(305, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "edit display";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PorttextBox
            // 
            this.PorttextBox.Location = new System.Drawing.Point(118, 35);
            this.PorttextBox.Name = "PorttextBox";
            this.PorttextBox.Size = new System.Drawing.Size(100, 20);
            this.PorttextBox.TabIndex = 9;
            this.PorttextBox.Text = "23";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Port";
            // 
            // terminalControl
            // 
            this.terminalControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.terminalControl.Host = "";
            this.terminalControl.Location = new System.Drawing.Point(12, 62);
            this.terminalControl.Name = "terminalControl";
            this.terminalControl.Port = 23;
            this.terminalControl.Size = new System.Drawing.Size(622, 366);
            this.terminalControl.TabIndex = 0;
            this.terminalControl.Text = "terminalControl";
            // 
            // DebugButton
            // 
            this.DebugButton.Location = new System.Drawing.Point(387, 33);
            this.DebugButton.Name = "DebugButton";
            this.DebugButton.Size = new System.Drawing.Size(75, 23);
            this.DebugButton.TabIndex = 11;
            this.DebugButton.Text = "Debug";
            this.DebugButton.UseVisualStyleBackColor = true;
            this.DebugButton.Click += new System.EventHandler(this.DebugButton_Click);
            // 
            // MudBot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 440);
            this.Controls.Add(this.DebugButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PorttextBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.servertextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.terminalControl);
            this.Name = "MudBot";
            this.Text = "MudBot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WalburySoftware.TerminalControl terminalControl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox servertextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox PorttextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DebugButton;
    }
}

