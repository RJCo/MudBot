/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: ThreeButtonMessageBox.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// ThreeButtonMessageBox の概要の説明です。
    /// </summary>
    internal class ThreeButtonMessageBox : Form
    {
        private Button _button1;
        private Button _button2;
        private Button _button3;
        private Label _message;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public ThreeButtonMessageBox()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
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
            _button1 = new Button();
            _button2 = new Button();
            _button3 = new Button();
            _message = new Label();
            SuspendLayout();
            // 
            // _button1
            // 
            _button1.DialogResult = DialogResult.Yes;
            _button1.Location = new Point(8, 56);
            _button1.Name = "_button1";
            _button1.FlatStyle = FlatStyle.System;
            _button1.Size = new Size(96, 23);
            _button1.TabIndex = 0;
            // 
            // _button2
            // 
            _button2.DialogResult = DialogResult.No;
            _button2.Location = new Point(112, 56);
            _button2.Name = "_button2";
            _button2.FlatStyle = FlatStyle.System;
            _button2.Size = new Size(96, 23);
            _button2.TabIndex = 1;
            // 
            // _button3
            // 
            _button3.DialogResult = DialogResult.Cancel;
            _button3.Location = new Point(216, 56);
            _button3.Name = "_button3";
            _button3.FlatStyle = FlatStyle.System;
            _button3.Size = new Size(96, 23);
            _button3.TabIndex = 2;
            // 
            // _message
            // 
            _message.Location = new Point(64, 8);
            _message.Name = "_message";
            _message.Size = new Size(232, 48);
            _message.TabIndex = 3;
            _message.TextAlign = ContentAlignment.TopLeft;
            // 
            // ThreeButtonMessageBox
            // 
            AcceptButton = _button1;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _button3;
            ClientSize = new Size(322, 85);
            Controls.AddRange(new Control[] {
                                                                          _message,
                                                                          _button3,
                                                                          _button2,
                                                                          _button1});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ThreeButtonMessageBox";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        public string YesButtonText
        {
            get
            {
                return _button1.Text;
            }
            set
            {
                _button1.Text = value;
            }
        }
        public string NoButtonText
        {
            get
            {
                return _button2.Text;
            }
            set
            {
                _button2.Text = value;
            }
        }
        public string CancelButtonText
        {
            get
            {
                return _button3.Text;
            }
            set
            {
                _button3.Text = value;
            }
        }
        public string Message
        {
            get
            {
                return _message.Text;
            }
            set
            {
                _message.Text = value;
            }
        }
        protected override void OnPaint(PaintEventArgs a)
        {
            base.OnPaint(a);
            //アイコンの描画　.NET Frameworkだけでシステムで持っているアイコンのロードはできないようだ
            if (_questionIcon == null)
            {
                LoadQuestionIcon();
            }

            a.Graphics.DrawIcon(_questionIcon, 16, 8);
        }
        private static Icon _questionIcon;
        private static void LoadQuestionIcon()
        {
            IntPtr hIcon = Win32.LoadIcon(IntPtr.Zero, new IntPtr(Win32.IDI_QUESTION));
            _questionIcon = Icon.FromHandle(hIcon);
        }
    }
}
