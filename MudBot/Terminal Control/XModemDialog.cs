/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: XModemDialog.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Connection;
using Poderosa.Terminal;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// XModemDialog の概要の説明です。
    /// </summary>
    internal class XModemDialog : Form
    {
        private ConnectionTag _connectionTag;
        private XModem _xmodemTask;

        private Button _okButton;
        private Button _cancelButton;
        private Label _fileNameLabel;
        private TextBox _fileNameBox;
        private Button _selectButton;
        private Label _progressText;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public XModemDialog()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            ReloadLanguage();
        }
        public bool Receiving { get; set; }

        public bool Executing { get; private set; }

        public ConnectionTag ConnectionTag
        {
            get => _connectionTag;
            set
            {
                if (Executing)
                {
                    throw new Exception("illegal!");
                }

                _connectionTag = value;
                FormatText();
            }
        }
        public void ReloadLanguage()
        {
            _okButton.Text = "Form.XModemDialog._okButton";
            _cancelButton.Text = "Cancel";
            _fileNameLabel.Text = "Form.XModemDialog._fileNameLabel";
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
            _fileNameLabel = new Label();
            _fileNameBox = new TextBox();
            _selectButton = new Button();
            _progressText = new Label();
            SuspendLayout();
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.Location = new Point(152, 64);
            _okButton.Name = "_okButton";
            _okButton.TabIndex = 0;
            _okButton.Click += OnOK;
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.Location = new Point(240, 64);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.TabIndex = 1;
            _cancelButton.Click += OnCancel;
            // 
            // _fileNameLabel
            // 
            _fileNameLabel.ImageAlign = ContentAlignment.MiddleLeft;
            _fileNameLabel.Location = new Point(8, 8);
            _fileNameLabel.Name = "_fileNameLabel";
            _fileNameLabel.Size = new Size(80, 16);
            _fileNameLabel.TabIndex = 2;
            // 
            // _fileNameBox
            // 
            _fileNameBox.Location = new Point(96, 8);
            _fileNameBox.Name = "_fileNameBox";
            _fileNameBox.Size = new Size(192, 19);
            _fileNameBox.TabIndex = 3;
            _fileNameBox.Text = "";
            // 
            // _selectButton
            // 
            _selectButton.Location = new Point(296, 8);
            _selectButton.FlatStyle = FlatStyle.System;
            _selectButton.Name = "_selectButton";
            _selectButton.Size = new Size(19, 19);
            _selectButton.TabIndex = 4;
            _selectButton.Text = "...";
            _selectButton.Click += OnSelectFile;
            // 
            // _progressText
            // 
            _progressText.Location = new Point(8, 28);
            _progressText.Name = "_progressText";
            _progressText.Size = new Size(296, 32);
            _progressText.TabIndex = 5;
            _progressText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // XModemDialog
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(328, 86);
            Controls.Add(_progressText);
            Controls.Add(_selectButton);
            Controls.Add(_fileNameBox);
            Controls.Add(_fileNameLabel);
            Controls.Add(_cancelButton);
            Controls.Add(_okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "XModemDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            ResumeLayout(false);

        }
        #endregion

        private void FormatText()
        {
            Text = String.Format("Caption.XModemDialog.DialogTitle", Receiving ? "Common.Reception" : "Common.Transmission", _connectionTag.FormatTabText());
            _progressText.Text = String.Format("Caption.XModemDialog.InitialPrompt", Receiving ? "Common.Transmission" : "Common.Reception".ToLower());
        }
        private void OnSelectFile(object sender, EventArgs args)
        {
            FileDialog dlg = null;
            if (Receiving)
            {
                SaveFileDialog sf = new SaveFileDialog
                {
                    Title = "Caption.XModemDialog.ReceptionFileSelect"
                };
                dlg = sf;
            }
            else
            {
                OpenFileDialog of = new OpenFileDialog
                {
                    Title = "Caption.XModemDialog.TransmissionFileSelect",
                    CheckFileExists = true,
                    Multiselect = false
                };
                dlg = of;
            }
            dlg.Filter = "All Files(*)|*";
            if (GCUtil.ShowModalDialog(this, dlg) == DialogResult.OK)
            {
                _fileNameBox.Text = dlg.FileName;
            }
        }

        private void OnOK(object sedner, EventArgs args)
        {
            Debug.Assert(!Executing);
            DialogResult = DialogResult.None;
            if (Receiving)
            {
                if (!StartReceive())
                {
                    return;
                }
            }
            else
            {
                if (!StartSend())
                {
                    return;
                }
            }

            Executing = true;
            _okButton.Enabled = false;
            _fileNameBox.Enabled = false;
            _selectButton.Enabled = false;
            _progressText.Text = "Caption.XModemDialog.Negotiating";
        }
        private bool StartReceive()
        {
            try
            {
                _xmodemTask = new XModemReceiver(_connectionTag, _fileNameBox.Text)
                {
                    NotifyTarget = Handle
                };
                _xmodemTask.Start();
                return true;
            }
            catch (Exception ex)
            {
                GUtil.Warning(this, ex.Message);
                return false;
            }
        }
        private bool StartSend()
        {
            try
            {
                _xmodemTask = new XModemSender(_connectionTag, _fileNameBox.Text)
                {
                    NotifyTarget = Handle
                };
                _xmodemTask.Start();
                return true;
            }
            catch (Exception ex)
            {
                GUtil.Warning(this, ex.Message);
                return false;
            }
        }

        private void Exit()
        {
            if (_xmodemTask != null)
            {
                _xmodemTask.Abort();
                _xmodemTask = null;
            }
            Executing = false;
            _okButton.Enabled = true;
            _fileNameBox.Enabled = true;
            _selectButton.Enabled = true;
        }

        private void OnCancel(object sender, EventArgs args)
        {
            if (Executing)
            {
                Exit();
            }
            else
            {
                Close();
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (Executing)
            {
                Exit();
            }

            GApp.Frame.XModemDialog = null;
        }

        private void UpdateStatusText(int wparam, int lparam)
        {
            if (wparam == XModem.NOTIFY_PROGRESS)
            {
                if (Receiving)
                {
                    _progressText.Text = String.Format("Caption.XModemDialog.ReceptionProgress", lparam);
                }
                else
                {
                    _progressText.Text = String.Format("Caption.XModemDialog.TransmissionProgress", lparam);
                }
            }
            else
            {
                //PROGRESS以外は単に閉じる。ダイアログボックスの表示などはプロトコル実装側がやる
                DialogResult = DialogResult.Abort;
                _progressText.Text = String.Format("Caption.XModemDialog.InitialPrompt", Receiving ? "Common.Transmission" : "Common.Reception".ToLower());
                Exit();
                if (wparam == XModem.NOTIFY_SUCCESS)
                {
                    Close();
                }
            }

        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == GConst.WMG_XMODEM_UPDATE_STATUS)
            {
                UpdateStatusText(m.WParam.ToInt32(), m.LParam.ToInt32());
            }
        }

    }
}
