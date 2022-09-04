/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: SerialLoginDialog.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.Drawing;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;
using Poderosa.Connection;
using Poderosa.ConnectionParam;
using Poderosa.Communication;
using Poderosa.Terminal;

namespace Poderosa.Forms
{
    /// <summary>
    /// SerialLoginDialog の概要の説明です。
    /// </summary>
    internal class SerialLoginDialog : Form
    {
		
		private ConnectionTag _result;

		private Button _loginButton;
		private Button _cancelButton;
		private GroupBox _terminalGroup;
		private ComboBox _logTypeBox;
		private Label _logTypeLabel;
		private ComboBox _newLineBox;
		private ComboBox _localEchoBox;
		private Label _localEchoLabel;
		private Label _newLineLabel;
		private ComboBox _logFileBox;
		private Label _logFileLabel;
		private ComboBox _encodingBox;
		private Label _encodingLabel;
		private Button _selectLogButton;
		private GroupBox _serialGroup;
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
		private ComboBox _portBox;
		private Label _portLabel;
		private Label _transmitDelayPerCharLabel;
		private TextBox _transmitDelayPerCharBox;
		private Label _transmitDelayPerLineLabel;
		private TextBox _transmitDelayPerLineBox;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private Container components = null;

		public SerialLoginDialog() {
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			_serialGroup.Text = "Form.SerialLoginDialog._serialGroup";
			//以下、SerialConfigとテキストを共用
			_portLabel.Text = "Form.SerialConfig._portLabel";
			_baudRateLabel.Text = "Form.SerialConfig._baudRateLabel";
			_dataBitsLabel.Text = "Form.SerialConfig._dataBitsLabel";
			_parityLabel.Text = "Form.SerialConfig._parityLabel";
			_stopBitsLabel.Text = "Form.SerialConfig._stopBitsLabel";
			_flowControlLabel.Text = "Form.SerialConfig._flowControlLabel";
			_transmitDelayPerLineLabel.Text = "Transmit Delay(msec/line)";
			_transmitDelayPerCharLabel.Text = "Transmit Delay(msec/char)";
			string bits = "Caption.SerialConfig.Bits";
			_dataBitsBox.Items.AddRange(new object[] {
															  String.Format("{0}{1}", 7, bits),
															  String.Format("{0}{1}", 8, bits)});

			_terminalGroup.Text = "Form.SerialLoginDialog._terminalGroup";
			
			//以下、LoginDialogとテキスト共用
			_localEchoLabel.Text = "Form.LoginDialog._localEchoLabel";
			_newLineLabel.Text = "Form.LoginDialog._newLineLabel";
			_logFileLabel.Text = "Form.LoginDialog._logFileLabel";
			_encodingLabel.Text = "Form.LoginDialog._encodingLabel";
			_logTypeLabel.Text = "Form.LoginDialog._logTypeLabel";
			_localEchoBox.Items.AddRange(new object[] {
															   "Common.DoNot",
															   "Common.Do"});
			_loginButton.Text = "OK";
			_cancelButton.Text = "Cancel";
			Text = "Form.SerialLoginDialog.Text";

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			InitUI();
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// this._logTypeBox.Items.AddRange(EnumDescAttributeT.For(typeof(LogType)).DescriptionCollection());
		/// this._newLineBox.Items.AddRange(EnumDescAttributeT.For(typeof(NewLine)).DescriptionCollection());
		/// this._encodingBox.Items.AddRange(GUtil.EncodingDescription(GApp.Options.Encodings));
		/// this._stopBitsBox.Items.AddRange(EnumDescAttributeT.For(typeof(StopBits)).DescriptionCollection());
		/// this._parityBox.Items.AddRange(EnumDescAttributeT.For(typeof(Parity)).DescriptionCollection());
		///this._flowControlBox.Items.AddRange(EnumDescAttributeTs.For(typeof(FlowControl)).DescriptionCollection());
		///this._baudRateBox.Items.AddRange(TerminalUtil.BaudRates);
		/// </summary>
		private void InitializeComponent() {
			_serialGroup = new GroupBox();
			_portLabel = new Label();
			_portBox = new ComboBox();
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
			
			_terminalGroup = new GroupBox();
			_logTypeBox = new ComboBox();
			_logTypeLabel = new Label();
			_newLineBox = new ComboBox();
			_localEchoBox = new ComboBox();
			_localEchoLabel = new Label();
			_newLineLabel = new Label();
			_logFileBox = new ComboBox();
			_logFileLabel = new Label();
			_encodingBox = new ComboBox();
			_encodingLabel = new Label();
			_selectLogButton = new Button();

			_terminalGroup.SuspendLayout();
			_serialGroup.SuspendLayout();
			_loginButton = new Button();
			_cancelButton = new Button();
			SuspendLayout();
			// 
			// _serialGroup
			// 
			_serialGroup.Controls.AddRange(new Control[] {
																					   _transmitDelayPerCharBox,
																					   _transmitDelayPerCharLabel,
																					   _transmitDelayPerLineBox,
																					   _transmitDelayPerLineLabel,
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
																					   _portLabel});
			_serialGroup.Location = new Point(8, 8);
			_serialGroup.Name = "_serialGroup";
			_serialGroup.FlatStyle = FlatStyle.System;
			_serialGroup.Size = new Size(296, 224);
			_serialGroup.TabIndex = 0;
			_serialGroup.TabStop = false;
			// 
			// _portLabel
			// 
			_portLabel.Location = new Point(8, 16);
			_portLabel.Name = "_portLabel";
			_portLabel.Size = new Size(88, 23);
			_portLabel.TabIndex = 1;
			_portLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _portBox
			// 
			_portBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_portBox.Location = new Point(112, 16);
			_portBox.Name = "_portBox";
			_portBox.Size = new Size(120, 20);
			_portBox.TabIndex = 2;
			// 
			// _baudRateLabel
			// 
			_baudRateLabel.Location = new Point(8, 40);
			_baudRateLabel.Name = "_baudRateLabel";
			_baudRateLabel.Size = new Size(88, 23);
			_baudRateLabel.TabIndex = 3;
			_baudRateLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _baudRateBox
			// 
			_baudRateBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_baudRateBox.Items.AddRange(TerminalUtil.BaudRates);
			_baudRateBox.Location = new Point(112, 40);
			_baudRateBox.Name = "_baudRateBox";
			_baudRateBox.Size = new Size(120, 20);
			_baudRateBox.TabIndex = 4;
			// 
			// _dataBitsLabel
			// 
			_dataBitsLabel.Location = new Point(8, 64);
			_dataBitsLabel.Name = "_dataBitsLabel";
			_dataBitsLabel.Size = new Size(88, 23);
			_dataBitsLabel.TabIndex = 5;
			_dataBitsLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _dataBitsBox
			// 
			_dataBitsBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_dataBitsBox.Location = new Point(112, 64);
			_dataBitsBox.Name = "_dataBitsBox";
			_dataBitsBox.Size = new Size(120, 20);
			_dataBitsBox.TabIndex = 6;
			// 
			// _parityLabel
			// 
			_parityLabel.Location = new Point(8, 88);
			_parityLabel.Name = "_parityLabel";
			_parityLabel.Size = new Size(88, 23);
			_parityLabel.TabIndex = 7;
			_parityLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _parityBox
			// 
			_parityBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_parityBox.Items.AddRange(EnumDescAttributeT.For(typeof(Parity)).DescriptionCollection());
			_parityBox.Location = new Point(112, 88);
			_parityBox.Name = "_parityBox";
			_parityBox.Size = new Size(120, 20);
			_parityBox.TabIndex = 8;
			// 
			// _stopBitsLabel
			// 
			_stopBitsLabel.Location = new Point(8, 112);
			_stopBitsLabel.Name = "_stopBitsLabel";
			_stopBitsLabel.Size = new Size(88, 23);
			_stopBitsLabel.TabIndex = 9;
			_stopBitsLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _stopBitsBox
			// 
			_stopBitsBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_stopBitsBox.Items.AddRange(EnumDescAttributeT.For(typeof(StopBits)).DescriptionCollection());
			_stopBitsBox.Location = new Point(112, 112);
			_stopBitsBox.Name = "_stopBitsBox";
			_stopBitsBox.Size = new Size(120, 20);
			_stopBitsBox.TabIndex = 10;
			// 
			// _flowControlLabel
			// 
			_flowControlLabel.Location = new Point(8, 136);
			_flowControlLabel.Name = "_flowControlLabel";
			_flowControlLabel.Size = new Size(88, 23);
			_flowControlLabel.TabIndex = 11;
			_flowControlLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _flowControlBox
			// 
			_flowControlBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_flowControlBox.Location = new Point(112, 136);
			_flowControlBox.Name = "_flowControlBox";
			_flowControlBox.Size = new Size(120, 20);
			_flowControlBox.Items.AddRange(EnumDescAttributeT.For(typeof(FlowControl)).DescriptionCollection());
			_flowControlBox.TabIndex = 12;
			// 
			// _transmitDelayPerCharLabel
			// 
			_transmitDelayPerCharLabel.Location = new Point(8, 160);
			_transmitDelayPerCharLabel.Name = "_transmitDelayPerCharLabel";
			_transmitDelayPerCharLabel.Size = new Size(88, 23);
			_transmitDelayPerCharLabel.TabIndex = 13;
			_transmitDelayPerCharLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _transmitDelayPerCharBox
			// 
			_transmitDelayPerCharBox.Location = new Point(112, 160);
			_transmitDelayPerCharBox.Name = "_transmitDelayPerCharBox";
			_transmitDelayPerCharBox.Size = new Size(120, 20);
			_transmitDelayPerCharBox.TabIndex = 14;
			_transmitDelayPerCharBox.MaxLength = 3;
			// 
			// _transmitDelayPerLineLabel
			// 
			_transmitDelayPerLineLabel.Location = new Point(8, 184);
			_transmitDelayPerLineLabel.Name = "_transmitDelayPerLineLabel";
			_transmitDelayPerLineLabel.Size = new Size(88, 23);
			_transmitDelayPerLineLabel.TabIndex = 15;
			_transmitDelayPerLineLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _transmitDelayPerLineBox
			// 
			_transmitDelayPerLineBox.Location = new Point(112, 184);
			_transmitDelayPerLineBox.Name = "_transmitDelayPerLineBox";
			_transmitDelayPerLineBox.Size = new Size(120, 20);
			_transmitDelayPerLineBox.TabIndex = 16;
			_transmitDelayPerLineBox.MaxLength = 3;
			// 
			// _terminalGroup
			// 
			_terminalGroup.Controls.AddRange(new Control[] {
																						 _logTypeBox,
																						 _logTypeLabel,
																						 _newLineBox,
																						 _localEchoBox,
																						 _localEchoLabel,
																						 _newLineLabel,
																						 _logFileBox,
																						 _logFileLabel,
																						 _encodingBox,
																						 _encodingLabel,
																						 _selectLogButton});
			_terminalGroup.Location = new Point(8, 240);
			_terminalGroup.Name = "_terminalGroup";
			_terminalGroup.FlatStyle = FlatStyle.System;
			_terminalGroup.Size = new Size(296, 144);
			_terminalGroup.TabIndex = 17;
			_terminalGroup.TabStop = false;
			// 
			// _logTypeLabel
			// 
			_logTypeLabel.ImeMode = ImeMode.NoControl;
			_logTypeLabel.Location = new Point(8, 16);
			_logTypeLabel.Name = "_logTypeLabel";
			_logTypeLabel.RightToLeft = RightToLeft.No;
			_logTypeLabel.Size = new Size(120, 16);
			_logTypeLabel.TabIndex = 18;
			_logTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _logTypeBox
			// 
			_logTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_logTypeBox.Items.AddRange(EnumDescAttributeT.For(typeof(LogType)).DescriptionCollection());
			_logTypeBox.Location = new Point(112, 16);
			_logTypeBox.Name = "_logTypeBox";
			_logTypeBox.Size = new Size(120, 20);
			_logTypeBox.TabIndex = 19;
			_logTypeBox.SelectedIndexChanged += new EventHandler(OnLogTypeChanged);
			// 
			// _logFileLabel
			// 
			_logFileLabel.ImeMode = ImeMode.NoControl;
			_logFileLabel.Location = new Point(8, 40);
			_logFileLabel.Name = "_logFileLabel";
			_logFileLabel.RightToLeft = RightToLeft.No;
			_logFileLabel.Size = new Size(88, 16);
			_logFileLabel.TabIndex = 20;
			_logFileLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _logFileBox
			// 
			_logFileBox.Location = new Point(112, 40);
			_logFileBox.Name = "_logFileBox";
			_logFileBox.Size = new Size(144, 20);
			_logFileBox.TabIndex = 21;
			// 
			// _selectLogButton
			// 
			_selectLogButton.FlatStyle = FlatStyle.Flat;
			_selectLogButton.ImageIndex = 0;
			_selectLogButton.FlatStyle = FlatStyle.System;
			_selectLogButton.ImeMode = ImeMode.NoControl;
			_selectLogButton.Location = new Point(256, 40);
			_selectLogButton.Name = "_selectLogButton";
			_selectLogButton.RightToLeft = RightToLeft.No;
			_selectLogButton.Size = new Size(19, 19);
			_selectLogButton.TabIndex = 22;
			_selectLogButton.Text = "...";
			_selectLogButton.Click += new EventHandler(SelectLog);
			// 
			// _encodingLabel
			// 
			_encodingLabel.ImeMode = ImeMode.NoControl;
			_encodingLabel.Location = new Point(8, 64);
			_encodingLabel.Name = "_encodingLabel";
			_encodingLabel.RightToLeft = RightToLeft.No;
			_encodingLabel.Size = new Size(96, 16);
			_encodingLabel.TabIndex = 23;
			_encodingLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _encodingBox
			// 
			_encodingBox.Items.AddRange(EnumDescAttributeT.For(typeof(EncodingType)).DescriptionCollection());
			_encodingBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_encodingBox.Location = new Point(112, 64);
			_encodingBox.Name = "_encodingBox";
			_encodingBox.Size = new Size(120, 20);
			_encodingBox.TabIndex = 24;
			// 
			// _localEchoLabel
			// 
			_localEchoLabel.ImeMode = ImeMode.NoControl;
			_localEchoLabel.Location = new Point(8, 88);
			_localEchoLabel.Name = "_localEchoLabel";
			_localEchoLabel.RightToLeft = RightToLeft.No;
			_localEchoLabel.Size = new Size(96, 16);
			_localEchoLabel.TabIndex = 25;
			_localEchoLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _localEchoBox
			// 
			_localEchoBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_localEchoBox.Location = new Point(112, 88);
			_localEchoBox.Name = "_localEchoBox";
			_localEchoBox.Size = new Size(120, 20);
			_localEchoBox.TabIndex = 26;
			// 
			// _newLineLabel
			// 
			_newLineLabel.ImeMode = ImeMode.NoControl;
			_newLineLabel.Location = new Point(8, 112);
			_newLineLabel.Name = "_newLineLabel";
			_newLineLabel.RightToLeft = RightToLeft.No;
			_newLineLabel.Size = new Size(96, 16);
			_newLineLabel.TabIndex = 27;
			_newLineLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _newLineBox
			// 
			_newLineBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_newLineBox.Items.AddRange(EnumDescAttributeT.For(typeof(NewLine)).DescriptionCollection());
			_newLineBox.Location = new Point(112, 112);
			_newLineBox.Name = "_newLineBox";
			_newLineBox.Size = new Size(120, 20);
			_newLineBox.TabIndex = 28;
			// 
			// _loginButton
			// 
			_loginButton.DialogResult = DialogResult.OK;
			_loginButton.Location = new Point(136, 392);
			_loginButton.Name = "_loginButton";
			_loginButton.FlatStyle = FlatStyle.System;
			_loginButton.TabIndex = 29;
			_loginButton.Click += new EventHandler(OnOK);
			// 
			// _cancelButton
			// 
			_cancelButton.DialogResult = DialogResult.Cancel;
			_cancelButton.Location = new Point(224, 392);
			_cancelButton.Name = "_cancelButton";
			_cancelButton.FlatStyle = FlatStyle.System;
			_cancelButton.TabIndex = 30;
			// 
			// SerialLoginDialog
			// 
			AcceptButton = _loginButton;
			AutoScaleBaseSize = new Size(5, 12);
			CancelButton = _cancelButton;
			ClientSize = new Size(314, 423);
			Controls.AddRange(new Control[] {
																		  _serialGroup,
																		  _terminalGroup,
																		  _cancelButton,
																		  _loginButton});
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "SerialLoginDialog";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			_terminalGroup.ResumeLayout(false);
			_serialGroup.ResumeLayout(false);
			ResumeLayout(false);

		}
		#endregion

		public ConnectionTag Result {
			get {
				return _result;
			}
		}

		private void InitUI() {
			for(int i=1; i<=GApp.Options.SerialCount; i++)
				_portBox.Items.Add(String.Format("COM{0}", i));
			
			StringCollection c = GApp.ConnectionHistory.LogPaths;
			foreach(string p in c) _logFileBox.Items.Add(p);

			if(GApp.Options.DefaultLogType!=LogType.None) {
				_logTypeBox.SelectedIndex = (int)GApp.Options.DefaultLogType;
				string t = GUtil.CreateLogFileName(null);
				_logFileBox.Items.Add(t);
				_logFileBox.Text = t;
			}
			else
				_logTypeBox.SelectedIndex = 0;

			AdjustUI();
		}
		private void AdjustUI() {
			bool e = _logTypeBox.SelectedIndex!=(int)LogType.None;
			_logFileBox.Enabled = e;
			_selectLogButton.Enabled = e;
		}

		public void ApplyParam(SerialTerminalParam param) {
			_portBox.SelectedIndex = param.Port-1;
			//これらのSelectedIndexの設定はコンボボックスに設定した項目順に依存しているので注意深くすること
			_baudRateBox.SelectedIndex = _baudRateBox.FindStringExact(param.BaudRate.ToString());
			_dataBitsBox.SelectedIndex = param.ByteSize==7? 0 : 1;
			_parityBox.SelectedIndex = (int)param.Parity;
			_stopBitsBox.SelectedIndex = (int)param.StopBits;
			_flowControlBox.SelectedIndex = (int)param.FlowControl;

			_encodingBox.SelectedIndex = (int)param.EncodingProfile.Type;
			_newLineBox.SelectedIndex = _newLineBox.FindStringExact(param.TransmitNL.ToString());
			_localEchoBox.SelectedIndex = param.LocalEcho? 1 : 0;
			
			_transmitDelayPerCharBox.Text = param.TransmitDelayPerChar.ToString();
			_transmitDelayPerLineBox.Text = param.TransmitDelayPerLine.ToString();

		}

		private void OnOK(object sender, EventArgs args) {
			_result = null;
			DialogResult = DialogResult.None;

			SerialTerminalParam param = ValidateParam();
			if(param==null) return;

			try {
				_result = CommunicationUtil.CreateNewSerialConnection(this, param);
				if(_result!=null)
					DialogResult = DialogResult.OK;
			}
			catch(Exception ex) {
				GUtil.Warning(this, ex.Message);
			}

		}


		private SerialTerminalParam ValidateParam() {
			SerialTerminalParam p = new SerialTerminalParam();
			try {
				p.LogType = (LogType)EnumDescAttributeT.For(typeof(LogType)).FromDescription(_logTypeBox.Text, LogType.None);
				if(p.LogType!=LogType.None) {
					p.LogPath = _logFileBox.Text;
					if(p.LogPath==GUtil.CreateLogFileName(null)) p.LogPath = GUtil.CreateLogFileName(String.Format("com{0}", _portBox.SelectedIndex+1));
					LogFileCheckResult r = GCUtil.CheckLogFileName(p.LogPath, this);
					if(r==LogFileCheckResult.Cancel || r==LogFileCheckResult.Error) return null;
					p.LogAppend = (r==LogFileCheckResult.Append);
				}

				p.Port = _portBox.SelectedIndex+1;
				p.BaudRate = Int32.Parse(_baudRateBox.Text);
				p.ByteSize = (byte)(_dataBitsBox.SelectedIndex==0? 7 : 8);
				p.StopBits = (StopBits)_stopBitsBox.SelectedIndex;
				p.Parity = (Parity)_parityBox.SelectedIndex;
				p.FlowControl = (FlowControl)_flowControlBox.SelectedIndex;

				p.EncodingProfile = EncodingProfile.Get((EncodingType)_encodingBox.SelectedIndex);

				p.LocalEcho = _localEchoBox.SelectedIndex==1;
				p.TransmitNL = (NewLine)EnumDescAttributeT.For(typeof(NewLine)).FromDescription(_newLineBox.Text, LogType.None);

				p.TransmitDelayPerChar = Int32.Parse(_transmitDelayPerCharBox.Text);
				p.TransmitDelayPerLine = Int32.Parse(_transmitDelayPerLineBox.Text);
				return p;
			}
			catch(Exception ex) {
				GUtil.Warning(this, ex.Message);
				return null;
			}

		}
		private void SelectLog(object sender, EventArgs e) {
			string fn = GCUtil.SelectLogFileByDialog(this);
			if(fn!=null) _logFileBox.Text = fn;
		}
		private void OnLogTypeChanged(object sender, EventArgs args) {
			AdjustUI();
		}
	}

}
