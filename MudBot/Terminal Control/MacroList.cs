/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: MacroList.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Poderosa.MacroEnv;
using Poderosa.Config;
using Poderosa.Toolkit;
using Poderosa.UI;

namespace Poderosa.Forms
{
	/// <summary>
	/// MacroList の概要の説明です。
	/// </summary>
	internal class MacroList : Form, IMacroEventListener
	{
		private Button _runButton;
		private Button _stopButton;
		private Button _propButton;
		private Button _addButton;
		private Button _deleteButton;
		private ListView _list;
		private ColumnHeader _titleHeader;
		private ColumnHeader _pathHeader;
		private ColumnHeader _shortCutHeader;
		private ColumnHeader _infoHeader;
		private Button _environmentButton;
		private Button _okButton;
		private Button _downButton;
		private Button _upButton;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private Container components = null;

		//通常はキーバインドはindexベースで関連づけられているが、マクロの設定時にはこれはちょっと面倒なのでこのフォームの中で別途管理する
		private Hashtable _keyToModule;

		//順序がかわったら初期実行マクロは除去
		private bool _macroOrderUpdated;


		public MacroList()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			_titleHeader.Text = "Form.MacroList._titleHeader";
			_pathHeader.Text = "Form.MacroList._pathHeader";
			_shortCutHeader.Text = "Form.MacroList._shortCutHeader";
			_infoHeader.Text = "Form.MacroList._infoHeader";
			_runButton.Text = "Form.MacroList._runButton";
			_stopButton.Text = "Form.MacroList._stopButton";
			_propButton.Text = "Form.MacroList._propButton";
			_downButton.Text = "Form.MacroList._downButton";
			_upButton.Text = "Form.MacroList._upButton";
			_addButton.Text = "Form.MacroList._addButton";
			_deleteButton.Text = "Form.MacroList._deleteButton";
			_environmentButton.Text = "Form.MacroList._environmentButton";
			_okButton.Text = "OK";
			Text = "Form.MacroList.Text";

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			InitUI();

			_macroOrderUpdated = false;
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		//マクロ編集フォームから呼ばれる。keyに割り当て済みのコマンド名があるならそれを返し、なければnullを返す。
		public string FindCommandDescription(Keys key) {
			MacroModule mod = (MacroModule)_keyToModule[key];
			if(mod!=null)
				return mod.Title;
			else {
				Commands.Entry e = GApp.Options.Commands.FindEntry(key);
				if(e!=null && e.Category!=Commands.Category.Macro)
					return e.Description;
				else
					return null;
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			_list = new ListView();
			_titleHeader = new ColumnHeader();
			_pathHeader = new ColumnHeader();
			_shortCutHeader = new ColumnHeader();
			_infoHeader = new ColumnHeader();
			_runButton = new Button();
			_stopButton = new Button();
			_propButton = new Button();
			_downButton = new Button();
			_upButton = new Button();
			_addButton = new Button();
			_deleteButton = new Button();
			_environmentButton = new Button();
			_okButton = new Button();
			SuspendLayout();
			// 
			// _list
			// 
			_list.Columns.AddRange(new ColumnHeader[] {
																					_titleHeader,
																					_pathHeader,
																					_shortCutHeader,
																					_infoHeader});
			_list.FullRowSelect = true;
			_list.GridLines = true;
			_list.MultiSelect = false;
			_list.Name = "_list";
			_list.Size = new Size(408, 280);
			_list.TabIndex = 0;
			_list.View = View.Details;
			_list.DoubleClick += new EventHandler(OnListDoubleClicked);
			_list.SelectedIndexChanged += new EventHandler(OnSelectedIndexChanged);
			// 
			// _titleHeader
			// 
			_titleHeader.Width = 90;
			// 
			// _pathHeader
			// 
			_pathHeader.Width = 160;
			// 
			// _shortCutHeader
			// 
			_shortCutHeader.Width = 80;
			// 
			// _infoHeader
			// 
			// 
			// _runButton
			// 
			_runButton.Location = new Point(416, 8);
			_runButton.Name = "_runButton";
			_runButton.FlatStyle = FlatStyle.System;
			_runButton.Size = new Size(88, 23);
			_runButton.TabIndex = 1;
			_runButton.Click += new EventHandler(OnRunButtonClicked);
			// 
			// _stopButton
			// 
			_stopButton.Location = new Point(416, 40);
			_stopButton.Name = "_stopButton";
			_stopButton.FlatStyle = FlatStyle.System;
			_stopButton.Size = new Size(88, 23);
			_stopButton.TabIndex = 2;
			_stopButton.Click += new EventHandler(OnStopButtonClicked);
			// 
			// _propButton
			// 
			_propButton.Location = new Point(416, 72);
			_propButton.Name = "_propButton";
			_propButton.FlatStyle = FlatStyle.System;
			_propButton.Size = new Size(88, 23);
			_propButton.TabIndex = 3;
			_propButton.Click += new EventHandler(OnPropButtonClicked);
			// 
			// _downButton
			// 
			_downButton.Location = new Point(416, 104);
			_downButton.Name = "_downButton";
			_downButton.FlatStyle = FlatStyle.System;
			_downButton.Size = new Size(40, 23);
			_downButton.TabIndex = 4;
			_downButton.Click += new EventHandler(OnDownButtonClicked);
			// 
			// _upButton
			// 
			_upButton.Location = new Point(464, 104);
			_upButton.Name = "_upButton";
			_upButton.FlatStyle = FlatStyle.System;
			_upButton.Size = new Size(40, 23);
			_upButton.TabIndex = 5;
			_upButton.Click += new EventHandler(OnUpButtonClicked);
			// 
			// _addButton
			// 
			_addButton.Location = new Point(416, 152);
			_addButton.Name = "_addButton";
			_addButton.FlatStyle = FlatStyle.System;
			_addButton.Size = new Size(88, 23);
			_addButton.TabIndex = 6;
			_addButton.Click += new EventHandler(OnAddButtonClicked);
			// 
			// _deleteButton
			// 
			_deleteButton.Location = new Point(416, 184);
			_deleteButton.Name = "_deleteButton";
			_deleteButton.FlatStyle = FlatStyle.System;
			_deleteButton.Size = new Size(88, 23);
			_deleteButton.TabIndex = 7;
			_deleteButton.Click += new EventHandler(OnDeleteButtonClicked);
			// 
			// _environmentButton
			// 
			_environmentButton.Location = new Point(416, 216);
			_environmentButton.Name = "_environmentButton";
			_environmentButton.FlatStyle = FlatStyle.System;
			_environmentButton.Size = new Size(88, 23);
			_environmentButton.TabIndex = 8;
			_environmentButton.Click += new EventHandler(OnEnvironmentButtonClicked);
			// 
			// _okButton
			// 
			_okButton.DialogResult = DialogResult.Cancel;
			_okButton.Location = new Point(416, 248);
			_okButton.Name = "_okButton";
			_okButton.FlatStyle = FlatStyle.System;
			_okButton.Size = new Size(88, 23);
			_okButton.TabIndex = 9;
			// 
			// MacroList
			// 
			AcceptButton = _okButton;
			AutoScaleBaseSize = new Size(5, 12);
			ClientSize = new Size(506, 279);
			Controls.AddRange(new Control[] {
																		  _upButton,
																		  _downButton,
																		  _environmentButton,
																		  _okButton,
																		  _deleteButton,
																		  _addButton,
																		  _propButton,
																		  _stopButton,
																		  _runButton,
																		  _list});
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "MacroList";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			ResumeLayout(false);

		}
		#endregion

		private void InitUI() {
			_keyToModule = new Hashtable();
			foreach(MacroModule mod in GApp.MacroManager.Modules) {
				AddListItem(mod, mod.ShortCut);
				if(mod.ShortCut!=Keys.None) _keyToModule[mod.ShortCut] = mod;
			}
			AdjustUI();
		}
		private void AddListItem(MacroModule mod, Keys shortcut) {
			ListViewItem li = new ListViewItem(mod.Title);
			li = _list.Items.Add(li);
			li.SubItems.Add(mod.Path);
			li.SubItems.Add(UILibUtil.KeyString(shortcut & Keys.Modifiers, shortcut & Keys.KeyCode, '+'));
			li.SubItems.Add(GetInfoString(mod));
		}

		private void OnSelectedIndexChanged(object sender, EventArgs args) {
			AdjustUI();
		}
		private void OnListDoubleClicked(object sender, EventArgs args) {
			ShowProp(_list.SelectedItems[0].Index);
		}
		private void OnRunButtonClicked(object sender, EventArgs args) {
			GApp.MacroManager.SetMacroEventListener(this);
			GApp.MacroManager.Execute(this, GApp.MacroManager.GetModule(_list.SelectedItems[0].Index));
		}
		private void OnStopButtonClicked(object sender, EventArgs args) {
			GApp.GlobalCommandTarget.StopMacro();
		}

		private void OnPropButtonClicked(object sender, EventArgs args) {
			ShowProp(_list.SelectedItems[0].Index);
		}
		private void OnAddButtonClicked(object sender, EventArgs args) {
			ModuleProperty dlg = new ModuleProperty(this, null, Keys.None);
			if(GCUtil.ShowModalDialog(this, dlg)==DialogResult.OK) {
				AddListItem(dlg.Module, dlg.ShortCut);
				GApp.MacroManager.AddModule(dlg.Module);
				if(dlg.ShortCut!=Keys.None) _keyToModule.Add(dlg.ShortCut, dlg.Module);
				AdjustUI();
			}
		}
		private void OnDeleteButtonClicked(object sender, EventArgs args) {
			MacroModule mod = GApp.MacroManager.GetModule(_list.SelectedItems[0].Index);
			GApp.MacroManager.RemoveModule(mod);
			IDictionaryEnumerator ie = _keyToModule.GetEnumerator();
			while(ie.MoveNext()) {
				if(ie.Value==mod) {
					_keyToModule.Remove(ie.Key);
					break;
				}
			}
			_list.Items.Remove(_list.SelectedItems[0]);
			_macroOrderUpdated = true;
			AdjustUI();
		}
		private void OnEnvironmentButtonClicked(object sender, EventArgs args) {
			EnvVariableList dlg = new EnvVariableList();
			GCUtil.ShowModalDialog(this, dlg);
		}
		private void OnDownButtonClicked(object sender, EventArgs args) {
			int n = _list.SelectedItems[0].Index;
			if(n==_list.Items.Count-1) return;

			ListViewItem li = _list.Items[n];
			_list.Items.RemoveAt(n);
			_list.Items.Insert(n+1, li);
			MacroModule mod = GApp.MacroManager.GetModule(n);
			GApp.MacroManager.RemoveAt(n);
			GApp.MacroManager.InsertModule(n+1, mod);
			_macroOrderUpdated = true;
		}
		private void OnUpButtonClicked(object sender, EventArgs args) {
			int n = _list.SelectedItems[0].Index;
			if(n==0) return;

			ListViewItem li = _list.Items[n];
			_list.Items.RemoveAt(n);
			_list.Items.Insert(n-1, li);
			MacroModule mod = GApp.MacroManager.GetModule(n);
			GApp.MacroManager.RemoveAt(n);
			GApp.MacroManager.InsertModule(n-1, mod);
			_macroOrderUpdated = true;
		}

		private void AdjustUI() {
			bool e = _list.SelectedItems.Count>0;
			_runButton.Enabled = e;
			_stopButton.Enabled = false;
			_propButton.Enabled = e;
			_addButton.Enabled = true;
			_deleteButton.Enabled = e;
			_downButton.Enabled = e;
			_upButton.Enabled = e;
		}
		private void ShowProp(int index) {
			MacroModule mod = GApp.MacroManager.GetModule(index);
			Keys key = Keys.None;
			IDictionaryEnumerator ie = _keyToModule.GetEnumerator();
			while(ie.MoveNext()) {
				if(ie.Value==mod) {
					key = (Keys)(ie.Key);
					break;
				}
			}

			ModuleProperty dlg = new ModuleProperty(this, mod, key);
			if(GCUtil.ShowModalDialog(this, dlg)==DialogResult.OK) {
				GApp.MacroManager.ReplaceModule(GApp.MacroManager.GetModule(index), dlg.Module);
				GApp.Options.Commands.ModifyMacroKey(dlg.Module.Index, dlg.ShortCut & Keys.Modifiers, dlg.ShortCut & Keys.KeyCode);
				ListViewItem li = _list.Items[index];
				li.Text = dlg.Module.Title;
				li.SubItems[1].Text = dlg.Module.Path;
				li.SubItems[2].Text = UILibUtil.KeyString(dlg.ShortCut);
				li.SubItems[3].Text = GetInfoString(dlg.Module);
				_keyToModule.Remove(key);
				if(dlg.ShortCut!=Keys.None) _keyToModule.Add(dlg.ShortCut, dlg.Module);

				AdjustUI();
			}
		}
		protected override void OnClosing(CancelEventArgs args) { //これを閉じるとき無条件で更新するが、いいのか？ OK/Cancel方式にすべき？
			base.OnClosed(args);
			GApp.Options.Commands.ClearVariableEntries();
			int c = 0;
			Hashtable ht = CollectionUtil.ReverseHashtable(_keyToModule);
			foreach(MacroModule mod in GApp.MacroManager.Modules) {
				mod.Index = c;
				object t = ht[mod];
				Keys key = t==null? Keys.None : (Keys)t;
				GApp.Options.Commands.AddEntry(new Commands.MacroEntry(mod.Title, key & Keys.Modifiers, key & Keys.KeyCode, c));
				c++;
			}
			
			if(_macroOrderUpdated) {
				if(GApp.Options.ActionOnLaunch==CID.ExecMacro) GApp.Options.ActionOnLaunch = CID.NOP;
			}

			GApp.Frame.AdjustMacroMenu();
			GApp.MacroManager.SetMacroEventListener(null);
		}
		public void IndicateMacroStarted() {
			_runButton.Enabled = false;
			_stopButton.Enabled = true;
		}
		public void IndicateMacroFinished() {
			_runButton.Enabled = true;
			_stopButton.Enabled = false;
		}

		private string GetInfoString(MacroModule mod) {
			return mod.DebugMode? "Caption.MacroList.Trace" : ""; //とりあえずはデバッグかどうかだけ
		}

	}
}
