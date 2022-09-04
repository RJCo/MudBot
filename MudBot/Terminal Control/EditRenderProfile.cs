/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: EditRenderProfile.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Config;
using Poderosa.ConnectionParam;
using Poderosa.Terminal;
using Poderosa.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;

namespace Poderosa.Forms
{
    /// <summary>
    /// EditRenderProfile の概要の説明です。
    /// </summary>
    public class EditRenderProfile : Form
    {
        private RenderProfile _profile;

        private Button _okButton;
        private Button _cancelButton;
        private Label _bgColorLabel;
        private ColorButton _bgColorBox;
        private Label _textColorLabel;
        private ColorButton _textColorBox;
        private Button _editColorEscapeSequence;
        private Label _fontLabel;
        private Label _fontDescription;
        private Button _fontSelectButton;
        private ClearTypeAwareLabel _fontSample;
        private Label _backgroundImageLabel;
        private TextBox _backgroundImageBox;
        private Button _backgroundImageSelectButton;
        private Label _imageStyleLabel;
        private ComboBox _imageStyleBox;

        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public EditRenderProfile(RenderProfile prof)
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();
            _imageStyleBox.Items.AddRange(EnumDescAttributeT.For(typeof(ImageStyle)).DescriptionCollection());
            _bgColorLabel.Text = "Back Color";
            _textColorLabel.Text = "Text Color";
            _fontSelectButton.Text = "Select...";
            _fontLabel.Text = "Font";
            _backgroundImageLabel.Text = "Back Picture";
            _imageStyleLabel.Text = "Picture Location";
            _cancelButton.Text = "Cancel";
            _okButton.Text = "OK";
            _fontSample.Text = "Poderosa";
            _editColorEscapeSequence.Text = "Color settings for escape sequences...";
            Text = "Edit Display Profile";

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            _profile = new RenderProfile(prof == null ? GEnv.DefaultRenderProfile : prof);
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

        #region Windows フォーム デザイナで生成されたコード 
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        protected void InitializeComponent()
        {
            _okButton = new Button();
            _cancelButton = new Button();
            _bgColorLabel = new Label();
            _bgColorBox = new ColorButton();
            _textColorLabel = new Label();
            _textColorBox = new ColorButton();
            _editColorEscapeSequence = new Button();
            _fontLabel = new Label();
            _fontDescription = new Label();
            _fontSelectButton = new Button();
            _fontSample = new ClearTypeAwareLabel();
            _backgroundImageLabel = new Label();
            _backgroundImageBox = new TextBox();
            _backgroundImageSelectButton = new Button();
            _imageStyleLabel = new Label();
            _imageStyleBox = new ComboBox();
            SuspendLayout();
            // 
            // _bgColorLabel
            // 
            _bgColorLabel.Location = new Point(16, 16);
            _bgColorLabel.Name = "_bgColorLabel";
            _bgColorLabel.Size = new Size(72, 24);
            _bgColorLabel.TabIndex = 0;
            _bgColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _bgColorBox
            // 
            _bgColorBox.Location = new Point(120, 16);
            _bgColorBox.Name = "_bgColorBox";
            _bgColorBox.Size = new Size(112, 20);
            _bgColorBox.TabIndex = 1;
            _bgColorBox.ColorChanged += new ColorButton.NewColorEventHandler(OnBGColorChanged);
            // 
            // _textColorLabel
            // 
            _textColorLabel.Location = new Point(16, 40);
            _textColorLabel.Name = "_textColorLabel";
            _textColorLabel.Size = new Size(72, 24);
            _textColorLabel.TabIndex = 2;
            _textColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _textColorBox
            // 
            _textColorBox.Location = new Point(120, 40);
            _textColorBox.Name = "_textColorBox";
            _textColorBox.Size = new Size(112, 20);
            _textColorBox.TabIndex = 3;
            _textColorBox.ColorChanged += new ColorButton.NewColorEventHandler(OnTextColorChanged);
            //
            // _editColorEscapeSequence
            //
            _editColorEscapeSequence.Location = new Point(120, 72);
            _editColorEscapeSequence.Size = new Size(216, 24);
            _editColorEscapeSequence.TabIndex = 4;
            _editColorEscapeSequence.Click += new EventHandler(OnEditColorEscapeSequence);
            _editColorEscapeSequence.FlatStyle = FlatStyle.System;
            // 
            // _fontLabel
            // 
            _fontLabel.Location = new Point(16, 96);
            _fontLabel.Name = "_fontLabel";
            _fontLabel.Size = new Size(72, 16);
            _fontLabel.TabIndex = 5;
            _fontLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _fontDescription
            // 
            _fontDescription.Location = new Point(120, 100);
            _fontDescription.Name = "_fontDescription";
            _fontDescription.Size = new Size(168, 24);
            _fontDescription.TabIndex = 6;
            _fontDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _fontSelectButton
            // 
            _fontSelectButton.Location = new Point(296, 100);
            _fontSelectButton.Name = "_fontSelectButton";
            _fontSelectButton.FlatStyle = FlatStyle.System;
            _fontSelectButton.Size = new Size(64, 23);
            _fontSelectButton.TabIndex = 7;
            _fontSelectButton.Click += new EventHandler(OnFontSelect);
            // 
            // _fontSample
            // 
            _fontSample.BackColor = Color.White;
            _fontSample.ClearType = false;
            _fontSample.Location = new Point(240, 16);
            _fontSample.Name = "_fontSample";
            _fontSample.Size = new Size(120, 46);
            _fontSample.TabIndex = 8;
            _fontSample.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _backgroundImageLabel
            // 
            _backgroundImageLabel.Location = new Point(16, 128);
            _backgroundImageLabel.Name = "_backgroundImageLabel";
            _backgroundImageLabel.Size = new Size(72, 16);
            _backgroundImageLabel.TabIndex = 9;
            _backgroundImageLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _backgroundImageBox
            // 
            _backgroundImageBox.Location = new Point(120, 128);
            _backgroundImageBox.Name = "_backgroundImageBox";
            _backgroundImageBox.Size = new Size(220, 19);
            _backgroundImageBox.TabIndex = 10;
            // 
            // _backgroundImageSelectButton
            // 
            _backgroundImageSelectButton.Location = new Point(340, 128);
            _backgroundImageSelectButton.Name = "_backgroundImageSelectButton";
            _backgroundImageSelectButton.FlatStyle = FlatStyle.System;
            _backgroundImageSelectButton.Size = new Size(19, 19);
            _backgroundImageSelectButton.TabIndex = 11;
            _backgroundImageSelectButton.Text = "...";
            _backgroundImageSelectButton.Click += new EventHandler(OnSelectBackgroundImage);
            // 
            // _imageStyleLabel
            // 
            _imageStyleLabel.Location = new Point(16, 152);
            _imageStyleLabel.Name = "_imageStyleLabel";
            _imageStyleLabel.Size = new Size(96, 16);
            _imageStyleLabel.TabIndex = 12;
            _imageStyleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _imageStyleBox
            // 
            _imageStyleBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _imageStyleBox.Location = new Point(120, 152);
            _imageStyleBox.Name = "_imageStyleBox";
            _imageStyleBox.Size = new Size(112, 19);
            _imageStyleBox.TabIndex = 13;
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(208, 180);
            _okButton.Name = "_okButton";
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.TabIndex = 14;
            _okButton.Click += new EventHandler(OnOK);
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(288, 180);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.TabIndex = 15;
            // 
            // EditRenderProfile
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(368, 208);
            Controls.Add(_cancelButton);
            Controls.Add(_okButton);
            Controls.Add(_bgColorLabel);
            Controls.Add(_bgColorBox);
            Controls.Add(_textColorLabel);
            Controls.Add(_textColorBox);
            Controls.Add(_editColorEscapeSequence);
            Controls.Add(_fontLabel);
            Controls.Add(_fontDescription);
            Controls.Add(_fontSelectButton);
            Controls.Add(_fontSample);
            Controls.Add(_backgroundImageLabel);
            Controls.Add(_backgroundImageBox);
            Controls.Add(_backgroundImageSelectButton);
            Controls.Add(_imageStyleLabel);
            Controls.Add(_imageStyleBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditRenderProfile";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        #endregion
        private void OnBGColorChanged(object sender, Color e)
        {
            _fontSample.BackColor = e;
            _profile.BackColor = e;
        }
        private void OnTextColorChanged(object sender, Color e)
        {
            _fontSample.ForeColor = e;
            _profile.ForeColor = e;
        }
        private void OnEditColorEscapeSequence(object sender, EventArgs args)
        {
            EditEscapeSequenceColor dlg = new EditEscapeSequenceColor(_fontSample.BackColor, _fontSample.ForeColor, _profile.ESColorSet);
            if (GCUtil.ShowModalDialog(this, dlg) == DialogResult.OK)
            {
                _profile.ESColorSet = dlg.Result;
                _profile.ForeColor = dlg.GForeColor;
                _profile.BackColor = dlg.GBackColor;
                _bgColorBox.SelectedColor = dlg.GBackColor;
                _textColorBox.SelectedColor = dlg.GForeColor;
                _fontSample.ForeColor = dlg.GForeColor;
                _fontSample.BackColor = dlg.GBackColor;
                _fontSample.Invalidate();
                _bgColorBox.Invalidate();
                _textColorBox.Invalidate();
            }
        }

        private void OnFontSelect(object sender, EventArgs args)
        {
            GFontDialog gd = new GFontDialog();
            Font nf = GUtil.CreateFont(_profile.FontName, _profile.FontSize);
            Font jf = GUtil.CreateFont(_profile.JapaneseFontName, _profile.FontSize);
            gd.SetFont(_profile.UseClearType, nf, jf);
            if (GCUtil.ShowModalDialog(this, gd) == DialogResult.OK)
            {
                Font f = gd.ASCIIFont;
                _profile.FontName = f.Name;
                _profile.JapaneseFontName = gd.JapaneseFont.Name;
                _profile.FontSize = f.Size;
                _profile.UseClearType = gd.UseClearType;
                _fontSample.Font = f;
                AdjustFontDescription(f.Name, gd.JapaneseFont.Name, f.Size);
            }
        }
        private void OnSelectBackgroundImage(object sender, EventArgs args)
        {
            string t = GCUtil.SelectPictureFileByDialog(this);
            if (t != null)
            {
                _backgroundImageBox.Text = t;
                _profile.BackgroundImageFileName = t;
            }
        }
        private void AdjustFontDescription(string ascii, string japanese, float fsz)
        {
            int sz = (int)(fsz + 0.5);
            if (GApp.Options.Language == Language.English || ascii == japanese)
                _fontDescription.Text = String.Format("{0},{1}pt", ascii, sz); //Singleをintにキャストすると切り捨てだが、四捨五入にしてほしいので0.5を足してから切り捨てる
            else
                _fontDescription.Text = String.Format("{0}/{1},{2}pt", ascii, japanese, sz);
        }

        private void InitUI()
        {
            AdjustFontDescription(_profile.FontName, _profile.JapaneseFontName, _profile.FontSize);
            _fontSample.Font = GUtil.CreateFont(_profile.FontName, _profile.FontSize);
            _fontSample.BackColor = _profile.BackColor;
            _fontSample.ForeColor = _profile.ForeColor;
            _fontSample.ClearType = _profile.UseClearType;
            _fontSample.Invalidate(true);
            _backgroundImageBox.Text = _profile.BackgroundImageFileName;
            _bgColorBox.SelectedColor = _profile.BackColor;
            _textColorBox.SelectedColor = _profile.ForeColor;
            _imageStyleBox.SelectedIndex = (int)_profile.ImageStyle;
        }

        private void OnOK(object sender, EventArgs args)
        {
            if (_backgroundImageBox.Text.Length > 0)
            {
                try
                {
                    Image.FromFile(_backgroundImageBox.Text);
                }
                catch (Exception)
                {
                    GUtil.Warning(this, $"{_backgroundImageBox.Text} is not a correct picture file.");
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            _profile.BackgroundImageFileName = _backgroundImageBox.Text;
            _profile.ImageStyle = (ImageStyle)_imageStyleBox.SelectedIndex;
        }

        public RenderProfile Result
        {
            get
            {
                return _profile;
            }
        }


    }
}
