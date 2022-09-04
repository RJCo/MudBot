/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: TerminalConnection.cs,v 1.2 2005/04/20 08:45:47 okajima Exp $
*/
using Poderosa.Connection;
using Poderosa.ConnectionParam;
using Poderosa.Log;
using Poderosa.Text;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Poderosa.Communication
{
    public interface IDataReceiver
    {
        void DataArrived(byte[] buf, int offset, int count);
        void DisconnectedFromServer();
        void ErrorOccurred(string msg);
    }

    internal abstract class AbstractGuevaraSocket
    {

        protected IDataReceiver _callback;

        internal abstract void Transmit(byte[] data, int offset, int length);
        internal abstract void Flush();
        internal abstract void Close();
        internal abstract bool DataAvailable { get; }
        internal abstract void RepeatAsyncRead(IDataReceiver receiver);
    }

    internal class PlainGuevaraSocket : AbstractGuevaraSocket
    {
        private Socket _socket;
        private byte[] _buf;
        internal PlainGuevaraSocket(Socket s)
        {
            _socket = s;
            _buf = new byte[0x1000];
        }
        internal override bool DataAvailable => _socket.Available > 0;

        internal override void Transmit(byte[] data, int offset, int length)
        {
            _socket.Send(data, offset, length, SocketFlags.None);
        }
        internal override void Flush()
        {
        }
        internal override void Close()
        {
            _socket.Close();
        }
        internal override void RepeatAsyncRead(IDataReceiver receiver)
        {
            _callback = receiver;
            _socket.BeginReceive(_buf, 0, _buf.Length, SocketFlags.None, RepeatCallback, null);
        }

        private void RepeatCallback(IAsyncResult result)
        {
            try
            {
                int n = _socket.EndReceive(result);
                //GUtil.WriteDebugLog(String.Format("t={0}, n={1} av={2}", DateTime.Now.ToString(), n, _socket.Available));
                //Debug.WriteLine(String.Format("r={0}, n={1} ", result.IsCompleted, n));

                if (n > 0)
                {
                    _callback.DataArrived(_buf, 0, n);
                    _socket.BeginReceive(_buf, 0, _buf.Length, SocketFlags.None, RepeatCallback, null);
                }
                else if (n < 0)
                {
                    //WindowsMEにおいては、ときどきここで-1が返ってきていることが発覚した。
                    _socket.BeginReceive(_buf, 0, _buf.Length, SocketFlags.None, RepeatCallback, null);
                }
                else
                {
                    _callback.DisconnectedFromServer();
                }
            }
            catch (Exception ex)
            {
                if ((ex is SocketException) && ((SocketException)ex).ErrorCode == 995)
                {
                    //GUtil.WriteDebugLog(String.Format("t={0} error995", DateTime.Now.ToString()));
                    _socket.BeginReceive(_buf, 0, _buf.Length, SocketFlags.None, RepeatCallback, null);
                }
                else
                {
                    _callback.ErrorOccurred(ex.Message);
                }
            }
        }
    }

    public abstract class TerminalConnection
    {

        internal TerminalParam _param;
        internal ITerminalTextLogger _loggerT;
        internal ITerminalBinaryLogger _loggerB;

        internal int _width;
        internal int _height;

        internal LogType _logType;
        internal string _logPath;
        //ログの一時的なオフのためのスイッチ
        internal bool _logsuspended;

        //送受信したデータサイズの情報
        internal int _sentPacketCount;
        internal int _sentDataSize;
        internal int _receivedPacketCount;
        internal int _receivedDataSize;

        internal IPAddress _serverAddress;
        internal string _serverName;
        //すでにクローズされたかどうかのフラグ
        internal bool _closed;

        protected TerminalConnection(TerminalParam p, int width, int height)
        {
            _param = p;
            _width = width;
            _height = height;
            ResetLog(p.LogType, p.LogPath, p.LogAppend);
            _logsuspended = false;
        }

        public IPAddress ServerAddress => _serverAddress;

        public string ServerName => _serverName;

        public abstract string ProtocolDescription
        {
            get;
        }

        public int SentPacketCount => _sentPacketCount;

        public int SentDataSize => _sentDataSize;

        public int ReceivedPacketCount => _receivedPacketCount;

        public int ReceivedDataSize => _receivedDataSize;

        public TerminalParam Param => _param;

        public int TerminalHeight => _height;

        public int TerminalWidth => _width;

        public bool LogSuspended
        {
            get => _logsuspended;
            set => _logsuspended = value;
        }

        public LogType LogType => _logType;

        public string LogPath => _logPath;

        public void SetServerInfo(string host, IPAddress address)
        {
            _serverName = host;
            _serverAddress = address;
        }
        public void CommentLog(string comment)
        {
            _loggerT.Comment(comment);
            _loggerB.Comment(comment);
        }
        public ITerminalTextLogger TextLogger => _loggerT;

        public ITerminalBinaryLogger BinaryLogger => _loggerB;

        public bool IsClosed => _closed;


        public abstract string[] ConnectionParameter { get; }

        public abstract bool Available { get; }

        public virtual void Resize(int width, int height)
        {
            Debug.Assert(width > 0 && height > 0);
            _width = width;
            _height = height;
        }

        public abstract ConnectionTag Reproduce(); //同じところへの接続をもう１本

        //終了処理
        internal virtual void Close()
        {
            if (_loggerT != null)
            {
                _loggerT.Flush();
                _loggerT.Close();
                _loggerT = null;
            }
            if (_loggerB != null)
            {
                _loggerB.Flush();
                _loggerB.Close();
                _loggerB = null;
            }
            _closed = true;
        }
        //接続を閉じる前の最後のアクション。こちらから切るときにはこれをよぶべきだが、サーバから切ってきたときは呼ぶ必要なし
        internal virtual void Disconnect()
        {
        }

        internal abstract void RepeatAsyncRead(IDataReceiver cb);


        public void WriteChars(char[] data)
        {
            byte[] b = _param.EncodingProfile.GetBytes(data);
            Write(b);
        }
        public void WriteChar(char data)
        {
            byte[] b = _param.EncodingProfile.GetBytes(data);
            Write(b);
        }
        public abstract void Write(byte[] data);
        public abstract void Write(byte[] data, int offset, int length);

        //デフォルト実装は空
        public virtual void AreYouThere()
        {
            throw new NotSupportedException("[AYT] unsupported");
        }
        public virtual void SendBreak()
        {
            throw new NotSupportedException("[Break] unsupported");
        }
        public virtual void SendKeepAliveData()
        {
        }

        internal void AddSentDataStats(int bytecount)
        {
            _sentPacketCount++;
            _sentDataSize += bytecount;
        }
        internal void AddReceivedDataStats(int bytecount)
        {
            _receivedPacketCount++;
            _receivedDataSize += bytecount;
        }

        public void ResetLog(LogType t, string path, bool append)
        {
            _logType = t;
            _logPath = path;

            if (_loggerT != null)
            {
                _loggerT.Close();
            }

            if (_loggerB != null)
            {
                _loggerB.Close();
            }

            switch (t)
            {
                case LogType.None:
                    _loggerT = new NullTextLogger();
                    _loggerB = new NullBinaryLogger();
                    break;
                case LogType.Default:
                    _loggerT = new DefaultLogger(new StreamWriter(path, append, Encoding.Default));
                    _loggerB = new NullBinaryLogger();
                    break;
                case LogType.Binary:
                    _loggerT = new NullTextLogger();
                    _loggerB = new BinaryLogger(new FileStream(path, append ? FileMode.Append : FileMode.Create));
                    break;
                case LogType.Xml:
                    _loggerT = new XmlLogger(new StreamWriter(path, append, Encoding.UTF8), _param); //DebugLogはUTF8
                    _loggerB = new NullBinaryLogger();
                    break;
            }
            _loggerT = new InternalLoggerT(_loggerT, this);
            _loggerB = new InternalLoggerB(_loggerB, this);
            _loggerT.TerminalResized(_width, _height);
        }


        private class InternalLoggerT : ITerminalTextLogger
        {
            private TerminalConnection _parent;
            private ITerminalTextLogger _logger;

            public InternalLoggerT(ITerminalTextLogger l, TerminalConnection p)
            {
                _parent = p;
                _logger = l;
            }

            public void Append(char ch)
            {
                if (!_parent.LogSuspended)
                {
                    _logger.Append(ch);
                }
            }
            public void Append(char[] ch)
            {
                if (!_parent.LogSuspended)
                {
                    _logger.Append(ch);
                }
            }
            public void Append(char[] ch, int offset, int length)
            {
                if (!_parent.LogSuspended)
                {
                    _logger.Append(ch, offset, length);
                }
            }
            public void BeginEscapeSequence()
            {
                if (!_parent.LogSuspended)
                {
                    _logger.BeginEscapeSequence();
                }
            }
            public void AbortEscapeSequence()
            {
                if (!_parent.LogSuspended)
                {
                    _logger.AbortEscapeSequence();
                }
            }
            public void CommitEscapeSequence()
            {
                if (!_parent.LogSuspended)
                {
                    _logger.CommitEscapeSequence();
                }
            }
            public void Comment(string comment)
            {
                if (!_parent.LogSuspended)
                {
                    _logger.Comment(comment);
                }
            }
            public void Flush()
            {
                _logger.Flush();
            }
            public void Close()
            {
                _logger.Close();
            }
            public bool IsActive => _logger.IsActive;

            public void PacketDelimiter()
            {
                if (!_parent.LogSuspended)
                {
                    _logger.PacketDelimiter();
                }
            }
            public void TerminalResized(int width, int height)
            {
                if (!_parent.LogSuspended)
                {
                    _logger.TerminalResized(width, height);
                }
            }
            public void WriteLine(GLine line)
            {
                if (!_parent.LogSuspended)
                {
                    _logger.WriteLine(line);
                }
            }

        }
        private class InternalLoggerB : ITerminalBinaryLogger
        {
            private TerminalConnection _parent;
            private ITerminalBinaryLogger _logger;

            public InternalLoggerB(ITerminalBinaryLogger l, TerminalConnection p)
            {
                _parent = p;
                _logger = l;
            }

            public void Append(byte[] data, int offset, int length)
            {
                if (!_parent.LogSuspended)
                {
                    _logger.Append(data, offset, length);
                }
            }
            public void Comment(string comment)
            {
                if (!_parent.LogSuspended)
                {
                    _logger.Comment(comment);
                }
            }
            public void Flush()
            {
                _logger.Flush();
            }
            public void Close()
            {
                _logger.Close();
            }
            public bool IsActive => _logger.IsActive;
        }
    }

    internal abstract class TCPTerminalConnection : TerminalConnection
    {

        protected bool _usingSocks;

        protected TCPTerminalConnection(TerminalParam p, int w, int h) : base(p, w, h)
        {
            _usingSocks = false;
        }
        public TCPTerminalParam TCPParam => _param as TCPTerminalParam;

        public override string ProtocolDescription
        {
            get
            {
                string s = string.Empty;
                if (_usingSocks)
                {
                    s += "Caption.TCPTerminalConnection.UsingSOCKS";
                }

                return s;
            }
        }

        //設定は最初だけ行う
        public bool UsingSocks
        {
            get => _usingSocks;
            set => _usingSocks = value;
        }
    }

    internal class TelnetTerminalConnection : TCPTerminalConnection, IDataReceiver
    {
        private AbstractGuevaraSocket _socket;
        private TelnetNegotiator _negotiator;

        public TelnetTerminalConnection(TerminalParam p, TelnetNegotiator neg, AbstractGuevaraSocket s, int width, int height) : base(p, width, height)
        {
            _socket = s;
            _negotiator = neg;
        }

        internal override void Close()
        {
            if (_closed)
            {
                return; //２度以上クローズしても副作用なし 
            }

            base.Close();
            try
            {
                _socket.Close();
            }
            catch (Exception ex)
            {
                GUtil.Warning(GEnv.Frame, "Message.SSHTerminalConnection.CloseError" + ex.Message);
            }
        }
        internal override void Disconnect()
        {
        }
        public override ConnectionTag Reproduce()
        {
            TerminalParam np = (TerminalParam)_param.Clone();
            if (GEnv.Options.DefaultLogType != LogType.None)
            {
                np.LogType = GEnv.Options.DefaultLogType;
                np.LogPath = GUtil.CreateLogFileName(np.ShortDescription);
            }
            else
            {
                np.LogType = LogType.None;
            }

            return CommunicationUtil.CreateNewConnection(np);
        }


        public override string[] ConnectionParameter
        {
            get
            {
                string[] d = new string[1];
                d[0] = String.Format("");
                return d;
            }
        }

        public override bool Available => !_closed && _socket.DataAvailable;

        private IDataReceiver _callback;
        internal override void RepeatAsyncRead(IDataReceiver cb)
        {
            if (_callback != null)
            {
                throw new InvalidOperationException("duplicated AsyncRead() is attempted");
            }

            _callback = cb;
            _socket.RepeatAsyncRead(this);
        }

        public void DataArrived(byte[] buf, int offset, int count)
        {
            ProcessBuffer(buf, offset, offset + count);
            _negotiator.Flush(_socket);
        }

        public void DisconnectedFromServer()
        {
            if (!_closed)
            {
                _callback.DisconnectedFromServer();
            }
        }

        public void ErrorOccurred(string msg)
        {
            if (!_closed)
            {
                _callback.ErrorOccurred(msg);
            }
        }

        //CR NUL -> CR
        private void ProcessBuffer(byte[] buf, int offset, int limit)
        {
            while (offset < limit)
            {
                while (offset < limit && _negotiator.InProcessing)
                {
                    if (_negotiator.Process(buf[offset++]) == TelnetNegotiator.ProcessResult.REAL_0xFF)
                    {
                        _callback.DataArrived(buf, offset - 1, 1);
                    }
                }

                int delim = offset;
                while (delim < limit)
                {
                    byte b = buf[delim];
                    if (b == 0xFF)
                    {
                        _negotiator.StartNegotiate();
                        break;
                    }
                    if (b == 0 && delim - 1 >= 0 && buf[delim - 1] == 0x0D)
                    {
                        break; //CR NULサポート
                    }

                    delim++;
                }

                if (delim > offset)
                {
                    _callback.DataArrived(buf, offset, delim - offset); //delimの手前まで処理
                }

                offset = delim + 1;
            }
        }

        public override void Resize(int width, int height)
        {
            base.Resize(width, height);
            if (!_closed)
            {
                TelnetOptionWriter wr = new TelnetOptionWriter();
                wr.WriteTerminalSize(width, height);
                wr.WriteTo(_socket);
            }
        }

        public override void Write(byte[] buf)
        {
            Write(buf, 0, buf.Length);
        }
        public override void Write(byte[] buf, int offset, int length)
        {
            AddSentDataStats(length);
            for (int i = 0; i < length; i++)
            {
                byte t = buf[offset + i];
                if (t == 0xFF || t == 0x0D)
                { //0xFFまたはCRLF以外のCRを見つけたら
                    WriteEscaping(buf, offset, length);
                    return;
                }
            }
            _socket.Transmit(buf, offset, length); //大抵の場合はこういうデータは入っていないので、高速化のためそのまま送り出す
        }
        private void WriteEscaping(byte[] buf, int offset, int length)
        {
            byte[] newbuf = new byte[length * 2];
            int newoffset = 0;
            for (int i = 0; i < length; i++)
            {
                byte t = buf[offset + i];
                if (t == 0xFF)
                {
                    newbuf[newoffset++] = 0xFF;
                    newbuf[newoffset++] = 0xFF; //２個
                }
                else if (t == 0x0D/* && (i==length-1 || buf[offset+i+1]!=0x0A)*/)
                {
                    newbuf[newoffset++] = 0x0D;
                    newbuf[newoffset++] = 0x00;
                }
                else
                {
                    newbuf[newoffset++] = t;
                }
            }
            _socket.Transmit(newbuf, 0, newoffset);
        }

        public override void AreYouThere()
        {
            byte[] data = new byte[2];
            data[0] = (byte)TelnetCode.IAC;
            data[1] = (byte)TelnetCode.AreYouThere;
            _socket.Transmit(data, 0, data.Length);
        }
        public override void SendBreak()
        {
            byte[] data = new byte[2];
            data[0] = (byte)TelnetCode.IAC;
            data[1] = (byte)TelnetCode.Break;
            _socket.Transmit(data, 0, data.Length);
        }
        public override void SendKeepAliveData()
        {
            byte[] data = new byte[2];
            data[0] = (byte)TelnetCode.IAC;
            data[1] = (byte)TelnetCode.NOP;
            _socket.Transmit(data, 0, data.Length);
        }


    }

    public class FakeConnection : TerminalConnection
    {
        public FakeConnection(TerminalParam param) : base(param, 80, 25) { }
        public override void Write(byte[] data)
        {
        }
        public override void Write(byte[] data, int offset, int length)
        {
        }
        internal override void RepeatAsyncRead(IDataReceiver r)
        {
        }
        public override string[] ConnectionParameter
        {
            get
            {
                return new string[1] { "fake connection" };
            }
        }
        public override bool Available => false;

        public override string ProtocolDescription => "";

        public override ConnectionTag Reproduce()
        {
            throw new NotSupportedException();
        }

    }
}
