/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: LoginDialog.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.Drawing;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;
using Poderosa.Communication;
using Poderosa.Connection;
using Poderosa.ConnectionParam;
using Poderosa.Config;
using Poderosa.SSH;

using Granados.SSHC;


namespace Poderosa.Forms
{
    internal class LoginDialog : Form, ISocketWithTimeoutClient
    {
        #region fields
        private string _errorMessage;
		private bool _initializing;
		private bool _firstFlag;
		private ConnectionHistory _history;
		private ConnectionTag _result;
		private SocketWithTimeout _connector;
		private IntPtr _savedHWND;

		private Container components = null;

		private Label _hostLabel;
		public ComboBox _hostBox;
		private Label _portLabel;
        public ComboBox _portBox;
		private Label _methodLabel;
        public ComboBox _methodBox;

		private GroupBox _sshGroup;
		private Label _usernameLabel;
        public ComboBox _userNameBox;
		private Label _authenticationLabel;
        public ComboBox _authOptions;
		private Label _passphraseLabel;
        public TextBox _passphraseBox;
		private Label _privateKeyLabel;
        public TextBox _privateKeyFile;
		private Button _privateKeySelect;

		private GroupBox _terminalGroup;
        public ComboBox _encodingBox;
		private Label _encodingLabel;
		private Label _logFileLabel;
        public ComboBox _logFileBox;
		private Button _selectLogButton;
		private Label _newLineLabel;
		private Label _localEchoLabel;
        public ComboBox _localEchoBox;
        public ComboBox _newLineBox;
		private Label _logTypeLabel;
        public ComboBox _logTypeBox;
        public ComboBox _terminalTypeBox;
		private Label _terminalTypeLabel;

		private Button _loginButton;
		private Button _cancelButton;
        #endregion
        public LoginDialog()
		{
			_firstFlag = true;
			_initializing = true;
			_history = GApp.ConnectionHistory;
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();
			InitializeText();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			InitializeLoginParams();
			_initializing = false;
		}
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
        #region Windows Form Designer generated code
		private void InitializeComponent()
		{
			_loginButton = new Button();
			_cancelButton = new Button();
			_sshGroup = new GroupBox();

			_hostLabel = new Label();
			_hostBox = new ComboBox();
			_methodLabel = new Label();
			_methodBox = new ComboBox();
			_portLabel = new Label();
			_portBox = new ComboBox();

			_authenticationLabel = new Label();
			_authOptions = new ComboBox();
			_passphraseLabel = new Label();
			_passphraseBox = new TextBox();
			_privateKeyLabel = new Label();
			_privateKeyFile = new TextBox();
			_privateKeySelect = new Button();
			_usernameLabel = new Label();
			_userNameBox = new ComboBox();

			_terminalGroup = new GroupBox();
			_newLineBox = new ComboBox();
			_localEchoBox = new ComboBox();
			_localEchoLabel = new Label();
			_newLineLabel = new Label();
			_logFileBox = new ComboBox();
			_logFileLabel = new Label();
			_encodingBox = new ComboBox();
			_encodingLabel = new Label();
			_selectLogButton = new Button();
			_logTypeLabel = new Label();
			_logTypeBox = new ComboBox();
			_terminalTypeBox = new ComboBox();
			_terminalTypeLabel = new Label();

			_sshGroup.SuspendLayout();
			_terminalGroup.SuspendLayout();
			SuspendLayout();
			// 
			// _hostLabel
			// 
			_hostLabel.ImeMode = ImeMode.NoControl;
			_hostLabel.Location = new Point(16, 12);
			_hostLabel.Name = "_hostLabel";
			_hostLabel.RightToLeft = RightToLeft.No;
			_hostLabel.Size = new Size(80, 16);
			_hostLabel.TabIndex = 0;
			_hostLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _hostBox
			// 
			_hostBox.Location = new Point(104, 8);
			_hostBox.Name = "_hostBox";
			_hostBox.Size = new Size(208, 20);
			_hostBox.TabIndex = 1;
			_hostBox.SelectedIndexChanged += new EventHandler(OnHostIsSelected);
			// 
			// _methodLabel
			// 
			_methodLabel.ImeMode = ImeMode.NoControl;
			_methodLabel.Location = new Point(16, 36);
			_methodLabel.Name = "_methodLabel";
			_methodLabel.RightToLeft = RightToLeft.No;
			_methodLabel.Size = new Size(80, 16);
			_methodLabel.TabIndex = 2;
			_methodLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _methodBox
			// 
			_methodBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_methodBox.Items.AddRange(new object[] { "Telnet",
															"SSH1",
															"SSH2"
															});
			_methodBox.Location = new Point(104, 32);
			_methodBox.Name = "_methodBox";
			_methodBox.Size = new Size(208, 20);
			_methodBox.TabIndex = 3;
			_methodBox.SelectedIndexChanged += new EventHandler(AdjustConnectionUI);
			// 
			// _portLabel
			// 
			_portLabel.ImeMode = ImeMode.NoControl;
			_portLabel.Location = new Point(16, 60);
			_portLabel.Name = "_portLabel";
			_portLabel.RightToLeft = RightToLeft.No;
			_portLabel.Size = new Size(80, 16);
			_portLabel.TabIndex = 4;
			_portLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _portBox
			// 
			_portBox.Location = new Point(104, 56);
			_portBox.Name = "_portBox";
			_portBox.Size = new Size(208, 20);
			_portBox.TabIndex = 5;
			// 
			// _sshGroup
			// 
			_sshGroup.Controls.AddRange(new Control[] {
																					_privateKeyFile,
																					_authOptions,
																					_privateKeyLabel,
																					_passphraseBox,
																					_userNameBox,
																					_authenticationLabel,
																					_passphraseLabel,
																					_usernameLabel,
																					_privateKeySelect});
			_sshGroup.Location = new Point(8, 88);
			_sshGroup.Name = "_sshGroup";
			_sshGroup.FlatStyle = FlatStyle.System;
			_sshGroup.Size = new Size(312, 112);
			_sshGroup.TabIndex = 6;
			_sshGroup.TabStop = false;
			// 
			// _usernameLabel
			// 
			_usernameLabel.ImeMode = ImeMode.NoControl;
			_usernameLabel.Location = new Point(8, 16);
			_usernameLabel.Name = "_usernameLabel";
			_usernameLabel.RightToLeft = RightToLeft.No;
			_usernameLabel.Size = new Size(80, 16);
			_usernameLabel.TabIndex = 7;
			_usernameLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _userNameBox
			// 
			_userNameBox.Location = new Point(96, 16);
			_userNameBox.Name = "_userNameBox";
			_userNameBox.Size = new Size(200, 20);
			_userNameBox.TabIndex = 8;
			// 
			// _authenticationLabel
			// 
			_authenticationLabel.ImeMode = ImeMode.NoControl;
			_authenticationLabel.Location = new Point(8, 40);
			_authenticationLabel.Name = "_authenticationLabel";
			_authenticationLabel.RightToLeft = RightToLeft.No;
			_authenticationLabel.Size = new Size(80, 16);
			_authenticationLabel.TabIndex = 9;
			_authenticationLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _authOptions
			// 
			_authOptions.DropDownStyle = ComboBoxStyle.DropDownList;
			_authOptions.Location = new Point(96, 40);
			_authOptions.Name = "_authOptions";
			_authOptions.Size = new Size(200, 20);
			_authOptions.TabIndex = 10;
			_authOptions.SelectedIndexChanged += new EventHandler(AdjustAuthenticationUI);
			// 
			// _passphraseLabel
			// 
			_passphraseLabel.ImeMode = ImeMode.NoControl;
			_passphraseLabel.Location = new Point(8, 64);
			_passphraseLabel.Name = "_passphraseLabel";
			_passphraseLabel.RightToLeft = RightToLeft.No;
			_passphraseLabel.Size = new Size(80, 16);
			_passphraseLabel.TabIndex = 11;
			_passphraseLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _passphraseBox
			// 
			_passphraseBox.Location = new Point(96, 64);
			_passphraseBox.Name = "_passphraseBox";
			_passphraseBox.PasswordChar = '*';
			_passphraseBox.Size = new Size(200, 19);
			_passphraseBox.TabIndex = 12;
			_passphraseBox.Text = "";
			// 
			// _privateKeyLabel
			// 
			_privateKeyLabel.ImeMode = ImeMode.NoControl;
			_privateKeyLabel.Location = new Point(8, 88);
			_privateKeyLabel.Name = "_privateKeyLabel";
			_privateKeyLabel.RightToLeft = RightToLeft.No;
			_privateKeyLabel.Size = new Size(72, 16);
			_privateKeyLabel.TabIndex = 14;
			_privateKeyLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _privateKeyFile
			// 
			_privateKeyFile.Location = new Point(96, 88);
			_privateKeyFile.Name = "_privateKeyFile";
			_privateKeyFile.Size = new Size(176, 19);
			_privateKeyFile.TabIndex = 15;
			_privateKeyFile.Text = "";
			// 
			// _privateKeySelect
			// 
			_privateKeySelect.FlatStyle = FlatStyle.Flat;
			_privateKeySelect.ImageIndex = 0;
			_privateKeySelect.FlatStyle = FlatStyle.System;
			_privateKeySelect.ImeMode = ImeMode.NoControl;
			_privateKeySelect.Location = new Point(272, 88);
			_privateKeySelect.Name = "_privateKeySelect";
			_privateKeySelect.RightToLeft = RightToLeft.No;
			_privateKeySelect.Size = new Size(19, 19);
			_privateKeySelect.TabIndex = 16;
			_privateKeySelect.Text = "...";
			_privateKeySelect.Click += new EventHandler(OnOpenPrivateKey);
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
																						 _selectLogButton, _terminalTypeLabel, _terminalTypeBox});
			_terminalGroup.Location = new Point(8, 208);
			_terminalGroup.Name = "_terminalGroup";
			_terminalGroup.FlatStyle = FlatStyle.System;
			_terminalGroup.Size = new Size(312, 168);
			_terminalGroup.TabIndex = 17;
			_terminalGroup.TabStop = false;
			// 
			// _logTypeLabel
			// 
			_logTypeLabel.ImeMode = ImeMode.NoControl;
			_logTypeLabel.Location = new Point(8, 16);
			_logTypeLabel.Name = "_logTypeLabel";
			_logTypeLabel.RightToLeft = RightToLeft.No;
			_logTypeLabel.Size = new Size(96, 16);
			_logTypeLabel.TabIndex = 18;
			_logTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _logTypeBox
			// 
			_logTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_logTypeBox.Items.AddRange(EnumDescAttributeT.For(typeof(LogType)).DescriptionCollection());
			_logTypeBox.Location = new Point(112, 16);
			_logTypeBox.Name = "_logTypeBox";
			_logTypeBox.Size = new Size(96, 20);
			_logTypeBox.TabIndex = 19;
			_logTypeBox.SelectionChangeCommitted += new EventHandler(OnLogTypeChanged);
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
			_logFileBox.Size = new Size(160, 20);
			_logFileBox.TabIndex = 21;
			// 
			// _selectLogButton
			// 
			_selectLogButton.FlatStyle = FlatStyle.Flat;
			_selectLogButton.ImageIndex = 0;
			_selectLogButton.FlatStyle = FlatStyle.System;
			_selectLogButton.ImeMode = ImeMode.NoControl;
			_selectLogButton.Location = new Point(272, 40);
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
			_encodingBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_encodingBox.Items.AddRange(EnumDescAttributeT.For(typeof(EncodingType)).DescriptionCollection());
			_encodingBox.Location = new Point(112, 64);
			_encodingBox.Name = "_encodingBox";
			_encodingBox.Size = new Size(96, 20);
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
			_localEchoBox.Items.AddRange(new object[] {
															   "Common.DoNot",
															   "Common.Do"});
			_localEchoBox.Location = new Point(112, 88);
			_localEchoBox.Name = "_localEchoBox";
			_localEchoBox.Size = new Size(96, 20);
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
			_newLineBox.Size = new Size(96, 20);
			_newLineBox.TabIndex = 28;
			// 
			// _terminalTypeLabel
			// 
			_terminalTypeLabel.Location = new Point(8, 136);
			_terminalTypeLabel.Name = "_terminalTypeLabel";
			_terminalTypeLabel.Size = new Size(96, 23);
			_terminalTypeLabel.TabIndex = 29;
			_terminalTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _terminalTypeBox
			// 
			_terminalTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_terminalTypeBox.Items.AddRange(EnumDescAttributeT.For(typeof(TerminalType)).DescriptionCollection());
			_terminalTypeBox.Location = new Point(112, 136);
			_terminalTypeBox.Name = "_terminalType";
			_terminalTypeBox.Size = new Size(96, 20);
			_terminalTypeBox.TabIndex = 30;
			// 
			// _loginButton
			// 
			_loginButton.ImageIndex = 0;
			_loginButton.ImeMode = ImeMode.NoControl;
			_loginButton.Location = new Point(160, 384);
			_loginButton.Name = "_loginButton";
			_loginButton.FlatStyle = FlatStyle.System;
			_loginButton.RightToLeft = RightToLeft.No;
			_loginButton.Size = new Size(72, 25);
			_loginButton.TabIndex = 29;
			_loginButton.Click += new EventHandler(OnOK);
			// 
			// _cancelButton
			// 
			_cancelButton.DialogResult = DialogResult.Cancel;
			_cancelButton.ImageIndex = 0;
			_cancelButton.FlatStyle = FlatStyle.System;
			_cancelButton.ImeMode = ImeMode.NoControl;
			_cancelButton.Location = new Point(248, 384);
			_cancelButton.Name = "_cancelButton";
			_cancelButton.RightToLeft = RightToLeft.No;
			_cancelButton.Size = new Size(72, 25);
			_cancelButton.TabIndex = 30;
			// 
			// LoginDialog
			// 
			AcceptButton = _loginButton;
			Anchor = AnchorStyles.None;
			AutoScaleBaseSize = new Size(5, 12);
			CancelButton = _cancelButton;
			ClientSize = new Size(330, 415);
			Controls.AddRange(new Control[] {
																		  _terminalGroup,
																		  _sshGroup,
																		  _hostBox,
																		  _methodBox,
																		  _portBox,
																		  _cancelButton,
																		  _loginButton,
																		  _methodLabel,
																		  _portLabel,
																		  _hostLabel});
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "LoginDialog";
			ShowInTaskbar = false;
			//this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            StartPosition = FormStartPosition.CenterScreen;
			_sshGroup.ResumeLayout(false);
			_terminalGroup.ResumeLayout(false);
			ResumeLayout(false);

		}
		#endregion
		private void InitializeText() {
			_hostLabel.Text = "Form.LoginDialog._hostLabel";
			_portLabel.Text = "Form.LoginDialog._portLabel";
			_methodLabel.Text = "Form.LoginDialog._methodLabel";
			_sshGroup.Text = "Form.LoginDialog._sshGroup";
			_privateKeyLabel.Text = "Form.LoginDialog._privateKeyLabel";
			_authenticationLabel.Text = "Form.LoginDialog._authenticationLabel";
			_passphraseLabel.Text = "Form.LoginDialog._passphraseLabel";
			_usernameLabel.Text = "Form.LoginDialog._usernameLabel";
			_terminalGroup.Text = "Form.LoginDialog._terminalGroup";
			_localEchoLabel.Text = "Form.LoginDialog._localEchoLabel";
			_newLineLabel.Text = "Form.LoginDialog._newLineLabel";
			_logFileLabel.Text = "Form.LoginDialog._logFileLabel";
			_encodingLabel.Text = "Form.LoginDialog._encodingLabel";
			_logTypeLabel.Text = "Form.LoginDialog._logTypeLabel";
			_terminalTypeLabel.Text = "Form.LoginDialog._terminalTypeLabel";
			_loginButton.Text = "OK";
			_cancelButton.Text = "Cancel";
			Text = "Form.LoginDialog.Text";

			_authOptions.Items.AddRange(EnumDescAttributeT.For(typeof(AuthType)).DescriptionCollection());
		}
		private void AdjustConnectionUI(object sender, EventArgs e) {
			if(_initializing) return;
			if(_methodBox.Text=="Telnet") {
				_portBox.SelectedIndex = 0; //Telnet:23
			}
			else {
				_portBox.SelectedIndex = 1; //SSH:22
				if(_authOptions.SelectedIndex==-1) _authOptions.SelectedIndex=0;
			}
			EnableValidControls();
		}
		private void AdjustAuthenticationUI(object sender, EventArgs e) {
			EnableValidControls();
		}
		private void InitializeLoginParams() {
			StringCollection c = _history.Hosts;
			foreach(string h in c) _hostBox.Items.Add(h);
			if(_hostBox.Items.Count>0) _hostBox.SelectedIndex = 0;

			c = _history.Accounts;
			foreach(string a in c) _userNameBox.Items.Add(a);
			if(_userNameBox.Items.Count>0) _userNameBox.SelectedIndex = 0;

			int[] ic = _history.Ports;
			foreach(int p in ic) _portBox.Items.Add(PortDescription(p));

			if(_hostBox.Items.Count > 0) {
				TCPTerminalParam last = _history.SearchByHost((string)_hostBox.Items[0]);
				if(last!=null) ApplyParam(last);
			}

			c = _history.LogPaths;
			foreach(string p in c) _logFileBox.Items.Add(p);

			if(GApp.Options.DefaultLogType!=LogType.None) {
				_logTypeBox.SelectedIndex = (int)GApp.Options.DefaultLogType;
				string t = GUtil.CreateLogFileName(null);
				_logFileBox.Items.Add(t);
				_logFileBox.Text = t;
			}
			else
				_logTypeBox.SelectedIndex = 0;

		}
		private void EnableValidControls() {
			bool ssh = _methodBox.Text!="Telnet";
			bool pubkey = _authOptions.SelectedIndex==(int)AuthType.PublicKey;
			bool kbd = _authOptions.SelectedIndex==(int)AuthType.KeyboardInteractive;

			_userNameBox.Enabled = ssh;
			_authOptions.Enabled = ssh;
			_passphraseBox.Enabled = ssh && (pubkey || !kbd);
			_privateKeyFile.Enabled = ssh && pubkey;
			_privateKeySelect.Enabled = ssh && pubkey;
			
			bool e = ((LogType)EnumDescAttributeT.For(typeof(LogType)).FromDescription(_logTypeBox.Text, LogType.None)!=LogType.None);
			_logFileBox.Enabled = e;
			_selectLogButton.Enabled = e;
		}
		
		public void ApplyParam(TCPTerminalParam param) {
			_initializing = true;
			_methodBox.SelectedIndex = (int)param.Method;
			_portBox.SelectedIndex = _portBox.FindStringExact(PortDescription(param.Port));
			_methodBox.SelectedIndex = _methodBox.FindStringExact(param.Method.ToString());
			if(param.IsSSH) {
				SSHTerminalParam sp = (SSHTerminalParam)param;
				_userNameBox.SelectedIndex = _userNameBox.FindStringExact(sp.Account);
				_passphraseBox.Text = sp.Passphrase;
				
				if(sp.AuthType==AuthType.PublicKey)
					_privateKeyFile.Text = sp.IdentityFile;
				else
					_privateKeyFile.Text = "";
				_authOptions.SelectedIndex = (int)sp.AuthType;
			}

			_encodingBox.SelectedIndex = (int)param.EncodingProfile.Type;
			_newLineBox.SelectedIndex = (int)param.TransmitNL;
			_localEchoBox.SelectedIndex = param.LocalEcho? 1 : 0;
			_terminalTypeBox.SelectedIndex = (int)param.TerminalType;
			_initializing = false;
			
			EnableValidControls();
		}

		public ConnectionTag Result {
			get {
				return _result;
			}
		}

		private void OnHostIsSelected(object sender, EventArgs e) {
			if(_initializing) return;
			string host = _hostBox.Text;
			TCPTerminalParam param = _history.SearchByHost(host);
			Debug.Assert(param!=null);
			ApplyParam(param);
		}

		private static int ParsePort(string text) {
			//頻出のやつ
			if(text.IndexOf("(22)")!=-1)
				return 22;
			if(text.IndexOf("(23)")!=-1)
				return 23;
			
			try {
				return Int32.Parse(text);
			}
			catch(FormatException) {
				throw new FormatException(String.Format("Message.LoginDialog.InvalidPort", text));
			}
		}
		private static ConnectionMethod ParseMethod(string text) {
			if(text.IndexOf("SSH1")!=-1)
				return ConnectionMethod.SSH1;
			else if(text.IndexOf("SSH2")!=-1)
				return ConnectionMethod.SSH2;
			else if(text.IndexOf("Telnet")!=-1)
				return ConnectionMethod.Telnet;
			else
				throw new ArgumentException("unknown method "+text);
		}

		private static string PortDescription(int port) {
			if(port==22)
				return "SSH(22)";
			else if(port==23)
				return "Telnet(23)";
			else
				return port.ToString();
		}


		private void OnOpenPrivateKey(object sender, EventArgs e) {
			string fn = GCUtil.SelectPrivateKeyFileByDialog(this);
			if(fn!=null) _privateKeyFile.Text = fn;
			_privateKeySelect.Focus(); //どっちにしても次のフォーカスは鍵選択ボタンへ
		}

		public void OnOK(object sender, EventArgs e) {
			DialogResult = DialogResult.None;
			TCPTerminalParam param = ValidateContent();
			if(param==null) return;  //パラメータに誤りがあれば即脱出

			_loginButton.Enabled = false;
			_cancelButton.Enabled = false;
			Cursor = Cursors.WaitCursor;
			Text = "Caption.LoginDialog.Connecting";
			_savedHWND = Handle;

			HostKeyCheckCallback checker = null;
			if(param.IsSSH)
				checker = new HostKeyCheckCallback(new HostKeyChecker(this, (SSHTerminalParam)param).CheckHostKeyCallback);

			_connector = CommunicationUtil.StartNewConnection(this, param, _passphraseBox.Text, checker);
			if(_connector==null) ClearConnectingState();
		}

		//入力内容に誤りがあればそれを警告してnullを返す。なければ必要なところを埋めたTCPTerminalParamを返す
		private TCPTerminalParam ValidateContent() {
			string msg = null;
			TCPTerminalParam p = null;
			SSHTerminalParam sp = null;
			try {
				ConnectionMethod m = ParseMethod(_methodBox.Text);
				if(m==ConnectionMethod.Telnet)
					p = new TelnetTerminalParam("");
				else {
					p = sp = new SSHTerminalParam(ConnectionMethod.SSH2, "", "", "");
					sp.Method = m;
					sp.Account = _userNameBox.Text;
				}

				p.Host = _hostBox.Text;
				try {
					p.Port = ParsePort(_portBox.Text);
				}
				catch(FormatException ex) {
					msg = ex.Message;
				}

				if(_hostBox.Text.Length==0)
					msg = "Message.LoginDialog.HostIsEmpty";

				p.LogType = (LogType)EnumDescAttributeT.For(typeof(LogType)).FromDescription(_logTypeBox.Text, LogType.None);

                if(p.LogType!=LogType.None) {
					p.LogPath = _logFileBox.Text;
					if(p.LogPath==GUtil.CreateLogFileName(null)) p.LogPath = GUtil.CreateLogFileName(_hostBox.Text);
					LogFileCheckResult r = GCUtil.CheckLogFileName(p.LogPath, this);
					if(r==LogFileCheckResult.Cancel || r==LogFileCheckResult.Error) return null;
					p.LogAppend = (r==LogFileCheckResult.Append);
				}

				if(p.IsSSH) {
					Debug.Assert(sp!=null);
					sp.AuthType = (AuthType)_authOptions.SelectedIndex;
					if(sp.AuthType==AuthType.PublicKey) {
						if(!File.Exists(_privateKeyFile.Text))
							msg = "Message.LoginDialog.KeyFileNotExist";
						else
							sp.IdentityFile = _privateKeyFile.Text;
					}
				}
				p.EncodingProfile = EncodingProfile.Get((EncodingType)_encodingBox.SelectedIndex);

				p.LocalEcho = _localEchoBox.SelectedIndex==1;
				p.TransmitNL = (NewLine)EnumDescAttributeT.For(typeof(NewLine)).FromDescription(_newLineBox.Text, NewLine.CR);
				p.TerminalType = (TerminalType)_terminalTypeBox.SelectedIndex;

				if(msg!=null) {
					ShowError(msg);
					return null;
				}
				else
					return p;
			}
			catch(Exception ex) {
				GUtil.Warning(this, ex.Message);
				return null;
			}

		}

		protected override void OnActivated(EventArgs args) {
			if(_firstFlag) {
				_firstFlag = false;
				_hostBox.Focus();
			}
		}

		private void SelectLog(object sender, EventArgs e) {
			string fn = GCUtil.SelectLogFileByDialog(this);
			if(fn!=null) _logFileBox.Text = fn;
		}
		private void OnLogTypeChanged(object sender, EventArgs e) {
			if(_initializing) return;
			EnableValidControls();
		}

		protected override bool ProcessDialogKey(Keys key) {
			if(_connector!=null && key==(Keys.Control | Keys.C)) {
				_connector.Interrupt();
				ClearConnectingState();
				return true;
			}
			else
				return base.ProcessDialogKey(key);
		}
		private void ClearConnectingState() {
			_loginButton.Enabled = true;
			_cancelButton.Enabled = true;
			Cursor = Cursors.Default;
			Text = "Form.LoginDialog.Text";
			_connector = null;
		}

		private void ShowError(string msg) {
			GUtil.Warning(this, msg, "Caption.LoginDialog.ConnectionError");
		}

		protected override void WndProc(ref Message msg) {
			base.WndProc(ref msg);
			if(msg.Msg==GConst.WMG_ASYNCCONNECT) {
				if(msg.LParam.ToInt32()==1) {
					DialogResult = DialogResult.OK;
					Close();
				}
				else {
					ClearConnectingState();
					ShowError(_errorMessage);
				}
			}
		}
		//ISocketWithTimeoutClient これらはこのウィンドウとは別のスレッドで実行されるので慎重に
		public void SuccessfullyExit(object result) {
			_result = (ConnectionTag)result;
			Win32.SendMessage(_savedHWND, GConst.WMG_ASYNCCONNECT, IntPtr.Zero, new IntPtr(1));
		}
		public void ConnectionFailed(string message) {
			_errorMessage = message;
			Win32.SendMessage(_savedHWND, GConst.WMG_ASYNCCONNECT, IntPtr.Zero, IntPtr.Zero);
		}
		public void CancelTimer() {
		}
		public IWin32Window GetWindow() {
			return this;
		}
	}
}
