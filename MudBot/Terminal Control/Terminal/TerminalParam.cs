/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: TerminalParam.cs,v 1.2 2005/04/20 08:45:48 okajima Exp $
*/
using System;

using Poderosa.Terminal;
#if !MACRODOC
using Poderosa.Config;
using Poderosa.Communication;
#endif
using Poderosa.Toolkit;

namespace Poderosa.ConnectionParam
{

    /*
	 * TerminalParamはマクロからもフルにアクセス可能にするためpublicにする
	 * 公開する必要のないメソッドをinternalにする
	 */

    //Granados内のAuthenticationTypeと同一だが、起動の高速化のため使わない

    /// <summary>
    /// <ja>SSHでの認証方法を示します。</ja>
    /// <en>Specifies the authemtication method of SSH.</en>
    /// </summary>
    [EnumDesc(typeof(AuthType))]
    public enum AuthType
    {
        /// <summary>
        /// <ja>コンソール上でパスワードを入力する認証</ja>
        /// <en>Authentication by sending the password through the console.</en>
        /// </summary>
        [EnumValue(Description = "Enum.AuthType.KeyboardInteractive")]
        KeyboardInteractive
    }

    /// <summary>
    /// <ja>エンコーディングを示します。</ja>
    /// <en>Specifies the encoding of the connection.</en>
    /// <seealso cref="TerminalParam.Encoding"/>
    /// </summary>
    [EnumDesc(typeof(EncodingType))]
    public enum EncodingType
    {
        /// <summary>
        /// <ja>iso-8859-1</ja>
        /// <en>iso-8859-1</en>
        /// </summary>
        [EnumValue(Description = "Enum.EncodingType.ISO8859_1")] ISO8859_1,

        /// <summary>
        /// <ja>utf-8</ja>
        /// <en>utf-8</en>
        /// </summary>
        [EnumValue(Description = "Enum.EncodingType.UTF8")] UTF8,
    }

    /// <summary>
    /// <ja>ログの種類を示します。</ja>
    /// <en>Specifies the log type.</en>
    /// </summary>
    [EnumDesc(typeof(LogType))]
    public enum LogType
    {
        /// <summary>
        /// <ja>ログはとりません。</ja>
        /// <en>The log is not recorded.</en>
        /// </summary>
        [EnumValue(Description = "Enum.LogType.None")] None,
        /// <summary>
        /// <ja>テキストモードのログです。これが標準です。</ja>
        /// <en>The log is a plain text file. This is standard.</en>
        /// </summary>
        [EnumValue(Description = "Enum.LogType.Default")] Default,
        /// <summary>
        /// <ja>バイナリモードのログです。</ja>
        /// <en>The log is a binary file.</en>
        /// </summary>
        [EnumValue(Description = "Enum.LogType.Binary")] Binary,
        /// <summary>
        /// <ja>XMLで保存します。また内部的なバグ追跡においてこのモードでのログ採取をお願いすることがあります。</ja>
        /// <en>The log is an XML file. We may ask you to record the log in this type for debugging.</en>
        /// </summary>
        [EnumValue(Description = "Enum.LogType.Xml")] Xml
    }

    /// <summary>
    /// <ja>送信時の改行の種類を示します。</ja>
    /// <en>Specifies the new-line characters for transmission.</en>
    /// </summary>
    [EnumDesc(typeof(NewLine))]
    public enum NewLine
    {
        /// <summary>
        /// CR
        /// </summary>
        [EnumValue(Description = "Enum.NewLine.CR")] CR,
        /// <summary>
        /// LF
        /// </summary>
        [EnumValue(Description = "Enum.NewLine.LF")] LF,
        /// <summary>
        /// CR+LF
        /// </summary>
        [EnumValue(Description = "Enum.NewLine.CRLF")] CRLF
    }

    /// <summary>
    /// <ja>ターミナルの種別を示します。</ja>
    /// <en>Specifies the type of the terminal.</en>
    /// </summary>
    /// <remarks>
    /// <ja>XTermにはVT100にはないいくつかのエスケープシーケンスが含まれています。</ja>
    /// <en>XTerm supports several escape sequences in addition to VT100.</en>
    /// <ja>KTermは中身はXTermと一緒ですが、SSHやTelnetの接続オプションにおいてターミナルの種類を示す文字列として"kterm"がセットされます。</ja>
    /// <en>Though the functionality of KTerm is identical to XTerm, the string "kterm" is used for specifying the type of the terminal in the connection of Telnet or SSH.</en>
    /// <ja>この設定は、多くの場合TERM環境変数の値に影響します。</ja>
    /// <en>In most cases, this setting affects the TERM environment variable.</en>
    /// </remarks>
    [EnumDesc(typeof(TerminalType))]
    public enum TerminalType
    {
        /// <summary>
        /// vt100
        /// </summary>
        [EnumValue(Description = "Enum.TerminalType.VT100")] VT100,
        /// <summary>
        /// xterm
        /// </summary>
        [EnumValue(Description = "Enum.TerminalType.XTerm")] XTerm,
        /// <summary>
        /// kterm
        /// </summary>
        [EnumValue(Description = "Enum.TerminalType.KTerm")] KTerm
    }

    /// <summary>
    /// <ja>背景画像の位置を指定します。</ja>
    /// <en>Specifies the position of the background image.</en>
    /// </summary>
    [EnumDesc(typeof(ImageStyle))]
    public enum ImageStyle
    {
        /// <summary>
        /// <ja>中央</ja>
        /// <en>Center</en>
        /// </summary>
        [EnumValue(Description = "Enum.ImageStyle.Center")] Center,
        /// <summary>
        /// <ja>左上</ja>
        /// <en>Upper left corner</en>
        /// </summary>
        [EnumValue(Description = "Enum.ImageStyle.TopLeft")] TopLeft,
        /// <summary>
        /// <ja>右上</ja>
        /// <en>Upper right corner</en>
        /// </summary>
        [EnumValue(Description = "Enum.ImageStyle.TopRight")] TopRight,
        /// <summary>
        /// <ja>左下</ja>
        /// <en>Lower left corner</en>
        /// </summary>
        [EnumValue(Description = "Enum.ImageStyle.BottomLeft")] BottomLeft,
        /// <summary>
        /// <ja>右下</ja>
        /// <en>Lower right corner</en>
        /// </summary>
        [EnumValue(Description = "Enum.ImageStyle.BottomRight")] BottomRight,
        /// <summary>
        /// <ja>伸縮して全体に表示</ja>
        /// <en>The image covers the whole area of the console by expansion</en>
        /// </summary>
        [EnumValue(Description = "Enum.ImageStyle.Scaled")] Scaled
    }

    /// <summary>
    /// <ja>受信した文字に対する改行方法を示します。</ja>
    /// <en>Specifies line breaking style.</en>
    /// </summary>
    [EnumDesc(typeof(LineFeedRule))]
    public enum LineFeedRule
    {
        /// <summary>
        /// <ja>標準</ja>
        /// <en>Standard</en>
        /// </summary>
        [EnumValue(Description = "Enum.LineFeedRule.Normal")] Normal,
        /// <summary>
        /// <ja>LFで改行しCRを無視</ja>
        /// <en>LF:Line Break, CR:Ignore</en>
        /// </summary>
        [EnumValue(Description = "Enum.LineFeedRule.LFOnly")] LFOnly,
        /// <summary>
        /// <ja>CRで改行しLFを無視</ja>
        /// <en>CR:Line Break, LF:Ignore</en>
        /// </summary>
        [EnumValue(Description = "Enum.LineFeedRule.CROnly")] CROnly
    }


    /// <summary>
    /// <ja>接続を開くときのパラメータの基底クラスです。</ja>
    /// <en>Implements the basic functionality common to connections.</en>
    /// <seealso cref="TCPTerminalParam"/>
    /// <seealso cref="TelnetTerminalParam"/>
    /// </summary>
    [Serializable()]
    public abstract class TerminalParam : ICloneable
    {

        internal EncodingType _encoding;
        internal TerminalType _terminalType;
        internal LogType _logtype;
        internal string _logpath;
        internal bool _logappend;
        internal bool _localecho;
        internal LineFeedRule _lineFeedRule;
        internal NewLine _transmitnl;
        internal string _caption;
        internal RenderProfile _renderProfile;

        internal TerminalParam()
        {
            _encoding = EncodingType.UTF8;
            _logtype = LogType.None;
            _terminalType = TerminalType.XTerm;
            _localecho = false;
            _lineFeedRule = LineFeedRule.Normal;
            _transmitnl = NewLine.CR;
            _renderProfile = null;
        }

        internal TerminalParam(TerminalParam r)
        {
            Import(r);
        }
        internal void Import(TerminalParam r)
        {
            _encoding = r._encoding;
            _logtype = r._logtype;
            _logpath = r._logpath;
            _localecho = r._localecho;
            _transmitnl = r._transmitnl;
            _lineFeedRule = r._lineFeedRule;
            _terminalType = r._terminalType;
            _renderProfile = r._renderProfile == null ? null : new RenderProfile(r._renderProfile);
            _caption = r._caption;
        }

        public override bool Equals(object t_)
        {
            TerminalParam t = t_ as TerminalParam;
            if (t == null)
            {
                return false;
            }

            return
                _encoding == t.Encoding &&
                _localecho == t.LocalEcho &&
                _transmitnl == t.TransmitNL &&
                _lineFeedRule == t.LineFeedRule &&
                _terminalType == t.TerminalType;
        }

        /// 
        /// 
        /// 
        public override int GetHashCode()
        {
            return _encoding.GetHashCode() + _localecho.GetHashCode() * 2 + _transmitnl.GetHashCode() * 3 + _lineFeedRule.GetHashCode() * 4 + _terminalType.GetHashCode() * 5;
        }

        /// 
        /// 
        /// 
        public abstract object Clone();

#if !MACRODOC
        public abstract string Description { get; }

        public abstract string ShortDescription { get; }

        public virtual void Export(ConfigNode node)
        {
            node["encoding"] = EnumDescAttribute.For(typeof(EncodingType)).GetDescription(_encoding);
            node["terminal-type"] = EnumDescAttribute.For(typeof(TerminalType)).GetName(_terminalType);
            node["transmit-nl"] = EnumDescAttribute.For(typeof(NewLine)).GetName(_transmitnl);
            node["localecho"] = _localecho.ToString();
            node["linefeed"] = EnumDescAttribute.For(typeof(LineFeedRule)).GetName(_lineFeedRule);
            if (_caption != null && _caption.Length > 0)
            {
                node["caption"] = _caption;
            }

            if (_renderProfile != null)
            {
                _renderProfile.Export(node);
            }
        }
        public virtual void Import(ConfigNode data)
        {
            _encoding = ParseEncoding(data["encoding"]);
            _terminalType = (TerminalType)EnumDescAttribute.For(typeof(TerminalType)).FromName(data["terminal-type"], TerminalType.VT100);
            _transmitnl = (NewLine)EnumDescAttribute.For(typeof(NewLine)).FromName(data["transmit-nl"], NewLine.CR);
            _localecho = GUtil.ParseBool(data["localecho"], false);
            //_lineFeedByCR = GUtil.ParseBool((string)data["linefeed-by-cr"], false);
            _lineFeedRule = (LineFeedRule)EnumDescAttribute.For(typeof(LineFeedRule)).FromName(data["linefeed"], LineFeedRule.Normal);
            _caption = data["caption"];
            if (data.Contains("font-name")) //項目がなければ空のまま
            {
                _renderProfile = new RenderProfile(data);
            }
        }

        public EncodingProfile EncodingProfile
        {
            get
            {
                return EncodingProfile.Get(_encoding);
            }
            set
            {
                _encoding = value.Type;
            }
        }

#endif
        /// <summary>
        /// <ja>このTerminalParamで接続を開いたときの色・フォントなどの設定を収録したオブジェクトです。</ja>
        /// <en>Gets or sets the appearances of the console such as colors or fonts.</en>
        /// </summary>
        /// <remarks>
        /// <ja>特に何も指定しなかいかnullをセットすると、オプションダイアログで設定した内容が使用されます。</ja>
        /// <en>If you do not set anything or set null, the appearance is same as the setting of the option dialo.</en>
        /// </remarks>
        /// <seealso cref="RenderProfile"/>
        public RenderProfile RenderProfile
        {
            get
            {
                return _renderProfile;
            }
            set
            {
                _renderProfile = value;
            }
        }


        /// <summary>
        /// <ja>この接続のエンコーディングです。</ja>
        /// <en>Gets or sets the encoding of the connection.</en>
        /// </summary>
        public EncodingType Encoding
        {
            get
            {
                return _encoding;
            }
            set
            {
                _encoding = value;
            }
        }

        /// <summary>
        /// <ja>ターミナルの種別です。</ja>
        /// <en>Gets or sets the type of the terminal.</en>
        /// </summary>
        public TerminalType TerminalType
        {
            get
            {
                return _terminalType;
            }
            set
            {
                _terminalType = value;
            }
        }

        /// <summary>
        /// <ja>ログの種別です。</ja>
        /// <en>Gets or sets the type of the log.</en>
        /// </summary>
        public LogType LogType
        {
            get
            {
                return _logtype;
            }
            set
            {
                _logtype = value;
            }
        }
        /// <summary>
        /// <ja>ログファイルのフルパスです。</ja>
        /// <en>Gets or sets the full path of the log file.</en>
        /// </summary>
        public string LogPath
        {
            get
            {
                return _logpath;
            }
            set
            {
                _logpath = value;
            }
        }

        /// <summary>
        /// <ja>ログの種別とファイル名をオプションダイアログで指定した内容に基づき自動的に設定します。</ja>
        /// <en>Sets the type and the file name of the log based on the settings in the option dialog.</en>
        /// </summary>
        public void AutoFillLogPath()
        {
#if !MACRODOC
            _logtype = GEnv.Options.DefaultLogType;
            if (_logtype != LogType.None)
            {
                _logpath = GUtil.CreateLogFileName(ShortDescription);
            }
            else
            {
                _logpath = "";
            }
#endif
        }

        /// <summary>
        /// <ja>同名ファイルがある場合、ログファイルに追記するか上書きするかを指定します。</ja>
        /// <en>Specifies whether the connection appends or overwrites the log file in case that the file exists already.</en>
        /// </summary>
        public bool LogAppend
        {
            get
            {
                return _logappend;
            }
            set
            {
                _logappend = false;
            }
        }

        /// <summary>
        /// <ja>送信時の改行設定です。</ja>
        /// <en>Gets or sets the new-line characters for transmission.</en>
        /// </summary>
        public NewLine TransmitNL
        {
            get
            {
                return _transmitnl;
            }
            set
            {
                _transmitnl = value;
            }
        }

        /// <summary>
        /// <ja>ローカルエコーをするかどうかです。</ja>
        /// <en>Specifies whether the local echo is performed.</en>
        /// </summary>
        public bool LocalEcho
        {
            get
            {
                return _localecho;
            }
            set
            {
                _localecho = value;
            }
        }

        /// <summary>
        /// <ja>受信した文字に対して改行するかどうかです。</ja>
        /// <en>Specifies line breaking style corresponding to received characters.</en>
        /// </summary>
        public LineFeedRule LineFeedRule
        {
            get
            {
                return _lineFeedRule;
            }
            set
            {
                _lineFeedRule = value;
            }
        }

        /// <summary>
        /// <ja>タブなどに表示するための見出しです。</ja>
        /// <en>Gets or sets the caption of the tab.</en>
        /// <ja>特にセットしない場合、接続先のホスト名を利用して自動的につけられます。</ja>
        /// <en>If you do not specify anything, the caption is set automatically using the host name.</en>
        /// </summary>
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
            }
        }

#if !MACRODOC

        public void FeedLogOption()
        {
            if (GEnv.Options.DefaultLogType != LogType.None)
            {
                _logtype = GEnv.Options.DefaultLogType;
                _logpath = GUtil.CreateLogFileName(ShortDescription);
            }
        }
        public static TerminalParam CreateFromConfigNode(ConfigNode sec)
        {
            string type = sec["type"];
            TerminalParam param;
            if (type == "tcp")
            {
                param = new TelnetTerminalParam();
            }
            else
            {
                throw new Exception("invalid format");
            }

            param.Import(sec);
            return param;
        }

        private static EncodingType ParseEncoding(string val)
        {
            return (EncodingType)EnumDescAttribute.For(typeof(EncodingType)).FromDescription(val, EncodingType.UTF8);
        }

#endif
    }

    /// <summary>
    /// <ja>TCPに基づいた接続のパラメータを表現します。</ja>
    /// <en>Implements the parameters of the connection using TCP. (i.e. Telnet and SSH)</en>
    /// <seealso cref="TelnetTerminalParam"/>
    /// <seealso cref="SSHTerminalParam"/>
    /// </summary>
    [Serializable()]
    public abstract class TCPTerminalParam : TerminalParam
    {

        internal string _host;
        internal int _port;

        internal TCPTerminalParam()
        {
        }

        internal TCPTerminalParam(TCPTerminalParam r) : base(r)
        {
            _host = r._host;
            _port = r._port;
        }

        internal void Import(TCPTerminalParam r)
        {
            base.Import(r);
            _host = r._host;
            _port = r._port;
        }

        /// 
        /// 
        /// 
        public override bool Equals(object t_)
        {
            TCPTerminalParam t = t_ as TCPTerminalParam;
            if (t == null)
            {
                return false;
            }

            return base.Equals(t) && _host == t.Host && _port == t.Port;
        }
        /// 
        /// 
        /// 
        public override int GetHashCode()
        {
            return base.GetHashCode() + _host.GetHashCode() + _port.GetHashCode() * 2;
        }

        /// <summary>
        /// <ja>接続先のホスト名です。</ja>
        /// <en>Gets or sets the host name.</en>
        /// </summary>
        /// <remarks>
        /// <ja>または"192.168.10.1"などのIPアドレスの文字列表現も可能です。</ja>
        /// <en>The IP address format such as "192.168.10.1" is also allowed.</en>
        /// </remarks>
        public virtual string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        /// <summary>
        /// <ja>接続先のポート番号です。</ja>
        /// <en>Gets or sets the port number.</en>
        /// </summary>
        public virtual int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

#if !MACRODOC
        public override void Export(ConfigNode node)
        {
            node["type"] = "tcp";
            node["host"] = _host;
            node["port"] = _port.ToString();
            base.Export(node);
        }

        public override void Import(ConfigNode data)
        {
            _host = data["host"];
            _port = ParsePort(data["port"]);
            base.Import(data);
        }

        //TerminalUtilへ移動すべきかも
        private static int ParsePort(string val)
        {
            try
            {
                return Int32.Parse(val);
            }
            catch (FormatException e)
            {
                throw e;
            }
        }

        public static TCPTerminalParam Fake
        {
            get
            {
                TelnetTerminalParam p = new TelnetTerminalParam
                {
                    EncodingProfile = EncodingProfile.Get(EncodingType.UTF8)
                };
                return p;
            }
        }
        public override string ShortDescription
        {
            get
            {
                return _host;
            }
        }
#endif

    }

    /// <summary>
    /// <ja>Telnetによる接続パラメータを示すクラス</ja>
    /// <en>Implements the parameters of the Telnet connections.</en>
    /// </summary>
    [Serializable()]
    public class TelnetTerminalParam : TCPTerminalParam
    {

        /// <summary>
        /// <ja>ホスト名を指定して作成します。</ja>
        /// <en>Initializes with the host name.</en>
        /// <seealso cref="Macro.ConnectionList.Open"/>
        /// </summary>
        /// <remarks>
        /// <ja>ポートは23に設定されます。</ja>
        /// <en>The port number is set to 23.</en>
        /// <ja>他のパラメータは次のように初期化されます。</ja>
        /// <en>Other parameters are initialized as following:</en>
        /// <list type="table">
        ///   <item><term><ja>エンコーディング</ja><en>Encoding</en></term><description><ja>EUC-JP</ja><en>iso-8859-1</en></description></item>　
        ///   <item><term><ja>ターミナルタイプ</ja><en>Terminal Type</en></term><description>xterm</description></item>  
        ///   <item><term><ja>ログ</ja><en>Log</en></term><description><ja>取得しない</ja><en>None</en></description></item>　　　　　　　
        ///   <item><term><ja>ローカルエコー</ja><en>Local echo</en></term><description><ja>しない</ja><en>Don't</en></description></item>　　
        ///   <item><term><ja>送信時改行</ja><en>New line</en></term><description>CR</description></item>　　　　
        /// </list>
        /// <ja>接続を開くには、<see cref="Macro.ConnectionList.Open"/>メソッドの引数としてTelnetTerminalParamオブジェクトを渡します。</ja>
        /// <en>To open a new connection, pass the TelnetTerminalParam object to the <see cref="Macro.ConnectionList.Open"/> method.</en>
        /// </remarks>
        /// <param name="host"><ja>ホスト名</ja><en>The host name.</en></param>
        public TelnetTerminalParam(string host)
        {
            _host = host;
            _port = 23;
        }


        internal TelnetTerminalParam()
        {

        }

        internal TelnetTerminalParam(TelnetTerminalParam r) : base(r)
        {
        }

        /// 
        /// 
        /// 
        public override object Clone()
        {
            return new TelnetTerminalParam(this);
        }

#if !MACRODOC
        public override void Import(ConfigNode data)
        {
            base.Import(data);
        }
        public override string Description
        {
            get
            {
                return _host;
            }
        }
#endif
    }

    /// <summary>
    /// <ja>フローコントロールの設定</ja>
    /// <en>Specifies the flow control.</en>
    /// </summary>
    [EnumDesc(typeof(FlowControl))]
    public enum FlowControl
    {
        /// <summary>
        /// <ja>なし</ja>
        /// <en>None</en>
        /// </summary>
        [EnumValue(Description = "Enum.FlowControl.None")] None,
        /// <summary>
        /// X ON / X OFf
        /// </summary>
        [EnumValue(Description = "Enum.FlowControl.Xon_Xoff")] Xon_Xoff,
        /// <summary>
        /// <ja>ハードウェア</ja>
        /// <en>Hardware</en>
        /// </summary>
        [EnumValue(Description = "Enum.FlowControl.Hardware")] Hardware
    }

    /// <summary>
    /// <ja>パリティの設定</ja>
    /// <en>Specifies the parity.</en>
    /// </summary>
    [EnumDesc(typeof(Parity))]
    public enum Parity
    {
        /// <summary>
        /// <ja>なし</ja>
        /// <en>None</en>
        /// </summary>
        [EnumValue(Description = "Enum.Parity.NOPARITY")] NOPARITY = 0,
        /// <summary>
        /// <ja>奇数</ja>
        /// <en>Odd</en>
        /// </summary>
        [EnumValue(Description = "Enum.Parity.ODDPARITY")] ODDPARITY = 1,
        /// <summary>
        /// <ja>偶数</ja>
        /// <en>Even</en>
        /// </summary>
        [EnumValue(Description = "Enum.Parity.EVENPARITY")] EVENPARITY = 2
        //MARKPARITY  =        3,
        //SPACEPARITY =        4
    }

    /// <summary>
    /// <ja>ストップビットの設定</ja>
    /// <en>Specifies the stop bits.</en>
    /// </summary>
    [EnumDesc(typeof(StopBits))]
    public enum StopBits
    {
        /// <summary>
        /// <ja>1ビット</ja>
        /// <en>1 bit</en>
        /// </summary>
        [EnumValue(Description = "Enum.StopBits.ONESTOPBIT")] ONESTOPBIT = 0,
        /// <summary>
        /// <ja>1.5ビット</ja>
        /// <en>1.5 bits</en>
        /// </summary>
        [EnumValue(Description = "Enum.StopBits.ONE5STOPBITS")] ONE5STOPBITS = 1,
        /// <summary>
        /// <ja>2ビット</ja>
        /// <en>2 bits</en>
        /// </summary>
        [EnumValue(Description = "Enum.StopBits.TWOSTOPBITS")] TWOSTOPBITS = 2
    }
}