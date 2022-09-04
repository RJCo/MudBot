/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: TerminalDocument.cs,v 1.2 2005/04/20 08:45:47 okajima Exp $
*/
using Poderosa.Communication;
using System;
using System.Diagnostics;

namespace Poderosa.Text
{
    /// <summary>
    /// ターミナルとして動かしているときの
    /// </summary>
    public class TerminalDocument
    {
        private TerminalConnection _connection; //!!これは幅と高さ取得のためにのみ必要なパラメタなのでTerminalConnectionはやりすぎ

        //描画の必要のあるIDの範囲

        internal TerminalDocument(TerminalConnection con)
        {
            _connection = con;
            Clear();
            ScrollingTop = -1;
            ScrollingBottom = -1;
        }

        public int InvalidatedFrom { get; private set; }

        public int InvalidatedTo { get; private set; }

        public bool InvalidatedAll { get; private set; }

        public void ResetInvalidatedRegion()
        {
            InvalidatedAll = false;
            InvalidatedFrom = -1;
            InvalidatedTo = -1;
        }
        public void InvalidateLine(int id)
        {
            if (InvalidatedFrom == -1 || InvalidatedFrom > id)
            {
                InvalidatedFrom = id;
            }

            if (InvalidatedTo == -1 || InvalidatedTo < id)
            {
                InvalidatedTo = id;
            }
        }
        public void InvalidateAll()
        {
            InvalidatedAll = true;
        }

        internal void Clear()
        {
            CaretColumn = 0;
            FirstLine = null;
            LastLine = null;
            Size = 0;
            AddLine(new GLine(_connection.TerminalWidth));
        }

        public int Size { get; private set; }

        //末尾に追加する
        internal void AddLine(GLine line)
        {
            if (FirstLine == null)
            { //空だった
                FirstLine = line;
                LastLine = line;
                CurrentLine = line;
                TopLine = line;
                Size = 1;
                line.ID = 0;
                InvalidateLine(0);
            }
            else
            { //通常の追加
                Debug.Assert(LastLine.NextLine == null);
                int lastID = LastLine.ID;
                LastLine.NextLine = line;
                line.PrevLine = LastLine;
                LastLine = line;
                line.ID = lastID + 1;
                Size++;
                InvalidateLine(lastID + 1);
            }

        }

        //整数インデクスから見つける　CurrentLineからそう遠くない位置だろうとあたりをつける
        public GLine FindLine(int index)
        {
            //currentとtopの近い方から順にみていく
            int d1 = Math.Abs(index - CurrentLine.ID);
            int d2 = Math.Abs(index - TopLine.ID);
            if (d1 < d2)
            {
                return FindLineByHint(index, CurrentLine);
            }
            else
            {
                return FindLineByHint(index, TopLine);
            }
        }

        public GLine FindLineOrNull(int index)
        {
            if (index < FirstLine.ID || index > LastLine.ID)
            {
                return null;
            }
            else
            {
                return FindLine(index);
            }
        }
        public GLine FindLineOrEdge(int index)
        {
            if (index < FirstLine.ID)
            {
                index = FirstLine.ID;
            }
            else if (index > LastLine.ID)
            {
                index = LastLine.ID;
            }

            return FindLine(index);
        }

        private GLine FindLineByHint(int index, GLine hintLine)
        {
            int h = hintLine.ID;
            GLine l = hintLine;
            if (index >= h)
            {
                for (int i = h; i < index; i++)
                {
                    l = l.NextLine;
                    if (l == null)
                    {
                        FindLineByHintFailed(index, hintLine);
                    }
                }
            }
            else
            {
                for (int i = h; i > index; i--)
                {
                    l = l.PrevLine;
                    if (l == null)
                    {
                        FindLineByHintFailed(index, hintLine);
                    }
                }
            }
            return l;
        }

        //FindLineByHintはしばしば失敗するのでデバッグ用に現在状態をダンプ
        private void FindLineByHintFailed(int index, GLine hintLine)
        {

#if DEBUG
            Dump(String.Format("FindLine {0}, hint_id={1}", index, hintLine.ID));
            Debugger.Break();
#endif
            GEnv.InterThreadUIService.InvalidDocumentOperation(this, "Message.TerminalDocument.UnexpectedCode");
        }

        internal void SetScrollingRegion(int top_offset, int bottom_offset)
        {
            ScrollingTop = TopLineNumber + top_offset;
            ScrollingBottom = TopLineNumber + bottom_offset;
            //GLine l = FindLine(_scrollingTop);
        }
        internal void ClearScrollingRegion()
        {
            ScrollingTop = -1;
            ScrollingBottom = -1;
        }
        public void EnsureLine(int id)
        {
            while (id > LastLine.ID)
            {
                AddLine(new GLine(_connection.TerminalWidth));
            }
        }

        public int CurrentLineNumber
        {
            get => CurrentLine.ID;
            set
            {
                if (value < FirstLine.ID)
                {
                    value = FirstLine.ID; //リサイズ時の微妙なタイミングで負になってしまうことがあったようだ
                }

                if (value > LastLine.ID + 100)
                {
                    value = LastLine.ID + 100; //極端に大きな値を食らって死ぬことがないようにする
                }

                while (value > LastLine.ID)
                {
                    AddLine(new GLine(_connection.TerminalWidth));
                }

                CurrentLine = FindLineOrEdge(value); //外部から変な値が渡されたり、あるいはどこかにバグがあるせいでこの中でクラッシュすることがまれにあるようだ。なのでOrEdgeバージョンにしてクラッシュは回避
                AssertValid();
            }
        }
        public int TopLineNumber
        {
            get => TopLine.ID;
            set
            {
                if (TopLine.ID != value)
                {
                    InvalidatedAll = true;
                }

                TopLine = FindLineOrEdge(value); //同上の理由でOrEdgeバージョンに変更
                AssertValid();
            }
        }
        public int FirstLineNumber => FirstLine.ID;

        public int LastLineNumber => LastLine.ID;
        public int CaretColumn { get; set; }

        public GLine CurrentLine { get; private set; }

        public GLine TopLine { get; private set; }

        public GLine FirstLine { get; private set; }

        public GLine LastLine { get; private set; }

        public bool CurrentIsLast => CurrentLine.NextLine == null;

        public int ScrollingTop { get; private set; }

        public int ScrollingBottom { get; private set; }

        internal void LineFeed()
        {
            if (ScrollingTop != -1 && CurrentLine.ID >= ScrollingBottom)
            { //ロックされていて下まで行っている
                ScrollDown();
            }
            else
            {
                if (_connection.TerminalHeight > 1)
                { //極端に高さがないときはこれで変な値になってしまうのでスキップ
                    if (CurrentLine.ID >= TopLine.ID + _connection.TerminalHeight - 1)
                    {
                        TopLineNumber = CurrentLine.ID - _connection.TerminalHeight + 2; //これで次のCurrentLineNumber++と合わせて行送りになる
                    }
                }
                CurrentLineNumber++; //これでプロパティセットがなされ、必要なら行の追加もされる。
            }
            AssertValid();

            //Debug.WriteLine(String.Format("c={0} t={1} f={2} l={3}", _currentLine.ID, _topLine.ID, _firstLine.ID, _lastLine.ID));
        }

        //スクロール範囲の最も下を１行消し、最も上に１行追加。現在行はその新規行になる。
        internal void ScrollUp()
        {
            if (ScrollingTop != -1 && ScrollingBottom != -1)
            {
                ScrollUp(ScrollingTop, ScrollingBottom);
            }
            else
            {
                ScrollUp(TopLineNumber, TopLineNumber + _connection.TerminalHeight - 1);
            }
        }

        internal void ScrollUp(int from, int to)
        {
            GLine top = FindLineOrEdge(from);
            GLine bottom = FindLineOrEdge(to);
            if (top == null || bottom == null)
            {
                return; //エラーハンドリングはFindLineの中で。ここではクラッシュ回避だけを行う
            }

            int bottom_id = bottom.ID;
            int topline_id = TopLine.ID;
            GLine nextbottom = bottom.NextLine;

            if (from == to)
            {
                CurrentLine = top;
                CurrentLine.Clear();
            }
            else
            {
                Remove(bottom);
                CurrentLine = new GLine(_connection.TerminalWidth);

                InsertBefore(top, CurrentLine);
                GLine c = CurrentLine;
                do
                {
                    c.ID = from++;
                    c = c.NextLine;
                } while (c != nextbottom);
                Debug.Assert(nextbottom == null || nextbottom.ID == from);
            }
            /*
			//id maintainance
			GLine c = newbottom;
			GLine end = _currentLine.PrevLine;
			while(c != end) {
				c.ID = bottom_id--;
				c = c.PrevLine;
			}
			*/

            //!!次の２行はxtermをやっている間に発見して修正。 VT100では何かの必要があってこうなったはずなので後で調べること
            //if(_scrollingTop<=_topLine.ID && _topLine.ID<=_scrollingBottom)
            //	_topLine = _currentLine;
            while (topline_id < TopLine.ID)
            {
                TopLine = TopLine.PrevLine;
            }

            AssertValid();

            InvalidatedAll = true;
        }

        //スクロール範囲の最も上を１行消し、最も下に１行追加。現在行はその新規行になる。
        internal void ScrollDown()
        {
            if (ScrollingTop != -1 && ScrollingBottom != -1)
            {
                ScrollDown(ScrollingTop, ScrollingBottom);
            }
            else
            {
                ScrollDown(TopLineNumber, TopLineNumber + _connection.TerminalHeight - 1);
            }
        }

        internal void ScrollDown(int from, int to)
        {
            GLine top = FindLineOrEdge(from);
            GLine bottom = FindLineOrEdge(to);
            int top_id = top.ID;
            GLine newtop = top.NextLine;

            if (from == to)
            {
                CurrentLine = top;
                CurrentLine.Clear();
            }
            else
            {
                Remove(top); //_topLineの調整は必要ならここで行われる
                CurrentLine = new GLine(_connection.TerminalWidth);
                InsertAfter(bottom, CurrentLine);

                //id maintainance
                GLine c = newtop;
                GLine end = CurrentLine.NextLine;
                while (c != end)
                {
                    c.ID = top_id++;
                    c = c.NextLine;
                }
            }
            AssertValid();

            InvalidatedAll = true;
        }

        internal void Replace(GLine target, GLine newline)
        {
            newline.NextLine = target.NextLine;
            newline.PrevLine = target.PrevLine;
            if (target.NextLine != null)
            {
                target.NextLine.PrevLine = newline;
            }

            if (target.PrevLine != null)
            {
                target.PrevLine.NextLine = newline;
            }

            if (target == FirstLine)
            {
                FirstLine = newline;
            }

            if (target == LastLine)
            {
                LastLine = newline;
            }

            if (target == TopLine)
            {
                TopLine = newline;
            }

            if (target == CurrentLine)
            {
                CurrentLine = newline;
            }

            newline.ID = target.ID;
            InvalidateLine(newline.ID);
            AssertValid();
        }
        internal void ReplaceCurrentLine(GLine line)
        {
#if DEBUG
            Replace(CurrentLine, line);
            AssertValid();
#else
			if(_currentLine!=null) //クラッシュレポートをみると、何かの拍子にnullになっていたとしか思えない
				Replace(_currentLine, line);
#endif
        }



        internal void Remove(GLine line)
        {
            if (Size <= 1)
            {
                Clear();
                return;
            }

            if (line.PrevLine != null)
            {
                line.PrevLine.NextLine = line.NextLine;
            }
            if (line.NextLine != null)
            {
                line.NextLine.PrevLine = line.PrevLine;
            }

            if (line == FirstLine)
            {
                FirstLine = line.NextLine;
            }

            if (line == LastLine)
            {
                LastLine = line.PrevLine;
            }

            if (line == TopLine)
            {
                TopLine = line.NextLine;
            }
            if (line == CurrentLine)
            {
                CurrentLine = line.NextLine;
                if (CurrentLine == null)
                {
                    CurrentLine = LastLine;
                }
            }

            Size--;
            InvalidatedAll = true;
        }

        private void InsertBefore(GLine pos, GLine line)
        {
            if (pos.PrevLine != null)
            {
                pos.PrevLine.NextLine = line;
            }

            line.PrevLine = pos.PrevLine;
            line.NextLine = pos;

            pos.PrevLine = line;

            if (pos == FirstLine)
            {
                FirstLine = line;
            }

            Size++;
            InvalidatedAll = true;
        }
        private void InsertAfter(GLine pos, GLine line)
        {
            if (pos.NextLine != null)
            {
                pos.NextLine.PrevLine = line;
            }

            line.NextLine = pos.NextLine;
            line.PrevLine = pos;

            pos.NextLine = line;

            if (pos == LastLine)
            {
                LastLine = line;
            }

            Size++;
            InvalidatedAll = true;
        }

        internal void RemoveAfter(int from)
        {
            if (from > LastLine.ID)
            {
                return;
            }

            GLine delete = FindLineOrEdge(from);
            if (delete == null)
            {
                return;
            }

            GLine remain = delete.PrevLine;
            delete.PrevLine = null;
            if (remain == null)
            {
                Clear();
            }
            else
            {
                remain.NextLine = null;
                LastLine = remain;

                while (delete != null)
                {
                    Size--;
                    if (delete == TopLine)
                    {
                        TopLine = remain;
                    }

                    if (delete == CurrentLine)
                    {
                        CurrentLine = remain;
                    }

                    delete = delete.NextLine;
                }
            }

            AssertValid();
            InvalidatedAll = true;
        }

        internal void ClearAfter(int from)
        {
            if (from > LastLine.ID)
            {
                return;
            }

            GLine l = FindLineOrEdge(from);
            if (l == null)
            {
                return;
            }

            while (l != null)
            {
                l.Clear();
                l = l.NextLine;
            }

            AssertValid();
            InvalidatedAll = true;
        }
        internal void ClearAfter(int from, TextDecoration dec)
        {
            if (from > LastLine.ID)
            {
                return;
            }

            GLine l = FindLineOrEdge(from);
            if (l == null)
            {
                return;
            }

            while (l != null)
            {
                l.Clear(dec);
                l = l.NextLine;
            }

            AssertValid();
            InvalidatedAll = true;
        }

        internal void ClearRange(int from, int to)
        {
            GLine l = FindLineOrEdge(from);
            if (l == null)
            {
                return;
            }

            while (l.ID < to)
            {
                l.Clear();
                InvalidateLine(l.ID);
                l = l.NextLine;
            }
            AssertValid();
        }
        internal void ClearRange(int from, int to, TextDecoration dec)
        {
            GLine l = FindLineOrEdge(from);
            if (l == null)
            {
                return;
            }

            while (l.ID < to)
            {
                l.Clear(dec);
                InvalidateLine(l.ID);
                l = l.NextLine;
            }
            AssertValid();
        }

        /// <summary>
        /// 最後のremain行以前を削除する
        /// </summary>
        internal int DiscardOldLines(int remain)
        {
            int delete_count = Size - remain;
            if (delete_count <= 0)
            {
                return 0;
            }

            GLine newfirst = FirstLine;
            for (int i = 0; i < delete_count; i++)
            {
                newfirst = newfirst.NextLine;
            }

            //新しい先頭を決める
            FirstLine = newfirst;
            newfirst.PrevLine.NextLine = null;
            newfirst.PrevLine = null;
            Size -= delete_count;
            Debug.Assert(Size == remain);

            AssertValid();

            if (TopLine.ID < FirstLine.ID)
            {
                TopLine = FirstLine;
            }

            if (CurrentLine.ID < FirstLine.ID)
            {
                CurrentLine = FirstLine;
                CaretColumn = 0;
            }

            return delete_count;
        }

        //再接続用に現在ドキュメントの前に挿入
        public void InsertBefore(TerminalDocument olddoc, int paneheight)
        {
            lock (this)
            {
                GLine c = olddoc.LastLine;
                int offset = CurrentLine.ID - TopLine.ID;
                bool flag = false;
                while (c != null)
                {
                    if (flag || c.Text[0] != '\0')
                    {
                        flag = true;
                        GLine nl = c.Clone();
                        nl.ID = FirstLine.ID - 1;
                        InsertBefore(FirstLine, nl); //最初に空でない行があれば以降は全部挿入
                        offset++;
                    }
                    c = c.PrevLine;
                }

                //IDが負になるのはちょっと怖いので修正
                if (FirstLine.ID < 0)
                {
                    int t = -FirstLine.ID;
                    c = FirstLine;
                    while (c != null)
                    {
                        c.ID += t;
                        c = c.NextLine;
                    }
                }

                TopLine = FindLineOrEdge(CurrentLine.ID - Math.Min(offset, paneheight));
                //Dump("insert doc");
            }
        }


        public void Dump(string title)
        {
            Debug.WriteLine("<<<< DEBUG DUMP [" + title + "] >>>>");
            Debug.WriteLine(String.Format("[size={0} top={1} current={2} caret={3} first={4} last={5} region={6},{7}]", Size, TopLineNumber, CurrentLineNumber, CaretColumn, FirstLineNumber, LastLineNumber, ScrollingTop, ScrollingBottom));
            GLine gl = FindLineOrEdge(TopLineNumber);
            int count = 0;
            while (gl != null && count++ < _connection.TerminalHeight)
            {
                Debug.Write(String.Format("{0,3}", gl.ID));
                Debug.Write(":");
                Debug.Write(GLineManipulator.SafeString(gl.Text));
                Debug.Write(":");
                Debug.WriteLine(gl.EOLType);
                gl = gl.NextLine;
            }
        }

        public virtual void AssertValid()
        {
#if false
			Debug.Assert(_currentLine.ID>=0);
			Debug.Assert(_currentLine.ID>=_topLine.ID);
			GLine l = _topLine;
			GLine n = l.NextLine;
			while(n!=null) {
				Debug.Assert(l.ID+1==n.ID);
				Debug.Assert(l==n.PrevLine);
				l = n;
				n = n.NextLine;
			}
#endif
        }

    }

}
