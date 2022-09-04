/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: SSHShortcutLoginDialog.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.Drawing;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using Granados.SSHC;
using Poderosa.Connection;
using Poderosa.ConnectionParam;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;
using Poderosa.Communication;
using Poderosa.SSH;

namespace Poderosa.Forms
{
    internal class SSHShortcutLoginDialog : Form, ISocketWithTimeoutClient
    {
        #region fields
        private SSHTerminalParam _terminalParam;
		private ConnectionTag _result;
		private SocketWithTimeout _connector;
		private string _errorMessage;

		private TextBox _privateKeyBox;
		private TextBox _passphraseBox;
		private Button _privateKeySelect;
		private Label _hostLabel;
		private Label _hostBox;
		private Label _methodLabel;
		private Label _methodBox;
		private Label _accountLabel;
		private Label _accountBox;
		private Label _authTypeLabel;
		private Label _authTypeBox;
		private Label _encodingLabel;
		private Label _encodingBox;
		private ComboBox _logFileBox;
		private Button _selectlogButton;
		private Button _cancelButton;
		private Button _loginButton;
		private Label _privateKeyLabel;
		private Label _passphraseLabel;
		private Label _logFileLabel;
		private ComboBox _logTypeBox;
		private Label _logTypeLabel;
        #endregion

		private Container components = null;
		public SSHShortcutLoginDialog(SSHTerminalParam param)
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			_privateKeyLabel.Text = "Form.SSHShortcutLoginDialog._privateKeyLabel";
			_passphraseLabel.Text = "Form.SSHShortcutLoginDialog._passphraseLabel";
			_logFileLabel.Text = "Form.SSHShortcutLoginDialog._logFileLabel";
			_hostLabel.Text = "Form.SSHShortcutLoginDialog._hostLabel";
			_methodLabel.Text = "Form.SSHShortcutLoginDialog._methodLabel";
			_accountLabel.Text = "Form.SSHShortcutLoginDialog._accountLabel";
			_authTypeLabel.Text = "Form.SSHShortcutLoginDialog._authTypeLabel";
			_encodingLabel.Text = "Form.SSHShortcutLoginDialog._encodingLabel";
			_logTypeLabel.Text = "Form.SSHShortcutLoginDialog._logTypeLabel";
			Text = "Form.SSHShortcutLoginDialog.Text";
			_cancelButton.Text = "Cancel";
			_loginButton.Text = "OK";

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			_terminalParam = param;
			InitUI();
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
		public ConnectionTag Result {
			get {
				return _result;
			}
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// this._logTypeBox.Items.AddRange(EnumDescAttributeT.For(typeof(LogType)).DescriptionCollection());
		/// </summary>
		private void InitializeComponent()
		{
			_privateKeyBox = new TextBox();
			_privateKeyLabel = new Label();
			_passphraseBox = new TextBox();
			_passphraseLabel = new Label();
			_privateKeySelect = new Button();
			_logFileBox = new ComboBox();
			_logFileLabel = new Label();
			_selectlogButton = new Button();
			_cancelButton = new Button();
			_loginButton = new Button();
			_hostLabel = new Label();
			_hostBox = new Label();
			_methodLabel = new Label();
			_methodBox = new Label();
			_accountLabel = new Label();
			_accountBox = new Label();
			_authTypeLabel = new Label();
			_authTypeBox = new Label();
			_encodingLabel = new Label();
			_encodingBox = new Label();
			_logTypeBox = new ComboBox();
			_logTypeLabel = new Label();
			SuspendLayout();
			// 
			// _privateKeyBox
			// 
			_privateKeyBox.Location = new Point(104, 128);
			_privateKeyBox.Name = "_privateKeyBox";
			_privateKeyBox.Size = new Size(160, 19);
			_privateKeyBox.TabIndex = 3;
			_privateKeyBox.Text = "";
			// 
			// _privateKeyLabel
			// 
			_privateKeyLabel.ImeMode = ImeMode.NoControl;
			_privateKeyLabel.Location = new Point(8, 128);
			_privateKeyLabel.Name = "_privateKeyLabel";
			_privateKeyLabel.RightToLeft = RightToLeft.No;
			_privateKeyLabel.Size = new Size(72, 16);
			_privateKeyLabel.TabIndex = 2;
			_privateKeyLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _passphraseBox
			// 
			_passphraseBox.Location = new Point(104, 104);
			_passphraseBox.Name = "_passphraseBox";
			_passphraseBox.PasswordChar = '*';
			_passphraseBox.Size = new Size(184, 19);
			_passphraseBox.TabIndex = 1;
			_passphraseBox.Text = "";
			// 
			// _passphraseLabel
			// 
			_passphraseLabel.ImeMode = ImeMode.NoControl;
			_passphraseLabel.Location = new Point(8, 104);
			_passphraseLabel.Name = "_passphraseLabel";
			_passphraseLabel.RightToLeft = RightToLeft.No;
			_passphraseLabel.Size = new Size(80, 16);
			_passphraseLabel.TabIndex = 0;
			_passphraseLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _privateKeySelect
			// 
			_privateKeySelect.FlatStyle = FlatStyle.System;
			_privateKeySelect.ImageIndex = 0;
			_privateKeySelect.ImeMode = ImeMode.NoControl;
			_privateKeySelect.Location = new Point(272, 128);
			_privateKeySelect.Name = "_privateKeySelect";
			_privateKeySelect.RightToLeft = RightToLeft.No;
			_privateKeySelect.Size = new Size(19, 19);
			_privateKeySelect.TabIndex = 4;
			_privateKeySelect.Text = "...";
			_privateKeySelect.Click += new EventHandler(OnOpenPrivateKey);
			// 
			// _logFileBox
			// 
			_logFileBox.Location = new Point(104, 176);
			_logFileBox.Name = "_logFileBox";
			_logFileBox.Size = new Size(160, 20);
			_logFileBox.TabIndex = 8;
			// 
			// _logFileLabel
			// 
			_logFileLabel.ImeMode = ImeMode.NoControl;
			_logFileLabel.Location = new Point(8, 176);
			_logFileLabel.Name = "_logFileLabel";
			_logFileLabel.RightToLeft = RightToLeft.No;
			_logFileLabel.Size = new Size(88, 16);
			_logFileLabel.TabIndex = 7;
			_logFileLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _selectlogButton
			// 
			_selectlogButton.FlatStyle = FlatStyle.System;
			_selectlogButton.ImageIndex = 0;
			_selectlogButton.ImeMode = ImeMode.NoControl;
			_selectlogButton.Location = new Point(272, 176);
			_selectlogButton.Name = "_selectlogButton";
			_selectlogButton.RightToLeft = RightToLeft.No;
			_selectlogButton.Size = new Size(19, 19);
			_selectlogButton.TabIndex = 9;
			_selectlogButton.Text = "...";
			_selectlogButton.Click += new EventHandler(SelectLog);
			// 
			// _cancelButton
			// 
			_cancelButton.DialogResult = DialogResult.Cancel;
			_cancelButton.ImageIndex = 0;
			_cancelButton.ImeMode = ImeMode.NoControl;
			_cancelButton.Location = new Point(216, 208);
			_cancelButton.Name = "_cancelButton";
			_cancelButton.FlatStyle = FlatStyle.System;
			_cancelButton.RightToLeft = RightToLeft.No;
			_cancelButton.Size = new Size(72, 25);
			_cancelButton.TabIndex = 11;
			// 
			// _loginButton
			// 
			_loginButton.DialogResult = DialogResult.OK;
			_loginButton.ImageIndex = 0;
			_loginButton.FlatStyle = FlatStyle.System;
			_loginButton.ImeMode = ImeMode.NoControl;
			_loginButton.Location = new Point(128, 208);
			_loginButton.Name = "_loginButton";
			_loginButton.RightToLeft = RightToLeft.No;
			_loginButton.Size = new Size(72, 25);
			_loginButton.TabIndex = 10;
			_loginButton.Click += new EventHandler(OnOK);
			// 
			// _hostLabel
			// 
			_hostLabel.ImeMode = ImeMode.NoControl;
			_hostLabel.Location = new Point(8, 8);
			_hostLabel.Name = "_hostLabel";
			_hostLabel.Size = new Size(80, 16);
			_hostLabel.TabIndex = 0;
			_hostLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _hostBox
			// 
			_hostBox.ImeMode = ImeMode.NoControl;
			_hostBox.Location = new Point(104, 8);
			_hostBox.Name = "_hostBox";
			_hostBox.Size = new Size(144, 16);
			_hostBox.TabIndex = 35;
			_hostBox.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _methodLabel
			// 
			_methodLabel.ImeMode = ImeMode.NoControl;
			_methodLabel.Location = new Point(8, 24);
			_methodLabel.Name = "_methodLabel";
			_methodLabel.Size = new Size(80, 16);
			_methodLabel.TabIndex = 0;
			_methodLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _methodBox
			// 
			_methodBox.ImeMode = ImeMode.NoControl;
			_methodBox.Location = new Point(104, 24);
			_methodBox.Name = "_methodBox";
			_methodBox.Size = new Size(144, 16);
			_methodBox.TabIndex = 0;
			_methodBox.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _accountLabel
			// 
			_accountLabel.ImeMode = ImeMode.NoControl;
			_accountLabel.Location = new Point(8, 40);
			_accountLabel.Name = "_accountLabel";
			_accountLabel.Size = new Size(80, 16);
			_accountLabel.TabIndex = 0;
			_accountLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _accountBox
			// 
			_accountBox.ImeMode = ImeMode.NoControl;
			_accountBox.Location = new Point(104, 40);
			_accountBox.Name = "_accountBox";
			_accountBox.Size = new Size(144, 16);
			_accountBox.TabIndex = 0;
			_accountBox.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _authTypeLabel
			// 
			_authTypeLabel.ImeMode = ImeMode.NoControl;
			_authTypeLabel.Location = new Point(8, 56);
			_authTypeLabel.Name = "_authTypeLabel";
			_authTypeLabel.Size = new Size(80, 16);
			_authTypeLabel.TabIndex = 0;
			_authTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _authTypeBox
			// 
			_authTypeBox.ImeMode = ImeMode.NoControl;
			_authTypeBox.Location = new Point(104, 56);
			_authTypeBox.Name = "_authTypeBox";
			_authTypeBox.Size = new Size(144, 16);
			_authTypeBox.TabIndex = 0;
			_authTypeBox.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _encodingLabel
			// 
			_encodingLabel.ImeMode = ImeMode.NoControl;
			_encodingLabel.Location = new Point(8, 72);
			_encodingLabel.Name = "_encodingLabel";
			_encodingLabel.Size = new Size(80, 16);
			_encodingLabel.TabIndex = 0;
			_encodingLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _encodingBox
			// 
			_encodingBox.ImeMode = ImeMode.NoControl;
			_encodingBox.Location = new Point(104, 72);
			_encodingBox.Name = "_encodingBox";
			_encodingBox.Size = new Size(144, 16);
			_encodingBox.TabIndex = 0;
			_encodingBox.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// _logTypeBox
			// 
			_logTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			_logTypeBox.Items.AddRange(EnumDescAttributeT.For(typeof(LogType)).DescriptionCollection());
			_logTypeBox.Location = new Point(104, 152);
			_logTypeBox.Name = "_logTypeBox";
			_logTypeBox.Size = new Size(96, 20);
			_logTypeBox.TabIndex = 6;
			_logTypeBox.SelectionChangeCommitted += new EventHandler(OnLogTypeChanged);
			// 
			// _logTypeLabel
			// 
			_logTypeLabel.ImeMode = ImeMode.NoControl;
			_logTypeLabel.Location = new Point(8, 152);
			_logTypeLabel.Name = "_logTypeLabel";
			_logTypeLabel.RightToLeft = RightToLeft.No;
			_logTypeLabel.Size = new Size(80, 16);
			_logTypeLabel.TabIndex = 5;
			_logTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// SSHShortcutLoginDialog
			// 
			AcceptButton = _loginButton;
			AutoScaleBaseSize = new Size(5, 12);
			CancelButton = _cancelButton;
			ClientSize = new Size(298, 239);
			Controls.AddRange(new Control[] {
																		  _logTypeBox,
																		  _logTypeLabel,
																		  _cancelButton,
																		  _loginButton,
																		  _logFileBox,
																		  _logFileLabel,
																		  _selectlogButton,
																		  _hostLabel,
																		  _hostBox,
																		  _methodLabel,
																		  _methodBox,
																		  _accountLabel,
																		  _accountBox,
																		  _authTypeLabel,
																		  _authTypeBox,
																		  _encodingLabel,
																		  _encodingBox,
																		  _privateKeyBox,
																		  _privateKeyLabel,
																		  _passphraseBox,
																		  _passphraseLabel,
																		  _privateKeySelect});
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "SSHShortcutLoginDialog";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			ResumeLayout(false);

		}
		#endregion
		private void InitUI() {
			_hostBox.Text = _terminalParam.Host;
			_methodBox.Text = _terminalParam.Method.ToString();
			if(_terminalParam.Port!=22) _methodBox.Text += String.Format("Caption.SSHShortcutLoginDialog.NotStandardPort", _terminalParam.Port);
			_accountBox.Text = _terminalParam.Account;
			_authTypeBox.Text = EnumDescAttributeT.For(typeof(AuthType)).GetDescription(_terminalParam.AuthType);
			_encodingBox.Text = EnumDescAttributeT.For(typeof(EncodingType)).GetDescription(_terminalParam.EncodingProfile.Type);
			
			if(_terminalParam.AuthType==AuthType.Password) {
				_privateKeyBox.Enabled = false;
				_privateKeySelect.Enabled = false;
			}
			else if(_terminalParam.AuthType==AuthType.PublicKey) {
				_privateKeyBox.Text = _terminalParam.IdentityFile;
			}
			else if(_terminalParam.AuthType==AuthType.KeyboardInteractive) {
				_privateKeyBox.Enabled = false;
				_privateKeySelect.Enabled = false;
				_passphraseBox.Enabled = false;
			}

			_passphraseBox.Text = _terminalParam.Passphrase;

			StringCollection c = GApp.ConnectionHistory.LogPaths;
			foreach(string p in c) _logFileBox.Items.Add(p);

			if(GApp.Options.DefaultLogType!=LogType.None) {
				_logTypeBox.SelectedIndex = (int)GApp.Options.DefaultLogType;
				string t = GUtil.CreateLogFileName(_terminalParam.Host);
				_logFileBox.Items.Add(t);
				_logFileBox.Text = t;
			}
			else
				_logTypeBox.SelectedIndex = 0;

			AdjustUI();
		}
		private void AdjustUI() {
			_passphraseBox.Enabled = _terminalParam.AuthType!=AuthType.KeyboardInteractive;
			
			bool e = _logTypeBox.SelectedIndex!=(int)LogType.None;
			_logFileBox.Enabled = e;
			_selectlogButton.Enabled = e;
		}

		private void OnOK(object sender, EventArgs e) {
			DialogResult = DialogResult.None;
			TCPTerminalParam param = ValidateContent();
			if(param==null) return;  //パラメータに誤りがあれば即脱出

			_loginButton.Enabled = false;
			_cancelButton.Enabled = false;
			Cursor = Cursors.WaitCursor;
			Text = "Caption.SSHShortcutLoginDialog.Connecting";

			HostKeyCheckCallback checker = new HostKeyCheckCallback(new HostKeyChecker(this, (SSHTerminalParam)param).CheckHostKeyCallback);
			_connector = CommunicationUtil.StartNewConnection(this, param, _passphraseBox.Text, checker);
			if(_connector==null) ClearConnectingState();
		}
		private SSHTerminalParam ValidateContent() {
			SSHTerminalParam p = _terminalParam;
			string msg = null;

			try {
				p.LogType = (LogType)EnumDescAttributeT.For(typeof(LogType)).FromDescription(_logTypeBox.Text, LogType.None);
				if(p.LogType!=LogType.None) {
					p.LogPath = _logFileBox.Text;
					LogFileCheckResult r = GCUtil.CheckLogFileName(p.LogPath, this);
					if(r==LogFileCheckResult.Cancel || r==LogFileCheckResult.Error) return null;
					p.LogAppend = (r==LogFileCheckResult.Append);
				}

				if(p.AuthType==AuthType.PublicKey) {
					if(!File.Exists(_privateKeyBox.Text))
						msg = "Message.SSHShortcutLoginDialog.KeyFileNotExist";
					else
						p.IdentityFile = _privateKeyBox.Text;
				}


				if(msg!=null) {
					GUtil.Warning(this, msg);
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
		private void OnOpenPrivateKey(object sender, EventArgs e) {
			string fn = GCUtil.SelectPrivateKeyFileByDialog(this);
			if(fn!=null) _privateKeyBox.Text = fn;
		}
		private void SelectLog(object sender, EventArgs e) {
			string fn = GCUtil.SelectLogFileByDialog(this);
			if(fn!=null) _logFileBox.Text = fn;
		}
		private void OnLogTypeChanged(object sender, EventArgs args) {
			AdjustUI();
		}
		private void ShowError(string msg) {
			GUtil.Warning(this, msg, "Message.SSHShortcutLoginDialog.ConnectionError");
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
			Text = "Form.SSHShortcutLoginDialog.Text";
			_connector = null;
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
		public void SuccessfullyExit(object result) {
			_result = (ConnectionTag)result;
			//_result.SetServerInfo(((TCPTerminalParam)_result.Param).Host, swt.IPAddress);
			Win32.SendMessage(Handle, GConst.WMG_ASYNCCONNECT, IntPtr.Zero, new IntPtr(1));
		}
		public void ConnectionFailed(string message) {
			_errorMessage = message;
			Win32.SendMessage(Handle, GConst.WMG_ASYNCCONNECT, IntPtr.Zero, IntPtr.Zero);
		}
		public void CancelTimer() {
		}
		public IWin32Window GetWindow() {
			return this;
		}

	}
}
