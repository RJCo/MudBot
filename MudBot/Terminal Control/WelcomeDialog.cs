using Poderosa.Config;
using Poderosa.ConnectionParam;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// ConfigConverter の概要の説明です。
    /// </summary>
    internal class WelcomeDialog : Form
    {
        private CID _cid;

        private Label _welcomeMessage;
        private RadioButton _optNewConnection;
        private RadioButton _optCygwin;
        private RadioButton _optConvert;
        private Button _cancelButton;
        private Button _okButton;
        private CheckBox _checkNext;

        private void InitializeComponent()
        {
            _welcomeMessage = new Label();
            _optNewConnection = new RadioButton();
            _optCygwin = new RadioButton();
            _optConvert = new RadioButton();
            _cancelButton = new Button();
            _okButton = new Button();
            _checkNext = new CheckBox();
            SuspendLayout();
            // 
            // _welcomeMessage
            // 
            _welcomeMessage.Location = new Point(8, 8);
            _welcomeMessage.Name = "_welcomeMessage";
            _welcomeMessage.Size = new Size(336, 56);
            _welcomeMessage.TabIndex = 0;
            // 
            // _optNewConnection
            // 
            _optNewConnection.Checked = true;
            _optNewConnection.FlatStyle = FlatStyle.System;
            _optNewConnection.Location = new Point(16, 72);
            _optNewConnection.Name = "_optNewConnection";
            _optNewConnection.Size = new Size(320, 24);
            _optNewConnection.TabIndex = 1;
            _optNewConnection.TabStop = true;
            // 
            // _optCygwin
            // 
            _optCygwin.FlatStyle = FlatStyle.System;
            _optCygwin.Location = new Point(16, 96);
            _optCygwin.Name = "_optCygwin";
            _optCygwin.Size = new Size(320, 24);
            _optCygwin.TabIndex = 2;
            // 
            // _optConvert
            // 
            _optConvert.FlatStyle = FlatStyle.System;
            _optConvert.Location = new Point(16, 120);
            _optConvert.Name = "_optConvert";
            _optConvert.Size = new Size(320, 24);
            _optConvert.TabIndex = 3;
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.Location = new Point(264, 184);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.TabIndex = 4;
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.Location = new Point(168, 184);
            _okButton.Name = "_okButton";
            _okButton.TabIndex = 5;
            _okButton.Click += new EventHandler(OnOK);
            // 
            // _checkNext
            // 
            _checkNext.FlatStyle = FlatStyle.System;
            _checkNext.Location = new Point(144, 160);
            _checkNext.Name = "_checkNext";
            _checkNext.Size = new Size(200, 16);
            _checkNext.TabIndex = 6;
            // 
            // WelcomeDialog
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(354, 216);
            Controls.Add(_checkNext);
            Controls.Add(_okButton);
            Controls.Add(_cancelButton);
            Controls.Add(_optConvert);
            Controls.Add(_optCygwin);
            Controls.Add(_optNewConnection);
            Controls.Add(_welcomeMessage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WelcomeDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);

        }
        public CID CID
        {
            get
            {
                return _cid;
            }
        }

        public WelcomeDialog()
        {
            _cid = CID.NOP;
            InitializeComponent();
            InitializeText();
            // 
            // TODO: コンストラクタ ロジックをここに追加してください。
            //
            _checkNext.Checked = GApp.Options.ShowWelcomeDialog;
        }
        private void InitializeText()
        {
            Text = "Form.WelcomeDialog.Text";
            _welcomeMessage.Text = "Form.WelcomeDialog._welcomeMessage";
            _optNewConnection.Text = "Form.WelcomeDialog._optNewConnection";
            _optCygwin.Text = "Form.WelcomeDialog._optCygwin";
            _optConvert.Text = "Form.WelcomeDialog._optConvert";
            _cancelButton.Text = "Cancel";
            _okButton.Text = "OK";
            _checkNext.Text = "Form.WelcomeDialog._checkNext";
        }

        private void OnOK(object sender, EventArgs args)
        {
            if (_optNewConnection.Checked)
            {
                _cid = CID.NewConnection;
            }
            else if (_optCygwin.Checked)
            {
                _cid = CID.NewCygwinConnection;
            }
            else if (_optConvert.Checked)
            {
                StartConvert();
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            GApp.Options.ShowWelcomeDialog = _checkNext.Checked;
        }


        private void StartConvert()
        {
            string dir = SelectDirectory();
            if (dir == null)
            {
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                ImportConfig(dir + "\\options.conf");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                GUtil.Warning(this, ex.Message);
                DialogResult = DialogResult.None;
                return;
            }
        }
        public string SelectDirectory()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                Description = "Caption.WelcomeDialog.SelectConfigDirectory"
            };
            string initial_dir = GuessVaraTermDir();
            if (initial_dir != null)
            {
                dlg.SelectedPath = initial_dir;
            }

            if (GCUtil.ShowModalDialog(this, dlg) == DialogResult.OK)
            {
                return dlg.SelectedPath;
            }
            else
            {
                return null;
            }
        }

        private void ImportConfig(string filename)
        {
            TextReader reader = null;
            try
            {
                GApp.ConnectionHistory.Clear();
                reader = new StreamReader(filename, Encoding.Default);
                reader.ReadLine(); //skip header
                string line = reader.ReadLine();
                int mru_count = 0;
                while (line != null)
                {
                    //全設定をインポートするわけではない
                    if (line.EndsWith("section terminal {"))
                    {
                        ImportTerminalSettings(ReadStringPair(reader));
                    }
                    else if (line.EndsWith("section key-definition {"))
                    {
                        ImportKeySettings(ReadStringPair(reader));
                    }
                    else if (line.EndsWith("section connection {"))
                    {
                        Hashtable t = ReadStringPair(reader);
                        if (t.Contains("type"))
                        { //socks設定と混同しないように
                            ImportConnectionSettings(t);
                            mru_count++;
                        }
                    }
                    line = reader.ReadLine();
                }
                GApp.Options.MRUSize = Math.Max(mru_count, 4);
                GApp.Frame.AdjustMRUMenu();
                GApp.Frame.ApplyHotKeys();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        private void ImportTerminalSettings(Hashtable values)
        {
            ContainerOptions opt = GApp.Options;
            opt.LeftAltKey = (AltKeyAction)GUtil.ParseEnum(typeof(AltKeyAction), (string)values["left-alt"], opt.LeftAltKey);
            opt.RightAltKey = (AltKeyAction)GUtil.ParseEnum(typeof(AltKeyAction), (string)values["right-alt"], opt.RightAltKey);
            opt.AutoCopyByLeftButton = GUtil.ParseBool((string)values["auto-copy-by-left-button"], opt.AutoCopyByLeftButton);
            opt.RightButtonAction = (RightButtonAction)GUtil.ParseEnum(typeof(RightButtonAction), (string)values["right-button"], opt.RightButtonAction);
            opt.TerminalBufferSize = GUtil.ParseInt((string)values["buffer-size"], opt.TerminalBufferSize);
            string fontname = (string)values["font-family"];
            string ja_fontname = (string)values["japanese-font-family"];
            float size = GUtil.ParseFloat((string)values["font-size"], opt.FontSize);
            opt.Font = new Font(fontname, size);
            opt.JapaneseFont = new Font(ja_fontname, size);
            opt.UseClearType = GUtil.ParseBool((string)values["cleartype"], opt.UseClearType);
            opt.BGColor = GUtil.ParseColor((string)values["bg-color"], opt.BGColor);
            opt.TextColor = GUtil.ParseColor((string)values["text-color"], opt.TextColor);
            opt.ESColorSet.Load((string)values["escapesequence-color"]);
            opt.BackgroundImageFileName = (string)values["bg-image"];
            opt.ImageStyle = (ImageStyle)GUtil.ParseEnum(typeof(ImageStyle), (string)values["bg-style"], opt.ImageStyle);
            opt.DefaultLogType = (LogType)GUtil.ParseEnum(typeof(LogType), (string)values["default-log-type"], opt.DefaultLogType);
            opt.DefaultLogDirectory = (string)values["default-log-directory"];
        }
        private void ImportKeySettings(Hashtable values)
        {
            IDictionaryEnumerator ie = values.GetEnumerator();
            while (ie.MoveNext())
            {
                string name = (string)ie.Key;
                CID cid = (CID)GUtil.ParseEnum(typeof(CID), name, CID.NOP);
                if (cid != CID.NOP)
                {
                    Keys k = GUtil.ParseKey(((string)ie.Value).Split(','));
                    GApp.Options.Commands.ModifyKey(cid, k & Keys.Modifiers, k & Keys.KeyCode);
                }
            }
        }
        private void ImportConnectionSettings(Hashtable values)
        {
            ConfigNode cn = ConfigNode.CreateIndirect("", values);
            GApp.ConnectionHistory.Append(TerminalParam.CreateFromConfigNode(cn));
        }

        private Hashtable ReadStringPair(TextReader reader)
        {
            Hashtable r = new Hashtable();
            string line = reader.ReadLine();
            while (!line.EndsWith("}"))
            {
                int start = 0;
                while (line[start] == '\t')
                {
                    start++;
                }

                int eq = line.IndexOf('=', start);
                if (eq != -1)
                {
                    string name = line.Substring(start, eq - start);
                    string value = line.Substring(eq + 1);
                    r[name] = value;
                }
                line = reader.ReadLine();
            }
            return r;
        }

        private string GuessVaraTermDir()
        {
            string candidate1 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Terminal Emulator VaraTerm";
            string candidate2 = candidate1 + "\\" + Environment.UserName;
            if (Directory.Exists(candidate2))
            {
                return candidate2;
            }
            else if (Directory.Exists(candidate1))
            {
                return candidate1;
            }
            else
            {
                return null;
            }
        }

    }
}
