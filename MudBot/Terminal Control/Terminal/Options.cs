/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: Options.cs,v 1.2 2005/04/20 08:45:46 okajima Exp $
*/
using Poderosa.ConnectionParam;
using Poderosa.Terminal;
using Poderosa.Toolkit;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

//起動の高速化のため、ここではGranadosを呼ばないように注意する

//新しいオプションを追加したら、Load/Save/Init/Cloneをそれぞれ修正すること！

namespace Poderosa.Config
{
    public class CommonOptions : ICloneable
    {

        //言語設定 (このメンバは最初であること！）
        [ConfigEnumElement(typeof(Language))] protected Language _language;

        //表示
        [ConfigStringElement(Initial = "Courier New")] protected string _fontName;
        [ConfigStringElement(Initial = "ＭＳ ゴシック")] protected string _japaneseFontName;
        [ConfigFloatElement(Initial = 9)] protected float _fontSize;

        //この２つは上記要素から作成
        protected Font _font;
        protected Font _japaneseFont;

        [ConfigBoolElement(Initial = true)] protected bool _useClearType;

        [ConfigColorElement(Initial = LateBindColors.Window)] protected Color _bgColor;
        [ConfigColorElement(Initial = LateBindColors.WindowText)] protected Color _textColor;
        /* Special Handlings Required */
        protected EscapesequenceColorSet _esColorSet;
        [ConfigStringElement(Initial = "")] protected string _backgroundImageFileName;
        [ConfigEnumElement(typeof(ImageStyle), InitialAsInt = (int)ImageStyle.Center)] protected ImageStyle _imageStyle;
        [ConfigColorElement(Initial = LateBindColors.Empty)] protected Color _caretColor; //Color.Emptyのときは通常テキストを反転するのみ

        //ターミナル
        [ConfigFlagElement(typeof(CaretType), Initial = (int)(CaretType.Blink | CaretType.Box), Max = (int)CaretType.Max)]
        protected CaretType _caretType;

        [ConfigBoolElement(Initial = true)] protected bool _closeOnDisconnect;
        [ConfigBoolElement(Initial = false)] protected bool _beepOnBellChar;
        [ConfigBoolElement(Initial = false)] protected bool _askCloseOnExit;
        [ConfigBoolElement(Initial = false)] protected bool _quitAppWithLastPane;
        [ConfigEnumElement(typeof(DisconnectNotification), InitialAsInt = (int)DisconnectNotification.StatusBar)]
        protected DisconnectNotification _disconnectNotification;
        [ConfigIntElement(Initial = 100)] protected int _terminalBufferSize;
        [ConfigBoolElement(Initial = false)] protected bool _send0x7FByDel;
        [ConfigEnumElement(typeof(LogType), InitialAsInt = (int)LogType.None)]
        protected LogType _defaultLogType;
        [ConfigStringElement(Initial = "")] protected string _defaultLogDirectory;
        [ConfigEnumElement(typeof(WarningOption), InitialAsInt = (int)WarningOption.MessageBox)]
        protected WarningOption _warningOption;
        [ConfigBoolElement(Initial = false)] protected bool _adjustsTabTitleToWindowTitle;
        [ConfigBoolElement(Initial = false)] protected bool _allowsScrollInAppMode;
        [ConfigIntElement(Initial = 0)] protected int _keepAliveInterval;
        [ConfigEnumElement(typeof(Keys), InitialAsInt = (int)Keys.None)]
        protected Keys _localBufferScrollModifier;

        //マウスとキーボード
        [ConfigEnumElement(typeof(AltKeyAction), InitialAsInt = (int)AltKeyAction.Menu)]
        protected AltKeyAction _leftAltKey;
        [ConfigEnumElement(typeof(AltKeyAction), InitialAsInt = (int)AltKeyAction.Menu)]
        protected AltKeyAction _rightAltKey;
        [ConfigBoolElement(Initial = false)] protected bool _autoCopyByLeftButton;
        [ConfigEnumElement(typeof(RightButtonAction), InitialAsInt = (int)RightButtonAction.ContextMenu)]
        protected RightButtonAction _rightButtonAction;
        [ConfigIntElement(Initial = 3)] protected int _wheelAmount;
        [ConfigStringElement(Initial = "")] protected string _additionalWordElement;

        //SSH
        [ConfigBoolElement(Initial = false)] protected bool _retainsPassphrase;
        [ConfigStringArrayElement(Initial = new[] { "AES128", "Blowfish", "TripleDES" })]
        protected string[] _cipherAlgorithmOrder;
        [ConfigStringArrayElement(Initial = new[] { "DSA", "RSA" })]
        protected string[] _hostKeyAlgorithmOrder;
        [ConfigIntElement(Initial = 4096)] protected int _sshWindowSize;
        [ConfigBoolElement(Initial = true)] protected bool _sshCheckMAC;

        //SOCKS関係
        [ConfigBoolElement(Initial = false)] protected bool _useSocks;
        [ConfigStringElement(Initial = "")] protected string _socksServer;
        [ConfigIntElement(Initial = 1080)] protected int _socksPort;
        [ConfigStringElement(Initial = "")] protected string _socksAccount;
        [ConfigStringElement(Initial = "")] protected string _socksPassword;
        [ConfigStringElement(Initial = "")] protected string _socksNANetworks;

        private static ArrayList _configAttributes;

        public CommonOptions()
        {
            _esColorSet = new EscapesequenceColorSet();
            if (_configAttributes == null)
            {
                InitConfigAttributes();
            }
        }
        public Language Language
        {
            get => _language;
            set => _language = value;
        }
        public bool CloseOnDisconnect
        {
            get => _closeOnDisconnect;
            set => _closeOnDisconnect = value;
        }
        public DisconnectNotification DisconnectNotification
        {
            get => _disconnectNotification;
            set => _disconnectNotification = value;
        }
        public bool BeepOnBellChar
        {
            get => _beepOnBellChar;
            set => _beepOnBellChar = value;
        }
        public bool AskCloseOnExit
        {
            get => _askCloseOnExit;
            set => _askCloseOnExit = value;
        }
        public bool QuitAppWithLastPane
        {
            get => _quitAppWithLastPane;
            set => _quitAppWithLastPane = value;
        }
        public bool Send0x7FByDel
        {
            get => _send0x7FByDel;
            set => _send0x7FByDel = value;
        }

        public AltKeyAction LeftAltKey
        {
            get => _leftAltKey;
            set => _leftAltKey = value;
        }
        public AltKeyAction RightAltKey
        {
            get => _rightAltKey;
            set => _rightAltKey = value;
        }
        public bool AutoCopyByLeftButton
        {
            get => _autoCopyByLeftButton;
            set => _autoCopyByLeftButton = value;
        }
        public RightButtonAction RightButtonAction
        {
            get => _rightButtonAction;
            set => _rightButtonAction = value;
        }
        public string AdditionalWordElement
        {
            get => _additionalWordElement;
            set => _additionalWordElement = value;
        }


        public bool AdjustsTabTitleToWindowTitle
        {
            get => _adjustsTabTitleToWindowTitle;
            set => _adjustsTabTitleToWindowTitle = value;
        }
        public bool AllowsScrollInAppMode
        {
            get => _allowsScrollInAppMode;
            set => _allowsScrollInAppMode = value;
        }

        public int WheelAmount
        {
            get => _wheelAmount;
            set
            {
                if (value < 1 || value > 99)
                {
                    throw new InvalidOptionException("Message.Options.WheelAmountRange");
                }

                _wheelAmount = value;
            }
        }

        public int TerminalBufferSize
        {
            get => _terminalBufferSize;
            set
            {
                if (value < 0 || value > 9999)
                {
                    throw new InvalidOptionException("Message.Options.BufferSizeRange");
                }

                _terminalBufferSize = value;
            }
        }

        public Font Font
        {
            get
            {
                if (_font == null)
                {
                    _font = GUtil.CreateFont(_fontName, _fontSize);
                }

                return _font;
            }
            set
            {
                _font = value;
                _fontName = _font.FontFamily.Name;
                _fontSize = _font.Size;
            }
        }
        public Font JapaneseFont
        {
            get
            {
                if (_japaneseFont == null)
                {
                    _japaneseFont = GUtil.CreateFont(_japaneseFontName, _fontSize);
                }

                return _japaneseFont;
            }
            set
            {
                _japaneseFont = value;
                _japaneseFontName = _japaneseFont.FontFamily.Name;
            }
        }
        //これらはRenderProfileが取得する目的
        public string FontName => _fontName;

        public string JapaneseFontName => _japaneseFontName;

        public float FontSize => _fontSize;

        public bool UseClearType
        {
            get => _useClearType;
            set => _useClearType = value;
        }

        public Color BGColor
        {
            get => _bgColor;
            set => _bgColor = value;
        }
        public Color TextColor
        {
            get => _textColor;
            set => _textColor = value;
        }
        public EscapesequenceColorSet ESColorSet
        {
            get => _esColorSet;
            set => _esColorSet = value;
        }

        public string BackgroundImageFileName
        {
            get => _backgroundImageFileName;
            set => _backgroundImageFileName = value;
        }
        public ImageStyle ImageStyle
        {
            get => _imageStyle;
            set => _imageStyle = value;
        }

        public CaretType CaretType
        {
            get => _caretType;
            set => _caretType = value;
        }
        public Color CaretColor
        {
            get => _caretColor;
            set => _caretColor = value;
        }
        public string[] CipherAlgorithmOrder
        {
            get => _cipherAlgorithmOrder;
            set => _cipherAlgorithmOrder = value;
        }

        public string[] HostKeyAlgorithmOrder
        {
            get => _hostKeyAlgorithmOrder;
            set => _hostKeyAlgorithmOrder = value;
        }
        public int SSHWindowSize
        {
            get => _sshWindowSize;
            set => _sshWindowSize = value;
        }
        public bool SSHCheckMAC
        {
            get => _sshCheckMAC;
            set => _sshCheckMAC = value;
        }

        public bool RetainsPassphrase
        {
            get => _retainsPassphrase;
            set => _retainsPassphrase = value;
        }
        public WarningOption WarningOption
        {
            get => _warningOption;
            set => _warningOption = value;
        }
        public LogType DefaultLogType
        {
            get => _defaultLogType;
            set => _defaultLogType = value;
        }
        public string DefaultLogDirectory
        {
            get => _defaultLogDirectory;
            set => _defaultLogDirectory = value;
        }


        public bool UseSocks
        {
            get => _useSocks;
            set => _useSocks = value;
        }
        public string SocksServer
        {
            get => _socksServer;
            set => _socksServer = value;
        }
        public int SocksPort
        {
            get => _socksPort;
            set => _socksPort = value;
        }
        public string SocksAccount
        {
            get => _socksAccount;
            set => _socksAccount = value;
        }
        public string SocksPassword
        {
            get => _socksPassword;
            set => _socksPassword = value;
        }
        public string SocksNANetworks
        {
            get => _socksNANetworks;
            set => _socksNANetworks = value;
        }
        public int KeepAliveInterval
        {
            get => _keepAliveInterval;
            set => _keepAliveInterval = value;
        }
        public Keys LocalBufferScrollModifier
        {
            get => _localBufferScrollModifier;
            set => _localBufferScrollModifier = value;
        }

        public virtual object Clone()
        {
            CommonOptions o = new CommonOptions();
            CopyTo(o);
            return o;
        }

        public void CopyTo(CommonOptions o)
        {
            o._closeOnDisconnect = _closeOnDisconnect;
            o._disconnectNotification = _disconnectNotification;
            o._beepOnBellChar = _beepOnBellChar;
            o._askCloseOnExit = _askCloseOnExit;
            o._quitAppWithLastPane = _quitAppWithLastPane;
            o._autoCopyByLeftButton = _autoCopyByLeftButton;
            o._send0x7FByDel = _send0x7FByDel;
            o._keepAliveInterval = _keepAliveInterval;
            o._adjustsTabTitleToWindowTitle = _adjustsTabTitleToWindowTitle;
            o._allowsScrollInAppMode = _allowsScrollInAppMode;
            o._wheelAmount = _wheelAmount;
            o._terminalBufferSize = _terminalBufferSize;
            o._retainsPassphrase = _retainsPassphrase;
            o._warningOption = _warningOption;
            o._localBufferScrollModifier = _localBufferScrollModifier;

            o._rightButtonAction = _rightButtonAction;
            o._leftAltKey = _leftAltKey;
            o._rightAltKey = _rightAltKey;
            o._additionalWordElement = _additionalWordElement;

            o._font = null;
            o._japaneseFont = null;
            o._fontName = _fontName;
            o._japaneseFontName = _japaneseFontName;
            o._useClearType = _useClearType;
            o._fontSize = _fontSize;
            o._bgColor = _bgColor;
            o._textColor = _textColor;
            o._esColorSet = (EscapesequenceColorSet)_esColorSet.Clone();
            o._backgroundImageFileName = _backgroundImageFileName;
            o._caretType = _caretType;
            o._caretColor = _caretColor;
            o._imageStyle = _imageStyle;

            o._cipherAlgorithmOrder = (string[])_cipherAlgorithmOrder.Clone();
            o._hostKeyAlgorithmOrder = (string[])_hostKeyAlgorithmOrder.Clone();
            o._sshWindowSize = _sshWindowSize;
            o._sshCheckMAC = _sshCheckMAC;
            o._defaultLogType = _defaultLogType;
            o._defaultLogDirectory = _defaultLogDirectory;
            o._useSocks = _useSocks;
            o._socksServer = _socksServer;
            o._socksPort = _socksPort;
            o._socksAccount = _socksAccount;
            o._socksPassword = _socksPassword;
            o._socksNANetworks = _socksNANetworks;
        }

        private static void InitConfigAttributes()
        {
            FieldInfo[] fields = typeof(CommonOptions).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            _configAttributes = new ArrayList(fields.Length);
            bool language_found = false;
            foreach (FieldInfo field in fields)
            {
                object[] attrs = field.GetCustomAttributes(typeof(ConfigElementAttribute), true);
                if (attrs.Length != 0)
                {
                    ConfigElementAttribute attr = (ConfigElementAttribute)attrs[0];
                    attr.FieldInfo = field;
                    if (!language_found)
                    {
                        Debug.Assert(field.Name == "_language");
                        //Languageの初期値は実行時でないと決まらない
                        (attr as ConfigEnumElementAttribute).InitialAsInt = (int)GUtil.CurrentLanguage;
                        language_found = true;
                    }
                    _configAttributes.Add(attr);
                }
            }
        }

        public virtual void Save(ConfigNode parent)
        {
            ConfigNode node = new ConfigNode("poderosa-terminal");
            foreach (ConfigElementAttribute attr in _configAttributes)
            {
                attr.ExportTo(this, node);
            }
            if (!_esColorSet.IsDefault)
            {
                node["escape-sequence-color"] = _esColorSet.Format();
            }

            parent.AddChild(node);
        }
        public virtual void Load(ConfigNode parent)
        {
            ConfigNode node = parent.FindChildConfigNode("poderosa-terminal");
            if (node != null)
            {
                foreach (ConfigElementAttribute attr in _configAttributes)
                {
                    attr.ImportFrom(this, node);
                }
                string es = node["escape-sequence-color"];
                if (es != null)
                {
                    _esColorSet.Load(es);
                }
            }
            else
            {
                Init();
            }
        }

        public virtual void Init()
        {
            foreach (ConfigElementAttribute attr in _configAttributes)
            {
                attr.Reset(this);
            }
        }

    }

    /// <summary>
    /// ペインの位置を指定します。
    /// </summary>
    public enum PanePosition
    {
        /// <summary>
        /// 上下分割のときの上、左右分割のときの左の位置です。
        /// </summary>
        First,
        /// <summary>
        /// 上下分割のときの下、左右分割のときの右の位置です。
        /// </summary>
        Second
    }

    [EnumDesc(typeof(Language))]
    public enum Language
    {
        [EnumValue(Description = "Enum.Language.English")] English,
        [EnumValue(Description = "Enum.Language.Japanese")] Japanese
    }


    //おかしな文字が来たときどうするか
    [EnumDesc(typeof(WarningOption))]
    public enum WarningOption
    {
        [EnumValue(Description = "Enum.WarningOption.Ignore")] Ignore,
        [EnumValue(Description = "Enum.WarningOption.StatusBar")] StatusBar,
        [EnumValue(Description = "Enum.WarningOption.MessageBox")] MessageBox
    }

    [EnumDesc(typeof(DisconnectNotification))]
    public enum DisconnectNotification
    {
        [EnumValue(Description = "Enum.DisconnectNotification.MessageBox")] MessageBox,
        [EnumValue(Description = "Enum.DisconnectNotification.StatusBar")] StatusBar
    }

    [EnumDesc(typeof(AltKeyAction))]
    public enum AltKeyAction
    {
        [EnumValue(Description = "Enum.AltKeyAction.Menu")] Menu,
        [EnumValue(Description = "Enum.AltKeyAction.ESC")] ESC,
        [EnumValue(Description = "Enum.AltKeyAction.Meta")] Meta
    }

    [EnumDesc(typeof(RightButtonAction))]
    public enum RightButtonAction
    {
        [EnumValue(Description = "Enum.RightButtonAction.ContextMenu")] ContextMenu,
        [EnumValue(Description = "Enum.RightButtonAction.Paste")] Paste
    }

    [Flags]
    public enum CaretType
    {
        None = 0,
        Blink = 1,

        Line = 0,
        Box = 2,
        Underline = 4,
        StyleMask = Box | Underline,
        Max = Blink | Box | Underline
    }

    public class InvalidOptionException : Exception
    {
        public InvalidOptionException(string msg) : base(msg) { }
    }
}
