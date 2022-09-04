/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: ModuleProperty.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.MacroEnv;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// ModuleProperty の概要の説明です。
    /// </summary>
    internal class ModuleProperty : Form
    {
        private Label _titleLabel;
        private TextBox _title;
        private Label _pathLabel;
        private TextBox _path;
        private Button _selectFileButton;
        private Label _additionalAssemblyLabel;
        private TextBox _additionalAssembly;
        private Label _shortcutLabel;
        private HotKey _shortcut;
        private CheckBox _debugOption;

        private Button _okButton;
        private Button _cancelButton;

        //編集対象のMacroModule 新規作成時はnull
        private MacroList _parent;
        private MacroModule _module;
        private Keys _prevShortCut;

        public MacroModule Module
        {
            get
            {
                return _module;
            }
        }
        public Keys ShortCut
        {
            get
            {
                return _shortcut.Key;
            }
        }

        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public ModuleProperty(MacroList p, MacroModule mod, Keys shortcut)
        {
            _parent = p;
            _prevShortCut = shortcut;
            _module = mod == null ? new MacroModule(0) : (MacroModule)mod.Clone();
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            _titleLabel.Text = "Form.ModuleProperty._titleLabel";
            _pathLabel.Text = "Form.ModuleProperty._pathLabel";
            _additionalAssemblyLabel.Text = "Form.ModuleProperty._additionalAssemblyLabel";
            _shortcutLabel.Text = "Form.ModuleProperty._shortcutLabel";
            _debugOption.Text = "Form.ModuleProperty._debugOption";
            _okButton.Text = "OK";
            _cancelButton.Text = "Cancel";
            Text = "Form.ModuleProperty.Text";

            if (mod != null)
            {
                _title.Text = _module.Title;
                _path.Text = _module.Path;
                _additionalAssembly.Text = Concat(_module.AdditionalAssemblies);
                _debugOption.Checked = _module.DebugMode;
                _shortcut.Key = shortcut;
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

        #region Windows Form Designer generated code
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            _titleLabel = new Label();
            _title = new TextBox();
            _pathLabel = new Label();
            _path = new TextBox();
            _selectFileButton = new Button();
            _additionalAssemblyLabel = new Label();
            _additionalAssembly = new TextBox();
            _shortcutLabel = new Label();
            _shortcut = new HotKey();
            _debugOption = new CheckBox();
            _okButton = new Button();
            _cancelButton = new Button();
            SuspendLayout();
            // 
            // _titleLabel
            // 
            _titleLabel.Location = new Point(8, 8);
            _titleLabel.Name = "_titleLabel";
            _titleLabel.TabIndex = 0;
            _titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _title
            // 
            _title.Location = new Point(120, 8);
            _title.Name = "_title";
            _title.Size = new Size(200, 19);
            _title.TabIndex = 1;
            _title.Text = "";
            // 
            // _pathLabel
            // 
            _pathLabel.Location = new Point(8, 32);
            _pathLabel.Name = "_pathLabel";
            _pathLabel.TabIndex = 2;
            _pathLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _path
            // 
            _path.Location = new Point(120, 32);
            _path.Name = "_path";
            _path.Size = new Size(181, 19);
            _path.TabIndex = 3;
            _path.Text = "";
            // 
            // _selectFileButton
            // 
            _selectFileButton.Location = new Point(301, 32);
            _selectFileButton.Name = "_selectFileButton";
            _selectFileButton.FlatStyle = FlatStyle.System;
            _selectFileButton.Size = new Size(19, 19);
            _selectFileButton.TabIndex = 4;
            _selectFileButton.Text = "...";
            _selectFileButton.Click += new EventHandler(OnSelectFile);
            // 
            // _additionalAssemblyLabel
            // 
            _additionalAssemblyLabel.Location = new Point(8, 56);
            _additionalAssemblyLabel.Name = "_additionalAssemblyLabel";
            _additionalAssemblyLabel.Size = new Size(100, 32);
            _additionalAssemblyLabel.TabIndex = 5;
            _additionalAssemblyLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _additionalAssembly
            // 
            _additionalAssembly.Location = new Point(120, 64);
            _additionalAssembly.Name = "_additionalAssembly";
            _additionalAssembly.Size = new Size(200, 19);
            _additionalAssembly.TabIndex = 6;
            _additionalAssembly.Text = "";
            // 
            // _shortcutLabel
            // 
            _shortcutLabel.Location = new Point(8, 88);
            _shortcutLabel.Name = "_shortcutLabel";
            _shortcutLabel.TabIndex = 7;
            _shortcutLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _shortcut
            // 
            _shortcut.DebugTextBox = null;
            _shortcut.ImeMode = ImeMode.Disable;
            _shortcut.Key = Keys.None;
            _shortcut.Location = new Point(120, 88);
            _shortcut.Name = "_shortcut";
            _shortcut.Size = new Size(80, 19);
            _shortcut.TabIndex = 8;
            _shortcut.Text = "";
            // 
            // _debugOption
            // 
            _debugOption.Location = new Point(8, 112);
            _debugOption.Name = "_debugOption";
            _debugOption.FlatStyle = FlatStyle.System;
            _debugOption.Size = new Size(296, 24);
            _debugOption.TabIndex = 9;
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(168, 136);
            _okButton.Name = "_okButton";
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.TabIndex = 10;
            _okButton.Click += new EventHandler(OnOK);
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(248, 136);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.TabIndex = 11;
            // 
            // ModuleProperty
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(330, 167);
            Controls.AddRange(new Control[] {
                                                                          _titleLabel,
                                                                          _title,
                                                                          _pathLabel,
                                                                          _path,
                                                                          _selectFileButton,
                                                                          _additionalAssemblyLabel,
                                                                          _additionalAssembly,
                                                                          _shortcutLabel,
                                                                          _shortcut,
                                                                          _debugOption,
                                                                          _cancelButton,
                                                                          _okButton});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ModuleProperty";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion

        private void OnSelectFile(object sender, EventArgs args)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                InitialDirectory = GApp.Options.DefaultFileDir,
                Title = "Caption.ModuleProperty.SelectMacroFile",
                Filter = "JScript.NET Files(*.js)|*.js|.NET Executables(*.exe;*.dll)|*.exe;*.dll"
            };
            if (GCUtil.ShowModalDialog(this, dlg) == DialogResult.OK)
            {
                GApp.Options.DefaultFileDir = GUtil.FileToDir(dlg.FileName);
                _path.Text = dlg.FileName;
                if (_title.Text.Length == 0)
                {
                    _title.Text = Path.GetFileName(dlg.FileName); //ファイル名本体をデフォルトのタイトルにする
                }
            }
        }
        private void OnOK(object sender, EventArgs args)
        {
            DialogResult = DialogResult.None;
            if (!File.Exists(_path.Text))
            {
                GUtil.Warning(this, String.Format("Message.ModuleProperty.FileNotExist", _path.Text));
            }
            else if (_title.Text.Length > 30)
            {
                GUtil.Warning(this, "Message.ModuleProperty.TooLongTitle");
            }
            else
            {
                if (_shortcut.Key != _prevShortCut)
                {
                    string n = _parent.FindCommandDescription(_shortcut.Key);
                    if (n != null)
                    {
                        GUtil.Warning(this, String.Format("Message.ModuleProperty.DuplicatedKey", n));
                        return;
                    }
                }

                _module.Title = _title.Text;
                _module.Path = _path.Text;
                _module.DebugMode = _debugOption.Checked;
                _module.AdditionalAssemblies = ParseAdditionalAssemblies(_additionalAssembly.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private string Concat(string[] v)
        {
            if (v == null)
            {
                return "";
            }

            StringBuilder b = new StringBuilder();
            foreach (string t in v)
            {
                if (b.Length > 0)
                {
                    b.Append(';');
                }

                b.Append(t);
            }
            return b.ToString();
        }

        private string[] ParseAdditionalAssemblies(string t)
        {
            string[] l = t.Split(new char[] { ';', ',' });
            return l;
        }
    }
}
