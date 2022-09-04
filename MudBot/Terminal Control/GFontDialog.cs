/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: GFontDialog.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Config;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// GFontDialog の概要の説明です。
    /// </summary>
    internal class GFontDialog : Form
    {

        //このダイアログは言語によって様子が違ってくる
        private Language _language;

        private ListBox _asciiFontList;
        private Label _lAsciiFont;
        private Label _lFontSize;
        private ComboBox _fontSizeList;
        private CheckBox _checkClearType;
        private Label _lJapaneseFont;
        private ListBox _japaneseFontList;
        private ClearTypeAwareLabel _lASCIISample;
        private ClearTypeAwareLabel _lJapaneseSample;
        private Button _okButton;
        private Button _cancelButton;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        private bool _ignoreEvent;


        private Font _japaneseFont;
        private Font _asciiFont;
        public Font JapaneseFont
        {
            get
            {
                return _japaneseFont;
            }
        }
        public Font ASCIIFont
        {
            get
            {
                return _asciiFont;
            }
        }
        public bool UseClearType
        {
            get
            {
                return _checkClearType.Checked;
            }
        }

        public void SetFont(bool cleartype, Font ascii, Font japanese)
        {
            _ignoreEvent = true;
            _asciiFont = ascii;
            _japaneseFont = japanese;
            _checkClearType.Checked = cleartype;
            _lASCIISample.ClearType = cleartype;
            _lJapaneseSample.ClearType = cleartype;
            int s = (int)ascii.Size;
            _fontSizeList.SelectedIndex = _fontSizeList.FindStringExact(s.ToString());
            _asciiFontList.SelectedIndex = _asciiFontList.FindStringExact(ascii.Name);
            _japaneseFontList.SelectedIndex = _japaneseFontList.FindStringExact(japanese.Name);

            if (_asciiFontList.SelectedIndex == -1)
            {
                _asciiFontList.SelectedIndex = _asciiFontList.FindStringExact("Courier New");
            }

            if (_japaneseFontList.SelectedIndex == -1)
            {
                _japaneseFontList.SelectedIndex = _japaneseFontList.FindStringExact("ＭＳ ゴシック");
            }

            _lASCIISample.Font = ascii;
            _lJapaneseSample.Font = japanese;
            _ignoreEvent = false;
        }

        public GFontDialog()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();
            _language = GApp.Options.Language;

            _lAsciiFont.Text = "Form.GFontDialog._lAsciiFont";
            _lJapaneseFont.Text = "Form.GFontDialog._lJapaneseFont";
            _lFontSize.Text = "Form.GFontDialog._lFontSize";
            _checkClearType.Text = "Form.GFontDialog._checkClearType";
            _okButton.Text = "OK";
            _cancelButton.Text = "Cancel";
            _lASCIISample.Text = "Common.FontSample";
            _lJapaneseSample.Text = "Common.JapaneseFontSample";
            Text = "Form.GFontDialog.Text";

            if (_language == Language.English)
            {
                _lJapaneseFont.Visible = false;
                _japaneseFontList.Visible = false;
                _lJapaneseSample.Visible = false;
                int dl = _lJapaneseFont.Width / 2 - 32;
                int dw = 72;
                _lAsciiFont.Left += dl;
                _lAsciiFont.Width += dw;
                _asciiFontList.Left += dl;
                _asciiFontList.Width += dw;
                _lASCIISample.Left += dl;
                _lASCIISample.Width += dw;
            }

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            InitUI();
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
            _asciiFontList = new ListBox();
            _lAsciiFont = new Label();
            _lFontSize = new Label();
            _fontSizeList = new ComboBox();
            _checkClearType = new CheckBox();
            _lJapaneseFont = new Label();
            _japaneseFontList = new ListBox();
            _lASCIISample = new ClearTypeAwareLabel();
            _lJapaneseSample = new ClearTypeAwareLabel();
            _okButton = new Button();
            _cancelButton = new Button();
            SuspendLayout();
            // 
            // _asciiFontList
            // 
            _asciiFontList.ItemHeight = 12;
            _asciiFontList.Location = new Point(8, 88);
            _asciiFontList.Name = "_asciiFontList";
            _asciiFontList.Size = new Size(128, 100);
            _asciiFontList.TabIndex = 4;
            _asciiFontList.SelectedIndexChanged += new EventHandler(OnASCIIFontChange);
            // 
            // _lAsciiFont
            // 
            _lAsciiFont.Location = new Point(8, 64);
            _lAsciiFont.Name = "_lAsciiFont";
            _lAsciiFont.Size = new Size(120, 23);
            _lAsciiFont.TabIndex = 3;
            _lAsciiFont.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _lFontSize
            // 
            _lFontSize.Location = new Point(16, 12);
            _lFontSize.Name = "_lFontSize";
            _lFontSize.Size = new Size(104, 16);
            _lFontSize.TabIndex = 0;
            // 
            // _fontSizeList
            // 
            _fontSizeList.DropDownStyle = ComboBoxStyle.DropDownList;
            _fontSizeList.Location = new Point(136, 8);
            _fontSizeList.Name = "_fontSizeList";
            _fontSizeList.Size = new Size(121, 20);
            _fontSizeList.TabIndex = 1;
            _fontSizeList.SelectedIndexChanged += new EventHandler(UpdateFontSample);
            // 
            // _checkClearType
            // 
            _checkClearType.Location = new Point(24, 32);
            _checkClearType.Name = "_checkClearType";
            _checkClearType.FlatStyle = FlatStyle.System;
            _checkClearType.Size = new Size(240, 32);
            _checkClearType.TabIndex = 2;
            _checkClearType.CheckedChanged += new EventHandler(UpdateFontSample);
            // 
            // _lJapaneseFont
            // 
            _lJapaneseFont.Location = new Point(144, 64);
            _lJapaneseFont.Name = "_lJapaneseFont";
            _lJapaneseFont.Size = new Size(128, 23);
            _lJapaneseFont.TabIndex = 5;
            _lJapaneseFont.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _japaneseFontList
            // 
            _japaneseFontList.ItemHeight = 12;
            _japaneseFontList.Location = new Point(144, 88);
            _japaneseFontList.Name = "_japaneseFontList";
            _japaneseFontList.Size = new Size(128, 100);
            _japaneseFontList.TabIndex = 6;
            _japaneseFontList.SelectedIndexChanged += new EventHandler(OnJapaneseFontChange);
            // 
            // _lASCIISample
            // 
            _lASCIISample.BorderStyle = BorderStyle.Fixed3D;
            _lASCIISample.ClearType = false;
            _lASCIISample.Location = new Point(8, 192);
            _lASCIISample.Name = "_lASCIISample";
            _lASCIISample.Size = new Size(128, 40);
            _lASCIISample.TabIndex = 7;
            _lASCIISample.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _lJapaneseSample
            // 
            _lJapaneseSample.BorderStyle = BorderStyle.Fixed3D;
            _lJapaneseSample.ClearType = false;
            _lJapaneseSample.Location = new Point(144, 192);
            _lJapaneseSample.Name = "_lJapaneseSample";
            _lJapaneseSample.Size = new Size(128, 40);
            _lJapaneseSample.TabIndex = 8;
            _lJapaneseSample.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(112, 236);
            _okButton.Name = "_okButton";
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.TabIndex = 9;
            _okButton.Click += new EventHandler(OnOK);
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(200, 236);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.TabIndex = 10;
            _cancelButton.Click += new EventHandler(OnCancel);
            // 
            // GFontDialog
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(282, 271);
            Controls.AddRange(new Control[] {
                                                                          _cancelButton,
                                                                          _okButton,
                                                                          _lJapaneseSample,
                                                                          _lASCIISample,
                                                                          _japaneseFontList,
                                                                          _lJapaneseFont,
                                                                          _checkClearType,
                                                                          _fontSizeList,
                                                                          _lFontSize,
                                                                          _lAsciiFont,
                                                                          _asciiFontList});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GFontDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion


        private void InitUI()
        {
            _fontSizeList.Items.Add("6");
            _fontSizeList.Items.Add("8");
            _fontSizeList.Items.Add("9");
            _fontSizeList.Items.Add("10");
            _fontSizeList.Items.Add("11");
            _fontSizeList.Items.Add("12");
            _fontSizeList.Items.Add("14");
            _fontSizeList.Items.Add("16");
            _fontSizeList.Items.Add("18");
            _fontSizeList.Items.Add("20");

            InitFontList();
            /*
			foreach(FontFamily f in FontFamily.Families) {
				if(!f.IsStyleAvailable(FontStyle.Regular|FontStyle.Underline|FontStyle.Bold)) continue;
				Win32.LOGFONT lf = new Win32.LOGFONT();
				new Font(f, 10).ToLogFont(lf);
				//if((lf.lfPitchAndFamily & 0x01)==0) continue; //fixed pitchのみ認める
				Debug.WriteLine(lf.lfFaceName+" " + lf.lfCharSet + " " + lf.lfPitchAndFamily);
				if(lf.lfCharSet==128)
					_japaneseFontList.Items.Add(f.GetName(0));
				if(lf.lfCharSet!=2) //Symbol用は除く
					_asciiFontList.Items.Add(f.GetName(0));
			}
			*/
        }

        private void InitFontList()
        {
            Win32.tagLOGFONT lf = new Win32.tagLOGFONT();
            Graphics g = CreateGraphics();
            IntPtr hDC = g.GetHdc();

            Win32.EnumFontFamExProc proc = new Win32.EnumFontFamExProc(FontProc);
            IntPtr lParam = new IntPtr(0);
            lf.lfCharSet = 1; //default
            Win32.EnumFontFamiliesEx(hDC, ref lf, proc, lParam, 0);
            //lf.lfCharSet = 128; //日本語
            //lParam = new IntPtr(128);
            //Win32.EnumFontFamiliesEx(hDC, ref lf, proc, lParam, 0);
            g.ReleaseHdc(hDC);
        }

        private int FontProc(ref Win32.ENUMLOGFONTEX lpelfe, ref Win32.NEWTEXTMETRICEX lpntme, uint FontType, IntPtr lParam)
        {
            //(lpelfe.lfPitchAndFamily & 2)==0)
            bool interesting = FontType == 4 && (lpntme.ntmTm.tmPitchAndFamily & 1) == 0 && lpelfe.lfFaceName[0] != '@';
            //if(!interesting)
            //	if(lpelfe.lfFaceName=="FixedSys" || lpelfe.lfFaceName=="Terminal") interesting = true; //この２つだけはTrueTypeでなくともリストにいれる

            if (interesting)
            { //縦書きでないことはこれでしか判定できないのか？
                if (_language == Language.Japanese && lpntme.ntmTm.tmCharSet == 128)
                {
                    _japaneseFontList.Items.Add(lpelfe.lfFaceName);
                    //日本語フォントでもASCIIは必ず表示できるはず
                    if (_asciiFontList.FindStringExact(lpelfe.lfFaceName) == -1)
                    {
                        _asciiFontList.Items.Add(lpelfe.lfFaceName);
                    }
                }
                else if (lpntme.ntmTm.tmCharSet == 0)
                {
                    if (_asciiFontList.FindStringExact(lpelfe.lfFaceName) == -1)
                    {
                        _asciiFontList.Items.Add(lpelfe.lfFaceName);
                    }
                }
            }
            return 1;
        }

        private void UpdateFontSample(object sender, EventArgs args)
        {
            if (_ignoreEvent)
            {
                return;
            }

            _lASCIISample.ClearType = _checkClearType.Checked;
            _lJapaneseSample.ClearType = _checkClearType.Checked;
            OnJapaneseFontChange(sender, args);
            OnASCIIFontChange(sender, args);
            _lASCIISample.Invalidate();
            _lJapaneseSample.Invalidate();
        }
        private void OnJapaneseFontChange(object sender, EventArgs args)
        {
            if (_ignoreEvent || _japaneseFontList.SelectedIndex == -1)
            {
                return;
            }

            string fontname = (string)_japaneseFontList.Items[_japaneseFontList.SelectedIndex];
            _japaneseFont = GUtil.CreateFont(fontname, GetFontSize());
            _lJapaneseSample.Font = _japaneseFont;
        }
        private void OnASCIIFontChange(object sender, EventArgs args)
        {
            if (_ignoreEvent || _asciiFontList.SelectedIndex == -1)
            {
                return;
            }

            string fontname = (string)_asciiFontList.Items[_asciiFontList.SelectedIndex];
            _asciiFont = GUtil.CreateFont(fontname, GetFontSize());
            _lASCIISample.Font = _asciiFont;
        }
        private void OnOK(object sender, EventArgs args)
        {
            if (!CheckFixedSizeFont("FixedSys", 14) || !CheckFixedSizeFont("Terminal", 6, 10, 14, 17, 20))
            {
                DialogResult = DialogResult.None;
            }
            else
            {
                DialogResult = DialogResult.OK;
                try
                {
                    Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                }
            }
        }
        private void OnCancel(object sender, EventArgs args)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //固定長フォントを使っているとき、認められていないサイズを指定していたら警告してfalseを返す。
        //allowed_sizesはサイズ指定のリストに含まれているものを使用すること！
        private bool CheckFixedSizeFont(string name, params float[] allowed_sizes)
        {
            if (_asciiFont.Name == name || _japaneseFont.Name == name)
            {
                float sz = GetFontSize();
                bool contained = false;
                float diff = Single.MaxValue;
                float nearest = 0;
                foreach (float t in allowed_sizes)
                {
                    if (t == sz)
                    {
                        contained = true;
                        break;
                    }
                    else
                    {
                        if (diff > Math.Abs(sz - t))
                        {
                            diff = Math.Abs(sz - t);
                            nearest = t;
                        }
                    }
                }

                if (!contained)
                {
                    GUtil.Warning(this, String.Format("Message.GFontDialog.NotTrueTypeWarning", name, nearest));
                    _fontSizeList.SelectedIndex = _fontSizeList.FindStringExact(nearest.ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private float GetFontSize()
        {
            return Single.Parse((string)_fontSizeList.Items[_fontSizeList.SelectedIndex]);
        }


    }

    internal class ClearTypeAwareLabel : Label
    {
        private bool _clearType;
        public bool ClearType
        {
            get
            {
                return _clearType;
            }
            set
            {
                _clearType = value;
            }
        }
        protected override void OnPaint(PaintEventArgs args)
        {
            base.OnPaint(args);
            args.Graphics.TextRenderingHint = _clearType ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;
        }
    }
}
