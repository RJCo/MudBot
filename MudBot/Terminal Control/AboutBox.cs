/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: AboutBox.cs,v 1.2 2005/04/20 08:44:12 okajima Exp $
*/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// AboutBox
    /// </summary>
    internal sealed class AboutBox : Form
    {
        //private Image _bgImage;
        private Button _okButton;
        private TextBox _versionText;
        private readonly Container _components = null;
        private PictureBox _pictureBox;
        private PictureBox _guevaraPicture;
        private Button _creditButton;

        public AboutBox()
        {
            InitializeComponent();

            Text = "About Poderosa";
            _okButton.Text = "OK";
            _creditButton.Text = "Credit...";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutBox));
            _okButton = new Button();
            _versionText = new TextBox();
            _pictureBox = new PictureBox();
            _guevaraPicture = new PictureBox();
            _creditButton = new Button();
            SuspendLayout();

            // 
            // _okButton
            // 
            _okButton.BackColor = SystemColors.Control;
            _okButton.DialogResult = DialogResult.OK;
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.Location = new Point(176, 192);
            _okButton.Name = "_okButton";
            _okButton.Size = new Size(88, 23);
            _okButton.TabIndex = 0;

            // 
            // _versionText
            // 
            _versionText.BackColor = SystemColors.Window;
            _versionText.Location = new Point(0, 88);
            _versionText.Multiline = true;
            _versionText.Name = "_versionText";
            _versionText.ReadOnly = true;
            _versionText.Size = new Size(280, 96);
            _versionText.TabIndex = 2;
            _versionText.Text = "";

            // 
            // _pictureBox
            // 
            _pictureBox.Image = ((Image)(resources.GetObject("_pictureBox.Image")));
            _pictureBox.Location = new Point(0, 0);
            _pictureBox.Name = "_pictureBox";
            _pictureBox.Size = new Size(280, 88);
            _pictureBox.TabIndex = 3;
            _pictureBox.TabStop = false;

            // 
            // _creditButton
            // 
            _creditButton.BackColor = SystemColors.Control;
            _creditButton.FlatStyle = FlatStyle.System;
            _creditButton.Location = new Point(8, 192);
            _creditButton.Name = "_creditButton";
            _creditButton.Size = new Size(88, 23);
            _creditButton.TabIndex = 5;
            _creditButton.Click += new EventHandler(OnCreditButton);

            // 
            // AboutBox
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _okButton;
            ClientSize = new Size(282, 224);
            Controls.Add(_creditButton);
            Controls.Add(_pictureBox);
            Controls.Add(_versionText);
            Controls.Add(_guevaraPicture);
            Controls.Add(_okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Load += new EventHandler(OnLoad);
            ResumeLayout(false);

        }
        #endregion
        
        private void OnLoad(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] s = new string[5];
            s[0] = "Terminal Emulator <Poderosa>";
            s[1] = "Copyright(c) 2005 Poderosa Project. All Rights Reserved.";
            s[2] = "";
            object[] t = asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            s[3] = " Version : " + ((AssemblyDescriptionAttribute)t[0]).Description;
            s[4] = " CLR     : " + Environment.Version;
            _versionText.Lines = s;
        }

        protected override bool ProcessDialogChar(char charCode)
        {
            if ('A' <= charCode && charCode <= 'Z')
            {
                charCode = (char)('a' + charCode - 'A');
            }

            return base.ProcessDialogChar(charCode);
        }

        private void OnCreditButton(object sender, EventArgs args)
        {
            CreditButtonClicked = true;
            Close();
        }
        public bool CreditButtonClicked { get; private set; }
    }
}
