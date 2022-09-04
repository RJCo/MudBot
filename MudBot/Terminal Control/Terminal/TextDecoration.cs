/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: TextDecoration.cs,v 1.2 2005/04/20 08:45:48 okajima Exp $
*/
using System;
using System.Drawing;
using System.Text;

namespace Poderosa.Text
{
    //TextDecorationで色を指定するのか、外部で定義された色を使うのかの区別につかう。ColorのAプロパティの値で代用すればちょっと効率は上がりそうだが...
    internal enum ColorType
    {
        DefaultBack,
        DefaultText,
        Custom
    }

    internal class TextDecoration : ICloneable
    {
        private Color _bgColor;
        private Color _textColor;

        private static TextDecoration _default;
        public static TextDecoration Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new TextDecoration(false, false);
                }
                return _default;
            }
        }
        public static TextDecoration ClonedDefault()
        {
            return new TextDecoration(false, false);
        }

        public object Clone()
        {
            TextDecoration t = new TextDecoration
            {
                BackColorType = BackColorType,
                _bgColor = _bgColor,
                TextColorType = TextColorType,
                _textColor = _textColor,
                Bold = Bold,
                Underline = Underline
            };
            return t;
        }
        private TextDecoration() { } //Clone()から使うためのコンストラクタ

        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                TextColorType = value == Color.Empty ? ColorType.DefaultText : ColorType.Custom;
            }
        }
        public Color BackColor
        {
            get => _bgColor;
            set
            {
                _bgColor = value;
                BackColorType = value == Color.Empty ? ColorType.DefaultBack : ColorType.Custom;
            }
        }
        public ColorType TextColorType { get; set; }

        public ColorType BackColorType { get; set; }

        public bool Bold { get; set; }

        public bool Underline { get; set; }

        public bool IsDefault => !Underline && !Bold && BackColorType == ColorType.DefaultBack && TextColorType == ColorType.DefaultText;

        public void Inverse()
        {
            Color tmp = _textColor;
            _textColor = _bgColor;
            _bgColor = tmp;

            ColorType tmp2 = TextColorType;
            TextColorType = BackColorType;
            BackColorType = tmp2;
        }
        public void ToCaretStyle()
        {
            Inverse();
            if (GEnv.Options.CaretColor != Color.Empty)
            {
                BackColorType = ColorType.Custom;
                _bgColor = GEnv.Options.CaretColor;
            }
        }

        public TextDecoration(bool underline, bool bold)
        {
            BackColorType = ColorType.DefaultBack;
            TextColorType = ColorType.DefaultText;
            Bold = bold;
            Underline = underline;
        }
        public TextDecoration(Color bg, Color txt, bool underline, bool bold)
        {
            Init(bg, txt, underline, bold);
        }
        public void Init(Color bg, Color txt, bool underline, bool bold)
        {
            _bgColor = bg;
            BackColorType = ColorType.Custom;
            _textColor = txt;
            TextColorType = ColorType.Custom;
            Bold = bold;
            Underline = underline;
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            b.Append(_bgColor.ToString()); //これでまっとうな文字列が出るのか?
            b.Append('/');
            b.Append(_textColor.ToString());
            b.Append('/');
            if (Bold)
            {
                b.Append('B');
            }

            return b.ToString();
        }

    }

    internal class FontHandle
    {
        private IntPtr _hFont;

        public FontHandle(Font f)
        {
            Font = f;
        }
        public Font Font { get; }

        public IntPtr HFONT
        {
            get
            {
                if (_hFont == IntPtr.Zero)
                {
                    _hFont = Font.ToHfont();
                }

                return _hFont;
            }
        }
    }
}
