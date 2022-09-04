/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: LogNoteForm.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    internal class LogNoteForm : Form
    {
        private TextBox _textBox;
        private Button _okButton;
        private Button _cancelButton;
        private Button _insertButton;
        private Container components = null;

        public LogNoteForm()
        {
            InitializeComponent();

            //
            // TODO: InitializeComponent
            //
            _okButton.Text = "OK";
            _cancelButton.Text = "Cancel";
            _insertButton.Text = "Form.LogNoteForm._insertButton";
            Text = "Form.LogNoteForm.Text";
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
        private void InitializeComponent()
        {
            _textBox = new TextBox();
            _okButton = new Button();
            _cancelButton = new Button();
            _insertButton = new Button();
            SuspendLayout();

            // 
            // _textBox
            // 
            _textBox.Location = new Point(8, 8);
            _textBox.Name = "_textBox";
            _textBox.Size = new Size(336, 19);
            _textBox.TabIndex = 0;
            _textBox.Text = "";

            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.Location = new Point(208, 32);
            _okButton.Name = "_okButton";
            _okButton.Size = new Size(64, 24);
            _okButton.TabIndex = 1;

            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.Location = new Point(280, 32);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.Size = new Size(64, 23);
            _cancelButton.TabIndex = 2;

            // 
            // _insertButton
            // 
            _insertButton.FlatStyle = FlatStyle.System;
            _insertButton.Location = new Point(8, 32);
            _insertButton.Name = "_insertButton";
            _insertButton.Size = new Size(72, 23);
            _insertButton.TabIndex = 3;
            _insertButton.Click += new EventHandler(OnClickInsertButton);

            // 
            // LogNoteForm
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(346, 61);
            Controls.Add(_insertButton);
            Controls.Add(_cancelButton);
            Controls.Add(_okButton);
            Controls.Add(_textBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LogNoteForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        protected override bool ProcessDialogKey(Keys k)
        {
            if (k == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return true;
            }
            else
            {
                return base.ProcessDialogKey(k);
            }
        }

        public string ResultText
        {
            get
            {
                return _textBox.Text;
            }
        }

        private void OnClickInsertButton(object sender, EventArgs args)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add(CreateMenuItem(0, "&Time", new EventHandler(OnInsertTime)));
            menu.Items.Add(CreateMenuItem(1, "&Date and Time", new EventHandler(OnInsertDateTime)));
            menu.Show(this, new Point(_insertButton.Left, _insertButton.Bottom));
        }

        private ToolStripMenuItem CreateMenuItem(int index, string text, EventHandler handler)
        {
            GMenuItem mex = new GMenuItem
            {
                Text = text
            };
            //mex.Index = index;
            mex.Click += handler;
            return mex;
        }

        private void OnInsertTime(object sender, EventArgs args)
        {
            InsertText(DateTime.Now.ToShortTimeString());
        }

        private void OnInsertDateTime(object sender, EventArgs args)
        {
            InsertText(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        }

        private void InsertText(string t)
        {
            for (int i = 0; i < t.Length; i++)
            {
                Win32.SendMessage(_textBox.Handle, Win32.WM_CHAR, new IntPtr((int)t[i]), IntPtr.Zero);
            }

            _textBox.Focus();
        }
    }
}
