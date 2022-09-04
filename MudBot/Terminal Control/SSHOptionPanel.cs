/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: SSHOptionPanel.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Granados.PKI;
using Granados.SSHC;
using Poderosa.Config;
using Poderosa.SSH;
using Poderosa.UI;
using System;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// SSHOptionPanel の概要の説明です。
    /// </summary>
    internal class SSHOptionPanel : OptionDialog.CategoryPanel
    {
        private string[] _cipherAlgorithmOrder;

        private GroupBox _cipherOrderGroup;
        private ListBox _cipherOrderList;
        private Button _algorithmOrderUp;
        private Button _algorithmOrderDown;
        private GroupBox _ssh2OptionGroup;
        private Label _hostKeyLabel;
        private ComboBox _hostKeyBox;
        private Label _windowSizeLabel;
        private TextBox _windowSizeBox;
        private GroupBox _sshMiscGroup;
        private CheckBox _retainsPassphrase;
        private CheckBox _sshCheckMAC;

        public SSHOptionPanel()
        {
            InitializeComponent();
            FillText();
        }
        private void InitializeComponent()
        {
            _cipherOrderGroup = new GroupBox();
            _cipherOrderList = new ListBox();
            _algorithmOrderUp = new Button();
            _algorithmOrderDown = new Button();
            _ssh2OptionGroup = new GroupBox();
            _hostKeyLabel = new Label();
            _hostKeyBox = new ComboBox();
            _windowSizeLabel = new Label();
            _windowSizeBox = new TextBox();
            _sshMiscGroup = new GroupBox();
            _retainsPassphrase = new CheckBox();
            _sshCheckMAC = new CheckBox();

            _cipherOrderGroup.SuspendLayout();
            _ssh2OptionGroup.SuspendLayout();
            _sshMiscGroup.SuspendLayout();

            Controls.AddRange(new Control[] {
                                                                                   _cipherOrderGroup,
                                                                                   _ssh2OptionGroup,
                                                                                   _sshMiscGroup});
            // 
            // _cipherOrderGroup
            // 
            _cipherOrderGroup.Controls.AddRange(new Control[] {
                                                                                            _cipherOrderList,
                                                                                            _algorithmOrderUp,
                                                                                            _algorithmOrderDown});
            _cipherOrderGroup.Location = new System.Drawing.Point(8, 8);
            _cipherOrderGroup.Name = "_cipherOrderGroup";
            _cipherOrderGroup.FlatStyle = FlatStyle.System;
            _cipherOrderGroup.Size = new System.Drawing.Size(416, 80);
            _cipherOrderGroup.TabIndex = 0;
            _cipherOrderGroup.TabStop = false;
            // 
            // _cipherOrderList
            // 
            _cipherOrderList.ItemHeight = 12;
            _cipherOrderList.Location = new System.Drawing.Point(8, 16);
            _cipherOrderList.Name = "_cipherOrderList";
            _cipherOrderList.Size = new System.Drawing.Size(208, 56);
            _cipherOrderList.TabIndex = 1;
            // 
            // _algorithmOrderUp
            // 
            _algorithmOrderUp.Location = new System.Drawing.Point(232, 16);
            _algorithmOrderUp.Name = "_algorithmOrderUp";
            _algorithmOrderUp.FlatStyle = FlatStyle.System;
            _algorithmOrderUp.TabIndex = 2;
            _algorithmOrderUp.Click += new EventHandler(OnCipherAlgorithmOrderUp);
            // 
            // _algorithmOrderDown
            // 
            _algorithmOrderDown.Location = new System.Drawing.Point(232, 48);
            _algorithmOrderDown.Name = "_algorithmOrderDown";
            _algorithmOrderDown.FlatStyle = FlatStyle.System;
            _algorithmOrderDown.TabIndex = 3;
            _algorithmOrderDown.Click += new EventHandler(OnCipherAlgorithmOrderDown);
            // 
            // _ssh2OptionGroup
            // 
            _ssh2OptionGroup.Controls.AddRange(new Control[] {
                                                                                           _hostKeyLabel,
                                                                                           _hostKeyBox,
                                                                                           _windowSizeLabel,
                                                                                           _windowSizeBox});
            _ssh2OptionGroup.Location = new System.Drawing.Point(8, 96);
            _ssh2OptionGroup.Name = "_ssh2OptionGroup";
            _ssh2OptionGroup.FlatStyle = FlatStyle.System;
            _ssh2OptionGroup.Size = new System.Drawing.Size(416, 80);
            _ssh2OptionGroup.TabIndex = 4;
            _ssh2OptionGroup.TabStop = false;
            // 
            // _hostKeyLabel
            // 
            _hostKeyLabel.Location = new System.Drawing.Point(8, 16);
            _hostKeyLabel.Name = "_hostKeyLabel";
            _hostKeyLabel.Size = new System.Drawing.Size(200, 23);
            _hostKeyLabel.TabIndex = 5;
            _hostKeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _hostKeyBox
            // 
            _hostKeyBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _hostKeyBox.Items.AddRange(new object[] {
                                                             "DSA",
                                                             "RSA"});
            _hostKeyBox.Location = new System.Drawing.Point(224, 16);
            _hostKeyBox.Name = "_hostKeyBox";
            _hostKeyBox.Size = new System.Drawing.Size(121, 20);
            _hostKeyBox.TabIndex = 6;
            // 
            // _windowSizeLabel
            // 
            _windowSizeLabel.Location = new System.Drawing.Point(8, 48);
            _windowSizeLabel.Name = "_windowSizeLabel";
            _windowSizeLabel.Size = new System.Drawing.Size(192, 23);
            _windowSizeLabel.TabIndex = 7;
            _windowSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _windowSizeBox
            // 
            _windowSizeBox.Location = new System.Drawing.Point(224, 48);
            _windowSizeBox.MaxLength = 5;
            _windowSizeBox.Name = "_windowSizeBox";
            _windowSizeBox.Size = new System.Drawing.Size(120, 19);
            _windowSizeBox.TabIndex = 8;
            _windowSizeBox.Text = "0";
            // 
            // _sshMiscGroup
            // 
            _sshMiscGroup.Controls.AddRange(new Control[] {
                                                                                        _sshCheckMAC,
                                                                                        _retainsPassphrase});
            _sshMiscGroup.Location = new System.Drawing.Point(8, 180);
            _sshMiscGroup.Name = "_sshMiscGroup";
            _sshMiscGroup.FlatStyle = FlatStyle.System;
            _sshMiscGroup.Size = new System.Drawing.Size(416, 80);
            _sshMiscGroup.TabIndex = 9;
            _sshMiscGroup.TabStop = false;
            // 
            // _retainsPassphrase
            // 
            _retainsPassphrase.Location = new System.Drawing.Point(8, 14);
            _retainsPassphrase.Name = "_retainsPassphrase";
            _retainsPassphrase.FlatStyle = FlatStyle.System;
            _retainsPassphrase.Size = new System.Drawing.Size(400, 23);
            _retainsPassphrase.TabIndex = 10;
            // 
            // _sshCheckMAC
            // 
            _sshCheckMAC.Location = new System.Drawing.Point(8, 33);
            _sshCheckMAC.Name = "_sshCheckMAC";
            _sshCheckMAC.FlatStyle = FlatStyle.System;
            _sshCheckMAC.Size = new System.Drawing.Size(400, 37);
            _sshCheckMAC.TabIndex = 11;

            BackColor = ThemeUtil.TabPaneBackColor;
            _cipherOrderGroup.ResumeLayout();
            _ssh2OptionGroup.ResumeLayout();
            _sshMiscGroup.ResumeLayout();
        }
        private void FillText()
        {
            _cipherOrderGroup.Text = "Form.OptionDialog._cipherOrderGroup";
            _algorithmOrderUp.Text = "Form.OptionDialog._algorithmOrderUp";
            _algorithmOrderDown.Text = "Form.OptionDialog._algorithmOrderDown";
            _ssh2OptionGroup.Text = "Form.OptionDialog._ssh2OptionGroup";
            _hostKeyLabel.Text = "Form.OptionDialog._hostKeyLabel";
            _windowSizeLabel.Text = "Form.OptionDialog._windowSizeLabel";
            _sshMiscGroup.Text = "Form.OptionDialog._sshMiscGroup";
            _retainsPassphrase.Text = "Form.OptionDialog._retainsPassphrase";
            _sshCheckMAC.Text = "Form.OptionDialog._sshCheckMAC";
        }
        public override void InitUI(ContainerOptions options)
        {
            _cipherOrderList.Items.Clear();
            string[] co = options.CipherAlgorithmOrder;
            foreach (string c in co)
                _cipherOrderList.Items.Add(c);
            _hostKeyBox.SelectedIndex = LocalSSHUtil.ParsePublicKeyAlgorithm(options.HostKeyAlgorithmOrder[0]) == PublicKeyAlgorithm.DSA ? 0 : 1; //これはDSA/RSAのどちらかしかない
            _windowSizeBox.Text = options.SSHWindowSize.ToString();
            _retainsPassphrase.Checked = options.RetainsPassphrase;
            _sshCheckMAC.Checked = options.SSHCheckMAC;
            _cipherAlgorithmOrder = options.CipherAlgorithmOrder;
        }
        public override bool Commit(ContainerOptions options)
        {
            //暗号アルゴリズム順序はoptionsを直接いじっているのでここでは何もしなくてよい
            try
            {
                PublicKeyAlgorithm[] pa = new PublicKeyAlgorithm[2];
                if (_hostKeyBox.SelectedIndex == 0)
                {
                    pa[0] = PublicKeyAlgorithm.DSA;
                    pa[1] = PublicKeyAlgorithm.RSA;
                }
                else
                {
                    pa[0] = PublicKeyAlgorithm.RSA;
                    pa[1] = PublicKeyAlgorithm.DSA;
                }
                options.HostKeyAlgorithmOrder = LocalSSHUtil.FormatPublicKeyAlgorithmList(pa);

                try
                {
                    options.SSHWindowSize = Int32.Parse(_windowSizeBox.Text);
                }
                catch (FormatException)
                {
                    GUtil.Warning(this, "Message.OptionDialog.InvalidWindowSize");
                    return false;
                }

                options.RetainsPassphrase = _retainsPassphrase.Checked;
                options.SSHCheckMAC = _sshCheckMAC.Checked;
                options.CipherAlgorithmOrder = _cipherAlgorithmOrder;

                return true;
            }
            catch (Exception ex)
            {
                GUtil.Warning(this, ex.Message);
                return false;
            }
        }

        //SSHオプション関係
        private void OnCipherAlgorithmOrderUp(object sender, EventArgs args)
        {
            int i = _cipherOrderList.SelectedIndex;
            if (i == -1 || i == 0) return; //選択されていないか既にトップなら何もしない

            string temp1 = _cipherAlgorithmOrder[i];
            _cipherAlgorithmOrder[i] = _cipherAlgorithmOrder[i - 1];
            _cipherAlgorithmOrder[i - 1] = temp1;

            object temp2 = _cipherOrderList.SelectedItem;
            _cipherOrderList.Items.RemoveAt(i);
            _cipherOrderList.Items.Insert(i - 1, temp2);

            _cipherOrderList.SelectedIndex = i - 1;
        }
        private void OnCipherAlgorithmOrderDown(object sender, EventArgs args)
        {
            int i = _cipherOrderList.SelectedIndex;
            if (i == -1 || i == _cipherOrderList.Items.Count - 1) return; //選択されていなければ何もしない

            string temp1 = _cipherAlgorithmOrder[i];
            _cipherAlgorithmOrder[i] = _cipherAlgorithmOrder[i + 1];
            _cipherAlgorithmOrder[i + 1] = temp1;

            object temp2 = _cipherOrderList.SelectedItem;
            _cipherOrderList.Items.RemoveAt(i);
            _cipherOrderList.Items.Insert(i + 1, temp2);

            _cipherOrderList.SelectedIndex = i + 1;
        }
        //アルゴリズム名
        private static string CipherAlgorithmName(CipherAlgorithm a)
        {
            switch (a)
            {
                case CipherAlgorithm.AES128:
                    return "AES(Rijndael) (SSH2 only)";
                case CipherAlgorithm.Blowfish:
                    return "Blowfish";
                case CipherAlgorithm.TripleDES:
                    return "TripleDES";
                default:
                    throw new Exception("Unexpected Algorithm " + a);
            }
        }
    }
}
