/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: ConnectionHistory.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using Poderosa.ConnectionParam;
using System.Collections;
using System.Collections.Specialized;

namespace Poderosa.Config
{
    /// <summary>
    /// �ߋ��̐ڑ������̕ێ��B�V���A���C�Y�@�\���܂ށB
    /// </summary>
    internal class ConnectionHistory : IEnumerable
    {

        protected ArrayList _history; //TCPTerminalParam�̃R���N�V����

        public ConnectionHistory()
        {
            _history = new ArrayList();
        }

        public IEnumerator GetEnumerator()
        {
            return _history.GetEnumerator();
        }

        public void Clear()
        {
            _history.Clear();
        }

        public void Append(TerminalParam tp)
        {
            _history.Add(tp);
        }

        public int Count
        {
            get
            {
                return _history.Count;
            }
        }

        public TCPTerminalParam TopTCPParam
        {
            get
            {
                foreach (TerminalParam p in _history)
                {
                    TCPTerminalParam tp = p as TCPTerminalParam;
                    if (tp != null)
                    {
                        return tp;
                    }
                }
                return new TelnetTerminalParam("");
            }
        }

        public TCPTerminalParam SearchByHost(string host)
        {
            foreach (TerminalParam p in _history)
            {
                TCPTerminalParam tp = p as TCPTerminalParam;
                if (tp != null && tp.Host == host)
                {
                    return tp;
                }
            }
            return null;
        }

        public void LimitCount(int count)
        {
            if (_history.Count > count)
            {
                _history.RemoveRange(count, _history.Count - count);
            }
        }

        public void Update(TerminalParam newparam_)
        {
            int n = 0;
            TerminalParam newparam = (TerminalParam)newparam_.Clone();
            newparam.LogPath = "";
            newparam.LogType = LogType.None;
            foreach (TerminalParam p in _history)
            {
                if (p.Equals(newparam))
                {
                    _history.RemoveAt(n);
                    _history.Insert(0, newparam);
                    return;
                }
                n++;
            }

            _history.Insert(0, newparam);
            if (_history.Count > 100)
            {
                _history.RemoveRange(GApp.Options.MRUSize, _history.Count - 100);
            }
        }

        public void ReplaceIdenticalParam(TerminalParam newparam_)
        {
            int n = 0;
            TerminalParam newparam = (TerminalParam)newparam_.Clone();
            newparam.LogPath = "";
            newparam.LogType = LogType.None;
            foreach (TerminalParam p in _history)
            {
                if (p.Equals(newparam))
                {
                    _history[n] = newparam;
                    return;
                }
                n++;
            }
        }
        public void Save(ConfigNode parent)
        {
            LimitCount(GApp.Options.MRUSize);

            ConfigNode node = new ConfigNode("connection-history");
            foreach (TerminalParam p in _history)
            {
                ConfigNode con = new ConfigNode("connection");
                p.Export(con);
                node.AddChild(con);
            }
            parent.AddChild(node);
        }
        public void Load(ConfigNode parent)
        {
            ConfigNode node = parent.FindChildConfigNode("connection-history");
            if (node != null)
            {
                foreach (ConfigNode ch in node.Children)
                {
                    _history.Add(TerminalParam.CreateFromConfigNode(ch));
                }
            }
        }


        private delegate string StrProp(TerminalParam p);
        private delegate int IntProp(TerminalParam p);

        private string ReturnHost(TerminalParam p)
        {
            TCPTerminalParam pp = p as TCPTerminalParam;
            return pp == null ? null : pp.Host;
        }

        private int ReturnPort(TerminalParam p)
        {
            TCPTerminalParam pp = p as TCPTerminalParam;
            return pp == null ? -1 : pp.Port;
        }

        private string ReturnLogPath(TerminalParam p)
        {
            return p.LogPath;
        }

        private StringCollection CollectString(StrProp prop)
        {
            StringCollection result = new StringCollection();
            foreach (TerminalParam param in _history)
            {
                string t = prop(param);
                if (t != null && t.Length > 0 && !result.Contains(t))
                {
                    result.Add(t);
                }
            }
            return result;
        }

        private int[] CollectInt(IntProp prop, ArrayList result)
        {
            foreach (TerminalParam param in _history)
            {
                int t = prop(param);
                if (t > 0 && !result.Contains(t))
                {
                    result.Add(t);
                }
            }
            return (int[])result.ToArray(typeof(int));
        }

        public StringCollection Hosts
        {
            get
            {
                return CollectString(new StrProp(ReturnHost));
            }
        }

        public int[] Ports
        {
            get
            {
                ArrayList a = new ArrayList();
                a.Add(23);
                a.Add(22); //Telnet���ɕ\������
                return CollectInt(new IntProp(ReturnPort), a);
            }
        }

        public StringCollection LogPaths
        {
            get
            {
                return CollectString(new StrProp(ReturnLogPath));
            }
        }
    }



}
