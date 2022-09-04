/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: ChangeLogDialog.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using Poderosa.Communication;
using Poderosa.ConnectionParam;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;

namespace Poderosa.Forms
{
    /// <summary>
    /// ChangeLogDialog の概要の説明です。
    /// </summary>
    internal sealed class ChangeLogDialog : Form
    {
        private readonly TerminalConnection _connection;

        private ComboBox _logTypeBox;
        private Label _logTypeLabel;
        private ComboBox _fileNameBox;
        private Label _fileNameLabel;
        private Button _selectlogButton;
        private Button _cancelButton;
        private Button _okButton;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private readonly Container _components = null;

        public ChangeLogDialog(TerminalConnection current)
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();
            _logTypeLabel.Text = "Log &Type";
            _fileNameLabel.Text = "&File Name";
            _cancelButton.Text = "Cancel";
            _okButton.Text = "OK";
            Text = "Log Configuration";

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            _connection = current;
            _logTypeBox.SelectedIndex = _logTypeBox.FindStringExact(EnumDescAttributeT.For(typeof(LogType)).GetDescription(_connection.LogType));

            if (_connection.LogType != LogType.None)
            {
                _fileNameBox.Items.Add(_connection.LogPath);
                _fileNameBox.SelectedIndex = 0;
            }

            foreach (string p in GApp.ConnectionHistory.LogPaths)
            {
                _fileNameBox.Items.Add(p);
            }

            AdjustUI();
        }

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_components != null)
                {
                    _components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// 消えちゃ困るコードの避難場所
        /// this._logTypeBox.Items.AddRange(EnumDescAttributeT.For(typeof(LogType)).DescriptionCollection());
        /// </summary>
        private void InitializeComponent()
        {
            _logTypeBox = new ComboBox();
            _logTypeLabel = new Label();
            _fileNameBox = new ComboBox();
            _fileNameLabel = new Label();
            _selectlogButton = new Button();
            _cancelButton = new Button();
            _okButton = new Button();
            SuspendLayout();
            // 
            // _logTypeBox
            // 
            _logTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _logTypeBox.Location = new Point(104, 8);
            _logTypeBox.Name = "_logTypeBox";
            _logTypeBox.Size = new Size(96, 20);
            _logTypeBox.TabIndex = 1;
            _logTypeBox.SelectedIndexChanged += new EventHandler(OnLogTypeChanged);
            _logTypeBox.Items.AddRange(EnumDescAttributeT.For(typeof(LogType)).DescriptionCollection());
            // 
            // _logTypeLabel
            // 
            _logTypeLabel.ImeMode = ImeMode.NoControl;
            _logTypeLabel.Location = new Point(5, 8);
            _logTypeLabel.Name = "_logTypeLabel";
            _logTypeLabel.RightToLeft = RightToLeft.No;
            _logTypeLabel.Size = new Size(80, 16);
            _logTypeLabel.TabIndex = 0;
            _logTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _fileNameBox
            // 
            _fileNameBox.Location = new Point(104, 32);
            _fileNameBox.Name = "_fileNameBox";
            _fileNameBox.Size = new Size(160, 20);
            _fileNameBox.TabIndex = 3;
            // 
            // _fileNameLabel
            // 
            _fileNameLabel.ImeMode = ImeMode.NoControl;
            _fileNameLabel.Location = new Point(5, 32);
            _fileNameLabel.Name = "_fileNameLabel";
            _fileNameLabel.RightToLeft = RightToLeft.No;
            _fileNameLabel.Size = new Size(88, 16);
            _fileNameLabel.TabIndex = 2;
            _fileNameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _selectlogButton
            // 
            _selectlogButton.FlatStyle = FlatStyle.System;
            _selectlogButton.ImageIndex = 0;
            _selectlogButton.ImeMode = ImeMode.NoControl;
            _selectlogButton.Location = new Point(269, 32);
            _selectlogButton.Name = "_selectlogButton";
            _selectlogButton.RightToLeft = RightToLeft.No;
            _selectlogButton.Size = new Size(19, 19);
            _selectlogButton.TabIndex = 4;
            _selectlogButton.Text = "...";
            _selectlogButton.Click += new EventHandler(OnSelectLogFile);
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.ImageIndex = 0;
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.ImeMode = ImeMode.NoControl;
            _cancelButton.Location = new Point(216, 56);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.RightToLeft = RightToLeft.No;
            _cancelButton.Size = new Size(72, 25);
            _cancelButton.TabIndex = 6;
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.ImageIndex = 0;
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.ImeMode = ImeMode.NoControl;
            _okButton.Location = new Point(128, 56);
            _okButton.Name = "_okButton";
            _okButton.RightToLeft = RightToLeft.No;
            _okButton.Size = new Size(72, 25);
            _okButton.TabIndex = 5;
            _okButton.Click += new EventHandler(OnOK);
            // 
            // ChangeLogDialog
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(292, 85);
            Controls.AddRange(new Control[] {
                                                                          _cancelButton,
                                                                          _okButton,
                                                                          _logTypeBox,
                                                                          _logTypeLabel,
                                                                          _fileNameBox,
                                                                          _fileNameLabel,
                                                                          _selectlogButton});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangeLogDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        private void AdjustUI()
        {
            bool e = ((LogType)EnumDescAttributeT.For(typeof(LogType)).FromDescription(_logTypeBox.Text, LogType.None) != LogType.None);
            _fileNameBox.Enabled = e;
            _selectlogButton.Enabled = e;
        }
        private void OnLogTypeChanged(object sender, EventArgs args)
        {
            AdjustUI();
        }
        private void OnSelectLogFile(object sender, EventArgs args)
        {
            string fn = GCUtil.SelectLogFileByDialog(this);
            if (fn != null)
            {
                _fileNameBox.Text = fn;
            }
        }
        private void OnOK(object sender, EventArgs args)
        {
            DialogResult = DialogResult.None;
            LogType t = (LogType)EnumDescAttributeT.For(typeof(LogType)).FromDescription(_logTypeBox.Text, LogType.None);
            string path = null;

            bool append = false;
            if (t != LogType.None)
            {
                path = _fileNameBox.Text;
                LogFileCheckResult r = GCUtil.CheckLogFileName(path, this);
                if (r == LogFileCheckResult.Cancel || r == LogFileCheckResult.Error)
                {
                    return;
                }

                append = (r == LogFileCheckResult.Append);
            }

            _connection.ResetLog(t, path, append);
            DialogResult = DialogResult.OK;
        }
    }
}
