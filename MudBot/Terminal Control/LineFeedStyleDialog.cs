/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: LineFeedStyleDialog.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Communication;
using Poderosa.ConnectionParam;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;

namespace Poderosa.Forms
{
    /// <summary>
    /// LineFeedStyleDialog の概要の説明です。
    /// </summary>
    internal class LineFeedStyleDialog : Form
    {
        private Label _lineFeedLabel;
        private ComboBox _lineFeedBox;
        private Button _okButton;
        private Button _cancelButton;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public LineFeedStyleDialog(TerminalConnection con)
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            _cancelButton.Text = "Cancel";
            _okButton.Text = "OK";
            _lineFeedLabel.Text = "Form.LineFeedStyleDialog._lineFeedLabel";
            Text = "Form.LineFeedStyleDialog.Text";
            _lineFeedBox.Items.AddRange(EnumDescAttributeT.For(typeof(LineFeedRule)).DescriptionCollection());
            _lineFeedBox.SelectedIndex = (int)con.Param.LineFeedRule;
        }

        public LineFeedRule LineFeedRule
        {
            get
            {
                return (LineFeedRule)_lineFeedBox.SelectedIndex;
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
            _lineFeedLabel = new Label();
            _lineFeedBox = new ComboBox();
            _okButton = new Button();
            _cancelButton = new Button();
            SuspendLayout();
            // 
            // _lineFeedLabel
            // 
            _lineFeedLabel.Location = new Point(8, 16);
            _lineFeedLabel.Name = "_lineFeedLabel";
            _lineFeedLabel.TabIndex = 0;
            _lineFeedLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _lineFeedBox
            // 
            _lineFeedBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _lineFeedBox.Location = new Point(96, 16);
            _lineFeedBox.Name = "_lineFeedBox";
            _lineFeedBox.Size = new Size(152, 20);
            _lineFeedBox.TabIndex = 1;
            _lineFeedBox.Text = "comboBox1";
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.Location = new Point(80, 48);
            _okButton.Name = "_okButton";
            _okButton.TabIndex = 2;
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.Location = new Point(168, 48);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.TabIndex = 3;
            // 
            // LineFeedStyleDialog
            // 
            AutoScaleBaseSize = new Size(5, 12);
            ClientSize = new Size(256, 78);
            Controls.Add(_cancelButton);
            Controls.Add(_okButton);
            Controls.Add(_lineFeedBox);
            Controls.Add(_lineFeedLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LineFeedStyleDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = _okButton;
            CancelButton = _cancelButton;
            ResumeLayout(false);

        }
        #endregion
    }
}
