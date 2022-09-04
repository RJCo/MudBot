/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: ChangePassphrase.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using Granados.SSHCV2;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// ChangePassphrase
    /// </summary>
    internal class ChangePassphrase : Form
    {
        private Label _lKeyFile;
        private TextBox _tKeyFile;
        private Button _selectKeyFile;
        private Label _lCurrentPassphrase;
        private TextBox _tCurrentPassphrase;
        private Label _lNewPassphrase;
        private TextBox _tNewPassphrase;
        private Label _lNewPassphraseAgain;
        private TextBox _tNewPassphraseAgain;
        private Button _okButton;
        private Button _cancelButton;

        private Container components = null;

        public ChangePassphrase()
        {
            //
            // Windows
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent
            //
            _lKeyFile.Text = "Private Key &File";
            _lCurrentPassphrase.Text = "&Current Passphrase";
            _lNewPassphrase.Text = "&New Passphrase";
            _lNewPassphraseAgain.Text = "Input &Again";
            _okButton.Text = "OK";
            _cancelButton.Text = "Cancel";
            Text = "Change Passphrase";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            _lKeyFile = new Label();
            _tKeyFile = new TextBox();
            _selectKeyFile = new Button();
            _tCurrentPassphrase = new TextBox();
            _lCurrentPassphrase = new Label();
            _tNewPassphrase = new TextBox();
            _lNewPassphrase = new Label();
            _tNewPassphraseAgain = new TextBox();
            _lNewPassphraseAgain = new Label();
            _okButton = new Button();
            _cancelButton = new Button();
            SuspendLayout();

            // 
            // _lKeyFile
            // 
            _lKeyFile.ImageAlign = ContentAlignment.MiddleLeft;
            _lKeyFile.Location = new Point(8, 8);
            _lKeyFile.Name = "_lKeyFile";
            _lKeyFile.Size = new Size(112, 16);
            _lKeyFile.TabIndex = 0;
            // 
            // _tKeyFile
            // 
            _tKeyFile.Location = new Point(136, 8);
            _tKeyFile.Name = "_tKeyFile";
            _tKeyFile.Size = new Size(120, 19);
            _tKeyFile.TabIndex = 1;
            // 
            // _selectKeyFile
            // 
            _selectKeyFile.Location = new Point(264, 8);
            _selectKeyFile.Name = "_selectKeyFile";
            _selectKeyFile.FlatStyle = FlatStyle.System;
            _selectKeyFile.Size = new Size(19, 19);
            _selectKeyFile.TabIndex = 2;
            _selectKeyFile.Text = "...";
            _selectKeyFile.Click += new EventHandler(OpenKeyFile);
            // 
            // _lCurrentPassphrase
            // 
            _lCurrentPassphrase.ImageAlign = ContentAlignment.MiddleLeft;
            _lCurrentPassphrase.Location = new Point(8, 32);
            _lCurrentPassphrase.Name = "_lCurrentPassphrase";
            _lCurrentPassphrase.Size = new Size(120, 16);
            _lCurrentPassphrase.TabIndex = 3;
            // 
            // _tCurrentPassphrase
            // 
            _tCurrentPassphrase.Location = new Point(136, 32);
            _tCurrentPassphrase.Name = "_tCurrentPassphrase";
            _tCurrentPassphrase.PasswordChar = '*';
            _tCurrentPassphrase.Size = new Size(152, 19);
            _tCurrentPassphrase.TabIndex = 4;
            // 
            // _lNewPassphrase
            // 
            _lNewPassphrase.ImageAlign = ContentAlignment.MiddleLeft;
            _lNewPassphrase.Location = new Point(8, 56);
            _lNewPassphrase.Name = "_lNewPassphrase";
            _lNewPassphrase.Size = new Size(120, 16);
            _lNewPassphrase.TabIndex = 5;
            // 
            // _tNewPassphrase
            // 
            _tNewPassphrase.Location = new Point(136, 56);
            _tNewPassphrase.Name = "_tNewPassphrase";
            _tNewPassphrase.PasswordChar = '*';
            _tNewPassphrase.Size = new Size(152, 19);
            _tNewPassphrase.TabIndex = 6;
            // 
            // _lNewPassphraseAgain
            // 
            _lNewPassphraseAgain.ImageAlign = ContentAlignment.MiddleLeft;
            _lNewPassphraseAgain.Location = new Point(8, 80);
            _lNewPassphraseAgain.Name = "_lNewPassphraseAgain";
            _lNewPassphraseAgain.Size = new Size(120, 16);
            _lNewPassphraseAgain.TabIndex = 7;
            // 
            // _tNewPassphraseAgain
            // 
            _tNewPassphraseAgain.Location = new Point(136, 80);
            _tNewPassphraseAgain.Name = "_tNewPassphraseAgain";
            _tNewPassphraseAgain.PasswordChar = '*';
            _tNewPassphraseAgain.Size = new Size(152, 19);
            _tNewPassphraseAgain.TabIndex = 8;
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(152, 112);
            _okButton.Name = "_okButton";
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.Size = new Size(64, 24);
            _okButton.TabIndex = 9;
            _okButton.Click += new EventHandler(OnOK);
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(224, 112);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.Size = new Size(64, 23);
            _cancelButton.TabIndex = 10;
            // 
            // ChangePassphrase
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(292, 141);
            Controls.AddRange(new Control[] {
                                                                          _cancelButton,
                                                                          _okButton,
                                                                          _tNewPassphraseAgain,
                                                                          _lNewPassphraseAgain,
                                                                          _tNewPassphrase,
                                                                          _lNewPassphrase,
                                                                          _tCurrentPassphrase,
                                                                          _lCurrentPassphrase,
                                                                          _selectKeyFile,
                                                                          _tKeyFile,
                                                                          _lKeyFile});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangePassphrase";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        private void OpenKeyFile(object sender, EventArgs args)
        {
            string fn = GCUtil.SelectPrivateKeyFileByDialog(this);
            if (fn != null)
            {
                _tKeyFile.Text = fn;
            }
        }

        private void OnOK(object sender, EventArgs args)
        {
            DialogResult = DialogResult.None;

            try
            {
                SSH2UserAuthKey key = SSH2UserAuthKey.FromSECSHStyleFile(_tKeyFile.Text, _tCurrentPassphrase.Text);
                if (_tNewPassphrase.Text != _tNewPassphraseAgain.Text)
                {
                    GUtil.Warning(this, "The new passphrase does not match the confirmation input.");
                }
                else
                {
                    if (_tNewPassphrase.Text.Length > 0 || GUtil.AskUserYesNo(this, "The new passphrase is empty. Do you wish to leave the passphrase empty?") == DialogResult.Yes)
                    {
                        FileStream s = new FileStream(_tKeyFile.Text, FileMode.Create);
                        key.WritePrivatePartInSECSHStyleFile(s, "", _tNewPassphrase.Text);
                        s.Close();
                        GUtil.Warning(this, "The passphrase has been changed.", MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                GUtil.Warning(this, ex.Message);
            }
        }
    }
}
