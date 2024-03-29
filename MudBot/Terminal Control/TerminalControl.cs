using Poderosa;
using Poderosa.Communication;
using Poderosa.Connection;
using Poderosa.ConnectionParam;
using Poderosa.Terminal;
using Poderosa.Toolkit;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WalburySoftware
{
    public class TerminalControl : Control
    {
        #region fields

        #endregion

        #region Constructors
        public TerminalControl(string Hostname, int Port)
        {
            Host = Hostname;
            this.Port = Port;

            InitializeTerminalPane();
        }

        public TerminalControl()
        {
            InitializeTerminalPane();
        }

        private void InitializeTerminalPane()
        {
            if (GApp._frame == null)
            {
                string[] args = new string[0];
                GApp.Run(args);
                GApp._frame._multiPaneControl.InitUI(null, GApp.Options);
                GEnv.InterThreadUIService.MainFrameHandle = GApp._frame.Handle;
            }
            TerminalPane = new TerminalPane();
            TerminalPane.Dock = DockStyle.Fill;
            Controls.Add(TerminalPane);
        }
        #endregion

        #region methods
        public void ApplyNewDisplayDialog()
        {

        }

        public void Connect()
        {
            // Save old log info in case this is a reconnect
            Poderosa.ConnectionParam.LogType logType = Poderosa.ConnectionParam.LogType.Default;
            string file = null;
            if (TerminalPane.Connection != null)
            {
                logType = TerminalPane.Connection.LogType;
                file = TerminalPane.Connection.LogPath;
                TerminalPane.Connection.Close();
                TerminalPane.Detach();
            }

            try
            {
                TCPTerminalParam connParam = null;
                SocketWithTimeout swt = null;
                CommunicationUtil.SilentClient s = new CommunicationUtil.SilentClient();
                Size sz = Size;

                connParam = new TelnetTerminalParam(Host)
                {
                    Encoding = EncodingType.ISO8859_1,
                    Port = Port,
                    RenderProfile = new RenderProfile(),
                    TerminalType = TerminalType.XTerm
                };

                swt = new TelnetConnector((TelnetTerminalParam)connParam, sz);

                swt.AsyncConnect(s, connParam.Host, connParam.Port);

                ConnectionTag ct = s.Wait(swt);

                TerminalPane.FakeVisible = true;

                TerminalPane.Attach(ct);
                ct.Receiver.Listen();

                //-------------------------------------------------------------
                if (file != null)
                {
                    SetLog((LogType)logType, file, true);
                }
                TerminalPane.ConnectionTag.RenderProfile = new RenderProfile();
                SetPaneColors(Color.LightBlue, Color.Black);
            }
            catch
            {
                //MessageBox.Show(e.Message, "Connection Error");
                return;
            }
        }

        public void Close()
        {
            if (TerminalPane.Connection != null)
            {
                TerminalPane.Connection.Close();
                TerminalPane.Detach();
            }
        }

        public void SendText(string command)
        {
            TerminalPane.Connection.WriteChars(command.ToCharArray());
        }

        public string GetLastLine()
        {
            return new string(TerminalPane.Document.LastLine.Text);
        }

        /// <summary>
        /// Wait until some data is recieved
        /// </summary>
        public void WaitConnected()
        {
            while (TerminalPane.Connection.ReceivedDataSize == 0)
            { }
        }

        /// <summary>
        /// Create New Log
        /// </summary>
        /// <param name="logType">I guess just use Default all the time here</param>
        /// <param name="File">This should be a full path. Example: @"C:\Temp\logfilename.txt"</param>
        /// <param name="append">Set this to true</param>
        public void SetLog(LogType logType, string File, bool append)
        {
            // make sure directory exists
            string dir = File.Substring(0, File.LastIndexOf(@"\"));
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }

            TerminalPane.Connection.ResetLog((Poderosa.ConnectionParam.LogType)logType, File, append);
        }

        public void CommentLog(string comment)
        {
            DateTime dt = new DateTime();
            string s = "\r\n----- Comment added " + dt.Date + " -----\r\n";

            TerminalPane.Connection.TextLogger.Comment(s);
            TerminalPane.Connection.BinaryLogger.Comment(s);


            TerminalPane.Connection.TextLogger.Comment(comment);
            TerminalPane.Connection.BinaryLogger.Comment(comment);

            s = "\r\n----------------------------------------------\r\n";
            TerminalPane.Connection.TextLogger.Comment(s);
            TerminalPane.Connection.BinaryLogger.Comment(s);

        }

        public void SetPaneColors(Color TextColor, Color BackColor)
        {
            RenderProfile prof = TerminalPane.ConnectionTag.RenderProfile;
            prof.BackColor = BackColor;
            prof.ForeColor = TextColor;

            TerminalPane.ApplyRenderProfile(prof);
        }

        public void CopySelectedTextToClipboard()
        {
            //GApp.GlobalCommandTarget.Copy();

            if (GEnv.TextSelection.IsEmpty)
            {
                return;
            }

            string t = GEnv.TextSelection.GetSelectedText();
            if (t.Length > 0)
            {
                Clipboard.SetDataObject(t, false);
            }
        }

        public void PasteTextFromClipboard()
        {
            string value = (string)Clipboard.GetDataObject().GetData("Text");
            if (value == null || value.Length == 0 || TerminalPane == null || TerminalPane.ConnectionTag == null)
            {
                return;
            }

            PasteProcessor p = new PasteProcessor(TerminalPane.ConnectionTag, value);
            p.Perform();

        }
        #endregion

        #region Properties
        public TerminalPane TerminalPane { get; private set; }

        public string Host { get; set; } = "";

        public int Port { get; set; } = 23;

        public static int ScrollBackBuffer
        {
            get => GApp.Options.TerminalBufferSize;
            set => GApp.Options.TerminalBufferSize = value;
        }
        #endregion

        #region overrides
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // this don't work :(
            //Console.WriteLine(e.KeyCode);
            base.OnKeyDown(e);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //Console.WriteLine(e.KeyChar);
            // this don't work :(
            base.OnKeyPress(e);
        }
        protected override void OnGotFocus(EventArgs e)
        {

            base.OnGotFocus(e);

            TerminalPane.Focus();
        }
        #endregion
    }

    #region enums
    public enum LogType
    {
        /// <summary>
        /// <en>The log is not recorded.</en>
        /// </summary>
        [EnumValue(Description = "Enum.LogType.None")]
        None,
        /// <summary>
        /// <en>The log is a plain text file. This is standard.</en>
        /// </summary>
        [EnumValue(Description = "Enum.LogType.Default")]
        Default,
        /// <summary>
        /// <en>The log is a binary file.</en>
        /// </summary>
        [EnumValue(Description = "Enum.LogType.Binary")]
        Binary,
        /// <summary>
        /// <en>The log is an XML file. We may ask you to record the log in this type for debugging.</en>
        /// </summary>
        [EnumValue(Description = "Enum.LogType.Xml")]
        Xml
    }
    #endregion
}
