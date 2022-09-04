/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: CommunicationUtil.cs,v 1.2 2005/04/20 08:45:46 okajima Exp $
*/
using Poderosa.Connection;
using Poderosa.ConnectionParam;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Poderosa.Communication
{
    internal class TelnetConnector : SocketWithTimeout
    {
        private TelnetTerminalParam _param;
        private Size _size;
        private ConnectionTag _result;

        public TelnetConnector(TelnetTerminalParam param, Size size)
        {
            _param = param;
            _size = size;
        }

        protected override void Negotiate()
        {
            TelnetNegotiator neg = new TelnetNegotiator(_param, _size.Width, _size.Height);
            TelnetTerminalConnection r = new TelnetTerminalConnection(_param, neg, new PlainGuevaraSocket(_socket), _size.Width, _size.Height)
            {
                UsingSocks = _socks != null
            };
            r.SetServerInfo(_param.Host, IPAddress);
            _result = new ConnectionTag(r);
        }

        protected override object Result => _result;
    }

    public class CommunicationUtil
    {

        public class SilentClient : ISocketWithTimeoutClient
        {

            private AutoResetEvent _event;
            private SocketWithTimeout _connector;
            private ConnectionTag _result;
            private string _errorMessage;

            public SilentClient()
            {
                _event = new AutoResetEvent(false);
            }

            public void SuccessfullyExit(object result)
            {
                _result = (ConnectionTag)result;
                //_result.SetServerInfo(((TCPTerminalParam)_result.Param).Host, swt.IPAddress);
                _event.Set();
            }
            public void ConnectionFailed(string message)
            {
                _errorMessage = message;
                _event.Set();
            }
            public void CancelTimer()
            {
            }

            public ConnectionTag Wait(SocketWithTimeout swt)
            {
                _connector = swt;
                //Form form = GEnv.Frame.AsForm();
                //if(form!=null) form.Cursor = Cursors.WaitCursor;
                _event.WaitOne();
                _event.Close();
                //if(form!=null) form.Cursor = Cursors.Default;
                if (_result == null)
                {
                    //GUtil.Warning(GEnv.Frame, _errorMessage);
                    //MessageBox.Show("error");
                    return null;
                }
                else
                {
                    return _result;
                }
            }
            public IWin32Window GetWindow()
            {
                return GEnv.Frame;
            }
        }

        public static ConnectionTag CreateNewConnection(TerminalParam param)
        {
            SilentClient s = new SilentClient();
            SocketWithTimeout swt = StartNewConnection(s, (TCPTerminalParam)param);
            if (swt == null)
            {
                return null;
            }
            else
            {
                return s.Wait(swt);
            }
        }

        public static SocketWithTimeout StartNewConnection(ISocketWithTimeoutClient client, TCPTerminalParam param)
        {
            Size sz = GEnv.Frame.TerminalSizeForNextConnection;
            //Size sz = new System.Drawing.Size(528, 316);

            SocketWithTimeout swt = new TelnetConnector((TelnetTerminalParam)param, sz);

            if (GEnv.Options.UseSocks)
            {
                swt.AsyncConnect(client, CreateSocksParam(param.Host, param.Port));
            }
            else
            {
                swt.AsyncConnect(client, param.Host, param.Port);
            }

            return swt;
        }
        private static Socks CreateSocksParam(string dest_host, int dest_port)
        {
            Socks s = new Socks
            {
                DestName = dest_host,
                DestPort = (short)dest_port,
                Account = GEnv.Options.SocksAccount,
                Password = GEnv.Options.SocksPassword,
                ServerName = GEnv.Options.SocksServer,
                ServerPort = (short)GEnv.Options.SocksPort,
                ExcludingNetworks = GEnv.Options.SocksNANetworks
            };
            return s;
        }

        public static bool FillDCB(IntPtr handle, ref Win32.DCB dcb)
        {
            dcb.DCBlength = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(Win32.DCB)); //sizeof‚­‚ç‚¢unsafe‚Å‚È‚­‚Ä‚àŽg‚í‚¹‚Ä‚­‚ê‚æ
            return Win32.GetCommState(handle, ref dcb);
        }
    }
}

