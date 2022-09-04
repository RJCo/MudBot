/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: Connections.cs,v 1.2 2005/04/20 08:45:46 okajima Exp $
*/
using Poderosa.Communication;
using Poderosa.Terminal;
using Poderosa.Text;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using ThTimer = System.Threading.Timer;

namespace Poderosa.Connection
{
    /// <summary>
    /// �A�v���P�[�V�����S�̂ł̃R�l�N�V�����̃��X�g�ƁA���ꂪ�ǂ̃y�C���Ɋ֘A�t�����Ă��邩���Ǘ�����
    /// </summary>
    public class Connections : IEnumerable
    {
        //�ڑ����J�������Ɋi�[�����ConnectionTag�z��
        private ArrayList _connections;

        //Active�ȋt���Ɋi�[�����z��
        private ArrayList _activatedOrder;

        internal Connections()
        {
            _connections = new ArrayList();
            ActiveIndex = -1;
            KeepAlive = new KeepAlive();

            _activatedOrder = new ArrayList();
        }
        internal KeepAlive KeepAlive { get; }

        public void Add(ConnectionTag t)
        {
            Debug.Assert(t != null);
            t.PositionIndex = GEnv.Frame.PositionForNextConnection;
            t.PreservedPositionIndex = t.PositionIndex;
            _connections.Add(t);
            _activatedOrder.Add(t);
        }

        internal void Remove(TerminalConnection con)
        {
            int i = IndexOf(con);
            if (i == -1)
            {
                return; //�{���͂��������̂͂�낵���Ȃ���
            }

            ConnectionTag ct = TagAt(i);
            _connections.RemoveAt(i);
            _activatedOrder.Remove(ct);
            ActiveIndex = Math.Min(ActiveIndex, _connections.Count - 1);
        }
        public void Replace(ConnectionTag old, ConnectionTag ct)
        {
            int i = IndexOf(old);
            Debug.Assert(i != -1);
            _connections[i] = ct;

            i = _activatedOrder.IndexOf(old);
            Debug.Assert(i != -1);
            _activatedOrder[i] = ct;
        }
        public void Clear()
        {
            _connections.Clear();
            _activatedOrder.Clear();
            ActiveIndex = -1;
        }
        public void CloseAllConnections()
        {
            ArrayList target = new ArrayList(_connections); //���[�J���R�s�[�����Ȃ��Ɣ񓯊��ɃR���N�V�������ύX���ꂩ�˂Ȃ�
            foreach (ConnectionTag ct in target)
            {
                ct.Connection.Close();
                ct.IsTerminated = true;
            }
        }
        public ArrayList GetSnapshot()
        {
            return new ArrayList(_connections);
        }
        public int Count => _connections.Count;

        public bool LiveConnectionsExist
        {
            get
            {
                foreach (ConnectionTag ct in _connections)
                {
                    if (!ct.Connection.IsClosed)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _connections.GetEnumerator();
        }
        public IEnumerable OrderedConnections
        {
            get
            {
                ArrayList t = new ArrayList(_activatedOrder);
                t.Reverse();
                return t;
            }
        }
        public int ActiveIndex { get; private set; }

        public void BringToActivationOrderTop(ConnectionTag ct)
        {
            ActiveIndex = _connections.IndexOf(ct);
            _activatedOrder.Remove(ct);
            _activatedOrder.Add(ct);
        }
        public TerminalConnection ActiveConnection
        {
            get
            {
                if (ActiveIndex == -1)
                {
                    return null;
                }
                else
                {
                    return TagAt(ActiveIndex).Connection;
                }
            }
        }

        //positionIndex����v���Aexcluding�ł͂Ȃ����Ŏ���Active�ɂȂ����Ԃ��B�Ȃ����null
        public ConnectionTag GetCandidateOfActivation(int positionIndex, ConnectionTag excluding)
        {
            for (int i = _activatedOrder.Count - 1; i >= 0; i--)
            {
                ConnectionTag ct = (ConnectionTag)_activatedOrder[i];
                if (ct.PositionIndex == positionIndex && ct != excluding)
                {
                    return ct;
                }
            }
            return null;
        }
        //Preserved�Ȃ���݂Č���
        public ConnectionTag GetCandidateOfLocation(int positionIndex, ConnectionTag excluding)
        {
            for (int i = _activatedOrder.Count - 1; i >= 0; i--)
            {
                ConnectionTag ct = (ConnectionTag)_activatedOrder[i];
                if (ct.PreservedPositionIndex == positionIndex && ct != excluding)
                {
                    return ct;
                }
            }
            return null;
        }


        public ConnectionTag ActiveTag
        {
            get
            {
                if (ActiveIndex == -1)
                {
                    return null;
                }
                else
                {
                    return TagAt(ActiveIndex);
                }
            }
        }

        public ConnectionTag TagAt(int index)
        {
            return (ConnectionTag)_connections[index];
        }
        public ConnectionTag FindTag(TerminalConnection con)
        {
            foreach (ConnectionTag t in _connections)
            {
                if (t.Connection == con)
                {
                    return t;
                }
            }
            return null;
        }
        public int IndexOf(TerminalConnection con)
        {
            int i = 0;
            foreach (ConnectionTag t in _connections)
            {
                if (t.Connection == con)
                {
                    return i;
                }

                i++;
            }
            return -1;
        }
        public int IndexOf(ConnectionTag tag)
        {
            int i = 0;
            foreach (ConnectionTag t in _connections)
            {
                if (t == tag)
                {
                    return i;
                }

                i++;
            }
            return -1;
        }


        public ConnectionTag NextConnection(ConnectionTag c)
        {
            int i = IndexOf(c);
            return TagAt(i == _connections.Count - 1 ? 0 : i + 1);
        }
        public ConnectionTag PrevConnection(ConnectionTag c)
        {
            int i = IndexOf(c);
            return TagAt(i == 0 ? _connections.Count - 1 : i - 1);
        }
        public void Reorder(int index, int newindex)
        {
            ConnectionTag ct = (ConnectionTag)_connections[index];
            _connections.RemoveAt(index);
            _connections.Insert(newindex, ct);
            if (ActiveIndex == index)
            {
                ActiveIndex = newindex;
            }
        }

        public ConnectionTag FirstMatch(TagCondition cond)
        {
            foreach (ConnectionTag ct in _connections)
            {
                if (cond(ct))
                {
                    return ct;
                }
            }
            return null;
        }

        internal void Dump()
        {
            Debug.WriteLine("Connection List");
            foreach (ConnectionTag ct in _connections)
            {
                Debug.WriteLine(String.Format("pos={0} close={1} pane={2} visible={3}", ct.PositionIndex, ct.Connection.IsClosed, (ct.Pane != null), (ct.Pane != null && ct.Pane.FakeVisible)));
                if (ct.Pane != null)
                {
                    Debug.Assert(ct.Pane.Connection == ct.Connection);
                }
            }
        }

    }

    public delegate bool TagCondition(ConnectionTag pane);

    //Invalidate�ɕK�v�ȃp�����[�^
    internal class InvalidateParam
    {
        private Delegate _delegate;
        private object[] _param;
        private bool _set;
        public void Set(Delegate d, object[] p)
        {
            _delegate = d;
            _param = p;
            _set = true;
        }
        public void Reset()
        {
            _set = false;
        }
        public void InvokeFor(Control c)
        {
            if (_set)
            {
                c.Invoke(_delegate, _param);
            }
        }
    }


    //�ڑ��ɑ΂��Ċ֘A�t����f�[�^
    [Serializable]
    public class ConnectionTag
    {
        private Control _tabButton; //�^�u�̒��ɓ���{�^��

        private RenderProfile _renderProfile;
        private IModalTerminalTask _modalTerminalTask;
        private Process _childProcess;

        //�E�B���h�E�̕\���p�e�L�X�g
        internal string _windowTitle; //�z�X�gOSC�V�[�P���X�Ŏw�肳�ꂽ�^�C�g��

        private bool _terminated;
        private int _positionIndex;
        private int _preservedPositionIndex; //Single���[�h�ɂȂ����Ƃ��̂��߂̑ޔ�p

        private ThTimer _timer;

        public ConnectionTag(TerminalConnection c)
        {
            Connection = c;
            AttachedPane = null;
            InvalidateParam = new InvalidateParam();
            _tabButton = null;
            Document = new TerminalDocument(Connection);
            Receiver = new TerminalDataReceiver(this);
            _terminated = false;
            _timer = null;
            _windowTitle = "";

            //null�̂Ƃ��̓f�t�H���g�v���t�@�C�����g��
            _renderProfile = c.Param.RenderProfile;

            //VT100�w��ł�xterm�V�[�P���X�𑗂��Ă���A�v���P�[�V��������������Ȃ��̂�
            Terminal = new XTerm(this, new JapaneseCharDecoder(Connection));
            /*
			if(c.Param.TerminalType==TerminalType.XTerm || c.Param.TerminalType==TerminalType.KTerm)
				_terminal = new XTerm(this, new JapaneseCharDecoder(_connection));
			else
				_terminal = new VT100Terminal(this, new JapaneseCharDecoder(_connection));
			*/
            GEnv.Connections.KeepAlive.SetTimerToConnectionTag(this);
        }

        //�h�L�������g�X�V�ʒm ��M�X���b�h�ł̎��s�Ȃ̂Œ���
        public interface IEventReceiver
        {
            void OnUpdate();
            void OnDisconnect();
        }
        private IEventReceiver _eventReceiver;

        public IEventReceiver EventReceiver
        {
            get => _eventReceiver;
            set => _eventReceiver = value;
        }
        public IModalTerminalTask ModalTerminalTask
        {
            get => _modalTerminalTask;
            set => _modalTerminalTask = value;
        }
        public Process ChildProcess
        {
            get => _childProcess;
            set => _childProcess = value;
        }

        internal InvalidateParam InvalidateParam { get; }


        public string WindowTitle
        {
            get => _windowTitle;
            set => _windowTitle = value;
        }
        public string FormatTabText()
        {
            string t = Connection.Param.Caption;
            if (t == null || t.Length == 0)
            {
                t = Connection.Param.ShortDescription;
            }

            if (Connection.IsClosed)
            {
                t += "Caption.ConnectionTag.Disconnected";
            }

            if (_modalTerminalTask != null)
            {
                t += "(" + _modalTerminalTask.Caption + ")";
            }

            return t;
        }
        //Frame�̃L���v�V�����p������
        public string FormatFrameText()
        {
            string t = FormatTabText();
            if (_windowTitle.Length != 0 && Connection.Param.Caption != _windowTitle) //TabCaption��WindowTitle�Ɉ�v������I�v�V�������g���Ă���ƟT�������Ȃ�̂ňقȂ�Ƃ��̂ݕ\��
            {
                t += "[" + _windowTitle + "]";
            }

            return t;
        }

        public Control Button
        {
            get => _tabButton;
            set => _tabButton = value;
        }
        public TerminalDocument Document { get; }

        public TerminalConnection Connection { get; }

        public TerminalPane AttachedPane { get; private set; }

        public ITerminal Terminal { get; }

        public TerminalDataReceiver Receiver { get; }

        public RenderProfile RenderProfile
        {
            get => _renderProfile;
            set
            {
                _renderProfile = value;
                Connection.Param.RenderProfile = value;
            }
        }
        internal RenderProfile GetCurrentRenderProfile()
        {
            return _renderProfile == null ? GEnv.DefaultRenderProfile : _renderProfile;
        }

        internal TerminalPane Pane
        {
            get => AttachedPane;
            set => AttachedPane = value;
        }
        internal ThTimer Timer
        {
            get => _timer;
            set => _timer = value;
        }

        internal void NotifyUpdate()
        {
            if (AttachedPane != null)
            {
                AttachedPane.DataArrived();
            }

            Terminal.SignalData();

            if (_eventReceiver != null)
            {
                _eventReceiver.OnUpdate();
            }
        }
        internal void NotifyDisconnect()
        {
            if (_eventReceiver != null)
            {
                _eventReceiver.OnDisconnect();
            }
        }


        public int PositionIndex
        {
            get => _positionIndex;
            set => _positionIndex = value;
        }

        public int PreservedPositionIndex
        {
            get => _preservedPositionIndex;
            set => _preservedPositionIndex = value;
        }
        public bool IsTerminated
        {
            get => _terminated;
            set
            {
                _terminated = value;
                if (value && _childProcess != null)
                {
                    try
                    {
                        //_childProcess.Kill(); ����Kill����Ƃ܂���bash�c���ł܂���
                        _childProcess = null;
                    }
                    catch (Exception)
                    { //���Ƀ\�P�b�g�ؒf�ɋN������Η�O�ɂȂ邱�Ƃ����邩������Ȃ�
                    }
                }
                if (value)
                {
                    GEnv.Connections.KeepAlive.ClearTimerToConnectionTag(this);
                }
            }
        }

        public void ImportProperties(ConnectionTag src)
        {
            _renderProfile = src.RenderProfile;
            _positionIndex = src.PositionIndex;
            _preservedPositionIndex = src.PreservedPositionIndex;

            if (src.Button != null)
            {
                src.Button.Tag = this;
            }

            _tabButton = src.Button;
        }
    }
}
