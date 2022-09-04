/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: SelectionKeyProcessor.cs,v 1.2 2005/04/20 08:45:47 okajima Exp $
*/
using Poderosa.Terminal;
using System.Diagnostics;
using System.Windows.Forms;

namespace Poderosa.Text
{
    /// <summary>
    /// テキスト選択モードに入った後でキーを処理して選択領域のコントロールを行う
    /// </summary>
    internal class SelectionKeyProcessor
    {
        //現在のカーソル位置
        private TerminalDocument _document;

        public TerminalPane Owner { get; }

        public GLine CurrentLine { get; private set; }

        public int CaretPos { get; private set; }

        //表示上のキャレット位置。行の右端よりも向こうに表示しないようにするため
        public int UICaretPos
        {
            get
            {
                if (CaretPos > CurrentLine.CharLength)
                {
                    return CurrentLine.CharLength;
                }
                else
                {
                    return CaretPos;
                }
            }
        }


        public SelectionKeyProcessor(TerminalPane owner, TerminalDocument doc, GLine line, int pos)
        {
            Owner = owner;
            _document = doc;
            Debug.Assert(line != null);
            CurrentLine = line;
            CaretPos = pos;
        }

        public bool ProcessKey(Keys key)
        {
            Keys body = key & Keys.KeyCode;
            bool shift = (key & Keys.Shift) != Keys.None;
            bool control = (key & Keys.Control) != Keys.None;
            bool processed = false;

            //移動先の行と桁の計算
            GLine nextLine = CurrentLine;
            _document.InvalidateLine(nextLine.ID);
            if (body == Keys.Up)
            {
                if (CurrentLine.PrevLine != null)
                {
                    nextLine = CurrentLine.PrevLine;
                }

                _document.InvalidateLine(nextLine.ID);
                processed = true;
            }
            else if (body == Keys.Down)
            {
                if (CurrentLine.NextLine != null)
                {
                    nextLine = CurrentLine.NextLine;
                }

                _document.InvalidateLine(nextLine.ID);
                processed = true;
            }
            else if (body == Keys.PageUp)
            {
                int n = CurrentLine.ID - Owner.Connection.TerminalHeight;
                nextLine = n <= _document.FirstLineNumber ? _document.FirstLine : _document.FindLine(n);
                _document.InvalidateAll();
                processed = true;
            }
            else if (body == Keys.PageDown)
            {
                int n = CurrentLine.ID + Owner.Connection.TerminalHeight;
                nextLine = n >= _document.LastLineNumber ? _document.LastLine : _document.FindLine(n);
                _document.InvalidateAll();
                processed = true;
            }

            int nextPos = CaretPos;
            if (body == Keys.Home)
            {
                nextPos = 0;
                processed = true;
            }
            else if (body == Keys.End)
            {
                nextPos = CurrentLine.CharLength - 1;
                processed = true;
            }
            else if (body == Keys.Left)
            {
                if (nextPos > 0)
                {
                    if (control)
                    {
                        nextPos = CurrentLine.FindPrevWordBreak(nextPos - 1) + 1;
                    }
                    else
                    {
                        nextPos--;
                    }
                }
                processed = true;
            }
            else if (body == Keys.Right)
            {
                if (nextPos < CurrentLine.CharLength - 1)
                {
                    if (control)
                    {
                        nextPos = CurrentLine.FindNextWordBreak(nextPos + 1);
                    }
                    else
                    {
                        nextPos++;
                    }
                }
                processed = true;
            }

            //選択領域の調整
            TextSelection sel = GEnv.TextSelection;
            if (shift && processed)
            {
                if (sel.IsEmpty)
                {
                    sel.StartSelection(Owner, CurrentLine, CaretPos, RangeType.Char, -1, -1);
                }

                sel.ExpandTo(nextLine, nextPos, RangeType.Char);
            }
            else if (processed || body == Keys.Menu || body == Keys.ControlKey || body == Keys.ShiftKey)
            {
                if (processed)
                {
                    sel.Clear();
                }

                processed = true;
            }
            else
            {
                //一般キーの入力があったら即時選択解除
                sel.Clear();
            }

            Debug.Assert(nextLine != null);
            CurrentLine = nextLine;
            CaretPos = nextPos;
            return processed;
        }
    }
}
