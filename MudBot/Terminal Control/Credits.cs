/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: Credits.cs,v 1.3 2005/04/27 23:27:18 okajima Exp $
*/
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Poderosa.UI;
using Poderosa.Config;


namespace Poderosa.Forms
{
	internal class Credits : Form
    {
		private PictureBox _pictureBox;
		private Label _subtitle;
		private Button _okButton;
		private Label _mainPanel;

		private System.ComponentModel.Container components = null;

		public Credits()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();
			_okButton.Text = "OK";

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			//AboutBoxから借りる
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutBox));
			_pictureBox.Image = ((Image)(resources.GetObject("_pictureBox.Image")));
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
				_timer.Dispose();
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
			_pictureBox = new PictureBox();
			_subtitle = new Label();
			_okButton = new Button();
			_mainPanel = new Label();
			SuspendLayout();
			// 
			// _pictureBox
			// 
			_pictureBox.BackColor = SystemColors.Window;
			_pictureBox.Location = new Point(0, 0);
			_pictureBox.Name = "_pictureBox";
			_pictureBox.Size = new Size(288, 80);
			_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			_pictureBox.TabIndex = 0;
			_pictureBox.TabStop = false;
			// 
			// _subtitle
			// 
			_subtitle.BackColor = SystemColors.Window;
			_subtitle.Font = new Font("Arial", 11.25F, FontStyle.Italic, GraphicsUnit.Point, ((System.Byte)(0)));
			_subtitle.Location = new Point(0, 80);
			_subtitle.Name = "_subtitle";
			_subtitle.Size = new Size(288, 24);
			_subtitle.TabIndex = 1;
			_subtitle.Text = "The Terminal, Reloaded";
			_subtitle.TextAlign = ContentAlignment.MiddleRight;
			// 
			// _okButton
			// 
			_okButton.DialogResult = DialogResult.OK;
			_okButton.FlatStyle = FlatStyle.System;
			_okButton.Location = new Point(104, 240);
			_okButton.Name = "_okButton";
			_okButton.TabIndex = 2;
			// 
			// _mainPanel
			// 
			_mainPanel.BackColor = SystemColors.Window;
			_mainPanel.Font = new Font("MS UI Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((System.Byte)(128)));
			_mainPanel.Location = new Point(0, 104);
			_mainPanel.Name = "_mainPanel";
			_mainPanel.Size = new Size(288, 128);
			_mainPanel.TabIndex = 3;
			_mainPanel.Paint += new PaintEventHandler(OnPaintCredit);
			// 
			// Credits
			// 
			AcceptButton = _okButton;
			AutoScaleBaseSize = new Size(5, 12);
			BackColor = SystemColors.Control;
			ClientSize = new Size(290, 266);
			ControlBox = false;
			Controls.Add(_mainPanel);
			Controls.Add(_okButton);
			Controls.Add(_subtitle);
			Controls.Add(_pictureBox);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Name = "Credits";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Poderosa";
			ResumeLayout(false);

		}
		#endregion

		private class Entry {
			public string _en_name;
			public string _ja_name;
			public Entry(string en, string ja) {
				_en_name = en;
				_ja_name = ja;
			}
		}
		private class CreditGroup {
			public string _name;
			public Entry[] _credits;
			
			public CreditGroup(string name, params Entry[] credits) {
				_name = name;
				_credits = credits;
			}
		}

		private ArrayList _creditGroups;
		
		private void CreateCreditData() {
			_creditGroups = new ArrayList();
			_creditGroups.Add(new CreditGroup(
				"",
				new Entry[0]));
			_creditGroups.Add(new CreditGroup(
                "Project Leader & Chief Developer",
				new Entry("Daisuke OKAJIMA", "岡嶋 大介")));
			_creditGroups.Add(new CreditGroup(
                "Website Manager",
				new Entry("Hiroshi Taketazu", "Hiroshi Taketazu"),
				new Entry("Tadashi \"ELF\" Jokagi", "Tadashi \"ELF\" Jokagi")));
			_creditGroups.Add(new CreditGroup(
                "Server Administrator",
				new Entry("yuk@lavans", "yuk@lavans")));
			_creditGroups.Add(new CreditGroup(
				"Developer",
				new Entry("Shintaro UNO", "宇野 信太郎")));
			_creditGroups.Add(new CreditGroup(
				"Special Thanks To",
				new Entry("Routrek Networks, Inc.", "(株)ルートレック・ネットワークス")));
			_creditGroups.Add(new CreditGroup(
				"Poderosa Project",
				new Entry("http://www.poderosa.org/", "http://www.poderosa.org/")));
		}
		
		protected override void OnLoad(EventArgs e) {
			base.OnLoad (e);
			if(!DesignMode) {
				CreateCreditData();
				_creditIndex = 0;
				_creditStep = 0;
				_boldFont = new Font(_mainPanel.Font, FontStyle.Bold);

				_timer = new Timer();
				_timer.Tick += new EventHandler(OnTimer);
				_timer.Interval = 50;
				_timer.Start();
			}
		}

		private Font _boldFont;
		private int _creditIndex;
		private int _creditStep;
		private const int STEPS_PER_GROUP = 60;
		private Timer _timer;

		private void OnTimer(object sender, EventArgs args) {
			if(_creditIndex==0) {
				//最初の表示を出すまでにやや間を空ける
				if(++_creditStep==30) {
					_creditIndex++;
					_creditStep = 0;
				}	
			}
			else if(_creditIndex==_creditGroups.Count-1) {
				if(++_creditStep==20) {
					_timer.Stop();
				}
			}
			else if(++_creditStep==STEPS_PER_GROUP) {
				_creditIndex++;
				_creditStep = 0;
			}
			_mainPanel.Invalidate(true);
		}
		private void OnPaintCredit(object sender, PaintEventArgs args) {
			if(_creditIndex==_creditGroups.Count) return;

			CreditGroup grp = (CreditGroup)_creditGroups[_creditIndex];

			Color col;
			if(_creditStep < 10)
				col = ColorUtil.CalculateColor(SystemColors.WindowText, SystemColors.Window, _creditStep*(255/10));
			else if(_creditStep < 40)
				col = SystemColors.WindowText;
			else if(_creditStep < 50)
				col = ColorUtil.CalculateColor(SystemColors.WindowText, SystemColors.Window, (50-_creditStep)*(255/10));
			else
				return; //no draw

			Graphics g = args.Graphics;
			SizeF name_size = g.MeasureString(grp._name, _boldFont);
			Brush br = new SolidBrush(col);
			float y = (_mainPanel.Height - (name_size.Height*(1+grp._credits.Length))) / 2;
			float width = Width;
			DrawString(g, grp._name, _boldFont, br, y);
			y += name_size.Height;
			foreach(Entry e in grp._credits) {
				DrawString(g, GEnv.Options.Language==Language.English? e._en_name : e._ja_name, _mainPanel.Font, br, y);
				y += name_size.Height;
			}
			br.Dispose();
		}

		private void DrawString(Graphics g, string text, Font font, Brush br, float y) {
			SizeF sz = g.MeasureString(text, font);
			g.DrawString(text, font, br, (_mainPanel.Width-sz.Width)/2, y);
		}
	}
}
