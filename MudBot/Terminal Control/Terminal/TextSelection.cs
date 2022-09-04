/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: TextSelection.cs,v 1.2 2005/04/20 08:45:48 okajima Exp $
*/
using Poderosa.Terminal;
using System.Diagnostics;
using System.Text;

namespace Poderosa.Text
{
    public enum RangeType
    {
        Char,
        Word,
        Line
    }
    public enum SelectionState
    {
        Empty,
        Pivot,
        Expansion,
        Fixed
    }

    /// <summary>
    /// テキストの選択領域を表現する
    /// </summary>
    public class TextSelection
    {

        //端点
        internal struct TextPoint
        {
            public int _line;
            public int _position;

            public void Clear()
            {
                _line = -1;
                _position = 0;
            }
        }

        //最初の選択点。単語や行を選択したときのために２つ設ける
        private TextPoint _forwardPivot;
        private TextPoint _backwardPivot;
        //選択の最終点
        private TextPoint _forwardDestination;
        private TextPoint _backwardDestination;

        //pivotの状態

        //選択を開始したときのマウス座標

        //ちょっと汚いフラグ
        private bool _disabledTemporary;

        public TextSelection()
        {
            Owner = null;
        }

        public TerminalPane Owner { get; private set; }

        public SelectionState State { get; private set; }

        public RangeType PivotType { get; private set; }

        public int StartX { get; private set; }

        public int StartY { get; private set; }


        public void Clear()
        {
            if (Owner != null)
            {
                Owner.ExitTextSelection();
            }
            Owner = null;
            State = SelectionState.Empty;
            PivotType = RangeType.Char;
            _forwardPivot.Clear();
            _backwardPivot.Clear();
            _forwardDestination.Clear();
            _backwardDestination.Clear();
            _disabledTemporary = false;
        }

        public void DisableTemporary()
        {
            _disabledTemporary = true;
        }

        //ドキュメントがDiscardされたときに呼ばれる。first_lineより前に選択領域が重なっていたらクリアする
        public void ClearIfOverlapped(int first_line)
        {
            if (_forwardPivot._line != -1 && _forwardPivot._line < first_line)
            {
                _forwardPivot._line = first_line;
                _forwardPivot._position = 0;
                _backwardPivot._line = first_line;
                _backwardPivot._position = 0;
            }

            if (_forwardDestination._line != -1 && _forwardDestination._line < first_line)
            {
                _forwardDestination._line = first_line;
                _forwardDestination._position = 0;
                _backwardDestination._line = first_line;
                _backwardDestination._position = 0;
            }
        }

        public bool IsEmpty =>
            Owner == null || _forwardPivot._line == -1 || _backwardPivot._line == -1 |
            _forwardDestination._line == -1 || _backwardDestination._line == -1 || _disabledTemporary;

        internal bool StartSelection(TerminalPane owner, GLine line, int position, RangeType type, int x, int y)
        {
            Debug.Assert(owner != null);
            Debug.Assert(position >= 0);
            line.ExpandBuffer(position + 1);

            _disabledTemporary = false;
            Owner = owner;
            PivotType = type;
            _forwardPivot._line = line.ID;
            _backwardPivot._line = line.ID;
            _forwardDestination._line = line.ID;
            _forwardDestination._position = position;
            _backwardDestination._line = line.ID;
            _backwardDestination._position = position;
            switch (type)
            {
                case RangeType.Char:
                    _forwardPivot._position = position;
                    _backwardPivot._position = position;
                    break;
                case RangeType.Word:
                    _forwardPivot._position = line.FindPrevWordBreak(position) + 1;
                    _backwardPivot._position = line.FindNextWordBreak(position);
                    break;
                case RangeType.Line:
                    _forwardPivot._position = 0;
                    _backwardPivot._position = line.CharLength;
                    break;
            }
            State = SelectionState.Pivot;
            StartX = x;
            StartY = y;
            return true;
        }

        internal bool ExpandTo(GLine line, int position, RangeType type)
        {
            line.ExpandBuffer(position + 1);
            _disabledTemporary = false;
            State = SelectionState.Expansion;

            _forwardDestination._line = line.ID;
            _backwardDestination._line = line.ID;
            //Debug.WriteLine(String.Format("ExpandTo Line{0} Position{1}", line.ID, position));
            switch (type)
            {
                case RangeType.Char:
                    _forwardDestination._position = position;
                    _backwardDestination._position = position;
                    break;
                case RangeType.Word:
                    _forwardDestination._position = line.FindPrevWordBreak(position) + 1;
                    _backwardDestination._position = line.FindNextWordBreak(position);
                    break;
                case RangeType.Line:
                    _forwardDestination._position = 0;
                    _backwardDestination._position = line.CharLength;
                    break;
            }

            return true;
        }

        internal void SelectAll(TerminalPane owner)
        {
            _disabledTemporary = false;
            Debug.Assert(owner != null);
            Owner = owner;
            _forwardPivot._line = Owner.Document.FirstLine.ID;
            _forwardPivot._position = 0;
            _backwardPivot = _forwardPivot;
            _forwardDestination._line = Owner.Document.LastLine.ID;
            _forwardDestination._position = Owner.Document.LastLine.CharLength - 1;
            _backwardDestination = _forwardDestination;

            State = SelectionState.Fixed;
            PivotType = RangeType.Char;
        }

        //ペイン外へマウスをドラッグしていった場合に位置を修正する
        internal void ConvertSelectionPosition(ref int line_id, ref int col)
        {
            if (PivotType == RangeType.Line)
            {
                if (line_id <= _forwardPivot._line)
                {
                    col = 0;
                }
                else
                {
                    col = Owner.Connection.TerminalWidth;
                }
            }
            else
            {
                if (line_id < _forwardPivot._line)
                {
                    if (col < 0)
                    {
                        col = 0;
                    }
                    else if (col >= Owner.Connection.TerminalWidth)
                    {
                        line_id++;
                        col = 0;
                    }
                }
                else if (line_id == _forwardPivot._line)
                {
                    if (col < 0)
                    {
                        col = 0;
                    }
                    else if (col >= Owner.Connection.TerminalWidth)
                    {
                        col = Owner.Connection.TerminalWidth;
                    }
                }
                else
                {
                    if (col < 0)
                    {
                        line_id--;
                        col = Owner.Connection.TerminalWidth;
                    }
                    else if (col >= Owner.Connection.TerminalWidth)
                    {
                        col = Owner.Connection.TerminalWidth;
                    }
                }
            }
        }


        internal void FixSelection()
        {
            State = SelectionState.Fixed;
        }

        public string GetSelectedText()
        {
            return GetSelectedText(NLOption.Default);
        }
        public string GetSelectedTextAsLook()
        {
            return GetSelectedText(NLOption.AsLook);
        }

        private enum NLOption
        {
            Default,
            AsLook
        }

        private string GetSelectedText(NLOption opt)
        {
            if (Owner == null || _disabledTemporary)
            {
                return null;
            }

            StringBuilder bld = new StringBuilder();
            TextPoint a = HeadPoint;
            TextPoint b = TailPoint;

            GLine l = Owner.Document.FindLineOrEdge(a._line);
            int pos = a._position;

            do
            {
                bool eol_required = (opt == NLOption.AsLook || l.EOLType != EOLType.Continue);
                if (l.ID == b._line)
                { //最終行
                  //末尾にNULL文字が入るケースがあるようだ
                    AppendTrim(bld, l.Text, pos, b._position - pos);
                    if (PivotType == RangeType.Line && eol_required)
                    {
                        bld.Append("\r\n");
                    }

                    break;
                }
                else
                { //最終以外の行
                    if (l.CharLength - pos > 0)
                    { //l.CharLength==posとなるケースがあった。真の理由は納得していないが
                        AppendTrim(bld, l.Text, pos, l.CharLength - pos);
                    }
                    if (eol_required && bld.Length > 0) //bld.Length>0は行単位選択で余計な改行が入るのを避けるための処置
                    {
                        bld.Append("\r\n"); //LFのみをクリップボードに持っていっても他のアプリの混乱があるだけなのでやめておく
                    }

                    l = l.NextLine;
                    if (l == null)
                    {
                        break; //!!本来これはないはずだがクラッシュレポートのため回避
                    }

                    pos = 0;
                }
            } while (true);

            //Debug.WriteLine("Selected Text Len="+bld.Length);

            return bld.ToString();
        }
        private void AppendTrim(StringBuilder bld, char[] text, int pos, int length)
        {
            Debug.Assert(pos >= 0);
            if (text[pos] == GLine.WIDECHAR_PAD)
            { //日本語文字の右端からのときは拡大する
                pos--;
                length++;
            }

            while (length-- > 0)
            {
                if (pos >= text.Length)
                {
                    break;
                }

                char ch = text[pos++];
                if (ch != GLine.WIDECHAR_PAD && ch != '\0')
                {
                    bld.Append(ch);
                }
            }
        }

        internal TextPoint HeadPoint => Min(ref _forwardPivot, ref _forwardDestination);

        internal TextPoint TailPoint => Max(ref _backwardPivot, ref _backwardDestination);

        private static TextPoint Min(ref TextPoint p1, ref TextPoint p2)
        {
            int id1 = p1._line;
            int id2 = p2._line;
            if (id1 == id2)
            {
                int pos1 = p1._position;
                int pos2 = p2._position;
                if (pos1 == pos2)
                {
                    return p1;
                }
                else
                {
                    return pos1 < pos2 ? p1 : p2;
                }
            }
            else
            {
                return id1 < id2 ? p1 : p2;
            }
        }
        private static TextPoint Max(ref TextPoint p1, ref TextPoint p2)
        {
            int id1 = p1._line;
            int id2 = p2._line;
            if (id1 == id2)
            {
                int pos1 = p1._position;
                int pos2 = p2._position;
                if (pos1 == pos2)
                {
                    return p1;
                }
                else
                {
                    return pos1 > pos2 ? p1 : p2;
                }
            }
            else
            {
                return id1 > id2 ? p1 : p2;
            }
        }


    }
}
