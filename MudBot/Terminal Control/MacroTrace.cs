/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: MacroTrace.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.MacroEnv
{
    /// <summary>
    /// MacroTrace �̊T�v�̐����ł��B
    /// </summary>
    internal class MacroTraceWindow : Form
    {
        internal static int _instanceCount;
        internal static Size _lastWindowSize = new Size();

        private TextBox _textBox;
        /// <summary>
        /// �K�v�ȃf�U�C�i�ϐ��ł��B
        /// </summary>
        private Container components = null;

        public MacroTraceWindow()
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
            //
            Icon = GApp.Options.GuevaraMode ? GIcons.GetOldGuevaraIcon() : GIcons.GetAppIcon();

            //�ʒu�ƃT�C�Y�̒���
            int n = _instanceCount % 5;
            Location = new Point(GApp.Frame.Left + 30 + 20 * n, GApp.Frame.Top + 30 + 20 * n);
            if (_instanceCount > 0)
            {
                Size = _lastWindowSize;
            }

            _instanceCount++;
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
            _textBox = new TextBox();
            SuspendLayout();
            // 
            // _textBox
            // 
            _textBox.Dock = DockStyle.Fill;
            _textBox.Multiline = true;
            _textBox.Name = "_textBox";
            _textBox.ReadOnly = true;
            _textBox.ScrollBars = ScrollBars.Vertical;
            _textBox.Size = new Size(352, 237);
            _textBox.TabIndex = 0;
            _textBox.Text = "";
            _textBox.BackColor = Color.FromKnownColor(KnownColor.Window);
            // 
            // MacroTrace
            // 
            StartPosition = FormStartPosition.Manual;
            AutoScaleBaseSize = new Size(5, 12);
            ClientSize = new Size(352, 237);
            Controls.AddRange(new Control[] {
                                                                          _textBox});
            Name = "MacroTrace";
            ShowInTaskbar = false;
            ResumeLayout(false);

        }
        #endregion

        public void AdjustTitle(MacroModule mod)
        {
            Text = "Caption.MacroTrace.Title" + mod.Title;
        }

        private string _lineToAdd;
        public void AddLine(string t)
        {
            //����̓}�N���X���b�h����Ă΂��̂�SendMessage���g���K�v������
            if (_textBox.TextLength != 0)
            {
                t = "\r\n" + t;
            }

            _lineToAdd = t;
            Win32.SendMessage(Handle, GConst.WMG_MACRO_TRACE, IntPtr.Zero, IntPtr.Zero);
        }
        protected override void OnClosed(EventArgs args)
        {
            base.OnClosed(args);
            _lastWindowSize = Size;
        }

        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            if (msg.Msg == GConst.WMG_MACRO_TRACE)
            {
                _textBox.AppendText(_lineToAdd);
            }
        }
    }
}
