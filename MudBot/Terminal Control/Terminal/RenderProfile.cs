/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: RenderProfile.cs,v 1.2 2005/04/20 08:45:47 okajima Exp $
*/
using System;
using System.Drawing;
using System.Text;
using System.Xml;

using Poderosa.Toolkit;
using Poderosa.ConnectionParam;
using Poderosa.Config;
using Poderosa.Text;

namespace Poderosa.Terminal
{

    /// <summary>
    /// <en>Implements the parameters for displaying the console. By setting this object to the RenderProfile property of the TerminalParam object, the macro can control colors, fonts, and background images.</en>
    /// </summary>
    [Serializable]
    public class RenderProfile : ICloneable
    {

        private string _fontName;
        private string _japaneseFontName;
        private float _fontSize;
        private bool _useClearType;
        private FontHandle _font;
        private FontHandle _boldfont;
        private FontHandle _underlinefont;
        private FontHandle _boldunderlinefont;
        private FontHandle _japaneseFont;
        private FontHandle _japaneseBoldfont;
        private FontHandle _japaneseUnderlinefont;
        private FontHandle _japaneseBoldunderlinefont;
        private EscapesequenceColorSet _esColorSet;
        private Color _forecolor;
        private Color _bgcolor;

        private Brush _brush;
        private Brush _bgbrush;

        private string _backgroundImageFileName;
        private Image _backgroundImage;
        private bool _imageLoadIsAttempted;
        private ImageStyle _imageStyle;

        private SizeF _pitch;
        private float _chargap; //文字列を表示するときに左右につく余白

        /// <summary>
        /// <en>Gets or sets the font name for normal characters.</en>
        /// </summary>
        public string FontName
        {
            get
            {
                return _fontName;
            }
            set
            {
                _fontName = value;
                ClearFont();
            }
        }
        /// <summary>
        /// <en>Gets or sets the font name for Japanese characters.</en>
        /// </summary>
        public string JapaneseFontName
        {
            get
            {
                return _japaneseFontName;
            }
            set
            {
                _japaneseFontName = value;
                ClearFont();
            }
        }
        /// <summary>
        /// <en>Gets or sets the font size.</en>
        /// </summary>
        public float FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                ClearFont();
            }
        }
        /// <summary>
        /// <en>If this property is true, the characters are drew by the ClearType when the font and the OS supports it.</en>
        /// </summary>
        public bool UseClearType
        {
            get => _useClearType;
            set => _useClearType = value;
        }

        /// <summary>
        /// <en>Gets or sets the color of characters.</en>
        /// </summary>
        public Color ForeColor
        {
            get
            {
                return _forecolor;
            }
            set
            {
                _forecolor = value;
                ClearBrush();
            }
        }
        /// <summary>
        /// <en>Because JScript cannot handle the Color structure, please use this method instead of the ForeColor property.</en>
        /// </summary>
        public void SetForeColor(object value)
        {
            _forecolor = (Color)value;
            ClearBrush();
        }

        /// <summary>
        /// <en>Gets or sets the background color.</en>
        /// </summary>
        public Color BackColor
        {
            get
            {
                return _bgcolor;
            }
            set
            {
                _bgcolor = value;
                ClearBrush();
            }
        }
        /// <summary>
        /// <en>Because JScript cannot handle the Color structure, please use this method instead of the BackColor property.</en>
        /// </summary>
        public void SetBackColor(object value)
        {
            _bgcolor = (Color)value;
            ClearBrush();
        }

        /// <summary>
        /// <en>Gets or set the file name of the background image.</en>
        /// </summary>
        public string BackgroundImageFileName
        {
            get => _backgroundImageFileName;
            set
            {
                _backgroundImageFileName = value;
                _backgroundImage = null;
            }
        }
        /// <summary>
        /// <en>Gets or sets the position of the background image.</en>
        /// </summary>
        public ImageStyle ImageStyle
        {
            get => _imageStyle;
            set => _imageStyle = value;
        }

        public EscapesequenceColorSet ESColorSet
        {
            get => _esColorSet;
            set => _esColorSet = value;
        }

        /// <summary>
        /// <en>Initializes with default values the user sets in the option dialog.</en>
        /// </summary>
        public RenderProfile()
        {
            Init(GEnv.Options);
        }

        /// <summary>
        /// <en>Initializes with another instance.</en>
        /// </summary>
        public RenderProfile(RenderProfile src)
        {
            _fontName = src._fontName;
            _japaneseFontName = src._japaneseFontName;
            _fontSize = src._fontSize;
            _useClearType = src._useClearType;
            _japaneseFont = _font = null;

            _forecolor = src._forecolor;
            _bgcolor = src._bgcolor;
            _esColorSet = (EscapesequenceColorSet)src._esColorSet.Clone();
            _bgbrush = _brush = null;

            _backgroundImageFileName = src._backgroundImageFileName;
            _imageLoadIsAttempted = false;
            _imageStyle = src.ImageStyle;
        }

        public object Clone()
        {
            return new RenderProfile(this);
        }

        public RenderProfile(CommonOptions opt)
        {
            Init(opt);
        }

        private void Init(CommonOptions opt)
        {
            //起動の高速化のため、フォントの作成は遅延評価
            _fontName = opt.FontName;
            _japaneseFontName = opt.JapaneseFontName;
            _fontSize = opt.FontSize;
            _useClearType = opt.UseClearType;
            _japaneseFont = _font = null;
            _esColorSet = (EscapesequenceColorSet)opt.ESColorSet.Clone();

            _forecolor = opt.TextColor;
            _bgcolor = opt.BGColor;
            _bgbrush = _brush = null;

            _backgroundImageFileName = opt.BackgroundImageFileName;
            _imageLoadIsAttempted = false;
            _imageStyle = opt.ImageStyle;
        }
        public RenderProfile(ConfigNode data)
        {
            Import(data);
        }


        public void Import(ConfigNode data)
        {
            CommonOptions opt = GEnv.Options;
            _fontName = data["font-name"];
            if (_fontName == null)
            {
                _fontName = opt.FontName;
            }

            _japaneseFontName = data["japanese-font-name"];
            if (_japaneseFontName == null)
            {
                _japaneseFontName = opt.JapaneseFontName;
            }

            _fontSize = GUtil.ParseInt(data["font-size"], 10);
            _useClearType = GUtil.ParseBool(data["clear-type"], false);
            ClearFont();

            unchecked
            {
                _forecolor = Color.FromArgb(GUtil.ParseHexInt(data["fore-color"], (int)0xFF000000));
                _bgcolor = Color.FromArgb(GUtil.ParseHexInt(data["back-color"], (int)0xFFFFFFFF));
            }
            if (_esColorSet == null)
            {
                _esColorSet = (EscapesequenceColorSet)opt.ESColorSet.Clone();
            }

            _esColorSet.Load(data["color-sequence"]);
            ClearBrush();

            _backgroundImageFileName = data["image-file"];
            _imageLoadIsAttempted = false;
            _imageStyle = (ImageStyle)EnumDescAttribute.For(typeof(ImageStyle)).FromName(data["bg-style"], ImageStyle.Center);
        }
        public void Export(XmlWriter writer)
        {
            writer.WriteAttributeString("font-name", _fontName);
            writer.WriteAttributeString("japanese-font-name", _japaneseFontName);
            writer.WriteAttributeString("font-size", _fontSize.ToString());
            writer.WriteAttributeString("clear-type", _useClearType.ToString());
            writer.WriteAttributeString("fore-color", _forecolor.ToArgb().ToString("X"));
            writer.WriteAttributeString("back-color", _bgcolor.ToArgb().ToString("X"));
            writer.WriteAttributeString("image-file", _backgroundImageFileName);
            writer.WriteAttributeString("bg-style", _imageStyle.ToString());
            if (!_esColorSet.IsDefault)
            {
                writer.WriteAttributeString("color-sequence", _esColorSet.Format());
            }
        }
        public void Export(ConfigNode node)
        {
            node["font-name"] = _fontName;
            node["japanese-font-name"] = _japaneseFontName;
            node["font-size"] = _fontSize.ToString();
            node["clear-type"] = _useClearType.ToString();
            node["fore-color"] = _forecolor.ToArgb().ToString("X");
            node["back-color"] = _bgcolor.ToArgb().ToString("X");
            node["image-file"] = _backgroundImageFileName;
            node["bg-style"] = _imageStyle.ToString();
            if (!_esColorSet.IsDefault)
            {
                node["color-sequence"] = _esColorSet.Format();
            }
        }

        private void ClearFont()
        {
            _font = null;
            _boldfont = null;
            _underlinefont = null;
            _boldunderlinefont = null;
            _japaneseFont = null;
            _japaneseBoldfont = null;
            _japaneseUnderlinefont = null;
            _japaneseBoldunderlinefont = null;
        }
        private void ClearBrush()
        {
            _brush = null;
            _bgbrush = null;
        }

        private void CreateFonts()
        {
            _font = new FontHandle(GUtil.CreateFont(_fontName, _fontSize));
            FontStyle fs = _font.Font.Style;
            _boldfont = new FontHandle(new Font(_font.Font, fs | FontStyle.Bold));
            _underlinefont = new FontHandle(new Font(_font.Font, fs | FontStyle.Underline));
            _boldunderlinefont = new FontHandle(new Font(_font.Font, fs | FontStyle.Underline | FontStyle.Bold));

            _japaneseFont = new FontHandle(new Font(_japaneseFontName, _fontSize));
            fs = _japaneseFont.Font.Style;
            _japaneseBoldfont = new FontHandle(new Font(_japaneseFont.Font, fs | FontStyle.Bold));
            _japaneseUnderlinefont = new FontHandle(new Font(_japaneseFont.Font, fs | FontStyle.Underline));
            _japaneseBoldunderlinefont = new FontHandle(new Font(_japaneseFont.Font, fs | FontStyle.Underline | FontStyle.Bold));

            UsingIdenticalFont = (_font.Font.Name == _japaneseFont.Font.Name);

            //通常版
            Graphics g = Graphics.FromHwnd(Win32.GetDesktopWindow());

            IntPtr hdc = g.GetHdc();
            Win32.SelectObject(hdc, _font.HFONT);
            Win32.SIZE charsize1, charsize2;
            Win32.GetTextExtentPoint32(hdc, "A", 1, out charsize1);
            Win32.GetTextExtentPoint32(hdc, "AAA", 3, out charsize2);

            _pitch = new SizeF((charsize2.width - charsize1.width) / 2, charsize1.height);
            _chargap = (charsize1.width - _pitch.Width) / 2;
            g.ReleaseHdc(hdc);
            g.Dispose();
        }
        private void CreateBrushes()
        {
            _brush = new SolidBrush(_forecolor);
            _bgbrush = new SolidBrush(_bgcolor);
        }

        public Brush Brush
        {
            get
            {
                if (_brush == null)
                {
                    CreateBrushes();
                }

                return _brush;
            }
        }
        public Brush BgBrush
        {
            get
            {
                if (_bgbrush == null)
                {
                    CreateBrushes();
                }

                return _bgbrush;
            }
        }
        public SizeF Pitch
        {
            get
            {
                if (_font == null)
                {
                    CreateFonts();
                }

                return _pitch;
            }
        }
        public Image GetImage()
        {
            try
            {
                if (!_imageLoadIsAttempted)
                {
                    _imageLoadIsAttempted = true;
                    _backgroundImage = null;
                    if (_backgroundImageFileName != null && _backgroundImageFileName.Length > 0)
                    {
                        _backgroundImage = Image.FromFile(_backgroundImageFileName);
                    }
                }

                return _backgroundImage;
            }
            catch (Exception)
            {
                GUtil.Warning(GEnv.Frame, String.Format("Message.RenderProfile.FailedToLoadPicture", _backgroundImageFileName));
                return null;
            }
        }

        public float CharGap
        {
            get
            {
                if (_font == null)
                {
                    CreateFonts();
                }

                return _chargap;
            }
        }
        public bool UsingIdenticalFont { get; private set; }

        internal Brush CalcTextBrush(TextDecoration dec)
        {
            if (_brush == null)
            {
                CreateBrushes();
            }

            if (dec == null)
            {
                return _brush;
            }

            switch (dec.TextColorType)
            {
                case ColorType.Custom:
                    return new SolidBrush(dec.TextColor);
                case ColorType.DefaultBack:
                    return _bgbrush;
                case ColorType.DefaultText:
                    return _brush;
                default:
                    throw new Exception("Unexpected decoration object");
            }
        }
        internal Color CalcTextColor(TextDecoration dec)
        {
            if (_brush == null)
            {
                CreateBrushes();
            }

            if (dec == null)
            {
                return _forecolor;
            }

            switch (dec.TextColorType)
            {
                case ColorType.Custom:
                    return dec.TextColor;
                case ColorType.DefaultBack:
                    return _bgcolor;
                case ColorType.DefaultText:
                    return _forecolor;
                default:
                    throw new Exception("Unexpected decoration object");
            }
        }
        internal Brush CalcBackBrush(TextDecoration dec)
        {
            if (_brush == null)
            {
                CreateBrushes();
            }

            if (dec == null)
            {
                return _bgbrush;
            }

            switch (dec.BackColorType)
            {
                case ColorType.Custom:
                    return new SolidBrush(dec.BackColor);
                case ColorType.DefaultBack:
                    return _bgbrush;
                case ColorType.DefaultText:
                    return _brush;
                default:
                    throw new Exception("Unexpected decoration object");
            }
        }
        internal Color CalcBackColor(TextDecoration dec)
        {
            if (_brush == null)
            {
                CreateBrushes();
            }

            if (dec == null)
            {
                return _bgcolor;
            }

            switch (dec.BackColorType)
            {
                case ColorType.Custom:
                    return dec.BackColor;
                case ColorType.DefaultBack:
                    return _bgcolor;
                case ColorType.DefaultText:
                    return _forecolor;
                default:
                    throw new Exception("Unexpected decoration object");
            }
        }
        internal Font CalcFont(TextDecoration dec, CharGroup cg)
        {
            return CalcFontInternal(dec, cg, false).Font;
        }
        internal IntPtr CalcHFONT_NoUnderline(TextDecoration dec, CharGroup cg)
        {
            return CalcFontInternal(dec, cg, true).HFONT;
        }

        private FontHandle CalcFontInternal(TextDecoration dec, CharGroup cg, bool ignore_underline)
        {
            if (_font == null)
            {
                CreateFonts();
            }

            if (cg == CharGroup.TwoBytes)
            {
                if (dec == null)
                {
                    return _japaneseFont;
                }

                if (dec.Bold)
                {
                    if (!ignore_underline && dec.Underline)
                    {
                        return _japaneseBoldunderlinefont;
                    }
                    else
                    {
                        return _japaneseBoldfont;
                    }
                }
                else if (!ignore_underline && dec.Underline)
                {
                    return _japaneseUnderlinefont;
                }
                else
                {
                    return _japaneseFont;
                }
            }
            else
            {
                if (dec == null)
                {
                    return _font;
                }

                if (dec.Bold)
                {
                    if (!ignore_underline && dec.Underline)
                    {
                        return _boldunderlinefont;
                    }
                    else
                    {
                        return _boldfont;
                    }
                }
                else if (!ignore_underline && dec.Underline)
                {
                    return _underlinefont;
                }
                else
                {
                    return _font;
                }
            }
        }
    }

    //Escape sequence color
    public class EscapesequenceColorSet : ICloneable
    {
        private Color[] _colors;

        public EscapesequenceColorSet()
        {
            _colors = new Color[8];
            SetDefault();
        }

        public bool IsDefault { get; private set; }

        public object Clone()
        {
            EscapesequenceColorSet newval = new EscapesequenceColorSet();
            for (int i = 0; i < _colors.Length; i++)
            {
                newval._colors[i] = _colors[i];
            }

            newval.IsDefault = IsDefault;
            return newval;
        }

        public Color this[int index]
        {
            get => _colors[index];
            set
            {
                _colors[index] = value;
                if (IsDefault)
                {
                    IsDefault = GetDefaultColor(index) == value;
                }
            }
        }

        public void SetDefault()
        {
            for (int i = 0; i < _colors.Length; i++)
            {
                _colors[i] = GetDefaultColor(i);
            }
            IsDefault = true;
        }
        public string Format()
        {
            if (IsDefault)
            {
                return "";
            }

            StringBuilder bld = new StringBuilder();
            for (int i = 0; i < _colors.Length; i++)
            {
                if (i > 0)
                {
                    bld.Append(',');
                }

                bld.Append(_colors[i].Name);
            }
            return bld.ToString();
        }
        public void Load(string value)
        {
            if (value == null)
            {
                SetDefault();
            }
            else
            {
                string[] cols = value.Split(',');
                if (cols.Length < _colors.Length)
                {
                    SetDefault();
                }
                else
                {
                    for (int i = 0; i < cols.Length; i++)
                    {
                        _colors[i] = GUtil.ParseColor(cols[i], Color.Empty);
                        if (_colors[i].IsEmpty)
                        {
                            _colors[i] = GetDefaultColor(i);
                        }
                    }
                    IsDefault = false;
                }
            }
        }

        public static Color GetDefaultColor(int index)
        {
            switch (index)
            {
                case 0:
                    return Color.Black;
                case 1:
                    return Color.Red;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Yellow;
                case 4:
                    return Color.Blue;
                case 5:
                    return Color.Magenta;
                case 6:
                    return Color.Cyan;
                case 7:
                    return Color.White;
                default:
                    return Color.Empty;
            }
        }
    }
}
