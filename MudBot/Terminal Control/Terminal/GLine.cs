/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: GLine.cs,v 1.3 2005/04/27 08:48:50 okajima Exp $
*/
using Poderosa.Terminal;
using Poderosa.UI;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace Poderosa.Text
{
    internal class RenderParameter
    {
        public int LineFrom { get; set; }

        public int LineCount { get; set; }

        public Rectangle TargetRect { get; set; }
    }

    /// <summary>
    /// �s���̎��
    /// </summary>
    public enum EOLType
    {
        Continue,
        CRLF,
        CR,
        LF
    }

    public enum CharGroup
    {
        SingleByte, //unicode��0x100�����̕���
        TwoBytes    //0x100�ȏ�̕���
    }

    /// <summary>
    /// GLine�̍\���v�f�BGWord��TextDecoration�Ȃǂ����L����B
    /// </summary>
    public class GWord
    {
        //���΂��ΎQ�Ƃ���̂ŃL���b�V������l
        internal int nextOffsetCache;
        internal int displayLengthCache;

        /// <summary>
        /// �\���p�̑���
        /// </summary>
        internal TextDecoration Decoration { get; }

        /// <summary>
        /// ��������GLine�̒��ŉ������ڂ���n�܂��Ă��邩
        /// </summary>
        public int Offset { get; }

        ///����Word
        public GWord Next { get; set; }

        public CharGroup CharGroup { get; set; }

        /// <summary>
        /// ������A�f�R���[�V�����A�I�t�Z�b�g���w�肷��R���X�g���N�^�BType��Normal�ɂȂ�B
        /// </summary>
        internal GWord(TextDecoration d, int o, CharGroup chargroup)
        {
            Debug.Assert(d != null);
            Offset = o;
            Decoration = d;
            Next = null;
            CharGroup = chargroup;
            nextOffsetCache = -1;
            displayLengthCache = -1;
        }

        //Next�̒l�ȊO���R�s�[����
        public GWord StandAloneClone()
        {
            return new GWord(Decoration, Offset, CharGroup);
        }

        public GWord DeepClone()
        {
            GWord w = new GWord(Decoration, Offset, CharGroup);
            if (Next != null)
            {
                w.Next = Next.DeepClone();
            }

            return w;
        }

    }


    /// <summary>
    /// �P�s�̃f�[�^
    /// GWord�ւ̕����͒x���]�������B
    /// </summary>
    public class GLine
    {
        static GLine()
        {
            InitLengthMap();
        }

        public const char WIDECHAR_PAD = '\uFFFF';

        public GLine(int length)
        {
            Debug.Assert(length > 0);
            Text = new char[length];
            FirstWord = new GWord(TextDecoration.ClonedDefault(), 0, CharGroup.SingleByte);
            ID = -1;
        }
        public GLine(char[] data, GWord firstWord)
        {
            Text = (char[])data.Clone();
            FirstWord = firstWord;
            ID = -1;
        }
        public GLine Clone()
        {
            GLine nl = new GLine(Text, FirstWord.DeepClone())
            {
                EOLType = EOLType,
                ID = ID
            };
            return nl;
        }


        public void Clear()
        {
            for (int i = 0; i < Text.Length; i++)
            {
                Text[i] = '\0';
            }

            FirstWord = new GWord(TextDecoration.ClonedDefault(), 0, CharGroup.SingleByte);
        }
        internal void Clear(TextDecoration dec)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                Text[i] = ' ';
            }

            FirstWord = new GWord(dec, 0, CharGroup.SingleByte);
        }
        public int Length => Text.Length;

        /// <summary>
        /// �C���f�N�X���w�肵��GWord��Ԃ��B�����_�����O�ς݂��ǂ����͍l�����Ă��Ȃ��B
        /// </summary>
        public GWord FirstWord { get; private set; }

        public char[] Text { get; private set; }

        public int DisplayLength
        {
            get
            {
                int i = 0;
                int m = Text.Length;
                for (i = 0; i < m; i++)
                {
                    if (Text[i] == '\0')
                    {
                        break;
                    }
                }
                return i;
            }
        }
        public int CharLength
        {
            get
            {
                int n = Text.Length - 1;
                while (n >= 0 && Text[n] == '\0')
                {
                    n--;
                }

                return n + 1;
            }
        }

        //�O��̒P���؂��������B�Ԃ��ʒu�́Apos��GetWordBreakGroup�̒l����v���钆�ŉ����n�_
        public int FindPrevWordBreak(int pos)
        {
            int v = ToCharGroupForWordBreak(Text[pos]);
            while (pos >= 0)
            {
                if (v != ToCharGroupForWordBreak(Text[pos]))
                {
                    return pos;
                }

                pos--;
            }
            return -1;
        }
        public int FindNextWordBreak(int pos)
        {
            int v = ToCharGroupForWordBreak(Text[pos]);
            while (pos < Text.Length)
            {
                if (v != ToCharGroupForWordBreak(Text[pos]))
                {
                    return pos;
                }

                pos++;
            }
            return Text.Length;
        }
        private static int ToCharGroupForWordBreak(char ch)
        {
            if (('0' <= ch && ch <= '9') || ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z') || ch == '_' || GEnv.Options.AdditionalWordElement.IndexOf(ch) != -1)
            {
                return 1;
            }
            else if (ch <= 0x20 || ch == 0x40)
            {
                return 2;
            }
            else if (ch <= 0x100)
            {
                return 3;
            }
            else //����ɂ�����UnicodeCategory�����݂ēK���ɂ����炦�邱�Ƃ��ł��邪
            {
                return 4;
            }
        }


        public int ID { get; set; }

        //�אڍs�̐ݒ�@���̕ύX��TerminalDocument����̂ݍs�����ƁI
        public GLine NextLine { get; set; }

        public GLine PrevLine { get; set; }

        public EOLType EOLType { get; set; }

        internal void ExpandBuffer(int length)
        {
            if (length <= Text.Length)
            {
                return;
            }

            char[] current = Text;
            Text = new char[length];
            Array.Copy(current, 0, Text, 0, current.Length);
        }

        internal void Render(IntPtr hdc, RenderParameter param, RenderProfile prof, int y)
        {
            if (Text[0] == '\0')
            {
                return; //�����`���Ȃ��Ă悢
            }

            float fx = param.TargetRect.Left;

            RectangleF rect = new RectangleF
            {
                Y = param.TargetRect.Top + y,
                Height = prof.Pitch.Height
            };

            GWord word = FirstWord;
            while (word != null)
            {

                rect.X = fx /*- prof.CharGap*/; //Native�ȕ`��ł͕s�v�H
                rect.Width = param.TargetRect.Right - rect.X;
                int ix = (int)rect.X;
                int iy = (int)rect.Y;

                TextDecoration dec = word.Decoration;

                //Brush brush = prof.CalcTextBrush(dec);
                uint forecolorref = DrawUtil.ToCOLORREF(prof.CalcTextColor(dec));
                Color bkcolor = prof.CalcBackColor(dec);
                uint bkcolorref = DrawUtil.ToCOLORREF(bkcolor);
                IntPtr hfont = prof.CalcHFONT_NoUnderline(dec, word.CharGroup);
                Win32.SelectObject(hdc, hfont);
                Win32.SetTextColor(hdc, forecolorref);
                Win32.SetBkColor(hdc, bkcolorref);
                Win32.SetBkMode(hdc, bkcolor == prof.BackColor ? 1 : 2); //��{�w�i�F�ƈꏏ�Ȃ�TRANSPARENT, �قȂ��OPAQUE
                IntPtr bkbrush = bkcolor == prof.BackColor ? IntPtr.Zero : Win32.CreateSolidBrush(bkcolorref);

                int display_length = WordDisplayLength(word);
                if (word.Decoration == null)
                { //�����Ȃ�
                  //g.DrawString(WordText(word), font, brush, rect);
                    DrawWord(hdc, ix, iy, word);
                }
                else
                {
                    //if(dec.Bold || (!prof.UsingIdenticalFont && word.CharGroup==CharGroup.TwoBytes))
                    if (dec.Bold || word.CharGroup == CharGroup.TwoBytes) //�����t�H���g�w��ł����{�ꂪ���p�̂Q�{�łȂ��ꍇ����B�p�t�H�[�}���X���̓N���A�������̂Ŋm���ɂP�������`��
                    {
                        DrawStringByOneChar2(hdc, word, display_length, bkbrush, rect.X, iy, prof);
                    }
                    else
                    {
                        DrawWord(hdc, ix, iy, word); //���܂�A�z�ȕ`��G���W���̖�肩��͉�����ꂽ�I
                    }
                }
                //Debug.WriteLine("PW="+p.Pitch.Width+",TL="+(pb.Text.Length*p.Pitch.Width)+", real="+g.MeasureString(pb.Text, p.Font).Width);
                if (dec.Underline)
                {
                    DrawUnderline(hdc, forecolorref, ix, iy + (int)prof.Pitch.Height - 1, (int)(prof.Pitch.Width * display_length));
                }

                fx += prof.Pitch.Width * display_length;
                word = word.Next;
                if (bkbrush != IntPtr.Zero)
                {
                    Win32.DeleteObject(bkbrush);
                }
            }
        }

        private void DrawUnderline(IntPtr hdc, uint col, int x, int y, int length)
        {
            //Underline�������Ƃ͂��܂�Ȃ����낤���疈��Pen�����B���ɂȂ肻���������炻�̂Ƃ��ɍl���悤
            IntPtr pen = Win32.CreatePen(0, 1, col);
            IntPtr prev = Win32.SelectObject(hdc, pen);
            Win32.POINT pt = new Win32.POINT();
            Win32.MoveToEx(hdc, x, y, out pt);
            Win32.LineTo(hdc, x + length, y);
            Win32.SelectObject(hdc, prev);
            Win32.DeleteObject(pen);
        }

        private void DrawWord(IntPtr hdc, int x, int y, GWord word)
        {
            unsafe
            {
                int len;

                if (word.CharGroup == CharGroup.SingleByte)
                {
                    fixed (char* p = &Text[0])
                    {
                        len = WordNextOffset(word) - word.Offset;
                        Win32.TextOut(hdc, x, y, p + word.Offset, len);
                        //Win32.ExtTextOut(hdc, x, y, 4, null, p+word.Offset, len, null);
                    }
                }
                else
                {
                    string t = WordText(word);
                    fixed (char* p = t)
                    {
                        len = t.Length;
                        Win32.TextOut(hdc, x, y, p, len);
                        //Win32.ExtTextOut(hdc, x, y, 4, null, p, len, null);
                    }
                }

            }
        }
        private void DrawStringByOneChar2(IntPtr hdc, GWord word, int display_length, IntPtr bkbrush, float fx, int y, RenderProfile prof)
        {
            float pitch = prof.Pitch.Width;
            int nextoffset = WordNextOffset(word);
            if (bkbrush != IntPtr.Zero)
            { //���ꂪ�Ȃ��Ɠ��{�ꕶ���s�b�`���������Ƃ��I�����̂����܂��ł���ꍇ������
                Win32.RECT rect = new Win32.RECT
                {
                    left = (int)fx,
                    top = y,
                    right = (int)(fx + pitch * display_length),
                    bottom = y + (int)prof.Pitch.Height
                };
                Win32.FillRect(hdc, ref rect, bkbrush);
            }
            for (int i = word.Offset; i < nextoffset; i++)
            {
                char ch = Text[i];
                if (ch == '\0')
                {
                    break;
                }

                if (ch == WIDECHAR_PAD)
                {
                    continue;
                }

                unsafe
                {
                    Win32.TextOut(hdc, (int)fx, y, &ch, 1);
                }
                fx += pitch * CalcDisplayLength(ch);
            }
        }

        /*
		 * //!!.NET�̕`��o�O
		 * Graphics#DrawString�ɓn��������́A�X�y�[�X�݂̂ō\������Ă���Ɩ�������Ă��܂��悤���B
		 * ���ꂾ�ƃA���_�[���C�����������������Ƃ��Ȃǂɍ���B���ׂ��Ƃ���A�����Ƀ^�u������Ƃ��̎d�g�݂����܂����Ƃ��ł��邱�Ƃ�����
		 * .NET�̎��̃o�[�W�����ł͒����Ă��邱�Ƃ�����
		 */
        private string WordTextForFuckingDotNet(GWord word)
        {
            int nextoffset = WordNextOffset(word);
            if (nextoffset == 0)
            {
                return "";
            }
            else
            {
                bool last_is_space = false;
                Debug.Assert(nextoffset - word.Offset >= 0);
                if (word.CharGroup == CharGroup.SingleByte)
                {
                    last_is_space = Text[nextoffset - 1] == ' ';
                    if (last_is_space)
                    {
                        return new string(Text, word.Offset, nextoffset - word.Offset) + '\t';
                    }
                    else
                    {
                        return new string(Text, word.Offset, nextoffset - word.Offset);
                    }
                }
                else
                {
                    char[] buf = new char[256];
                    int o = word.Offset, i = 0;
                    while (o < nextoffset)
                    {
                        char ch = Text[o];
                        if (ch != WIDECHAR_PAD)
                        {
                            last_is_space = ch == ' ';
                            buf[i++] = ch;
                        }
                        o++;
                    }

                    if (last_is_space)
                    {
                        buf[i++] = '\t';
                    }

                    return new string(buf, 0, i);
                }
            }
        }

        private string WordText(GWord word)
        {
            int nextoffset = WordNextOffset(word);
            if (nextoffset == 0)
            {
                return "";
            }
            else
            {
                Debug.Assert(nextoffset - word.Offset >= 0);
                if (word.CharGroup == CharGroup.SingleByte)
                {
                    return new string(Text, word.Offset, nextoffset - word.Offset);
                }
                else
                {
                    char[] buf = new char[256];
                    int o = word.Offset, i = 0;
                    while (o < nextoffset)
                    {
                        char ch = Text[o];
                        if (ch != WIDECHAR_PAD)
                        {
                            buf[i++] = ch;
                        }

                        o++;
                    }
                    return new string(buf, 0, i);
                }
            }
        }
        private int WordDisplayLength(GWord word)
        {
            //�����͌Ă΂�邱�Ƃ��ƂĂ������̂ŃL���b�V����݂���
            int cache = word.displayLengthCache;
            if (cache < 0)
            {
                int nextoffset = WordNextOffset(word);
                int l = nextoffset - word.Offset;
                word.displayLengthCache = l;
                return l;
            }
            else
            {
                return cache;
            }
        }

        internal int WordNextOffset(GWord word)
        {
            //�����͌Ă΂�邱�Ƃ��ƂĂ������̂ŃL���b�V����݂���
            int cache = word.nextOffsetCache;
            if (cache < 0)
            {
                if (word.Next == null)
                {
                    int i = Text.Length - 1;
                    while (i >= 0 && Text[i] == '\0')
                    {
                        i--;
                    }

                    word.nextOffsetCache = i + 1;
                    return i + 1;
                }
                else
                {
                    word.nextOffsetCache = word.Next.Offset;
                    return word.Next.Offset;
                }
            }
            else
            {
                return cache;
            }
        }
        internal void Append(GWord w)
        {
            if (FirstWord == null)
            {
                FirstWord = w;
            }
            else
            {
                LastWord.Next = w;
            }
        }
        public GWord LastWord
        {
            get
            {
                GWord w = FirstWord;
                while (w.Next != null)
                {
                    w = w.Next;
                }

                return w;
            }
        }


        //index�̈ʒu�̕\���𔽓]�����V����GLine��Ԃ�
        //inverse��false���ƁAGWord�̕����͂��邪Decoration�̔��]�͂��Ȃ��B�q�N�q�N���̑Ώ��Ƃ��Ď����B
        internal GLine InverseCaret(int index, bool inverse, bool underline)
        {
            ExpandBuffer(index + 1);
            if (Text[index] == WIDECHAR_PAD)
            {
                index--;
            }

            GLine ret = new GLine(Text, null)
            {
                ID = ID,
                EOLType = EOLType
            };

            GWord w = FirstWord;
            int nextoffset = 0;
            while (w != null)
            {
                nextoffset = WordNextOffset(w);
                if (w.Offset <= index && index < nextoffset)
                {
                    //!!tail���珇�ɘA���������������͂悢
                    if (w.Offset < index)
                    {
                        GWord head = new GWord(w.Decoration, w.Offset, w.CharGroup);
                        ret.Append(head);
                    }

                    TextDecoration dec = (TextDecoration)w.Decoration.Clone();
                    if (inverse)
                    {
                        //�F���L�����b�g�̃T�|�[�g
                        dec.ToCaretStyle();
                    }
                    if (underline)
                    {
                        dec.Underline = true;
                    }

                    GWord mid = new GWord(dec, index, w.CharGroup);
                    ret.Append(mid);

                    if (index + CalcDisplayLength(Text[index]) < nextoffset)
                    {
                        GWord tail = new GWord(w.Decoration, index + CalcDisplayLength(Text[index]), w.CharGroup);
                        ret.Append(tail);
                    }
                }
                else
                {
                    ret.Append(w.StandAloneClone());
                }

                w = w.Next;
            }

            //!!���́A�L�����b�g�ʒu�ɃX�y�[�X������̂�Inverse�Ƃ͈Ⴄ�����ł��邩�番�����邱��
            if (nextoffset <= index)
            {
                while (nextoffset <= index)
                {
                    Debug.Assert(nextoffset < ret.Text.Length);
                    ret.Text[nextoffset++] = ' ';
                }
                TextDecoration dec = TextDecoration.ClonedDefault();
                if (inverse)
                {
                    dec.ToCaretStyle();
                }
                if (underline)
                {
                    dec.Underline = true;
                }

                ret.Append(new GWord(dec, index, CharGroup.SingleByte));
            }

            return ret;
        }

        internal GLine InverseRange(int from, int to)
        {
            ExpandBuffer(Math.Max(from + 1, to)); //���������T�C�Y�����Ƃ��Ȃǂɂ��̏������������Ȃ����Ƃ�����
            Debug.Assert(from >= 0 && from < Text.Length);
            if (from < Text.Length && Text[from] == WIDECHAR_PAD)
            {
                from--;
            }

            if (to > 0 && to - 1 < Text.Length && Text[to - 1] == WIDECHAR_PAD)
            {
                to--;
            }

            GLine ret = new GLine(Text, null)
            {
                ID = ID,
                EOLType = EOLType
            };
            //�����̔z����Z�b�g
            TextDecoration[] dec = new TextDecoration[Text.Length];
            GWord w = FirstWord;
            while (w != null)
            {
                Debug.Assert(w.Decoration != null);
                dec[w.Offset] = w.Decoration;
                w = w.Next;
            }

            //���]�J�n�_
            TextDecoration original = null;
            TextDecoration inverse = null;
            for (int i = from; i >= 0; i--)
            {
                if (dec[i] != null)
                {
                    original = dec[i];
                    break;
                }
            }
            Debug.Assert(original != null);
            inverse = (TextDecoration)original.Clone();
            inverse.Inverse();
            dec[from] = inverse;

            //�͈͂ɓn���Ĕ��]���
            for (int i = from + 1; i < to; i++)
            {
                if (i < dec.Length && dec[i] != null)
                {
                    original = dec[i];
                    inverse = (TextDecoration)original.Clone();
                    inverse.Inverse();
                    dec[i] = inverse;
                }
            }

            if (to < dec.Length && dec[to] == null)
            {
                dec[to] = original;
            }

            //����ɏ]����GWord�����
            w = null;
            for (int i = dec.Length - 1; i >= 0; i--)
            {
                char ch = Text[i];
                if (dec[i] != null && ch != '\0')
                {
                    int j = i;
                    if (ch == WIDECHAR_PAD)
                    {
                        j++;
                    }

                    GWord ww = new GWord(dec[i], j, CalcCharGroup(ch))
                    {
                        Next = w
                    };
                    w = ww;
                }
            }
            ret.Append(w);

            return ret;
        }

        //���̗̈�̕����̕��͖{���̓t�H���g�ˑ������A�����̓��{����ł͑S�p�Ƃ��Ĉ�����͗l�BBS�ŏ����ƂQ�����肷��
        private static byte[] _length_map_0x80_0xFF;
        private static byte[] _length_map_0x2500_0x25FF;

        private static void InitLengthMap()
        {
            _length_map_0x80_0xFF = new byte[0x80];
            for (int i = 0; i < _length_map_0x80_0xFF.Length; i++)
            {
                int t = i + 0x80;
                //    ��         �N         ��         �}         �L         ��          �~         ��
                if (t == 0xA7 || t == 0xA8 || t == 0xB0 || t == 0xB1 || t == 0xB4 || t == 0xB6 || t == 0xD7 || t == 0xF7)
                {
                    _length_map_0x80_0xFF[i] = 2;
                }
                else
                {
                    _length_map_0x80_0xFF[i] = 1;
                }
            }

            //�S�p���p���݃]�[��
            _length_map_0x2500_0x25FF = new byte[] {
              //�� �� �� ��                        ��       ��
                2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, //2500-0F
              //��       �� ��       �� ��       �� �� ��
                2, 1, 1, 2, 2, 1, 1, 2, 2, 1, 1, 2, 2, 2, 1, 1, //2510-1F
              //��       �� �� ��       ��       �� ��       ��
                2, 1, 1, 2, 2, 2, 1, 1, 2, 1, 1, 2, 2, 1, 1, 2, //2520-2F
              //��       �� ��       �� ��       �� ��       ��
                2, 1, 1, 2, 2, 1, 1, 2, 2, 1, 1, 2, 2, 1, 1, 2, //2530-3F
              //      ��                         ��
                1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, //2540-4F
              //
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //2550-5F
              //
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //2560-6F
              //
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //2570-7F
              //
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //2580-8F
              //
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //2590-9F
              //�� ��
                2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //25A0-AF
              //      �� ��                         �� ��
                1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, //25B0-BF
              //                  �� ��          ��       �� ��
                1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 2, 1, 1, 2, 2, //25C0-CF
              //
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, //25D0-DF
              //                                             ��
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, //25E0-EF
              //
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  //25F0-FF
			};
            Debug.Assert(_length_map_0x2500_0x25FF.Length == 256);

        }

        //�����ɂ���ĕ`�敝�����߂�
        internal static int CalcDisplayLength(char ch)
        {
            if (ch >= 0x100)
            {
                if (0xFF61 <= ch && ch <= 0xFF9F) //���p�J�i
                {
                    return 1;
                }
                else if (0x2500 <= ch && ch <= 0x25FF) //�r��������L��
                {
                    return _length_map_0x2500_0x25FF[ch - 0x2500];
                }
                else
                {
                    return 2;
                }
            }
            else if (ch >= 0x80)
            {
                return _length_map_0x80_0xFF[ch - 0x80];
            }
            else
            {
                return 1; //�{����tab�Ȃǂ��邩������Ȃ��̂ł��������^�ʖڂɌv�Z���ׂ�
            }
        }
        //ASCII�����{�ꕶ���� �t�H���g�̑I���Ɏg��
        internal static CharGroup CalcCharGroup(char ch)
        {
            if (ch < 0x80)
            {
                return CharGroup.SingleByte;
            }
            else if (ch < 0x100)
            {
                return _length_map_0x80_0xFF[ch - 0x80] == 1 ? CharGroup.SingleByte : CharGroup.TwoBytes;
            }
            else
            {
                if (0x2500 <= ch && ch <= 0x25FF) //�r���͓��{��t�H���g�͎g��Ȃ�
                {
                    return _length_map_0x2500_0x25FF[ch - 0x2500] == 1 ? CharGroup.SingleByte : CharGroup.TwoBytes;
                }
                else
                {
                    return CharGroup.TwoBytes;
                }
            }
        }
    }

    /// <summary>
    /// �����̒ǉ��폜�Ȃǂ�����GLine�𑀍삷��B�Ⴆ�΃^�[�~�i�������̃N���X���o�R���ăh�L�������g�̓����GLine������������̂Ɏg���B
    /// </summary>
    internal class GLineManipulator
    {

        private char[] _text;
        private TextDecoration[] _decorations;

        private int _caretColumn;

        private EOLType _eolType;

        /// <summary>
        /// ��ō\�z
        /// </summary>
        public GLineManipulator(int length)
        {
            _decorations = new TextDecoration[length];
            _text = new char[length];
            Clear(length);
        }
        /// <summary>
        /// �S���e��j������
        /// </summary>
        public void Clear(int length)
        {
            if (length != _text.Length)
            {
                _decorations = new TextDecoration[length];
                _text = new char[length];
            }
            else
            {
                for (int i = 0; i < _decorations.Length; i++)
                {
                    _decorations[i] = null;
                }

                for (int i = 0; i < _text.Length; i++)
                {
                    _text[i] = '\0';
                }
            }
            _caretColumn = 0;
            _eolType = EOLType.Continue;
        }

        public int CaretColumn
        {
            get => _caretColumn;
            set
            {
                Debug.Assert(value >= 0 && value <= _text.Length);
                _caretColumn = value;
                value--;
                while (value >= 0 && _text[value] == '\0')
                {
                    _text[value--] = ' ';
                }
            }
        }

        public void CarriageReturn()
        {
            _caretColumn = 0;
            _eolType = EOLType.CR;
        }

        public bool IsEmpty =>
            //_text��S������K�v�͂Ȃ����낤
            _caretColumn == 0 && _text[0] == '\0';
        public TextDecoration DefaultDecoration { get; set; }

        /// <summary>
        /// �����Ɠ������e�ŏ���������Bline�̓��e�͔j�󂳂�Ȃ��B
        /// ������null�̂Ƃ��͈����Ȃ��̃R���X�g���N�^�Ɠ������ʂɂȂ�B
        /// </summary>
        public void Load(GLine line, int cc)
        {
            if (line == null)
            { //���ꂪnull�ɂȂ��Ă���Ƃ����v���Ȃ��N���b�V�����|�[�g���������B�{���͂Ȃ��͂��Ȃ񂾂�...
                Clear(80);
                return;
            }

            Clear(line.Length);
            GWord w = line.FirstWord;
            Debug.Assert(line.Text.Length == _text.Length);
            Array.Copy(line.Text, 0, _text, 0, _text.Length);

            int n = 0;
            while (w != null)
            {
                int nextoffset = line.WordNextOffset(w);
                while (n < nextoffset)
                {
                    _decorations[n++] = w.Decoration;
                }

                w = w.Next;
            }

            _eolType = line.EOLType;
            ExpandBuffer(cc + 1);
            CaretColumn = cc; //' '�Ŗ��߂邱�Ƃ�����̂Ńv���p�e�B�Z�b�g���g��
        }
        public int BufferSize => _text.Length;

        public void ExpandBuffer(int length)
        {
            if (length <= _text.Length)
            {
                return;
            }

            char[] current = _text;
            _text = new char[length];
            Array.Copy(current, 0, _text, 0, current.Length);
            TextDecoration[] current2 = _decorations;
            _decorations = new TextDecoration[length];
            Array.Copy(current2, 0, _decorations, 0, current2.Length);
        }

        public void PutChar(char ch, TextDecoration dec)
        {
            Debug.Assert(dec != null);
            Debug.Assert(_caretColumn >= 0);
            Debug.Assert(_caretColumn < _text.Length);

            //�ȉ��킩��ɂ������A�v�͏ꍇ�����B����̎d�l����������������
            bool onZenkakuRight = (_text[_caretColumn] == GLine.WIDECHAR_PAD);
            bool onZenkaku = onZenkakuRight || (_text.Length > _caretColumn + 1 && _text[_caretColumn + 1] == GLine.WIDECHAR_PAD);

            if (onZenkaku)
            {
                //�S�p�̏�ɏ���
                if (!onZenkakuRight)
                {
                    _text[_caretColumn] = ch;
                    _decorations[_caretColumn] = dec;
                    if (GLine.CalcDisplayLength(ch) == 1)
                    {
                        //�S�p�̏�ɔ��p���������ꍇ�A�ׂɃX�y�[�X�����Ȃ��ƕ\���������
                        if (_caretColumn + 1 < _text.Length)
                        {
                            _text[_caretColumn + 1] = ' ';
                        }

                        _caretColumn++;
                    }
                    else
                    {
                        _decorations[_caretColumn + 1] = dec;
                        _caretColumn += 2;
                    }
                }
                else
                {
                    _text[_caretColumn - 1] = ' ';
                    _text[_caretColumn] = ch;
                    _decorations[_caretColumn] = dec;
                    if (GLine.CalcDisplayLength(ch) == 2)
                    {
                        if (GLine.CalcDisplayLength(_text[_caretColumn + 1]) == 2)
                        {
                            if (_caretColumn + 2 < _text.Length)
                            {
                                _text[_caretColumn + 2] = ' ';
                            }
                        }

                        _text[_caretColumn + 1] = GLine.WIDECHAR_PAD;
                        _decorations[_caretColumn + 1] = _decorations[_caretColumn];
                        _caretColumn += 2;
                    }
                    else
                    {
                        _caretColumn++;
                    }
                }
            }
            else
            { //���p�̏�ɏ���
                _text[_caretColumn] = ch;
                _decorations[_caretColumn] = dec;
                if (GLine.CalcDisplayLength(ch) == 2)
                {
                    if (GLine.CalcDisplayLength(_text[_caretColumn + 1]) == 2) //���p�A�S�p�ƂȂ��Ă���Ƃ���ɑS�p����������
                    {
                        if (_caretColumn + 2 < _text.Length)
                        {
                            _text[_caretColumn + 2] = ' ';
                        }
                    }

                    _text[_caretColumn + 1] = GLine.WIDECHAR_PAD;
                    _decorations[_caretColumn + 1] = _decorations[_caretColumn];
                    _caretColumn += 2;
                }
                else
                {
                    _caretColumn++; //���ꂪ�ł�common�ȃP�[�X����
                }
            }
        }
        public void SetDecoration(TextDecoration dec)
        {
            if (_caretColumn < _decorations.Length)
            {
                _decorations[_caretColumn] = dec;
            }
        }

        public char CharAt(int index)
        {
            return _text[index];
        }

        public void BackCaret()
        {
            if (_caretColumn > 0)
            { //�ō��[�ɂ���Ƃ��͉������Ȃ�
                _caretColumn--;
            }
        }

        public void RemoveAfterCaret()
        {
            for (int i = _caretColumn; i < _text.Length; i++)
            {
                _text[i] = '\0';
                _decorations[i] = null;
            }
        }
        public void FillSpace(int from, int to)
        {
            if (to > _text.Length)
            {
                to = _text.Length;
            }

            for (int i = from; i < to; i++)
            {
                _text[i] = ' ';
                _decorations[i] = null;
            }
        }
        public void FillSpace(int from, int to, TextDecoration dec)
        {
            if (to > _text.Length)
            {
                to = _text.Length;
            }

            for (int i = from; i < to; i++)
            {
                _text[i] = ' ';
                _decorations[i] = dec;
            }
        }
        //start����count�������������ċl�߂�B�E�[�ɂ�null������
        public void DeleteChars(int start, int count)
        {
            for (int i = start; i < _text.Length; i++)
            {
                int j = i + count;
                if (j < _text.Length)
                {
                    _text[i] = _text[j];
                    _decorations[i] = _decorations[j];
                }
                else
                {
                    _text[i] = '\0';
                    _decorations[i] = null;
                }
            }
        }
        public void InsertBlanks(int start, int count)
        {
            for (int i = _text.Length - 1; i >= _caretColumn; i--)
            {
                int j = i - count;
                if (j >= _caretColumn)
                {
                    _text[i] = _text[j];
                    _decorations[i] = _decorations[j];
                }
                else
                {
                    _text[i] = ' ';
                    _decorations[i] = null;
                }
            }
        }

        public GLine Export()
        {
            GWord w = new GWord(_decorations[0] == null ? TextDecoration.ClonedDefault() : _decorations[0], 0, GLine.CalcCharGroup(_text[0]));

            GLine line = new GLine(_text, w)
            {
                EOLType = _eolType
            };
            int m = _text.Length;
            for (int offset = 1; offset < m; offset++)
            {
                char ch = _text[offset];
                if (ch == '\0')
                {
                    break;
                }
                else if (ch == GLine.WIDECHAR_PAD)
                {
                    continue;
                }

                TextDecoration dec = _decorations[offset];
                if (_decorations[offset - 1] != dec || w.CharGroup != GLine.CalcCharGroup(ch))
                {
                    if (dec == null)
                    {
                        dec = TextDecoration.ClonedDefault(); //!!�{���͂�����null�ɂȂ��Ă���̂͂��肦�Ȃ��͂��B��Œ������邱��
                    }

                    GWord ww = new GWord(dec, offset, GLine.CalcCharGroup(ch));
                    w.Next = ww;
                    w = ww;
                }
            }
            return line;
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            b.Append(_text);
            //�A�g���r���[�g�܂��̕\���͂܂����Ă��Ȃ�
            return b.ToString();
        }
        /*
		public static void TestPutChar() {
			TestPutChar(1, "aaaaz ", 0, '��');
			TestPutChar(2, "a��\uFFFFaz ", 0, '��');
			TestPutChar(3, "��\uFFFF��\uFFFFz ", 0, 'b');
			TestPutChar(4, "��\uFFFF��\uFFFFz ", 0, '��');
			TestPutChar(5, "��\uFFFF��\uFFFFz ", 1, 'b');
			TestPutChar(6, "��\uFFFFaaz ", 1, '��');
			TestPutChar(7, "��\uFFFF��\uFFFFz ", 1, '��');
		}*/
        private static void TestPutChar(int num, string initial, int col, char ch)
        {
            GLineManipulator m = new GLineManipulator(10)
            {
                _text = initial.ToCharArray(),
                CaretColumn = col
            };
            Debug.WriteLine(String.Format("Test{0}  [{1}] col={2} char={3}", num, SafeString(m._text), m.CaretColumn, ch));
            m.PutChar(ch, TextDecoration.Default);
            Debug.WriteLine(String.Format("Result [{0}] col={1}", SafeString(m._text), m.CaretColumn));
        }
        public static string SafeString(char[] d)
        {
            StringBuilder bld = new StringBuilder();
            for (int i = 0; i < d.Length; i++)
            {
                char ch = d[i];
                if (ch == '\0')
                {
                    break;
                }

                if (ch != GLine.WIDECHAR_PAD)
                {
                    bld.Append(ch);
                }
            }
            return bld.ToString();
        }

    }
}
