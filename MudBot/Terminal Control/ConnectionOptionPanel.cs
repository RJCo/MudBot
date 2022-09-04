/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: ConnectionOptionPanel.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using Poderosa.Communication;
using Poderosa.Config;
using Poderosa.UI;
using System;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    internal class ConnectionOptionPanel : OptionDialog.CategoryPanel
    {
        private GroupBox _socksGroup;
        private CheckBox _useSocks;
        private Label _socksServerLabel;
        private TextBox _socksServerBox;
        private Label _socksPortLabel;
        private TextBox _socksPortBox;
        private Label _socksAccountLabel;
        private TextBox _socksAccountBox;
        private Label _socksPasswordLabel;
        private TextBox _socksPasswordBox;
        private Label _socksNANetworksLabel;
        private TextBox _socksNANetworksBox;

        public ConnectionOptionPanel()
        {
            InitializeComponent();
            FillText();
        }
        private void InitializeComponent()
        {
            _socksGroup = new GroupBox();
            _useSocks = new CheckBox();
            _socksServerLabel = new Label();
            _socksServerBox = new TextBox();
            _socksPortLabel = new Label();
            _socksPortBox = new TextBox();
            _socksAccountLabel = new Label();
            _socksAccountBox = new TextBox();
            _socksPasswordLabel = new Label();
            _socksPasswordBox = new TextBox();
            _socksNANetworksLabel = new Label();
            _socksNANetworksBox = new TextBox();

            _socksGroup.SuspendLayout();

            Controls.AddRange(new Control[] {
                                                                                          _useSocks,
                                                                                          _socksGroup});
            //
            //_useSocks
            //
            _useSocks.Location = new System.Drawing.Point(16, 3);
            _useSocks.Name = "_useSocksAuthentication";
            _useSocks.FlatStyle = FlatStyle.System;
            _useSocks.Size = new System.Drawing.Size(160, 23);
            _useSocks.TabIndex = 1;
            _useSocks.CheckedChanged += OnUseSocksOptionChanged;
            //
            //_socksGroup
            //
            _socksGroup.Controls.AddRange(new Control[] {
                                                                                      _socksServerLabel,
                                                                                      _socksServerBox,
                                                                                      _socksPortLabel,
                                                                                      _socksPortBox,
                                                                                      _socksAccountLabel,
                                                                                      _socksAccountBox,
                                                                                      _socksPasswordLabel,
                                                                                      _socksPasswordBox,
                                                                                      _socksNANetworksLabel,
                                                                                      _socksNANetworksBox});
            _socksGroup.Location = new System.Drawing.Point(8, 8);
            _socksGroup.Name = "_socksGroup";
            _socksGroup.FlatStyle = FlatStyle.System;
            _socksGroup.Size = new System.Drawing.Size(416, 128);
            _socksGroup.TabIndex = 2;
            _socksGroup.TabStop = false;
            _socksGroup.Text = "";
            //
            //_socksServerLabel
            //
            _socksServerLabel.Location = new System.Drawing.Point(8, 18);
            _socksServerLabel.Name = "_socksServerLabel";
            _socksServerLabel.Size = new System.Drawing.Size(80, 23);
            _socksServerLabel.TabIndex = 0;
            _socksServerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            //_socksServerBox
            //
            _socksServerBox.Location = new System.Drawing.Point(96, 18);
            _socksServerBox.Name = "_socksServerBox";
            _socksServerBox.Size = new System.Drawing.Size(104, 19);
            _socksServerBox.Enabled = false;
            _socksServerBox.TabIndex = 1;
            //
            //_socksPortLabel
            //
            _socksPortLabel.Location = new System.Drawing.Point(216, 18);
            _socksPortLabel.Name = "_socksPortLabel";
            _socksPortLabel.Size = new System.Drawing.Size(80, 23);
            _socksPortLabel.TabIndex = 2;
            _socksPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            //_socksPortBox
            //
            _socksPortBox.Location = new System.Drawing.Point(304, 18);
            _socksPortBox.Name = "_socksPortBox";
            _socksPortBox.Size = new System.Drawing.Size(104, 19);
            _socksPortBox.Enabled = false;
            _socksPortBox.TabIndex = 3;
            _socksPortBox.MaxLength = 5;
            //
            //_socksAccountLabel
            //
            _socksAccountLabel.Location = new System.Drawing.Point(8, 40);
            _socksAccountLabel.Name = "_socksAccountLabel";
            _socksAccountLabel.Size = new System.Drawing.Size(80, 23);
            _socksAccountLabel.TabIndex = 4;
            _socksAccountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            //_socksAccountBox
            //
            _socksAccountBox.Location = new System.Drawing.Point(96, 40);
            _socksAccountBox.Name = "_socksAccountBox";
            _socksAccountBox.Size = new System.Drawing.Size(104, 19);
            _socksAccountBox.Enabled = false;
            _socksAccountBox.TabIndex = 5;
            //
            //_socksPasswordLabel
            //
            _socksPasswordLabel.Location = new System.Drawing.Point(216, 40);
            _socksPasswordLabel.Name = "_socksPasswordLabel";
            _socksPasswordLabel.Size = new System.Drawing.Size(80, 23);
            _socksPasswordLabel.TabIndex = 6;
            _socksPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            //_socksPasswordBox
            //
            _socksPasswordBox.Location = new System.Drawing.Point(304, 40);
            _socksPasswordBox.Name = "_socksPasswordBox";
            _socksPasswordBox.PasswordChar = '*';
            _socksPasswordBox.Enabled = false;
            _socksPasswordBox.Size = new System.Drawing.Size(104, 19);
            _socksPasswordBox.TabIndex = 7;
            //
            //_socksNANetworksLabel
            //
            _socksNANetworksLabel.Location = new System.Drawing.Point(8, 68);
            _socksNANetworksLabel.Name = "_socksNANetworksLabel";
            _socksNANetworksLabel.Size = new System.Drawing.Size(400, 28);
            _socksNANetworksLabel.TabIndex = 8;
            _socksNANetworksLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            //_socksNANetworksBox
            //
            _socksNANetworksBox.Location = new System.Drawing.Point(8, 98);
            _socksNANetworksBox.Name = "_socksNANetworksBox";
            _socksNANetworksBox.Enabled = false;
            _socksNANetworksBox.Size = new System.Drawing.Size(400, 19);
            _socksNANetworksBox.TabIndex = 9;

            BackColor = ThemeUtil.TabPaneBackColor;
            _socksGroup.ResumeLayout();
        }

        private void FillText()
        {
            _useSocks.Text = "Use SOCKS5";
            _socksServerLabel.Text = "Server Name";
            _socksPortLabel.Text = "Port";
            _socksAccountLabel.Text = "Account";
            _socksPasswordLabel.Text = "Password";
            _socksNANetworksLabel.Text = @"Exclude the following networks for SOCKS
(concatenate strings in the form '192.168.10.0/24' using semicolons)";
        }

        public override void InitUI(ContainerOptions options)
        {
            _useSocks.Checked = options.UseSocks;
            _socksServerBox.Text = options.SocksServer;
            _socksPortBox.Text = options.SocksPort.ToString();
            _socksAccountBox.Text = options.SocksAccount;
            _socksPasswordBox.Text = options.SocksPassword;
            _socksNANetworksBox.Text = options.SocksNANetworks;
        }

        public override bool Commit(ContainerOptions options)
        {
            string itemname = "";
            try
            {
                options.UseSocks = _useSocks.Checked;
                if (options.UseSocks && _socksServerBox.Text.Length == 0)
                {
                    throw new Exception("The SOCKS server name is empty.");
                }

                options.SocksServer = _socksServerBox.Text;
                itemname = "SOCKS port number";
                options.SocksPort = Int32.Parse(_socksPortBox.Text);
                options.SocksAccount = _socksAccountBox.Text;
                options.SocksPassword = _socksPasswordBox.Text;
                itemname = "network address";
                foreach (string c in _socksNANetworksBox.Text.Split(';'))
                {
                    if (!NetUtil.IsNetworkAddress(c))
                    {
                        throw new FormatException();
                    }
                }
                options.SocksNANetworks = _socksNANetworksBox.Text;

                return true;
            }
            catch (FormatException)
            {
                GUtil.Warning(this, $"The value of {itemname} is not valid.");
                return false;
            }
            catch (Exception ex)
            {
                GUtil.Warning(this, ex.Message);
                return false;
            }
        }

        private void OnUseSocksOptionChanged(object sender, EventArgs args)
        {
            bool e = _useSocks.Checked;
            _socksServerBox.Enabled = e;
            _socksPortBox.Enabled = e;
            _socksAccountBox.Enabled = e;
            _socksPasswordBox.Enabled = e;
            _socksNANetworksBox.Enabled = e;
        }
    }
}
