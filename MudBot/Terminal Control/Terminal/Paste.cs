/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: Paste.cs,v 1.2 2005/04/20 08:45:47 okajima Exp $
*/
using Poderosa.Connection;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Poderosa.Terminal
{
    public class PasteProcessor : IModalTerminalTask
    {
        private ArrayList _data;
        private ConnectionTag _tag;
        private int _length;
        private bool _async;
        private bool _abortFlag;

        public delegate void EventHandler(int param);

        public void Input(byte[] data, int offset, int length)
        {
        }
        public bool CanReceive => false;

        public string Caption => "Caption.ConnectionCommandTarget.DuringPaste";

        public int LineCount => _data.Count;

        public event EventHandler LineProcessed;

        public PasteProcessor(ConnectionTag tag, string text)
        {
            _tag = tag;
            StringReader r = new StringReader(text);
            Fill(r);
            r.Close();
        }
        public PasteProcessor(ConnectionTag tag, TextReader reader)
        {
            _tag = tag;
            Fill(reader);
        }
        private void Fill(TextReader reader)
        {
            _data = new ArrayList();
            string l = reader.ReadLine();
            while (l != null)
            {
                _data.Add(l);
                _length += l.Length;
                l = reader.ReadLine();
            }
        }
        public CommandResult Perform()
        {
            const int lim = 1000;
            if (_length < lim)
            { //1000字以下なら直行便でやっつける
                _async = false;
                return Paste();
            }
            else
            {
                _async = true;
                _tag.ModalTerminalTask = this;
                new Thread(Run).Start();
                return CommandResult.Success;
            }
        }
        private void Run()
        {
            try
            {
                Paste();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                GEnv.InterThreadUIService.Warning("Message.ConnectionCommandTarget.SendError" + ex.Message);
            }
        }

        public void SetAbortFlag()
        {
            _abortFlag = true;
        }

        private CommandResult Paste()
        {
            //改行は送り先ペインの改行設定に合わせて送られる。ただし、最終要素は改行文字で終わっているか否かで挙動が変わる
            if (_async)
            {
                RefreshConnection();
            }

            for (int i = 0; i < _data.Count - 1; i++)
            {
                SendLine((string)_data[i], true);
                if (_abortFlag)
                {
                    return CommandResult.Cancelled;
                }

                if (LineProcessed != null)
                {
                    LineProcessed(i);
                }
            }

            string lastline = (string)_data[_data.Count - 1];
            char last = lastline.Length == 0 ? '\0' : lastline[lastline.Length - 1];
            if (last == '\n' || last == '\r')
            {
                SendLine(lastline, true);
            }
            else
            {
                SendLine(lastline, false);
            }

            if (_async)
            {
                _tag.ModalTerminalTask = null;
                RefreshConnection();
            }
            if (LineProcessed != null)
            {
                LineProcessed(-1);
            }

            return CommandResult.Success;
        }
        private void SendLine(string line, bool appendnl)
        {
            if (_tag.Connection.IsClosed || GEnv.Frame.MacroIsRunning)
            {
                return;
            }

            //GEnv.Frame.StatusBar.IndicateSendData();
            if (appendnl)
            {
                line += new string(TerminalUtil.NewLineChars(_tag.Connection.Param.TransmitNL));
            }

            char[] chars = line.ToCharArray();
            if (_tag.Connection.Param.LocalEcho)
            {
                lock (_tag.Document)
                {
                    _tag.Terminal.Input(chars, 0, chars.Length);
                }
            }

            _tag.Connection.WriteChars(chars);
        }
        private void RefreshConnection()
        {
            //InterThreadはもともと描画スレッドから呼ぶためのもので、Documentをロックしているのが前提。なのであえてロックしてから呼びにいくことで調整をとる
            lock (_tag.Document)
            {
                GEnv.InterThreadUIService.RefreshConnection(_tag);
            }
        }
    }
}
