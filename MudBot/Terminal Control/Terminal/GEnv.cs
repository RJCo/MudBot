/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: GEnv.cs,v 1.2 2005/04/20 08:45:46 okajima Exp $
*/
using Poderosa.Communication;
using Poderosa.Config;
using Poderosa.Connection;
using Poderosa.Terminal;
using Poderosa.Text;

namespace Poderosa
{
    /// <summary>
    /// </summary>
    public class GEnv
    {
        //

        //�
        private static CommonOptions _options;
        private static GlobalCommandTarget _commandTarget;
        private static InterThreadUIService _interThreadUIService;

        //
        private static Win32.SystemMetrics _systemMetrics;
        private static RenderProfile _defaultRenderProfile;

        public static void Init(IPoderosaContainer container)
        {
            GConst.Init();
            Frame = container;
            TextSelection = new TextSelection();
            Connections = new Connections();
        }

        public static void Terminate()
        {
        }

        public static Connections Connections { get; private set; }

        public static IPoderosaContainer Frame { get; private set; }

        public static void ReloadStringResource()
        {
        }

        internal static Win32.SystemMetrics SystemMetrics
        {
            get
            {
                if (_systemMetrics == null)
                {
                    _systemMetrics = new Win32.SystemMetrics();
                }

                return _systemMetrics;
            }
        }

        public static CommonOptions Options
        {
            get
            {
                if (_options == null)
                {
                    _options = new CommonOptions();
                    _options.Init();
                }
                return _options;
            }
            set
            {
                bool k = _options == null || _options.KeepAliveInterval != value.KeepAliveInterval;
                _options = value;
                if (k)
                {
                    Connections.KeepAlive.SetTimerToAllConnectionTags();
                }
            }
        }

        public static RenderProfile DefaultRenderProfile
        {
            get
            {
                if (_defaultRenderProfile == null)
                {
                    _defaultRenderProfile = new RenderProfile(_options);
                }

                return _defaultRenderProfile;
            }
            set => _defaultRenderProfile = value;
        }
        public static TextSelection TextSelection { get; private set; }

        public static GlobalCommandTarget GlobalCommandTarget
        {
            get
            {
                if (_commandTarget == null)
                {
                    _commandTarget = new GlobalCommandTarget();
                }

                return _commandTarget;
            }
            set => _commandTarget = value;
        }
        public static InterThreadUIService InterThreadUIService
        {
            get
            {
                if (_interThreadUIService == null)
                {
                    _interThreadUIService = new InterThreadUIService();
                }

                return _interThreadUIService;
            }
            set => _interThreadUIService = value;
        }

        // no args -> CommandTarget for active connection
        internal static ConnectionCommandTarget GetConnectionCommandTarget()
        {
            TerminalConnection con = Connections.ActiveConnection;
            return con == null ? null : new ConnectionCommandTarget(con);
        }

        internal static ConnectionCommandTarget GetConnectionCommandTarget(TerminalConnection con)
        {
            return new ConnectionCommandTarget(con);
        }

        internal static ConnectionCommandTarget GetConnectionCommandTarget(ConnectionTag tag)
        {
            return new ConnectionCommandTarget(tag.Connection);
        }
    }

    public class GConst
    {
        public static int WMG_ASYNCCONNECT;
        public static int WMG_MACRO_TRACE;
        public static int WMG_SENDLINE_PROGRESS;
        public static int WMG_MAINTHREADTASK;

        public static int WMG_KEYGEN_PROGRESS;
        public static int WMG_KEYGEN_FINISHED;
        public static int WMG_XMODEM_UPDATE_STATUS;
        public static int WMG_UIMESSAGE;
        public static int WMG_DELAYED_COMMAND;

        //init
        public static void Init()
        {
            int t = Win32.RegisterWindowMessage("guevara.synchronization");
            WMG_ASYNCCONNECT = t;
            WMG_MACRO_TRACE = t;
            WMG_SENDLINE_PROGRESS = t;
            WMG_MAINTHREADTASK = t;
            WMG_KEYGEN_PROGRESS = t;
            WMG_XMODEM_UPDATE_STATUS = t;
            WMG_UIMESSAGE = t;

            //����͕ʂ̔ԍ��łȂ��Ƃ��߂�
            t = Win32.RegisterWindowMessage("guevara.synchronization2");
            WMG_KEYGEN_FINISHED = t;
            WMG_DELAYED_COMMAND = t;
        }

        //WMG_MAINTHREADTASK��WParam
        public const int WPG_ADJUSTIMECOMPOSITION = 1;
        public const int WPG_TOGGLESELECTIONMODE = 2;
        public const int WPG_DATA_ARRIVED = 3;
        public const int WPG_INVALIDATERECT = 4;
        public const int WPG_POST_ONGOTFOCUS = 5;
        public const int WPG_POST_ONTIMER = 6;

        public const string NSURI = "http://www.poderosa.org/";
    }
}
