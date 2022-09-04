/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: KeyGenWizard.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Granados.PKI;
using Granados.SSHCV2;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// KeyGenWizard の概要の説明です。
    /// </summary>
    public class KeyGenWizard : Form
    {
        //現在のページ
        private enum Page
        {
            Parameter,
            Generation,
            Store
        }
        private Page _page;

        private KeyGenThread _keyGenThread;
        private SSH2UserAuthKey _resultKey;

        private Panel _parameterPanel;
        private Button _nextButton;
        public Button _cancelButton;
        private Label _promptLabel1;
        private Label _algorithmLabel;
        private Label _bitCountLabel;
        private ComboBox _algorithmBox;
        private ComboBox _bitCountBox;
        private Panel _generationPanel;
        private Label _keygenLabel;
        private ProgressBar _generationBar;
        private Panel _storePanel;
        private Label _completeLabel;
        private Button _storePrivateKey;
        private Button _storeSECSHPublicKeyButton;
        private Button _storeOpenSSHPublicKeyButton;
        private Label _passphraseLabel;
        private TextBox _passphraseBox;
        private TextBox _confirmBox;
        private Label _confirmLabel;
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private Container components = null;

        public KeyGenWizard()
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
            //
            if (!DesignMode)
            {
                Width = PanelPitch;
            }

            _confirmLabel.Text = "Form.KeyGenWizard._confirmLabel";
            _passphraseLabel.Text = "Form.KeyGenWizard._passphraseLabel";
            _bitCountLabel.Text = "Form.KeyGenWizard._bitCountLabel";
            _algorithmLabel.Text = "Form.KeyGenWizard._algorithmLabel";
            _promptLabel1.Text = "Form.KeyGenWizard._promptLabel1";
            _nextButton.Text = "Form.KeyGenWizard._nextButton";
            _cancelButton.Text = "Cancel";
            _keygenLabel.Text = "Form.KeyGenWizard._keygenLabel";
            _storePrivateKey.Text = "Form.KeyGenWizard._storePrivateKey";
            _storeSECSHPublicKeyButton.Text = "Form.KeyGenWizard._storeSECSHPublicKeyButton";
            _storeOpenSSHPublicKeyButton.Text = "Form.KeyGenWizard._storeOpenSSHPublicKeyButton";
            _completeLabel.Text = "Form.KeyGenWizard._completeLabel";
            Text = "Form.KeyGenWizard.Text";

            _page = Page.Parameter;
        }
        public void SetResultKey(SSH2UserAuthKey key)
        {
            _resultKey = key;
        }

        private int PanelPitch
        {
            get
            {
                return _parameterPanel.Width + 8;
            }
        }

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            _parameterPanel = new Panel();
            _confirmBox = new TextBox();
            _confirmLabel = new Label();
            _passphraseBox = new TextBox();
            _passphraseLabel = new Label();
            _bitCountBox = new ComboBox();
            _bitCountLabel = new Label();
            _algorithmLabel = new Label();
            _promptLabel1 = new Label();
            _algorithmBox = new ComboBox();
            _nextButton = new Button();
            _cancelButton = new Button();
            _generationPanel = new Panel();
            _generationBar = new ProgressBar();
            _keygenLabel = new Label();
            _storePanel = new Panel();
            _storeSECSHPublicKeyButton = new Button();
            _storeOpenSSHPublicKeyButton = new Button();
            _storePrivateKey = new Button();
            _completeLabel = new Label();
            _parameterPanel.SuspendLayout();
            _generationPanel.SuspendLayout();
            _storePanel.SuspendLayout();
            SuspendLayout();
            // 
            // _parameterPanel
            // 
            _parameterPanel.Controls.AddRange(new Control[] {
                                                                                          _confirmBox,
                                                                                          _confirmLabel,
                                                                                          _passphraseBox,
                                                                                          _passphraseLabel,
                                                                                          _bitCountBox,
                                                                                          _bitCountLabel,
                                                                                          _algorithmLabel,
                                                                                          _promptLabel1,
                                                                                          _algorithmBox});
            _parameterPanel.Location = new Point(0, 8);
            _parameterPanel.Name = "_parameterPanel";
            _parameterPanel.Size = new Size(304, 184);
            _parameterPanel.TabIndex = 0;
            // 
            // _confirmBox
            // 
            _confirmBox.Location = new Point(128, 128);
            _confirmBox.Name = "_confirmBox";
            _confirmBox.PasswordChar = '*';
            _confirmBox.Size = new Size(152, 19);
            _confirmBox.TabIndex = 8;
            _confirmBox.Text = "";
            // 
            // _confirmLabel
            // 
            _confirmLabel.Location = new Point(16, 128);
            _confirmLabel.Name = "_confirmLabel";
            _confirmLabel.TabIndex = 7;
            _confirmLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _passphraseBox
            // 
            _passphraseBox.Location = new Point(128, 104);
            _passphraseBox.Name = "_passphraseBox";
            _passphraseBox.PasswordChar = '*';
            _passphraseBox.Size = new Size(152, 19);
            _passphraseBox.TabIndex = 6;
            _passphraseBox.Text = "";
            // 
            // _passphraseLabel
            // 
            _passphraseLabel.Location = new Point(16, 104);
            _passphraseLabel.Name = "_passphraseLabel";
            _passphraseLabel.TabIndex = 5;
            _passphraseLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _bitCountBox
            // 
            _bitCountBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _bitCountBox.Items.AddRange(new object[] {
                                                              "768",
                                                              "1024",
                                                              "2048"});
            _bitCountBox.Location = new Point(128, 80);
            _bitCountBox.SelectedIndex = 0;
            _bitCountBox.Name = "_bitCountBox";
            _bitCountBox.Size = new Size(121, 20);
            _bitCountBox.TabIndex = 4;
            // 
            // _bitCountLabel
            // 
            _bitCountLabel.Location = new Point(16, 80);
            _bitCountLabel.Name = "_bitCountLabel";
            _bitCountLabel.TabIndex = 3;
            _bitCountLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _algorithmLabel
            // 
            _algorithmLabel.Location = new Point(16, 56);
            _algorithmLabel.Name = "_algorithmLabel";
            _algorithmLabel.TabIndex = 1;
            _algorithmLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _promptLabel1
            // 
            _promptLabel1.Location = new Point(8, 8);
            _promptLabel1.Name = "_promptLabel1";
            _promptLabel1.Size = new Size(288, 40);
            _promptLabel1.TabIndex = 0;
            // 
            // _algorithmBox
            // 
            _algorithmBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _algorithmBox.Items.AddRange(new object[] {
                                                               "DSA",
                                                               "RSA"});
            _algorithmBox.Location = new Point(128, 56);
            _algorithmBox.Name = "_algorithmBox";
            _algorithmBox.SelectedIndex = 0;
            _algorithmBox.Size = new Size(121, 20);
            _algorithmBox.TabIndex = 2;
            // 
            // _nextButton
            // 
            _nextButton.Location = new Point(224, 192);
            _nextButton.Name = "_nextButton";
            _nextButton.FlatStyle = FlatStyle.System;
            _nextButton.TabIndex = 1;
            _nextButton.Click += new EventHandler(OnNext);
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(136, 192);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.TabIndex = 2;
            // 
            // _generationPanel
            // 
            _generationPanel.Controls.AddRange(new Control[] {
                                                                                           _generationBar,
                                                                                           _keygenLabel});
            _generationPanel.Location = new Point(312, 8);
            _generationPanel.Name = "_generationPanel";
            _generationPanel.Size = new Size(304, 184);
            _generationPanel.TabIndex = 5;
            _generationPanel.Visible = false;
            // 
            // _generationBar
            // 
            _generationBar.Location = new Point(8, 80);
            _generationBar.Maximum = 200;
            _generationBar.Minimum = 0;
            _generationBar.Name = "_generationBar";
            _generationBar.Size = new Size(288, 24);
            _generationBar.Step = 1;
            _generationBar.TabIndex = 1;
            _generationBar.Value = 0;
            // 
            // _keygenLabel
            // 
            _keygenLabel.Location = new Point(8, 8);
            _keygenLabel.Name = "_keygenLabel";
            _keygenLabel.Size = new Size(288, 40);
            _keygenLabel.TabIndex = 0;
            // 
            // _storePanel
            // 
            _storePanel.Controls.AddRange(new Control[] {
                                                                                      _storeSECSHPublicKeyButton,
                                                                                      _storeOpenSSHPublicKeyButton,
                                                                                      _storePrivateKey,
                                                                                      _completeLabel});
            _storePanel.Location = new Point(624, 8);
            _storePanel.Name = "_storePanel";
            _storePanel.Size = new Size(304, 184);
            _storePanel.TabIndex = 6;
            _storePanel.Visible = false;
            // 
            // _storePrivateKey
            // 
            _storePrivateKey.Location = new Point(24, 56);
            _storePrivateKey.Name = "_storePrivateKey";
            _storePrivateKey.FlatStyle = FlatStyle.System;
            _storePrivateKey.Size = new Size(256, 23);
            _storePrivateKey.TabIndex = 2;
            _storePrivateKey.Click += new EventHandler(OnSavePrivateKey);
            // 
            // _storeSECSHPublicKeyButton
            // 
            _storeSECSHPublicKeyButton.Location = new Point(24, 96);
            _storeSECSHPublicKeyButton.Name = "_storeSECSHPublicKeyButton";
            _storeSECSHPublicKeyButton.FlatStyle = FlatStyle.System;
            _storeSECSHPublicKeyButton.Size = new Size(256, 23);
            _storeSECSHPublicKeyButton.TabIndex = 3;
            _storeSECSHPublicKeyButton.Click += new EventHandler(OnSaveSECSHPublicKey);
            // 
            // _storeOpenSSHPublicKeyButton
            // 
            _storeOpenSSHPublicKeyButton.Location = new Point(24, 136);
            _storeOpenSSHPublicKeyButton.Name = "_storeOpenSSHPublicKeyButton";
            _storeOpenSSHPublicKeyButton.FlatStyle = FlatStyle.System;
            _storeOpenSSHPublicKeyButton.Size = new Size(256, 23);
            _storeOpenSSHPublicKeyButton.TabIndex = 4;
            _storeOpenSSHPublicKeyButton.Click += new EventHandler(OnSaveOpenSSHPublicKey);
            // 
            // _completeLabel
            // 
            _completeLabel.Location = new Point(8, 8);
            _completeLabel.Name = "_completeLabel";
            _completeLabel.Size = new Size(288, 40);
            _completeLabel.TabIndex = 1;
            // 
            // KeyGenWizard
            // 
            AcceptButton = _nextButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(930, 223);
            Controls.AddRange(new Control[] {
                                                                          _storePanel,
                                                                          _cancelButton,
                                                                          _nextButton,
                                                                          _parameterPanel,
                                                                          _generationPanel});
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "KeyGenWizard";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            _parameterPanel.ResumeLayout(false);
            _generationPanel.ResumeLayout(false);
            _storePanel.ResumeLayout(false);
            ResumeLayout(false);

        }
        #endregion

        public ProgressBar GenerationBar
        {
            get
            {
                return _generationBar;
            }
        }
        private PublicKeyAlgorithm KeyAlgorithm
        {
            get
            {
                if (_algorithmBox.Text == "RSA")
                {
                    return PublicKeyAlgorithm.RSA;
                }
                else
                {
                    return PublicKeyAlgorithm.DSA;
                }
            }
        }
        private bool VerifyPassphrase()
        {
            if (_passphraseBox.Text != _confirmBox.Text)
            {
                GUtil.Warning(this, "Message.KeyGenWizard.NotMatch");
                return false;
            }
            else if (_passphraseBox.Text.Length == 0)
            {
                return DialogResult.Yes == GUtil.AskUserYesNo(this, "Message.KeyGenWizard.ConfirmEmptyPassphrase");
            }
            else
            {
                return true;
            }
        }


        private void OnNext(object sender, EventArgs args)
        {
            switch (_page)
            {
                case Page.Parameter:
                    if (!VerifyPassphrase())
                    {
                        return;
                    }

                    _parameterPanel.Visible = false;
                    _generationPanel.Visible = true;
                    _generationPanel.Left -= PanelPitch;
                    _keyGenThread = new KeyGenThread(this, KeyAlgorithm, Int32.Parse(_bitCountBox.Text));
                    MouseMove += new MouseEventHandler(_keyGenThread.OnMouseMove);
                    _generationPanel.MouseMove += new MouseEventHandler(_keyGenThread.OnMouseMove);
                    _nextButton.Enabled = false;
                    _page = Page.Generation;
                    _keyGenThread.Start();
                    break;
                case Page.Generation:
                    _generationPanel.Visible = false;
                    _storePanel.Visible = true;
                    _storePanel.Left -= PanelPitch * 2;
                    _page = Page.Store;
                    _nextButton.Text = "Message.KeyGenWizard.Finish";
                    break;
                case Page.Store:
                    Close();
                    break;
            }
        }

        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            if (msg.Msg == GConst.WMG_KEYGEN_PROGRESS)
            {
                _generationBar.Value = msg.LParam.ToInt32();
            }
            else if (msg.Msg == GConst.WMG_KEYGEN_FINISHED)
            {
                CheckGenerationComplete();
            }
        }
        protected override void OnClosed(EventArgs args)
        {
            if (_keyGenThread != null)
            {
                _keyGenThread.SetAbortFlag();
            }

            base.OnClosed(args);
        }

        public void SetProgressValue(int v)
        {
            _generationBar.Value = v;
            if (v == _generationBar.Maximum)
            {
                _keygenLabel.Text = "Message.KeyGenWizard.RandomNumberCompleted";
                Cursor = Cursors.WaitCursor;
                CheckGenerationComplete();
            }
        }
        private void CheckGenerationComplete()
        {
            //プログレスバーが終端にいくのと、鍵の生成が終わるのは両方満たさないといけない
            if (_generationBar.Value == _generationBar.Maximum && _resultKey != null)
            {
                _nextButton.Enabled = true;
                _keygenLabel.Text = "Message.KeyGenWizard.GenerationCompleted";
                Cursor = Cursors.Default;
            }
        }


        private void OnSavePrivateKey(object sender, EventArgs args)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                InitialDirectory = GApp.Options.DefaultKeyDir,
                Title = "Caption.KeyGenWizard.SavePrivateKey"
            };
            if (GCUtil.ShowModalDialog(this, dlg) == DialogResult.OK)
            {
                GApp.Options.DefaultKeyDir = GUtil.FileToDir(dlg.FileName);
                try
                {
                    string pp = _passphraseBox.Text;
                    if (pp.Length == 0)
                    {
                        pp = null; //空パスフレーズはnull指定
                    }

                    _resultKey.WritePrivatePartInSECSHStyleFile(new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write), "", pp);
                }
                catch (Exception ex)
                {
                    GUtil.Warning(this, String.Format("Message.KeyGenWizard.KeySaveError", ex.Message));
                }
            }
        }
        private void OnSaveSECSHPublicKey(object sender, EventArgs args)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                InitialDirectory = GApp.Options.DefaultKeyDir,
                Title = "Caption.KeyGenWizard.SavePublicInSECSH",
                DefaultExt = "pub",
                Filter = "SSH Public Key(*.pub)|*.pub|All Files(*.*)|*.*"
            };
            if (GCUtil.ShowModalDialog(this, dlg) == DialogResult.OK)
            {
                GApp.Options.DefaultKeyDir = GUtil.FileToDir(dlg.FileName);
                try
                {
                    _resultKey.WritePublicPartInSECSHStyle(new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write), "");
                }
                catch (Exception ex)
                {
                    GUtil.Warning(this, String.Format("Message.KeyGenWizard.KeySaveError", ex.Message));
                }
            }
        }
        private void OnSaveOpenSSHPublicKey(object sender, EventArgs args)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                InitialDirectory = GApp.Options.DefaultKeyDir,
                Title = "Caption.KeyGenWizard.SavePublicInOpenSSH",
                DefaultExt = "pub",
                Filter = "SSH Public Key(*.pub)|*.pub|All Files(*.*)|*.*"
            };
            if (GCUtil.ShowModalDialog(this, dlg) == DialogResult.OK)
            {
                GApp.Options.DefaultKeyDir = GUtil.FileToDir(dlg.FileName);
                try
                {
                    _resultKey.WritePublicPartInOpenSSHStyle(new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write));
                }
                catch (Exception ex)
                {
                    GUtil.Warning(this, String.Format("Message.KeyGenWizard.KeySaveError", ex.Message));
                }
            }
        }
    }

    /*
	 * いくつか試したところ、鍵作成にいくつの乱数が必要かはかなりばらつきがある。そこで次のようにする。
	 * 1. MouseMove100回を必ず受信する。
	 * 2. １回のMouseMoveにつき100個の乱数を計算する。100個消費したら次のMouseMoveが来るまでブロック。
	 * 4. 途中で鍵作成が終了しても、100個のMouseMoveが来るまではUI上は生成をしているふりをする。
	 */

    internal class KeyGenThread
    {
        private KeyGenWizard _parent;
        private PublicKeyAlgorithm _algorithm;
        private int _bitCount;
        private KeyGenRandomGenerator _rnd;
        private int _mouseMoveCount;

        public KeyGenThread(KeyGenWizard p, PublicKeyAlgorithm a, int b)
        {
            _parent = p;
            _algorithm = a;
            _bitCount = b;
            _rnd = new KeyGenRandomGenerator();
        }

        public void Start()
        {
            GUtil.CreateThread(new ThreadStart(EntryPoint)).Start();
        }
        public void SetAbortFlag()
        {
            _rnd.SetAbortFlag();
        }

        private void EntryPoint()
        {
            try
            {
                _mouseMoveCount = 0;
                KeyPair kp;
                if (_algorithm == PublicKeyAlgorithm.DSA)
                {
                    kp = DSAKeyPair.GenerateNew(_bitCount, _rnd);
                }
                else
                {
                    kp = RSAKeyPair.GenerateNew(_bitCount, _rnd);
                }

                _parent.SetResultKey(new SSH2UserAuthKey(kp));
                Win32.PostMessage(_parent.Handle, GConst.WMG_KEYGEN_FINISHED, IntPtr.Zero, IntPtr.Zero);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);

            }
        }

        //これはフォームのスレッドで実行される。注意！
        public void OnMouseMove(object sender, MouseEventArgs args)
        {

            if (_mouseMoveCount == _parent.GenerationBar.Maximum)
            {
                return;
            }

            int n = (int)DateTime.Now.Ticks;
            n ^= (args.X << 16);
            n ^= args.Y;
            n ^= (int)0x31031293; //これぐらいやれば十分ばらけるだろう

            if (++_mouseMoveCount == _parent.GenerationBar.Maximum)
            {
                _rnd.RefreshFinal(n);
            }
            else
            {
                _rnd.Refresh(n);
            }

            _parent.SetProgressValue(_mouseMoveCount);
        }

        private class KeyGenRandomGenerator : Random
        {
            private Random _internal;
            public int _doubleCount;
            private int _internalAvailableCount;
            private bool _abortFlag;

            public KeyGenRandomGenerator()
            {
                _internalAvailableCount = 0;
                _abortFlag = false;
            }

            public override double NextDouble()
            {

                while (_internalAvailableCount == 0)
                {
                    Thread.Sleep(100); //同期オブジェクトを使うまでもないだろう
                    if (_abortFlag)
                    {
                        throw new Exception("key generation aborted");
                    }
                }

                _internalAvailableCount--;
                _doubleCount++;
                return _internal.NextDouble();
            }
            //他はoverrideしない

            public void Refresh(int seed)
            {
                _internal = new Random(seed);
                _internalAvailableCount = 50;
            }
            public void RefreshFinal(int seed)
            {
                _internal = new Random(seed);
                _internalAvailableCount = Int32.MaxValue;
            }

            public void SetAbortFlag()
            {
                _abortFlag = true;
            }
        }
    }
}
