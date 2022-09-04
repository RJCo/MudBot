/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: SerialConfig.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Communication;
using Poderosa.ConnectionParam;
using Poderosa.Terminal;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;

namespace Poderosa.Forms
{
    /// <summary>
    /// 接続後にシリアルのパラメータを変更するためのUI
    /// ログインダイアログとかぶっている処理は多いので何とかしたいところではあるが...
    /// </summary>
    internal class SerialConfigForm : Form
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        private Button _loginButton;
        private Button _cancelButton;
        private ComboBox _flowControlBox;
        private Label _flowControlLabel;
        private ComboBox _stopBitsBox;
        private Label _stopBitsLabel;
        private ComboBox _parityBox;
        private Label _parityLabel;
        private ComboBox _dataBitsBox;
        private Label _dataBitsLabel;
        private ComboBox _baudRateBox;
        private Label _baudRateLabel;
        private Label _portBox; //ポートは変更できない
        private Label _portLabel;
        private Label _transmitDelayPerCharLabel;
        private TextBox _transmitDelayPerCharBox;
        private Label _transmitDelayPerLineLabel;
        private TextBox _transmitDelayPerLineBox;


        public SerialConfigForm()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            _portLabel.Text = "Form.SerialConfig._portLabel";
            _baudRateLabel.Text = "Form.SerialConfig._baudRateLabel";
            _dataBitsLabel.Text = "Form.SerialConfig._dataBitsLabel";
            _parityLabel.Text = "Form.SerialConfig._parityLabel";
            _stopBitsLabel.Text = "Form.SerialConfig._stopBitsLabel";
            _flowControlLabel.Text = "Form.SerialConfig._flowControlLabel";
            string bits = "Caption.SerialConfig.Bits";
            _dataBitsBox.Items.AddRange(new object[] {
                                                              String.Format("{0}{1}", 7, bits),
                                                              String.Format("{0}{1}", 8, bits)});
            Text = "Form.SerialConfig.Text";
            _loginButton.Text = "OK";
            _cancelButton.Text = "Cancel";

            _transmitDelayPerLineLabel.Text = "Transmit Delay(msec/line)";
            _transmitDelayPerCharLabel.Text = "Transmit Delay(msec/char)";
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


        //			this._flowControlBox.Items.AddRange(EnumDescAttributeT.For(typeof(FlowControl)).DescriptionCollection());
        //			this._stopBitsBox.Items.AddRange(EnumDescAttributeT.For(typeof(StopBits)).DescriptionCollection());
        //			this._parityBox.Items.AddRange(EnumDescAttributeT.For(typeof(Parity)).DescriptionCollection());
        //			this._baudRateBox.Items.AddRange(TerminalUtil.BaudRates);


        #region Windows Form Designer generated code
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            _portLabel = new Label();
            _portBox = new Label();
            _baudRateLabel = new Label();
            _baudRateBox = new ComboBox();
            _dataBitsLabel = new Label();
            _dataBitsBox = new ComboBox();
            _parityLabel = new Label();
            _parityBox = new ComboBox();
            _stopBitsLabel = new Label();
            _stopBitsBox = new ComboBox();
            _flowControlLabel = new Label();
            _flowControlBox = new ComboBox();
            _transmitDelayPerCharLabel = new Label();
            _transmitDelayPerCharBox = new TextBox();
            _transmitDelayPerLineLabel = new Label();
            _transmitDelayPerLineBox = new TextBox();
            _loginButton = new Button();
            _cancelButton = new Button();
            SuspendLayout();
            // 
            // _portLabel
            // 
            _portLabel.Location = new Point(8, 16);
            _portLabel.Name = "_portLabel";
            _portLabel.Size = new Size(88, 23);
            _portLabel.TabIndex = 0;
            _portLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _portBox
            // 
            _portBox.Location = new Point(112, 20);
            _portBox.Name = "_portBox";
            _portBox.Size = new Size(96, 20);
            _portBox.TabIndex = 1;
            // 
            // _baudRateLabel
            // 
            _baudRateLabel.Location = new Point(8, 40);
            _baudRateLabel.Name = "_baudRateLabel";
            _baudRateLabel.Size = new Size(88, 23);
            _baudRateLabel.TabIndex = 2;
            _baudRateLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _baudRateBox
            // 
            _baudRateBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _baudRateBox.Items.AddRange(TerminalUtil.BaudRates);
            _baudRateBox.Location = new Point(112, 40);
            _baudRateBox.Name = "_baudRateBox";
            _baudRateBox.Size = new Size(96, 20);
            _baudRateBox.TabIndex = 3;
            // 
            // _dataBitsLabel
            // 
            _dataBitsLabel.Location = new Point(8, 64);
            _dataBitsLabel.Name = "_dataBitsLabel";
            _dataBitsLabel.Size = new Size(88, 23);
            _dataBitsLabel.TabIndex = 4;
            _dataBitsLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _dataBitsBox
            // 
            _dataBitsBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _dataBitsBox.Location = new Point(112, 64);
            _dataBitsBox.Name = "_dataBitsBox";
            _dataBitsBox.Size = new Size(96, 20);
            _dataBitsBox.TabIndex = 5;
            // 
            // _parityLabel
            // 
            _parityLabel.Location = new Point(8, 88);
            _parityLabel.Name = "_parityLabel";
            _parityLabel.Size = new Size(88, 23);
            _parityLabel.TabIndex = 6;
            _parityLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _parityBox
            // 
            _parityBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _parityBox.Items.AddRange(EnumDescAttributeT.For(typeof(Parity)).DescriptionCollection());
            _parityBox.Location = new Point(112, 88);
            _parityBox.Name = "_parityBox";
            _parityBox.Size = new Size(96, 20);
            _parityBox.TabIndex = 7;
            // 
            // _stopBitsLabel
            // 
            _stopBitsLabel.Location = new Point(8, 112);
            _stopBitsLabel.Name = "_stopBitsLabel";
            _stopBitsLabel.Size = new Size(88, 23);
            _stopBitsLabel.TabIndex = 8;
            _stopBitsLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _stopBitsBox
            // 
            _stopBitsBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _stopBitsBox.Items.AddRange(EnumDescAttributeT.For(typeof(StopBits)).DescriptionCollection());
            _stopBitsBox.Location = new Point(112, 112);
            _stopBitsBox.Name = "_stopBitsBox";
            _stopBitsBox.Size = new Size(96, 20);
            _stopBitsBox.TabIndex = 9;
            // 
            // _flowControlLabel
            // 
            _flowControlLabel.Location = new Point(8, 136);
            _flowControlLabel.Name = "_flowControlLabel";
            _flowControlLabel.Size = new Size(88, 23);
            _flowControlLabel.TabIndex = 10;
            _flowControlLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _flowControlBox
            // 
            _flowControlBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _flowControlBox.Items.AddRange(EnumDescAttributeT.For(typeof(FlowControl)).DescriptionCollection());
            _flowControlBox.Location = new Point(112, 136);
            _flowControlBox.Name = "_flowControlBox";
            _flowControlBox.Size = new Size(96, 20);
            _flowControlBox.TabIndex = 11;
            // 
            // _transmitDelayPerCharLabel
            // 
            _transmitDelayPerCharLabel.Location = new Point(8, 160);
            _transmitDelayPerCharLabel.Name = "_transmitDelayPerCharLabel";
            _transmitDelayPerCharLabel.Size = new Size(88, 23);
            _transmitDelayPerCharLabel.TabIndex = 12;
            _transmitDelayPerCharLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _transmitDelayPerCharBox
            // 
            _transmitDelayPerCharBox.Location = new Point(112, 160);
            _transmitDelayPerCharBox.Name = "_transmitDelayPerCharBox";
            _transmitDelayPerCharBox.Size = new Size(96, 20);
            _transmitDelayPerCharBox.TabIndex = 13;
            _transmitDelayPerCharBox.MaxLength = 3;
            // 
            // _transmitDelayPerLineLabel
            // 
            _transmitDelayPerLineLabel.Location = new Point(8, 184);
            _transmitDelayPerLineLabel.Name = "_transmitDelayPerLineLabel";
            _transmitDelayPerLineLabel.Size = new Size(88, 23);
            _transmitDelayPerLineLabel.TabIndex = 14;
            _transmitDelayPerLineLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _transmitDelayPerLineBox
            // 
            _transmitDelayPerLineBox.Location = new Point(112, 184);
            _transmitDelayPerLineBox.Name = "_transmitDelayPerLineBox";
            _transmitDelayPerLineBox.Size = new Size(96, 20);
            _transmitDelayPerLineBox.TabIndex = 15;
            _transmitDelayPerLineBox.MaxLength = 3;
            // 
            // _loginButton
            // 
            _loginButton.DialogResult = DialogResult.OK;
            _loginButton.Location = new Point(48, 216);
            _loginButton.Name = "_loginButton";
            _loginButton.FlatStyle = FlatStyle.System;
            _loginButton.TabIndex = 16;
            _loginButton.Click += new EventHandler(OnOK);
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(136, 216);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.TabIndex = 17;
            // 
            // SerialConfig
            // 
            AcceptButton = _loginButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(218, 247);
            Controls.AddRange(new Control[] {
                                                                            _transmitDelayPerCharLabel,
                                                                            _transmitDelayPerCharBox,
                                                                            _transmitDelayPerLineLabel,
                                                                            _transmitDelayPerLineBox,
                                                                          _flowControlBox,
                                                                          _flowControlLabel,
                                                                          _stopBitsBox,
                                                                          _stopBitsLabel,
                                                                          _parityBox,
                                                                          _parityLabel,
                                                                          _dataBitsBox,
                                                                          _dataBitsLabel,
                                                                          _baudRateBox,
                                                                          _baudRateLabel,
                                                                          _portBox,
                                                                          _portLabel,
                                                                          _loginButton,
                                                                          _cancelButton});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SerialConfig";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        private TerminalConnection _con;
        public void ApplyParam(TerminalConnection con)
        {
            _con = con;
            SerialTerminalParam param = (SerialTerminalParam)con.Param;
            _portBox.Text = "COM" + param.Port;
            _baudRateBox.SelectedIndex = _baudRateBox.FindStringExact(param.BaudRate.ToString());
            _dataBitsBox.SelectedIndex = param.ByteSize == 7 ? 0 : 1;
            _parityBox.SelectedIndex = (int)param.Parity;
            _stopBitsBox.SelectedIndex = (int)param.StopBits;
            _flowControlBox.SelectedIndex = (int)param.FlowControl;
            _transmitDelayPerCharBox.Text = param.TransmitDelayPerChar.ToString();
            _transmitDelayPerLineBox.Text = param.TransmitDelayPerLine.ToString();
        }
        private void OnOK(object sender, EventArgs args)
        {
            SerialTerminalParam p = (SerialTerminalParam)_con.Param.Clone();
            p.BaudRate = Int32.Parse(_baudRateBox.Text);
            p.ByteSize = (byte)(_dataBitsBox.SelectedIndex == 0 ? 7 : 8);
            p.StopBits = (StopBits)_stopBitsBox.SelectedIndex;
            p.Parity = (Parity)_parityBox.SelectedIndex;
            p.FlowControl = (FlowControl)_flowControlBox.SelectedIndex;
            try
            {
                p.TransmitDelayPerChar = Int32.Parse(_transmitDelayPerCharBox.Text);
                p.TransmitDelayPerLine = Int32.Parse(_transmitDelayPerLineBox.Text);
            }
            catch (Exception ex)
            {
                GUtil.Warning(this, ex.Message);
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                ((SerialTerminalConnection)_con).ApplySerialParam(p);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                GUtil.Warning(this, ex.Message);
                DialogResult = DialogResult.None;
            }
        }

    }
}
