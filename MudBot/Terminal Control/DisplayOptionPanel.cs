/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: DisplayOptionPanel.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using Poderosa.Terminal;
using Poderosa.Config;
using Poderosa.ConnectionParam;
using Poderosa.UI;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;

namespace Poderosa.Forms
{
    /// <summary>
    /// DisplayOptionPanel の概要の説明です。
    /// </summary>
    internal class DisplayOptionPanel : OptionDialog.CategoryPanel
    {
        private EscapesequenceColorSet _ESColorSet;
        private string _defaultFileDir;
        private bool _useClearType;
        private Font _font;
        private Font _japaneseFont;

        private GroupBox _colorFontGroup;
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
        private Button _applyRenderProfileButton;
        private GroupBox _caretGroup;
        private Label _caretStyleLabel;
        private ComboBox _caretStyleBox;
        private CheckBox _caretSpecifyColor;
        private Label _caretColorLabel;
        private ColorButton _caretColorBox;
        private CheckBox _caretBlink;

        public DisplayOptionPanel()
        {
            InitializeComponent();
            FillText();
        }
        private void InitializeComponent()
        {
            _colorFontGroup = new GroupBox();
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
            _applyRenderProfileButton = new Button();
            _caretGroup = new GroupBox();
            _caretStyleLabel = new Label();
            _caretStyleBox = new ComboBox();
            _caretSpecifyColor = new CheckBox();
            _caretColorLabel = new Label();
            _caretColorBox = new ColorButton();
            _caretBlink = new CheckBox();

            _colorFontGroup.SuspendLayout();
            _caretGroup.SuspendLayout();

            Controls.AddRange(new Control[] {
                                                                                       _caretGroup,
                                                                                       _colorFontGroup});
            // 
            // _colorFontGroup
            // 
            _colorFontGroup.Controls.AddRange(new Control[] {
                                                                                          _bgColorLabel,
                                                                                          _bgColorBox,
                                                                                          _textColorLabel,
                                                                                          _textColorBox,
                                                                                          _editColorEscapeSequence,
                                                                                          _fontLabel,
                                                                                          _fontDescription,
                                                                                          _fontSelectButton,
                                                                                          _fontSample,
                                                                                          _backgroundImageLabel,
                                                                                          _backgroundImageBox,
                                                                                          _backgroundImageSelectButton,
                                                                                          _imageStyleLabel,
                                                                                          _imageStyleBox,
                                                                                          _applyRenderProfileButton});
            _colorFontGroup.Location = new Point(9, 8);
            _colorFontGroup.Name = "_colorFontGroup";
            _colorFontGroup.FlatStyle = FlatStyle.System;
            _colorFontGroup.Size = new Size(416, 208);
            _colorFontGroup.TabIndex = 0;
            _colorFontGroup.TabStop = false;
            // 
            // _bgColorLabel
            // 
            _bgColorLabel.Location = new Point(16, 16);
            _bgColorLabel.Name = "_bgColorLabel";
            _bgColorLabel.Size = new Size(72, 24);
            _bgColorLabel.TabIndex = 1;
            _bgColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _bgColorBox
            // 
            //this._bgColorBox.BackColor = System.Drawing.Color.White;
            _bgColorBox.Location = new Point(120, 16);
            _bgColorBox.Name = "_bgColorBox";
            _bgColorBox.Size = new Size(152, 20);
            _bgColorBox.TabIndex = 2;
            _bgColorBox.ColorChanged += new ColorButton.NewColorEventHandler(OnBGColorChanged);
            // 
            // _textColorLabel
            // 
            _textColorLabel.Location = new Point(16, 40);
            _textColorLabel.Name = "_textColorLabel";
            _textColorLabel.Size = new Size(72, 24);
            _textColorLabel.TabIndex = 3;
            _textColorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _textColorBox
            // 
            _textColorBox.Location = new Point(120, 40);
            _textColorBox.Name = "_textColorBox";
            _textColorBox.Size = new Size(152, 20);
            _textColorBox.TabIndex = 4;
            _textColorBox.ColorChanged += new ColorButton.NewColorEventHandler(OnTextColorChanged);
            //
            // _editColorEscapeSequence
            //
            _editColorEscapeSequence.Location = new Point(120, 66);
            _editColorEscapeSequence.Size = new Size(224, 24);
            _editColorEscapeSequence.TabIndex = 5;
            _editColorEscapeSequence.Click += new EventHandler(OnEditColorEscapeSequence);
            _editColorEscapeSequence.FlatStyle = FlatStyle.System;
            // 
            // _fontLabel
            // 
            _fontLabel.Location = new Point(16, 96);
            _fontLabel.Name = "_fontLabel";
            _fontLabel.Size = new Size(72, 16);
            _fontLabel.TabIndex = 6;
            _fontLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _fontDescription
            // 
            _fontDescription.Location = new Point(120, 92);
            _fontDescription.Name = "_fontDescription";
            _fontDescription.Size = new Size(152, 24);
            _fontDescription.TabIndex = 7;
            _fontDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _fontSelectButton
            // 
            _fontSelectButton.Location = new Point(280, 92);
            _fontSelectButton.Name = "_fontSelectButton";
            _fontSelectButton.FlatStyle = FlatStyle.System;
            _fontSelectButton.Size = new Size(64, 23);
            _fontSelectButton.TabIndex = 8;
            _fontSelectButton.Click += new EventHandler(OnFontSelect);
            // 
            // _fontSample
            // 
            _fontSample.BackColor = Color.White;
            _fontSample.BorderStyle = BorderStyle.FixedSingle;
            _fontSample.ClearType = false;
            _fontSample.Location = new Point(280, 16);
            _fontSample.Name = "_fontSample";
            _fontSample.Size = new Size(120, 46);
            _fontSample.TabIndex = 9;
            _fontSample.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _backgroundImageLabel
            // 
            _backgroundImageLabel.Location = new Point(16, 128);
            _backgroundImageLabel.Name = "_backgroundImageLabel";
            _backgroundImageLabel.Size = new Size(72, 16);
            _backgroundImageLabel.TabIndex = 10;
            _backgroundImageLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _backgroundImageBox
            // 
            _backgroundImageBox.Location = new Point(120, 128);
            _backgroundImageBox.Name = "_backgroundImageBox";
            _backgroundImageBox.Size = new Size(260, 19);
            _backgroundImageBox.TabIndex = 11;
            // 
            // _backgroundImageSelectButton
            // 
            _backgroundImageSelectButton.Location = new Point(380, 128);
            _backgroundImageSelectButton.Name = "_backgroundImageSelectButton";
            _backgroundImageSelectButton.FlatStyle = FlatStyle.System;
            _backgroundImageSelectButton.Size = new Size(19, 19);
            _backgroundImageSelectButton.TabIndex = 12;
            _backgroundImageSelectButton.Text = "...";
            _backgroundImageSelectButton.Click += new EventHandler(OnSelectBackgroundImage);
            // 
            // _imageStyleLabel
            // 
            _imageStyleLabel.Location = new Point(16, 153);
            _imageStyleLabel.Name = "_imageStyleLabel";
            _imageStyleLabel.Size = new Size(96, 16);
            _imageStyleLabel.TabIndex = 13;
            _imageStyleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _imageStyleBox
            // 
            _imageStyleBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _imageStyleBox.Location = new Point(120, 152);
            _imageStyleBox.Name = "_imageStyleBox";
            _imageStyleBox.Size = new Size(112, 19);
            _imageStyleBox.TabIndex = 14;
            // 
            // _applyRenderProfileButton
            // 
            _applyRenderProfileButton.Location = new Point(280, 176);
            _applyRenderProfileButton.Name = "_applyRenderProfileButton";
            _applyRenderProfileButton.FlatStyle = FlatStyle.System;
            _applyRenderProfileButton.Size = new Size(120, 24);
            _applyRenderProfileButton.TabIndex = 15;
            _applyRenderProfileButton.Click += new EventHandler(OnApplyRenderProfile);
            // 
            // _caretGroup
            // 
            _caretGroup.Controls.AddRange(new Control[] {
                                                                                      _caretStyleLabel,
                                                                                      _caretStyleBox,
                                                                                      _caretSpecifyColor,
                                                                                      _caretColorBox,
                                                                                      _caretBlink});
            _caretGroup.Location = new Point(9, 220);
            _caretGroup.Name = "_caretGroup";
            _caretGroup.FlatStyle = FlatStyle.System;
            _caretGroup.Size = new Size(416, 72);
            _caretGroup.TabIndex = 16;
            _caretGroup.TabStop = false;
            // 
            // _caretStyleLabel
            // 
            _caretStyleLabel.Location = new Point(16, 16);
            _caretStyleLabel.Name = "_caretStyleLabel";
            _caretStyleLabel.Size = new Size(104, 23);
            _caretStyleLabel.TabIndex = 17;
            _caretStyleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _caretStyleBox
            // 
            _caretStyleBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _caretStyleBox.Items.AddRange(GetCaretStyleDescriptions());
            _caretStyleBox.Location = new Point(120, 16);
            _caretStyleBox.Name = "_caretStyle";
            _caretStyleBox.Size = new Size(152, 20);
            _caretStyleBox.TabIndex = 18;
            // 
            // _caretBlink
            // 
            _caretBlink.Location = new Point(296, 16);
            _caretBlink.Name = "_caretBlink";
            _caretBlink.Size = new Size(96, 20);
            _caretBlink.TabIndex = 19;
            _caretBlink.FlatStyle = FlatStyle.System;
            // 
            // _caretSpecifyColor
            // 
            _caretSpecifyColor.Location = new Point(16, 40);
            _caretSpecifyColor.Name = "_caretSpecifyColor";
            _caretSpecifyColor.Size = new Size(104, 20);
            _caretSpecifyColor.TabIndex = 20;
            _caretSpecifyColor.FlatStyle = FlatStyle.System;
            _caretSpecifyColor.CheckedChanged += new EventHandler(OnCaretSpecifyColorChanged);
            // 
            // _caretColorBox
            // 
            _caretColorBox.Location = new Point(120, 40);
            _caretColorBox.Name = "_caretColor";
            _caretColorBox.Size = new Size(152, 20);
            _caretColorBox.TabIndex = 21;
            _caretColorBox.Enabled = false;

            BackColor = ThemeUtil.TabPaneBackColor;
            _colorFontGroup.ResumeLayout();
            _caretGroup.ResumeLayout();
        }
        private void FillText()
        {
            _colorFontGroup.Text = "Color / Font / Background ";
            _bgColorLabel.Text = "Back Color";
            _textColorLabel.Text = "Text Color";
            _editColorEscapeSequence.Text = "Color settings for escape sequences...";
            _fontLabel.Text = "Font";
            _fontSample.Text = "Poderosa";
            _fontSelectButton.Text = "Change...";
            _backgroundImageLabel.Text = "Back Image";
            _imageStyleLabel.Text = "Image Location";
            _applyRenderProfileButton.Text = "Apply to All";
            _caretGroup.Text = "Caret";
            _caretStyleLabel.Text = "Style";
            _caretSpecifyColor.Text = "Specify Color";
            _caretColorLabel.Text = "Color";
            _caretBlink.Text = "Blink";

            _imageStyleBox.Items.AddRange(EnumDescAttributeT.For(typeof(ImageStyle)).DescriptionCollection());
        }

        public override void InitUI(ContainerOptions options)
        {
            AdjustFontDescription(options.Font, options.JapaneseFont);
            _fontSample.Font = options.Font;
            _fontSample.BackColor = options.BGColor;
            _fontSample.ForeColor = options.TextColor;
            _fontSample.ClearType = options.UseClearType;
            _fontSample.Invalidate(true);
            _backgroundImageBox.Text = options.BackgroundImageFileName;
            _imageStyleBox.SelectedIndex = (int)options.ImageStyle;
            _bgColorBox.SelectedColor = options.BGColor;
            _textColorBox.SelectedColor = options.TextColor;
            _caretStyleBox.SelectedIndex = CaretTypeToIndex(options.CaretType);
            _caretSpecifyColor.Checked = !options.CaretColor.IsEmpty;
            _caretBlink.Checked = (options.CaretType & CaretType.Blink) == CaretType.Blink;
            _caretColorBox.SelectedColor = options.CaretColor;

            _ESColorSet = options.ESColorSet;
            _defaultFileDir = options.DefaultFileDir;
            _useClearType = options.UseClearType;
            _font = options.Font;
            _japaneseFont = options.JapaneseFont;
        }
        public override bool Commit(ContainerOptions options)
        {
            string itemname = null;
            bool successful = false;
            try
            {
                if (_backgroundImageBox.Text.Length > 0)
                {
                    try
                    {
                        Image.FromFile(_backgroundImageBox.Text);
                    }
                    catch (Exception)
                    {
                        GUtil.Warning(this, $"{_backgroundImageBox.Text} is not a valid picture file.");
                        return false;
                    }
                }
                options.BackgroundImageFileName = _backgroundImageBox.Text;
                options.ImageStyle = (ImageStyle)_imageStyleBox.SelectedIndex;

                options.BGColor = _bgColorBox.SelectedColor;
                options.TextColor = _textColorBox.SelectedColor;
                options.Font = _fontSample.Font;

                options.CaretColor = _caretSpecifyColor.Checked ? _caretColorBox.SelectedColor : Color.Empty;
                options.CaretType = IndexToCaretType(_caretStyleBox.SelectedIndex) | (_caretBlink.Checked ? CaretType.Blink : CaretType.None);
                options.ESColorSet = _ESColorSet;
                options.DefaultFileDir = _defaultFileDir;
                options.UseClearType = _useClearType;
                options.Font = _font;
                options.JapaneseFont = _japaneseFont;
                successful = true;
            }
            catch (FormatException)
            {
                GUtil.Warning(this, $"The value of {itemname} is not valid.");
            }
            catch (InvalidOptionException ex)
            {
                GUtil.Warning(this, ex.Message);
            }

            return successful;
        }

        private void OnEditColorEscapeSequence(object sender, EventArgs args)
        {
            EditEscapeSequenceColor dlg = new EditEscapeSequenceColor(_fontSample.BackColor, _fontSample.ForeColor, _ESColorSet);
            if (GCUtil.ShowModalDialog(FindForm(), dlg) == DialogResult.OK)
            {
                _ESColorSet = dlg.Result;
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
            gd.SetFont(_useClearType, _font, _japaneseFont);
            DialogResult r = GCUtil.ShowModalDialog(FindForm(), gd);
            if (r == DialogResult.OK)
            {
                Font f = gd.ASCIIFont;
                _font = f;
                _japaneseFont = gd.JapaneseFont;
                _useClearType = gd.UseClearType;
                _fontSample.Font = f;
                AdjustFontDescription(f, gd.JapaneseFont);
            }
        }
        private void OnSelectBackgroundImage(object sender, EventArgs args)
        {
            string t = GCUtil.SelectPictureFileByDialog(FindForm());
            if (t != null)
            {
                _backgroundImageBox.Text = t;
                _defaultFileDir = GUtil.FileToDir(t);
            }
        }
        private void OnApplyRenderProfile(object sender, EventArgs args)
        {
            if (Commit(GApp.Options))
                GApp.GlobalCommandTarget.ResetAllRenderProfiles(new RenderProfile(GApp.Options));
        }

        private void AdjustFontDescription(Font ascii, Font japanese)
        {
            int sz = (int)(ascii.Size + 0.5);
            if (GEnv.Options.Language == Language.English || ascii.Name == japanese.Name)
                _fontDescription.Text = String.Format("{0},{1}pt", ascii.Name, sz); //Singleをintにキャストすると切り捨てだが、四捨五入にしてほしいので0.5を足してから切り捨てる
            else
                _fontDescription.Text = String.Format("{0}/{1},{2}pt", ascii.Name, japanese.Name, sz);
        }

        private void OnBGColorChanged(object sender, Color e)
        {
            _fontSample.BackColor = e;
        }
        private void OnTextColorChanged(object sender, Color e)
        {
            _fontSample.ForeColor = e;
        }
        private void OnCaretSpecifyColorChanged(object sender, EventArgs args)
        {
            _caretColorBox.Enabled = _caretSpecifyColor.Checked;
        }


        private static CaretType IndexToCaretType(int index)
        {
            switch (index)
            {
                case 0:
                    return CaretType.Box;
                case 1:
                    return CaretType.Line;
                case 2:
                    return CaretType.Underline;
                default:
                    Debug.Assert(false);
                    return CaretType.None;
            }
        }

        private static int CaretTypeToIndex(CaretType t)
        {
            int i = 0;
            if ((t & CaretType.StyleMask) == CaretType.Box) i = 0;
            else if ((t & CaretType.StyleMask) == CaretType.Line) i = 1;
            else if ((t & CaretType.StyleMask) == CaretType.Underline) i = 2;

            return i;
        }
        private static object[] GetCaretStyleDescriptions()
        {
            object[] r = new object[3];
            r[0] = "Box";
            r[1] = "Bar";
            r[2] = "Underline";
            return r;
        }
    }
}
