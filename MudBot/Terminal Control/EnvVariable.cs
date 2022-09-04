/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: EnvVariable.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// EnvVariable の概要の説明です。
    /// </summary>
    internal sealed class EnvVariableList : Form
    {
        private ListView _list;
        private Button _addButton;
        private Button _editButton;
        private Button _deleteButton;
        private Button _okButton;
        private ColumnHeader _nameHeader;
        private ColumnHeader _valueHeader;
        private Button _cancelButton;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public EnvVariableList()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            Text = "Poderosa Environment Variables";
            _nameHeader.Text = "Name";
            _valueHeader.Text = "Value";
            _addButton.Text = "Add...";
            _editButton.Text = "Edit...";
            _deleteButton.Text = "Delete...";
            _cancelButton.Text = "Cancel";
            _okButton.Text = "OK";

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            Init();
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
            _list = new ListView();
            _nameHeader = new ColumnHeader();
            _valueHeader = new ColumnHeader();
            _addButton = new Button();
            _editButton = new Button();
            _deleteButton = new Button();
            _okButton = new Button();
            _cancelButton = new Button();
            SuspendLayout();
            // 
            // _list
            // 
            _list.Columns.AddRange(new ColumnHeader[] {
                                                                                    _nameHeader,
                                                                                    _valueHeader});
            _list.FullRowSelect = true;
            _list.GridLines = true;
            _list.LabelEdit = true;
            _list.Name = "_list";
            _list.Size = new Size(272, 168);
            _list.TabIndex = 0;
            _list.MultiSelect = false;
            _list.Sorting = SortOrder.Ascending;
            _list.View = View.Details;
            _list.SelectedIndexChanged += new EventHandler(OnSelectedItemChanged);
            // 
            // _nameHeader
            // 
            _nameHeader.Width = 80;
            // 
            // _valueHeader
            // 
            _valueHeader.Width = 180;
            // 
            // _addButton
            // 
            _addButton.Location = new Point(280, 8);
            _addButton.Name = "_addButton";
            _addButton.FlatStyle = FlatStyle.System;
            _addButton.TabIndex = 1;
            _addButton.Click += new EventHandler(OnAddButtonClicked);
            // 
            // _editButton
            // 
            _editButton.Location = new Point(280, 40);
            _editButton.Name = "_editButton";
            _editButton.FlatStyle = FlatStyle.System;
            _editButton.TabIndex = 2;
            _editButton.Click += new EventHandler(OnEditButtonClicked);
            // 
            // _deleteButton
            // 
            _deleteButton.Location = new Point(280, 72);
            _deleteButton.Name = "_deleteButton";
            _deleteButton.FlatStyle = FlatStyle.System;
            _deleteButton.TabIndex = 3;
            _deleteButton.Click += new EventHandler(OnDeleteButtonClicked);
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(280, 112);
            _okButton.Name = "_okButton";
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.TabIndex = 4;
            _okButton.Click += new EventHandler(OnOK);
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(280, 144);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.TabIndex = 5;
            // 
            // EnvVariable
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(360, 175);
            Controls.AddRange(new Control[] {
                                                                          _editButton,
                                                                          _cancelButton,
                                                                          _okButton,
                                                                          _deleteButton,
                                                                          _addButton,
                                                                          _list});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EnvVariableList";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        private void Init()
        {
            IDictionaryEnumerator de = GApp.MacroManager.EnvironmentVariables;
            while (de.MoveNext())
            {
                if (!(de.Value is string))
                {
                    continue;
                }

                ListViewItem li = new ListViewItem((string)de.Key);
                li = _list.Items.Add(li);
                li.SubItems.Add((string)de.Value);
            }
            _editButton.Enabled = false;
            _deleteButton.Enabled = false;
        }
        private void OnSelectedItemChanged(object sender, EventArgs args)
        {
            _editButton.Enabled = true;
            _deleteButton.Enabled = true;
        }

        private void OnAddButtonClicked(object sender, EventArgs args)
        {
            EditEnvVariable d = new EditEnvVariable(this)
            {
                IsNewVariable = true
            };
            if (GCUtil.ShowModalDialog(this, d) == DialogResult.OK)
            {
                ListViewItem li = new ListViewItem(d.VarName);
                li = _list.Items.Add(li);
                li.SubItems.Add(d.VarValue);
                li.Selected = true;
            }
        }
        private void OnEditButtonClicked(object sender, EventArgs args)
        {
            ListViewItem li = _list.SelectedItems[0];
            EditEnvVariable d = new EditEnvVariable(this)
            {
                VarName = li.Text,
                VarValue = li.SubItems[1].Text,
                IsNewVariable = false
            };
            if (GCUtil.ShowModalDialog(this, d) == DialogResult.OK)
            {
                li.Text = d.VarName;
                li.SubItems[1].Text = d.VarValue;
            }
        }
        private void OnDeleteButtonClicked(object sender, EventArgs args)
        {
            if (_list.SelectedIndices.Count > 0)
            {
                _list.Items.RemoveAt(_list.SelectedIndices[0]);
            }
        }

        private void OnOK(object sender, EventArgs args)
        {
            Hashtable t = new Hashtable();
            foreach (ListViewItem li in _list.Items)
            {
                t[li.Text] = li.SubItems[1].Text;
            }

            GApp.MacroManager.ResetEnvironmentVariables(t);
        }

        public bool HasVariable(string name)
        {
            foreach (ListViewItem li in _list.Items)
            {
                if (name == li.Text)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
