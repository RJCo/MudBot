/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: InputBox.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// InputBox の概要の説明です。
    /// </summary>
    internal class InputBox : Form
    {
        private bool _allowsZeroLenString;

        private TextBox _textBox;
        private Button _okButton;
        private Button _cancelButton;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public InputBox()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
        }
        public bool AllowsZeroLenString
        {
            get
            {
                return _allowsZeroLenString;
            }
            set
            {
                _allowsZeroLenString = value;
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
            _textBox = new TextBox();
            _okButton = new Button();
            _cancelButton = new Button();
            SuspendLayout();
            // 
            // _textBox
            // 
            _textBox.Location = new Point(8, 8);
            _textBox.MaxLength = 30;
            _textBox.Name = "_textBox";
            _textBox.Size = new Size(192, 19);
            _textBox.TabIndex = 0;
            _textBox.Text = "";
            _textBox.GotFocus += new EventHandler(OnTextBoxGotFocus);
            _textBox.TextChanged += new EventHandler(OnTextChanged);
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(48, 32);
            _okButton.Name = "_okButton";
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.Size = new Size(64, 23);
            _okButton.TabIndex = 1;
            _okButton.Text = "OK";
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(128, 32);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.Size = new Size(72, 23);
            _cancelButton.TabIndex = 2;
            _cancelButton.Text = "Cancel";
            // 
            // InputBox
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(208, 61);
            Controls.Add(_cancelButton);
            Controls.Add(_okButton);
            Controls.Add(_textBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputBox";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        public string Content
        {
            get
            {
                return _textBox.Text;
            }
            set
            {
                _textBox.Text = value;
                _okButton.Enabled = _allowsZeroLenString || (value != null && value.Length != 0);
            }
        }

        private void OnTextBoxGotFocus(object sender, EventArgs args)
        {
            _textBox.SelectAll(); //この挙動が望ましくない場合もあるかもしれないが、最初の用途がタブのテキスト変更なので...
        }
        private void OnTextChanged(object sender, EventArgs args)
        {
            _okButton.Enabled = _allowsZeroLenString || (_textBox.Text != null && _textBox.Text.Length != 0);
        }
    }
}
