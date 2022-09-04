/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: ServerInfo.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Communication;
using Poderosa.ConnectionParam;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;

namespace Poderosa.Forms
{
    /// <summary>
    /// ServerInfo の概要の説明です。
    /// </summary>
    internal class ServerInfo : Form
    {
        private Button _okButton;
        private Label _serverNamesLabel;
        private TextBox _serverNamesBox;
        private Label _IPAddressLabel;
        private TextBox _IPAddressBox;
        private TextBox _protocolBox;
        private Label _protocolLabel;
        private TextBox _terminalTypeBox;
        private Label _terminalTypeLabel;
        private Label _parameterLabel;
        private TextBox _parameterBox;
        private Label _statsLabel;
        private Label _transmitBytes;
        private Label _receiveBytes;
        private Label _logLabel;
        private TextBox _logBox;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public ServerInfo(TerminalConnection con)
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            _serverNamesBox.Text = con.ServerName;
            _IPAddressBox.Text = con.ServerAddress == null ? "" : con.ServerAddress.ToString();
            _protocolBox.Text = con.ProtocolDescription;
            _parameterBox.Lines = con.ConnectionParameter;
            _terminalTypeBox.Text = EnumDescAttributeT.For(typeof(TerminalType)).GetDescription(con.Param.TerminalType);
            string li = EnumDescAttributeT.For(typeof(LogType)).GetDescription(con.LogType);
            if (con.LogType != LogType.None)
            {
                li += "(" + con.LogPath + ")";
            }

            _logBox.Text = li;
            _receiveBytes.Text = String.Format("{0,10}{1}", con.ReceivedDataSize, "Caption.ServerInfo.BytesReceived");
            _transmitBytes.Text = String.Format("{0,10}{1}", con.SentDataSize, "Caption.ServerInfo.BytesSent");

            _okButton.Text = "OK";
            _serverNamesLabel.Text = "Form.ServerInfo._serverNamesLabel";
            _IPAddressLabel.Text = "Form.ServerInfo._IPAddressLabel";
            _protocolLabel.Text = "Form.ServerInfo._protocolLabel";
            _terminalTypeLabel.Text = "Form.ServerInfo._terminalTypeLabel";
            _parameterLabel.Text = "Form.ServerInfo._parameterLabel";
            _statsLabel.Text = "Form.ServerInfo._statsLabel";
            _logLabel.Text = "Form.ServerInfo._logLabel";
            Text = "Form.ServerInfo.Text";
        }

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
        /// </summary>
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
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            _okButton = new Button();
            _serverNamesLabel = new Label();
            _serverNamesBox = new TextBox();
            _IPAddressLabel = new Label();
            _IPAddressBox = new TextBox();
            _protocolBox = new TextBox();
            _protocolLabel = new Label();
            _terminalTypeBox = new TextBox();
            _terminalTypeLabel = new Label();
            _parameterLabel = new Label();
            _parameterBox = new TextBox();
            _statsLabel = new Label();
            _transmitBytes = new Label();
            _receiveBytes = new Label();
            _logLabel = new Label();
            _logBox = new TextBox();
            SuspendLayout();
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(112, 256);
            _okButton.Name = "_okButton";
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.TabIndex = 0;
            // 
            // _serverNamesLabel
            // 
            _serverNamesLabel.Location = new Point(8, 8);
            _serverNamesLabel.Name = "_serverNamesLabel";
            _serverNamesLabel.Size = new Size(80, 16);
            _serverNamesLabel.TabIndex = 1;
            _serverNamesLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _serverNamesBox
            // 
            _serverNamesBox.Location = new Point(88, 8);
            _serverNamesBox.Name = "_serverNamesBox";
            _serverNamesBox.ReadOnly = true;
            _serverNamesBox.Size = new Size(224, 19);
            _serverNamesBox.TabIndex = 2;
            _serverNamesBox.Text = "";
            // 
            // _IPAddressLabel
            // 
            _IPAddressLabel.Location = new Point(8, 32);
            _IPAddressLabel.Name = "_IPAddressLabel";
            _IPAddressLabel.Size = new Size(80, 16);
            _IPAddressLabel.TabIndex = 3;
            _IPAddressLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _IPAddressBox
            // 
            _IPAddressBox.Location = new Point(88, 32);
            _IPAddressBox.Name = "_IPAddressBox";
            _IPAddressBox.ReadOnly = true;
            _IPAddressBox.Size = new Size(224, 19);
            _IPAddressBox.TabIndex = 4;
            _IPAddressBox.Text = "";
            // 
            // _protocolBox
            // 
            _protocolBox.Location = new Point(88, 56);
            _protocolBox.Name = "_protocolBox";
            _protocolBox.ReadOnly = true;
            _protocolBox.Size = new Size(224, 19);
            _protocolBox.TabIndex = 5;
            _protocolBox.Text = "";
            // 
            // _protocolLabel
            // 
            _protocolLabel.Location = new Point(8, 56);
            _protocolLabel.Name = "_protocolLabel";
            _protocolLabel.Size = new Size(80, 16);
            _protocolLabel.TabIndex = 6;
            _protocolLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _terminalTypeBox
            // 
            _terminalTypeBox.Location = new Point(88, 80);
            _terminalTypeBox.Name = "_terminalTypeBox";
            _terminalTypeBox.ReadOnly = true;
            _terminalTypeBox.Size = new Size(224, 19);
            _terminalTypeBox.TabIndex = 5;
            _terminalTypeBox.Text = "";
            // 
            // _terminalTypeLabel
            // 
            _terminalTypeLabel.Location = new Point(8, 80);
            _terminalTypeLabel.Name = "_terminalTypeLabel";
            _terminalTypeLabel.Size = new Size(80, 16);
            _terminalTypeLabel.TabIndex = 6;
            _terminalTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _parameterLabel
            // 
            _parameterLabel.Location = new Point(8, 104);
            _parameterLabel.Name = "_parameterLabel";
            _parameterLabel.Size = new Size(80, 16);
            _parameterLabel.TabIndex = 7;
            _parameterLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _parameterBox
            // 
            _parameterBox.Location = new Point(88, 104);
            _parameterBox.Multiline = true;
            _parameterBox.Name = "_parameterBox";
            _parameterBox.ReadOnly = true;
            _parameterBox.ScrollBars = ScrollBars.Vertical;
            _parameterBox.Size = new Size(224, 80);
            _parameterBox.TabIndex = 8;
            _parameterBox.Text = "";
            // 
            // _statsLabel
            // 
            _statsLabel.Location = new Point(8, 224);
            _statsLabel.Name = "_statsLabel";
            _statsLabel.Size = new Size(104, 16);
            _statsLabel.TabIndex = 9;
            _statsLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _transmitBytes
            // 
            _transmitBytes.Location = new Point(120, 224);
            _transmitBytes.Name = "_transmitBytes";
            _transmitBytes.Size = new Size(184, 16);
            _transmitBytes.TabIndex = 10;
            _transmitBytes.Text = "0";
            _transmitBytes.TextAlign = ContentAlignment.MiddleRight;
            // 
            // _receiveBytes
            // 
            _receiveBytes.Location = new Point(120, 240);
            _receiveBytes.Name = "_receiveBytes";
            _receiveBytes.Size = new Size(184, 16);
            _receiveBytes.TabIndex = 11;
            _receiveBytes.Text = "0";
            _receiveBytes.TextAlign = ContentAlignment.MiddleRight;
            // 
            // _logLabel
            // 
            _logLabel.Location = new Point(8, 192);
            _logLabel.Name = "_logLabel";
            _logLabel.Size = new Size(80, 16);
            _logLabel.TabIndex = 13;
            _logLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _logBox
            // 
            _logBox.Location = new Point(88, 192);
            _logBox.Name = "_logBox";
            _logBox.ReadOnly = true;
            _logBox.Size = new Size(224, 19);
            _logBox.TabIndex = 12;
            _logBox.Text = "";
            // 
            // ServerInfo
            // 
            AcceptButton = _okButton;
            CancelButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            ClientSize = new Size(314, 287);
            Controls.AddRange(new Control[] {
                                                                          _logLabel,
                                                                          _logBox,
                                                                          _receiveBytes,
                                                                          _transmitBytes,
                                                                          _statsLabel,
                                                                          _parameterBox,
                                                                          _parameterLabel,
                                                                          _protocolLabel,
                                                                          _protocolBox,
                                                                          _terminalTypeLabel,
                                                                          _terminalTypeBox,
                                                                          _IPAddressBox,
                                                                          _IPAddressLabel,
                                                                          _serverNamesBox,
                                                                          _serverNamesLabel,
                                                                          _okButton});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ServerInfo";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        private static string FormatStrings(string[] v)
        {
            StringBuilder b = new StringBuilder();
            if (v != null)
            {
                foreach (string t in v)
                {
                    if (b.Length > 0)
                    {
                        b.Append(", ");
                    }

                    b.Append(t);
                }
            }
            else
            {
                b.Append("-");
            }

            return b.ToString();
        }
        private static string FormatIPs(IPAddress[] v)
        {
            StringBuilder b = new StringBuilder();
            if (v != null)
            {
                foreach (IPAddress t in v)
                {
                    if (b.Length > 0)
                    {
                        b.Append(", ");
                    }

                    b.Append(t.ToString());
                }
            }
            else
            {
                b.Append("-");
            }

            return b.ToString();
        }

    }
}
