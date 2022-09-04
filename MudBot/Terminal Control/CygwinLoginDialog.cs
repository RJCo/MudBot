/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: CygwinLoginDialog.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using System;
using System.Drawing;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;

using Poderosa.Connection;
using Poderosa.ConnectionParam;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;
using Poderosa.Communication;
using Poderosa.LocalShell;

namespace Poderosa.Forms
{
    internal class LocalShellLoginDialog : Form, ISocketWithTimeoutClient
    {
        private string _errorMessage;
        private LocalShellTerminalParam _param;
        private ConnectionTag _result;
        private LocalShellUtil.Connector _connector;
        private IntPtr _savedHWND;

        private Label _logTypeLabel;
        private ComboBox _logTypeBox;
        private Label _logFileLabel;
        private ComboBox _logFileBox;
        private Button _selectlogButton;
        private CheckBox _advancedOptionCheck;
        private GroupBox _advancedOptionGroup;
        private Label _homeDirectoryLabel;
        private Label _shellLabel;
        private TextBox _homeDirectoryBox;
        private TextBox _shellBox;
        private Label _lMessage;
        private Button _okButton;
        private Button _cancelButton;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public LocalShellLoginDialog()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            _okButton.Text = "OK";
            _cancelButton.Text = "Cancel";
            _homeDirectoryLabel.Text = "Home Directory";
            _lMessage.Text = "If you cannot start the shell, please compile CygTerm again.";
            _advancedOptionCheck.Text = "Advanced Configuration";
            _shellLabel.Text = "Shell";
            _logFileLabel.Text = "Log File";
            _logTypeLabel.Text = "Log Type";
        }

        public ConnectionTag Result
        {
            get
            {
                return _result;
            }
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

        #region Windows フォーム デザイナで生成されたコード 
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            _okButton = new Button();
            _cancelButton = new Button();
            _logTypeBox = new ComboBox();
            _logTypeLabel = new Label();
            _logFileBox = new ComboBox();
            _logFileLabel = new Label();
            _selectlogButton = new Button();
            _advancedOptionCheck = new CheckBox();
            _advancedOptionGroup = new GroupBox();
            _homeDirectoryLabel = new Label();
            _homeDirectoryBox = new TextBox();
            _shellLabel = new Label();
            _shellBox = new TextBox();
            _lMessage = new Label();
            SuspendLayout();
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.Location = new Point(144, 176);
            _okButton.Name = "_okButton";
            _okButton.TabIndex = 0;
            _okButton.Click += new EventHandler(OnOK);
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.Location = new Point(232, 176);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.TabIndex = 1;
            // 
            // _logTypeLabel
            // 
            _logTypeLabel.ImeMode = ImeMode.NoControl;
            _logTypeLabel.Location = new Point(8, 8);
            _logTypeLabel.Name = "_logTypeLabel";
            _logTypeLabel.RightToLeft = RightToLeft.No;
            _logTypeLabel.Size = new Size(80, 16);
            _logTypeLabel.TabIndex = 2;
            _logTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _logTypeBox
            // 
            _logTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _logTypeBox.Items.AddRange(EnumDescAttributeT.For(typeof(LogType)).DescriptionCollection());
            _logTypeBox.Location = new Point(104, 8);
            _logTypeBox.Name = "_logTypeBox";
            _logTypeBox.Size = new Size(96, 20);
            _logTypeBox.TabIndex = 3;
            _logTypeBox.SelectionChangeCommitted += new EventHandler(OnLogTypeChanged);
            // 
            // _logFileLabel
            // 
            _logFileLabel.ImeMode = ImeMode.NoControl;
            _logFileLabel.Location = new Point(8, 32);
            _logFileLabel.Name = "_logFileLabel";
            _logFileLabel.RightToLeft = RightToLeft.No;
            _logFileLabel.Size = new Size(88, 16);
            _logFileLabel.TabIndex = 4;
            _logFileLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _logFileBox
            // 
            _logFileBox.Location = new Point(104, 32);
            _logFileBox.Name = "_logFileBox";
            _logFileBox.Size = new Size(160, 20);
            _logFileBox.TabIndex = 5;
            // 
            // _selectlogButton
            // 
            _selectlogButton.FlatStyle = FlatStyle.System;
            _selectlogButton.ImageIndex = 0;
            _selectlogButton.ImeMode = ImeMode.NoControl;
            _selectlogButton.Location = new Point(272, 32);
            _selectlogButton.Name = "_selectlogButton";
            _selectlogButton.RightToLeft = RightToLeft.No;
            _selectlogButton.Size = new Size(19, 19);
            _selectlogButton.TabIndex = 6;
            _selectlogButton.Text = "...";
            _selectlogButton.Click += new EventHandler(SelectLog);
            // 
            // _advancedOptionCheck
            // 
            _advancedOptionCheck.Location = new Point(20, 56);
            _advancedOptionCheck.Size = new Size(152, 20);
            _advancedOptionCheck.TabIndex = 7;
            _advancedOptionCheck.FlatStyle = FlatStyle.System;
            _advancedOptionCheck.CheckedChanged += new EventHandler(OnAdvancedOptionCheckedChanged);
            // 
            // _advancedOptionGroup
            // 
            _advancedOptionGroup.Location = new Point(8, 58);
            _advancedOptionGroup.Size = new Size(300, 110);
            _advancedOptionGroup.TabIndex = 8;
            _advancedOptionGroup.Enabled = false;
            _advancedOptionGroup.FlatStyle = FlatStyle.System;
            // 
            // _homeDirectoryLabel
            // 
            _homeDirectoryLabel.Location = new Point(8, 24);
            _homeDirectoryLabel.Name = "_homeDirectoryLabel";
            _homeDirectoryLabel.Size = new Size(112, 23);
            _homeDirectoryLabel.TabIndex = 9;
            _homeDirectoryLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _homeDirectoryBox
            // 
            _homeDirectoryBox.Location = new Point(120, 24);
            _homeDirectoryBox.Name = "_homeDirectoryBox";
            _homeDirectoryBox.Size = new Size(172, 19);
            _homeDirectoryBox.TabIndex = 10;
            _homeDirectoryBox.Text = "";
            // 
            // _shellLabel
            // 
            _shellLabel.Location = new Point(8, 48);
            _shellLabel.Name = "_shellLabel";
            _shellLabel.TabIndex = 11;
            _shellLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _shellBox
            // 
            _shellBox.Location = new Point(120, 48);
            _shellBox.Name = "_shellBox";
            _shellBox.Size = new Size(172, 19);
            _shellBox.TabIndex = 12;
            _shellBox.Text = "";
            // 
            // _lMessage
            // 
            _lMessage.Location = new Point(8, 72);
            _lMessage.Name = "_lMessage";
            _lMessage.Size = new Size(288, 32);
            _lMessage.TabIndex = 6;
            _lMessage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CygwinLoginDialog
            // 
            _advancedOptionGroup.Controls.Add(_lMessage);
            _advancedOptionGroup.Controls.Add(_shellBox);
            _advancedOptionGroup.Controls.Add(_shellLabel);
            _advancedOptionGroup.Controls.Add(_homeDirectoryBox);
            _advancedOptionGroup.Controls.Add(_homeDirectoryLabel);
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(314, 208);
            Controls.Add(_cancelButton);
            Controls.Add(_okButton);
            Controls.Add(_logTypeLabel);
            Controls.Add(_logTypeBox);
            Controls.Add(_logFileLabel);
            Controls.Add(_logFileBox);
            Controls.Add(_selectlogButton);
            Controls.Add(_advancedOptionCheck);
            Controls.Add(_advancedOptionGroup);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CygwinLoginDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        public void ApplyParam(LocalShellTerminalParam param)
        {
            _param = param;
            if (param is CygwinTerminalParam)
                Text = "Connection to Cygwin";
            else
                Text = "Connection to SFU";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_param == null)
                _param = new CygwinTerminalParam();
            else
                _param = (LocalShellTerminalParam)_param.Clone();
            _param.RenderProfile = null;
            _param.Caption = null;

            _homeDirectoryBox.Text = _param.Home;
            _shellBox.Text = _param.Shell;

            StringCollection c = GApp.ConnectionHistory.LogPaths;
            foreach (string p in c) _logFileBox.Items.Add(p);

            if (GApp.Options.DefaultLogType != LogType.None)
            {
                _logTypeBox.SelectedIndex = (int)GApp.Options.DefaultLogType;
                string t = GUtil.CreateLogFileName("cygwin");
                _logFileBox.Items.Add(t);
                _logFileBox.Text = t;
            }
            else
                _logTypeBox.SelectedIndex = 0;

            AdjustUI();
        }

        private void OnOK(object sender, EventArgs args)
        {
            DialogResult = DialogResult.None;
            if (_homeDirectoryBox.Text.Length == 0)
                GUtil.Warning(this, "The home directory is empty.");
            else if (_shellBox.Text.Length == 0)
                GUtil.Warning(this, "The shell is empty.");

            _param.LogType = (LogType)EnumDescAttributeT.For(typeof(LogType)).FromDescription(_logTypeBox.Text, LogType.None);
            if (_param.LogType != LogType.None)
            {
                _param.LogPath = _logFileBox.Text;
                LogFileCheckResult r = GCUtil.CheckLogFileName(_param.LogPath, this);
                if (r == LogFileCheckResult.Cancel || r == LogFileCheckResult.Error) return;
                _param.LogAppend = (r == LogFileCheckResult.Append);
            }

            _param.Home = _homeDirectoryBox.Text;
            _param.Shell = _shellBox.Text;

            _okButton.Enabled = false;
            _cancelButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
            _savedHWND = Handle;
            if (_param is CygwinTerminalParam)
                Text = "Connection to Cygwin - Connecting... (Ctrl+C to Stop)";
            else
                Text = "Connection to SFU - Connecting... (Ctrl+C to Stop)";

            _connector = LocalShellUtil.AsyncPrepareSocket(this, _param);
            if (_connector == null) ClearConnectingState();
        }

        private void OnAdvancedOptionCheckedChanged(object sender, EventArgs args)
        {
            _advancedOptionGroup.Enabled = _advancedOptionCheck.Checked;
        }
        private void OnLogTypeChanged(object sender, EventArgs args)
        {
            AdjustUI();
        }
        private void AdjustUI()
        {
            bool e = _logTypeBox.SelectedIndex != (int)LogType.None;
            _logFileBox.Enabled = e;
            _selectlogButton.Enabled = e;
        }
        private void SelectLog(object sender, EventArgs e)
        {
            string fn = GCUtil.SelectLogFileByDialog(this);
            if (fn != null) _logFileBox.Text = fn;
        }

        private void ShowError(string msg)
        {
            GUtil.Warning(this, msg, "Connection Error");
        }

        protected override bool ProcessDialogKey(Keys key)
        {
            if (_connector != null && key == (Keys.Control | Keys.C))
            {
                _connector.Interrupt();
                ClearConnectingState();
                return true;
            }
            else
                return base.ProcessDialogKey(key);
        }
        private void ClearConnectingState()
        {
            _okButton.Enabled = true;
            _cancelButton.Enabled = true;
            Cursor = Cursors.Default;
            Text = "";
            _connector = null;
        }
        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            if (msg.Msg == GConst.WMG_ASYNCCONNECT)
            {
                if (msg.LParam.ToInt32() == 1)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    ClearConnectingState();
                    ShowError(_errorMessage);
                }
            }
        }
        //ISocketWithTimeoutClient これらはこのウィンドウとは別のスレッドで実行されるので慎重に
        public void SuccessfullyExit(object result)
        {
            _result = (ConnectionTag)result;
            //_result.SetServerInfo(((TCPTerminalParam)_result.Param).Host, swt.IPAddress);
            Win32.SendMessage(_savedHWND, GConst.WMG_ASYNCCONNECT, IntPtr.Zero, new IntPtr(1));
        }
        public void ConnectionFailed(string message)
        {
            _errorMessage = message;
            Win32.SendMessage(_savedHWND, GConst.WMG_ASYNCCONNECT, IntPtr.Zero, IntPtr.Zero);
        }
        public void CancelTimer()
        {
        }
        public IWin32Window GetWindow()
        {
            return this;
        }
    }
}
