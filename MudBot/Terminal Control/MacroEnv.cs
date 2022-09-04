/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: MacroEnv.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Config;
using Poderosa.Connection;
using Poderosa.ConnectionParam;
using Poderosa.Macro;
using Poderosa.Terminal;
using System;
using System.Collections;
using PConnection = Poderosa.Macro.Connection;

namespace Poderosa.MacroEnv
{
    internal class ConnectionListImpl : ConnectionList
    {
        public virtual int Count => GEnv.Connections.Count;

        public override IEnumerator GetEnumerator()
        {
            return new EnumeratorWrapper(GEnv.Connections.GetEnumerator());
        }
        public override PConnection ActiveConnection => GEnv.Connections.ActiveConnection == null ? null : new ConnectionImpl(GEnv.Connections.ActiveTag);

        public override PConnection Open(TerminalParam param)
        {
            ConnectionTag con = GApp.InterThreadUIService.OpenConnection(param);
            return con == null ? null : new ConnectionImpl(con);
        }
        public virtual PConnection OpenShortcutFile(string filename)
        {
            ConnectionTag con = GApp.InterThreadUIService.OpenShortcut(filename);
            return con == null ? null : new ConnectionImpl(con);
        }


    }
    internal class EnumeratorWrapper : IEnumerator
    {
        private IEnumerator _inner;

        public EnumeratorWrapper(IEnumerator i)
        {
            _inner = i;
        }

        public void Reset()
        {
            _inner.Reset();
        }
        public bool MoveNext()
        {
            return _inner.MoveNext();
        }
        public object Current => new ConnectionImpl((ConnectionTag)_inner.Current);
    }

    internal class ConnectionImpl : PConnection
    {

        private readonly ConnectionTag _tag;

        public ConnectionImpl(ConnectionTag t)
        {
            _tag = t;
        }

        public override string ReceiveLine()
        {
            return _tag.Terminal.ReadLineFromMacroBuffer();
        }
        public override string ReceiveData()
        {
            return _tag.Terminal.ReadAllFromMacroBuffer();
        }
        public override void WriteComment(string comment)
        {
            _tag.Terminal.Logger.Comment(comment);
        }
    }

    internal class FrameImpl : Frame
    {
        public virtual GFrameStyle FrameStyle
        {
            get => GApp.Options.FrameStyle;
            set => GApp.InterThreadUIService.SetFrameStyle(value, -1, -1, -1, -1);
        }

        public virtual void SetStyleS(int width, int height)
        {
            if (width <= 0 || width >= 256)
            {
                throw new ArgumentException("Message.MacroEnv.WidthIsOutOfRange");
            }

            if (height <= 0 || height >= 256)
            {
                throw new ArgumentException("Message.MacroEnv.HeightIsOutOfRange");
            }

            GApp.InterThreadUIService.SetFrameStyle(GFrameStyle.Single, width, height, -1, -1);
        }
        public virtual void SetStyleH(int width, int height1, int height2)
        {
            if (width <= 0 || width >= 256)
            {
                throw new ArgumentException("Message.MacroEnv.WidthIsOutOfRange");
            }

            if (height1 <= 0 || height1 >= 256)
            {
                throw new ArgumentException("Message.MacroEnv.HeightIsOutOfRange");
            }

            if (height2 <= 0 || height2 >= 256)
            {
                throw new ArgumentException("Message.MacroEnv.HeightIsOutOfRange");
            }

            GApp.InterThreadUIService.SetFrameStyle(GFrameStyle.DivHorizontal, width, height1, width, height2);
        }
        public virtual void SetStyleV(int width1, int width2, int height)
        {
            if (width1 <= 0 || width1 >= 256)
            {
                throw new ArgumentException("Message.MacroEnv.WidthIsOutOfRange");
            }

            if (width2 <= 0 || width2 >= 256)
            {
                throw new ArgumentException("Message.MacroEnv.WidthIsOutOfRange");
            }

            if (height <= 0 || height >= 256)
            {
                throw new ArgumentException("Message.MacroEnv.HeightIsOutOfRange");
            }

            GApp.InterThreadUIService.SetFrameStyle(GFrameStyle.DivVertical, width1, height, width2, height);
        }
    }

    internal class UtilImpl : Util
    {
        public virtual void MessageBox(string msg)
        {
            GApp.InterThreadUIService.Warning(msg);
        }
        public virtual void ShellExecute(string verb, string filename)
        {
            int r = Win32.ShellExecute(Win32.GetDesktopWindow(), verb, filename, "", "", 1).ToInt32(); //1‚ÍSW_SHOWNORMAL
            if (r <= 31)
            {
                throw new ArgumentException(String.Format("Message.MacroEnv.ShellExecuteError", verb, filename));
            }
        }
        public virtual void Exec(string command)
        {
            int r = Win32.WinExec(command, 1);
            if (r <= 31)
            {
                throw new ArgumentException(String.Format("Message.MacroEnv.ExecError", command));
            }
        }
    }

    internal class DebugServiceImpl : DebugService
    {

        private MacroTraceWindow _debugWindow;

        public DebugServiceImpl(MacroTraceWindow dw)
        {
            _debugWindow = dw;
        }

        public virtual void Trace(string msg)
        {
            if (_debugWindow == null || _debugWindow.IsDisposed)
            {
                return;
            }

            _debugWindow.AddLine(msg);
        }
        public virtual void PrintStackTrace()
        {
            if (_debugWindow == null || _debugWindow.IsDisposed)
            {
                return;
            }

            string[] s = System.Environment.StackTrace.Split('\n', '\r');
            //‚±‚ÌPrintStackTrace‚©‚çæ‚ª•K—v
            bool f = false;
            foreach (string l in s)
            {
                if (f && l.Length > 0)
                {
                    _debugWindow.AddLine(l);
                }

                if (!f && l.IndexOf("PrintStackTrace") != -1)
                {
                    f = true;
                }
            }
        }
    }
}
