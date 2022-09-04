/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: WarningWithDisableOption.cs,v 1.2 2005/04/20 08:45:48 okajima Exp $
*/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// WarningWithDisableOption の概要の説明です。
    /// </summary>
    internal sealed class WarningWithDisableOption : Form
    {
        private static Icon _warningIcon;

        private Button _okButton;
        private Label _messageLabel;
        private CheckBox _disableCheckBox;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public WarningWithDisableOption(string message)
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            _messageLabel.Text = message;
            Text = "Form.WarningWithDisableOption.Text";
            _disableCheckBox.Text = "Form.WarningWithDisableOption._disableCheckBox";
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
            _messageLabel = new Label();
            _disableCheckBox = new CheckBox();
            SuspendLayout();
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(120, 72);
            _okButton.Name = "_okButton";
            _okButton.TabIndex = 0;
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.Text = "OK";
            // 
            // _messageLabel
            // 
            _messageLabel.Location = new Point(56, 8);
            _messageLabel.Name = "_messageLabel";
            _messageLabel.Size = new Size(248, 40);
            _messageLabel.TabIndex = 1;
            _messageLabel.Text = "a";
            _messageLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _disableCheckBox
            // 
            _disableCheckBox.Location = new Point(56, 48);
            _disableCheckBox.Name = "_disableCheckBox";
            _disableCheckBox.Size = new Size(248, 24);
            _disableCheckBox.TabIndex = 2;
            _disableCheckBox.FlatStyle = FlatStyle.System;
            // 
            // WarningWithDisableOption
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            ClientSize = new Size(314, 103);
            Controls.AddRange(new Control[] {
                                                                          _disableCheckBox,
                                                                          _messageLabel,
                                                                          _okButton});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WarningWithDisableOption";
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            ResumeLayout(false);

        }
        #endregion

        protected override void OnPaint(PaintEventArgs a)
        {
            base.OnPaint(a);
            //アイコンの描画　.NET Frameworkだけでシステムで持っているアイコンのロードはできないようだ
            if (_warningIcon == null)
            {
                LoadWarningIcon();
            }

            a.Graphics.DrawIcon(_warningIcon, 12, 24);
        }

        public bool CheckedDisableOption => _disableCheckBox.Checked;

        private static void LoadWarningIcon()
        {
            IntPtr hIcon = Win32.LoadIcon(IntPtr.Zero, new IntPtr(Win32.IDI_EXCLAMATION));
            _warningIcon = Icon.FromHandle(hIcon);
        }

    }
}
