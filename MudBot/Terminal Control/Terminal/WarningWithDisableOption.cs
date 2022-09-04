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
    /// WarningWithDisableOption �̊T�v�̐����ł��B
    /// </summary>
    internal sealed class WarningWithDisableOption : Form
    {
        private static Icon _warningIcon;

        private Button _okButton;
        private Label _messageLabel;
        private CheckBox _disableCheckBox;
        /// <summary>
        /// �K�v�ȃf�U�C�i�ϐ��ł��B
        /// </summary>
        private Container components = null;

        public WarningWithDisableOption(string message)
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
            //
            _messageLabel.Text = message;
            Text = "Form.WarningWithDisableOption.Text";
            _disableCheckBox.Text = "Form.WarningWithDisableOption._disableCheckBox";
        }

        /// <summary>
        /// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
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
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
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
            //�A�C�R���̕`��@.NET Framework�����ŃV�X�e���Ŏ����Ă���A�C�R���̃��[�h�͂ł��Ȃ��悤��
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
