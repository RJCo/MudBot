/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: SendingLargeText.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Terminal;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// SendingLargeText �̊T�v�̐����ł��B
    /// </summary>
    internal class SendingLargeText : Form
    {
        private ProgressBar _progressBar;
        private Label _lineCountLabel;
        private Button _cancelButton;
        /// <summary>
        /// �K�v�ȃf�U�C�i�ϐ��ł��B
        /// </summary>
        private Container components = null;

        private PasteProcessor _proc;

        public SendingLargeText(PasteProcessor proc)
        {
            _proc = proc;

            Init();
        }

        private void Init()
        {

            InitializeComponent();

            //
            // TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
            //
            Text = "Form.SendingLargeText.Text";
            _cancelButton.Text = "Cancel";
            _progressBar.Maximum = _proc.LineCount;
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

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
        /// </summary>
        private void InitializeComponent()
        {
            _cancelButton = new Button();
            _progressBar = new ProgressBar();
            _lineCountLabel = new Label();
            SuspendLayout();
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.Location = new Point(208, 56);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.TabIndex = 0;
            _cancelButton.Click += new EventHandler(OnCancel);
            // 
            // _progressBar
            // 
            _progressBar.Location = new Point(8, 24);
            _progressBar.Name = "_progressBar";
            _progressBar.Size = new Size(272, 23);
            _progressBar.Step = 1;
            _progressBar.TabIndex = 1;
            // 
            // _lineCountLabel
            // 
            _lineCountLabel.Location = new Point(8, 8);
            _lineCountLabel.Name = "_lineCountLabel";
            _lineCountLabel.Size = new Size(144, 16);
            _lineCountLabel.TabIndex = 2;
            // 
            // SendingLargeText
            // 
            AutoScaleBaseSize = new Size(5, 12);
            ClientSize = new Size(292, 86);
            Controls.Add(_lineCountLabel);
            Controls.Add(_progressBar);
            Controls.Add(_cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            CancelButton = _cancelButton;
            Name = "SendingLargeText";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _proc.LineProcessed += new PasteProcessor.EventHandler(OnLineProcessed);
            _proc.Perform();
        }


        private void OnLineProcessed(int i)
        {
            if (i == -1) // finish
                Win32.SendMessage(Handle, GConst.WMG_SENDLINE_PROGRESS, IntPtr.Zero, new IntPtr(-1));
            else
                Win32.SendMessage(Handle, GConst.WMG_SENDLINE_PROGRESS, IntPtr.Zero, new IntPtr(i));
        }

        private void OnCancel(object sender, EventArgs args)
        {
            _proc.SetAbortFlag();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == GConst.WMG_SENDLINE_PROGRESS)
            {
                if (m.LParam.ToInt32() == -1)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    _progressBar.Value = m.LParam.ToInt32();
                    _lineCountLabel.Text = String.Format("Form.SendingLargeText._progressLabel", _progressBar.Value + 1, _proc.LineCount);
                }
            }
        }

    }
}
