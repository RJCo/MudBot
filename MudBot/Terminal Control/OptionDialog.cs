/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: OptionDialog.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

using Poderosa.Config;
using Poderosa.UI;

namespace Poderosa.Forms
{
    internal class OptionDialog : Form
    {
		private enum PageID {
			NotSelected = -1,
			Display = 0,
			Terminal,
			Peripheral,
			Command,
			SSH,
			Connection,
			Generic,
			COUNT
		}
		public abstract class CategoryPanel : Panel {
			public abstract void InitUI(ContainerOptions options);
			public abstract bool Commit(ContainerOptions options);
		}
		private CategoryPanel[] _pages;

		//再度ダイアログを開いても開いていたページが残るようにstatic
		private static PageID _currentPageID = PageID.Display;

		private ContainerOptions _options;

		private ImageList _imageList;
		private Panel _categoryItems;
		private Button _okButton;
		private Button _cancelButton;
		private IContainer components;

		public OptionDialog()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			if(!DesignMode) {
				FillText();
				InitItems();
				_options = (ContainerOptions)GApp.Options.Clone();
				_pages = new CategoryPanel[(int)PageID.COUNT];
			}
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

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			components = new Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OptionDialog));
			_imageList = new ImageList(components);
			_categoryItems = new Panel();
			_okButton = new Button();
			_cancelButton = new Button();
			SuspendLayout();
			// 
			// _imageList
			// 
			_imageList.ImageSize = new Size(32, 32);
			_imageList.ImageStream = ((ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
			_imageList.TransparentColor = Color.Teal;
			// 
			// _categoryItems
			// 
			_categoryItems.BackColor = SystemColors.Window;
			_categoryItems.BorderStyle = BorderStyle.FixedSingle;
			_categoryItems.Location = new Point(4, 0);
			_categoryItems.Name = "_categoryItems";
			_categoryItems.Size = new Size(72, 376);
			_categoryItems.TabIndex = 3;
			// 
			// _okButton
			// 
			_okButton.DialogResult = DialogResult.OK;
			_okButton.FlatStyle = FlatStyle.System;
			_okButton.Location = new Point(336, 384);
			_okButton.Name = "_okButton";
			_okButton.TabIndex = 1;
			_okButton.Click += new EventHandler(OnOK);
			// 
			// _cancelButton
			// 
			_cancelButton.DialogResult = DialogResult.Cancel;
			_cancelButton.FlatStyle = FlatStyle.System;
			_cancelButton.Location = new Point(432, 384);
			_cancelButton.Name = "_cancelButton";
			_cancelButton.TabIndex = 2;
			// 
			// OptionDialog
			// 
			AcceptButton = _okButton;
			AutoScaleBaseSize = new Size(5, 12);
			CancelButton = _cancelButton;
			ClientSize = new Size(528, 414);
			Controls.Add(_cancelButton);
			Controls.Add(_okButton);
			Controls.Add(_categoryItems);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "OptionDialog";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "OptionDialog";
			ResumeLayout(false);

		}

		#endregion

		private void FillText() {
			_okButton.Text = "OK";
			_cancelButton.Text = "Cancel";
			Text = "Form.OptionDialog.Text";
		}
		private void InitItems() {
			int y = 8;
			for(int i=0; i<(int)PageID.COUNT; i++) {
                PanelItem item = new PanelItem(this, i, _imageList.Images[i], GetStringIDFor(i))
                {
                    Location = new Point(4, y)
                };
                _categoryItems.Controls.Add(item);

				y += 52;
			}
		}
		private PanelItem PanelItemAt(PageID index) {
			return (PanelItem)_categoryItems.Controls[(int)index];
		}

		public void SelectItem(int index) {
			if((PageID)index==_currentPageID) return;

			//現在の内容でCommitできた場合のみ選択されたページを表示
			if(ClosePage())
				ShowPage((PageID)index);
		}
		public void SetHilightingItemIndex(int index) {
			foreach(PanelItem item in _categoryItems.Controls) {
				item.Hilight = item.Index==index;
			}
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad (e);
			ShowPage(_currentPageID);
		}

		private bool ClosePage() {
			CategoryPanel cp = _pages[(int)_currentPageID];
			if(!cp.Commit(_options)) return false;
			Controls.Remove(cp);
			PanelItemAt(_currentPageID).Selected = false;
			_categoryItems.Invalidate(true);
			return true;
		}

		private void ShowPage(PageID p) {
			if(_pages[(int)p]==null)
				_pages[(int)p] = CreatePage(p);

			_currentPageID = p;
			CategoryPanel cp = _pages[(int)p];
			cp.InitUI(_options);
			Controls.Add(cp);
			PanelItemAt(p).Selected = true;
			_categoryItems.Invalidate(true);
		}

		private CategoryPanel CreatePage(PageID p) {
			CategoryPanel panel = null;
			switch(p) {
				case PageID.Display:
					panel = new DisplayOptionPanel();
					break;
				case PageID.Terminal:
					panel = new TerminalOptionPanel();
					break;
				case PageID.Peripheral:
					panel = new PeripheralOptionPanel();
					break;
				case PageID.Command:
					panel = new CommandOptionPanel();
					break;
				case PageID.SSH:
					panel = new SSHOptionPanel();
					break;
				case PageID.Connection:
					panel = new ConnectionOptionPanel();
					break;
				case PageID.Generic:
					panel = new GenericOptionPanel();
					break;
			}
			
			Debug.Assert(panel!=null);
			panel.BorderStyle = BorderStyle.FixedSingle;
			panel.Location = new Point(_categoryItems.Right + 4, _categoryItems.Top);
			panel.Size = new Size(Width - _categoryItems.Width - 16, _categoryItems.Height);
			return panel;
		}



		private void OnOK(object sender, EventArgs args) {
			bool ok = ClosePage();
			if(ok) {
				DialogResult = DialogResult.OK;

				GApp.UpdateOptions(_options);
			}
			else {
				DialogResult = DialogResult.None;
			}
		}

		private static string GetStringIDFor(int id) {
			switch((PageID)id) {
				case PageID.Display:
					return "Form.OptionDialog._displayPanel";
				case PageID.Terminal:
					return "Form.OptionDialog._terminalPanel";
				case PageID.Peripheral:
					return "Form.OptionDialog._peripheralConfigPanel";
				case PageID.Command:
					return "Form.OptionDialog._commandPanel";
				case PageID.SSH:
					return "Form.OptionDialog._sshPanel";
				case PageID.Connection:
					return "Form.OptionDialog._connectionPanel";
				case PageID.Generic:
					return "Form.OptionDialog._genericPanel";
			}
			Debug.Assert(false, "should not reach here");
			return null;
		}

	}

	internal class PanelItem : UserControl {
		private int _index;
		private Image _image;
		private OptionDialog _parent;
		private string _caption;

		private bool _selected;
		private bool _hilight;

		private static Brush _textBrush = new SolidBrush(SystemColors.WindowText);
		private static Size _defaultSize = new Size(64, 48);
		private static DrawUtil.RoundRectColors _selectedColors;
		private static DrawUtil.RoundRectColors _hilightColors;

		public PanelItem(OptionDialog parent, int index, Image image, string caption) {
			_parent = parent;
			_index = index;
			_image = image;
			_caption = caption;
			Size = _defaultSize;
			TabStop = true;
			AdjustBackColor();
		}
		public int Index {
			get {
				return _index;
			}
		}
		public bool Selected {
			get {
				return _selected;
			}
			set {
				_selected = value;
				AdjustBackColor();
			}
		}

		public bool Hilight {
			get {
				return _hilight;
			}
			set {
				_hilight = value;
				AdjustBackColor();
			}
		}

		protected override void OnMouseEnter(EventArgs e) {
			base.OnMouseEnter(e);
			_parent.SetHilightingItemIndex(_selected? -1 : _index);
		}

		protected override void OnGotFocus(EventArgs e) {
			base.OnGotFocus (e);
			_parent.SetHilightingItemIndex(_selected? -1 : _index);
		}
		protected override void OnKeyDown(KeyEventArgs e) {
			base.OnKeyDown (e);
			if(e.KeyCode==Keys.Space) {
				_parent.SelectItem(_index);
			}
		}
		protected override void OnClick(EventArgs e) {
			base.OnClick (e);
			_parent.SelectItem(_index);
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint (e);
			const int image_size = 32; //square image
			if(_selectedColors==null) CreateColor();

			Graphics g = e.Graphics;

			if(_selected)
				DrawUtil.DrawRoundRect(g, 0, 0, Width-1, Height-1, _selectedColors);
			else if(_hilight)
				DrawUtil.DrawRoundRect(g, 0, 0, Width-1, Height-1, _hilightColors);
			g.DrawImage(_image, (Width - image_size)/2, 0);
			SizeF sz = g.MeasureString(_caption, Font);
			g.DrawString(_caption, Font, _textBrush, (int)(Width-sz.Width)/2, image_size);
		}

		private void AdjustBackColor() {
			if(_selected)
				BackColor = Color.Orange;
			else if(_hilight)
				BackColor = DrawUtil.LightColor(Color.Orange);
			else
				BackColor = SystemColors.Window;
		}


		private static void CreateColor() {
            _selectedColors = new DrawUtil.RoundRectColors
            {
                border_color = DrawUtil.ToCOLORREF(Color.DarkRed),
                inner_color = DrawUtil.ToCOLORREF(Color.Orange),
                outer_color = DrawUtil.ToCOLORREF(SystemColors.Window)
            };
            _selectedColors.lightlight_color = DrawUtil.MergeColor(_selectedColors.border_color, _selectedColors.outer_color);
			_selectedColors.light_color = DrawUtil.MergeColor(_selectedColors.lightlight_color, _selectedColors.border_color);

            _hilightColors = new DrawUtil.RoundRectColors
            {
                border_color = DrawUtil.ToCOLORREF(Color.Pink),
                inner_color = DrawUtil.ToCOLORREF(DrawUtil.LightColor(Color.Orange)),
                outer_color = DrawUtil.ToCOLORREF(SystemColors.Window)
            };
            _hilightColors.lightlight_color = DrawUtil.MergeColor(_hilightColors.border_color, _hilightColors.outer_color);
			_hilightColors.light_color = DrawUtil.MergeColor(_hilightColors.lightlight_color, _hilightColors.border_color);
		}
	}
}
