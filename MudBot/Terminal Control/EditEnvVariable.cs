/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: EditEnvVariable.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// EditEnvVariable の概要の説明です。
    /// </summary>
    internal class EditEnvVariable : Form
    {
        private bool _isNew;
        private EnvVariableList _parent;

        private Label _nameLabel;
        private TextBox _nameBox;
        private Label _valueLabel;
        private TextBox _valueBox;
        private Button _okButton;
        private Button _cancelButton;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public EditEnvVariable(EnvVariableList p)
        {
            _parent = p;
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            _nameLabel.Text = "Name";
            _valueLabel.Text = "Value";
            _okButton.Text = "OK";
            _cancelButton.Text = "Cancel";
            Text = "Edit Value";
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
            _nameLabel = new Label();
            _nameBox = new TextBox();
            _valueLabel = new Label();
            _valueBox = new TextBox();
            _okButton = new Button();
            _cancelButton = new Button();
            SuspendLayout();
            // 
            // _nameLabel
            // 
            _nameLabel.ImageAlign = ContentAlignment.MiddleLeft;
            _nameLabel.Location = new Point(8, 8);
            _nameLabel.Name = "_nameLabel";
            _nameLabel.Size = new Size(56, 16);
            _nameLabel.TabIndex = 0;
            // 
            // _nameBox
            // 
            _nameBox.Location = new Point(72, 8);
            _nameBox.Name = "_nameBox";
            _nameBox.Size = new Size(216, 19);
            _nameBox.TabIndex = 1;
            _nameBox.Text = "";
            // 
            // _valueLabel
            // 
            _valueLabel.ImageAlign = ContentAlignment.MiddleLeft;
            _valueLabel.Location = new Point(8, 32);
            _valueLabel.Name = "_valueLabel";
            _valueLabel.Size = new Size(56, 16);
            _valueLabel.TabIndex = 2;
            // 
            // _valueBox
            // 
            _valueBox.Location = new Point(72, 32);
            _valueBox.Name = "_valueBox";
            _valueBox.Size = new Size(216, 19);
            _valueBox.TabIndex = 3;
            _valueBox.Text = "";
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(136, 64);
            _okButton.Name = "_okButton";
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.TabIndex = 4;
            _okButton.Click += new EventHandler(OnOK);
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(216, 64);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.TabIndex = 5;
            // 
            // EditEnvVariable
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(292, 93);
            Controls.AddRange(new Control[] {
                                                                          _cancelButton,
                                                                          _okButton,
                                                                          _valueBox,
                                                                          _valueLabel,
                                                                          _nameBox,
                                                                          _nameLabel});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditEnvVariable";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        public string VarName
        {
            get
            {
                return _nameBox.Text;
            }
            set
            {
                _nameBox.Text = value;
            }
        }
        public string VarValue
        {
            get
            {
                return _valueBox.Text;
            }
            set
            {
                _valueBox.Text = value;
            }
        }
        public bool IsNewVariable
        {
            get
            {
                return _isNew;
            }
            set
            {
                _isNew = value;
                _nameBox.Enabled = _isNew;
            }
        }

        private void OnOK(object sender, EventArgs args)
        {
            DialogResult = DialogResult.None;
            string n = _nameBox.Text;
            if (n.Length == 0)
                GUtil.Warning(this, "The variable name must not be empty.");
            else if (n.IndexOf('=') != -1 || n.IndexOf(' ') != -1)
                GUtil.Warning(this, "The variable name must not contain '=' or space.");
            else if (_isNew && _parent.HasVariable(n))
                GUtil.Warning(this, "An identical name already exists.");
            else //success
                DialogResult = DialogResult.OK;
        }
    }
}
