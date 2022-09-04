/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: GDialogBar.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Communication;
using Poderosa.Config;
using Poderosa.ConnectionParam;
using Poderosa.UI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using EnumDescAttributeT = Poderosa.Toolkit.EnumDescAttribute;

namespace Poderosa.Forms
{
    /// <summary>
    /// GDialogBar の概要の説明です。
    /// 
    /// ツールバーは、標準的なアイコンは24pxピッチ、アイコン自体は16px。縦棒のエリアは8px
    /// </summary>
    internal class GDialogBar : UserControl
    {
        private GButton _newConnection;
        private GButton _newSerialConnection;
        private GButton _newCygwinConnection;
        private GButton _newSFUConnection;
        private GButton _openShortcut;
        private GButton _saveShortcut;
        private ToggleButton _singleStyle;
        private ToggleButton _divHorizontalStyle;
        private ToggleButton _divVerticalStyle;
        private ToggleButton _divHorizontal3Style;
        private ToggleButton _divVertical3Style;
        private Label _newLineLabel;
        private ComboBox _newLineOption;
        private ToggleButton _localEcho;
        private GButton _lineFeedRule;
        private ToggleButton _logSuspend;
        private GButton _commentLog;
        private GButton _serverInfo;
        private Label _encodingLabel;
        private ComboBox _encodingBox;
        private IContainer components = null;
        private bool _toolTipInitialized;
        private ToolTip _toolTip;
        private bool _blockEventHandler;

        public GDialogBar()
        {
            // この呼び出しは、Windows.Forms フォーム デザイナで必要です。
            InitializeComponent();
            _newLineOption.BringToFront(); //日本語・英語で若干配置を変える都合で、これをトップにもってくる
            ReloadLanguage(GEnv.Options.Language);

            _toolTipInitialized = false;
            // TODO: InitForm を呼び出しの後に初期化処理を追加します。
            LoadImages();
        }
        public void ReloadLanguage(Language l)
        {
            _encodingLabel.Text = "Form.GDialogBar._encodingLabel";
            _newLineLabel.Text = "Form.GDialogBar._newLineLabel";
            _newLineOption.Left = l == Language.Japanese ? 340 : 356; //配置の都合
            InitToolTip();
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

        #region Component Designer generated code
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// this._newLineOption.Items.AddRange(EnumDescAttributeT.For(typeof(NewLine)).DescriptionCollection());
        /// 
        /// this._encodingBox.Items.AddRange(EnumDescAttributeT.For(typeof(EncodingType)).DescriptionCollection());
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GDialogBar));
            _openShortcut = new GButton();
            _newConnection = new GButton();
            _newSerialConnection = new GButton();
            _newCygwinConnection = new GButton();
            _newSFUConnection = new GButton();
            _saveShortcut = new GButton();
            _singleStyle = new ToggleButton();
            _divHorizontalStyle = new ToggleButton();
            _divVerticalStyle = new ToggleButton();
            _divHorizontal3Style = new ToggleButton();
            _divVertical3Style = new ToggleButton();
            _newLineLabel = new Label();
            _newLineOption = new ComboBox();
            _logSuspend = new ToggleButton();
            _lineFeedRule = new GButton();
            _localEcho = new ToggleButton();
            _serverInfo = new GButton();
            _commentLog = new GButton();
            _encodingLabel = new Label();
            _encodingBox = new ComboBox();
            SuspendLayout();
            // 
            // _newConnection
            // 
            _newConnection.BorderStyle = BorderStyle.None;
            _newConnection.ForeColor = SystemColors.ControlText;
            _newConnection.Location = new Point(8, 2);
            _newConnection.Name = "_newConnection";
            _newConnection.Size = new Size(24, 23);
            _newConnection.TabIndex = 0;
            _newConnection.TabStop = false;
            _newConnection.Click += new EventHandler(OpenNewConnection);
            _newConnection.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _newConnection.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _newConnection.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _newSerialConnection
            // 
            _newSerialConnection.BorderStyle = BorderStyle.None;
            _newSerialConnection.ForeColor = SystemColors.ControlText;
            _newSerialConnection.Location = new Point(32, 2);
            _newSerialConnection.Name = "_newSerialConnection";
            _newSerialConnection.Size = new Size(24, 23);
            _newSerialConnection.TabIndex = 0;
            _newSerialConnection.TabStop = false;
            _newSerialConnection.Click += new EventHandler(OpenNewSerialConnection);
            _newSerialConnection.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _newSerialConnection.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _newSerialConnection.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _newCygwinConnection
            // 
            _newCygwinConnection.BorderStyle = BorderStyle.None;
            _newCygwinConnection.ForeColor = SystemColors.ControlText;
            _newCygwinConnection.Location = new Point(56, 2);
            _newCygwinConnection.Name = "_newCygwinConnection";
            _newCygwinConnection.Size = new Size(24, 23);
            _newCygwinConnection.TabIndex = 0;
            _newCygwinConnection.TabStop = false;
            _newCygwinConnection.Click += new EventHandler(OpenNewCygwinConnection);
            _newCygwinConnection.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _newCygwinConnection.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _newCygwinConnection.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _newSFUConnection
            // 
            _newSFUConnection.BorderStyle = BorderStyle.None;
            _newSFUConnection.ForeColor = SystemColors.ControlText;
            _newSFUConnection.Location = new Point(80, 2);
            _newSFUConnection.Name = "_newSFUConnection";
            _newSFUConnection.Size = new Size(24, 23);
            _newSFUConnection.TabIndex = 0;
            _newSFUConnection.TabStop = false;
            _newSFUConnection.Click += new EventHandler(OpenNewSFUConnection);
            _newSFUConnection.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _newSFUConnection.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _newSFUConnection.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _openShortcut
            // 
            _openShortcut.BorderStyle = BorderStyle.None;
            _openShortcut.ForeColor = SystemColors.ControlText;
            _openShortcut.Location = new Point(112, 2);
            _openShortcut.Name = "_openShortcut";
            _openShortcut.Size = new Size(24, 23);
            _openShortcut.TabIndex = 0;
            _openShortcut.TabStop = false;
            _openShortcut.Click += new EventHandler(OpenShortCut);
            _openShortcut.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _openShortcut.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _openShortcut.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _saveShortcut
            // 
            _saveShortcut.BorderStyle = BorderStyle.None;
            _saveShortcut.Enabled = false;
            _saveShortcut.Location = new Point(136, 2);
            _saveShortcut.Name = "_saveShortcut";
            _saveShortcut.Size = new Size(24, 23);
            _saveShortcut.TabIndex = 0;
            _saveShortcut.TabStop = false;
            _saveShortcut.Click += new EventHandler(SaveShortCut);
            _saveShortcut.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _saveShortcut.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _saveShortcut.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _singleStyle
            // 
            _singleStyle.BorderStyle = BorderStyle.None;
            _singleStyle.Checked = false;
            _singleStyle.TabStop = false;
            _singleStyle.AutoToggle = false;
            _singleStyle.Location = new Point(168, 2);
            _singleStyle.Name = "_singleStyle";
            _singleStyle.Size = new Size(24, 23);
            _singleStyle.TabIndex = 1;
            _singleStyle.Click += new EventHandler(ToggleSingleStyle);
            _singleStyle.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _singleStyle.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _singleStyle.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _divHorizontalStyle
            // 
            _divHorizontalStyle.BorderStyle = BorderStyle.None;
            _divHorizontalStyle.Checked = false;
            _divHorizontalStyle.AutoToggle = false;
            _divHorizontalStyle.Location = new Point(192, 2);
            _divHorizontalStyle.Name = "_divHorizontalStyle";
            _divHorizontalStyle.Size = new Size(24, 23);
            _divHorizontalStyle.TabStop = false;
            _divHorizontalStyle.TabIndex = 2;
            _divHorizontalStyle.Click += new EventHandler(ToggleDivHorizontalStyle);
            _divHorizontalStyle.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _divHorizontalStyle.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _divHorizontalStyle.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _divVerticalStyle
            // 
            _divVerticalStyle.BorderStyle = BorderStyle.None;
            _divVerticalStyle.Checked = false;
            _divVerticalStyle.AutoToggle = false;
            _divVerticalStyle.Location = new Point(216, 2);
            _divVerticalStyle.Name = "_divVerticalStyle";
            _divVerticalStyle.Size = new Size(24, 23);
            _divVerticalStyle.TabStop = false;
            _divVerticalStyle.Click += new EventHandler(ToggleDivVerticalStyle);
            _divVerticalStyle.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _divVerticalStyle.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _divVerticalStyle.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _divHorizontal3Style
            // 
            _divHorizontal3Style.BorderStyle = BorderStyle.None;
            _divHorizontal3Style.Checked = false;
            _divHorizontal3Style.AutoToggle = false;
            _divHorizontal3Style.Location = new Point(240, 2);
            _divHorizontal3Style.Name = "_divHorizontal3Style";
            _divHorizontal3Style.Size = new Size(24, 23);
            _divHorizontal3Style.TabStop = false;
            _divHorizontal3Style.Click += new EventHandler(ToggleDivHorizontal3Style);
            _divHorizontal3Style.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _divHorizontal3Style.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _divHorizontal3Style.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _divVertical3Style
            // 
            _divVertical3Style.BorderStyle = BorderStyle.None;
            _divVertical3Style.Checked = false;
            _divVertical3Style.AutoToggle = false;
            _divVertical3Style.Location = new Point(264, 2);
            _divVertical3Style.Name = "_divVertical3Style";
            _divVertical3Style.Size = new Size(24, 23);
            _divVertical3Style.TabStop = false;
            _divVertical3Style.Click += new EventHandler(ToggleDivVertical3Style);
            _divVertical3Style.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _divVertical3Style.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _divVertical3Style.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _newLineLabel
            // 
            _newLineLabel.Location = new Point(296, 7);
            _newLineLabel.Name = "_newLineLabel";
            _newLineLabel.Size = new Size(60, 15);
            _newLineLabel.TabIndex = 0;
            _newLineLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _newLineOption
            // 
            _newLineOption.DropDownStyle = ComboBoxStyle.DropDownList;
            _newLineOption.Enabled = false;
            _newLineOption.Items.AddRange(EnumDescAttributeT.For(typeof(NewLine)).DescriptionCollection());
            _newLineOption.Location = new Point(340, 4);
            _newLineOption.Name = "_newLineOption";
            _newLineOption.Size = new Size(72, 20);
            _newLineOption.TabIndex = 0;
            _newLineOption.TabStop = false;
            _newLineOption.SelectedIndexChanged += new EventHandler(ChangeNewLine);
            // 
            // _encodingLabel
            // 
            _encodingLabel.Location = new Point(416, 7);
            _encodingLabel.Name = "_encodingLabel";
            _encodingLabel.Size = new Size(80, 15);
            _encodingLabel.TabIndex = 0;
            _encodingLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // _encodingBox
            // 
            _encodingBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _encodingBox.Enabled = false;
            _encodingBox.Location = new Point(496, 4);
            _encodingBox.Name = "_encodingBox";
            _encodingBox.Size = new Size(96, 20);
            _encodingBox.TabIndex = 0;
            _encodingBox.TabStop = false;
            _encodingBox.SelectedIndexChanged += new EventHandler(ChangeEncoding);
            _encodingBox.Items.AddRange(EnumDescAttributeT.For(typeof(EncodingType)).DescriptionCollection());
            // 
            // _localEcho
            // 
            _localEcho.BorderStyle = BorderStyle.None;
            _localEcho.Checked = false;
            _localEcho.Enabled = false;
            _localEcho.Location = new Point(600, 2);
            _localEcho.Name = "_localEcho";
            _localEcho.Size = new Size(24, 23);
            _localEcho.TabIndex = 0;
            _localEcho.TabStop = false;
            _localEcho.Click += new EventHandler(ToggleLocalEcho);
            _localEcho.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _localEcho.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _localEcho.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _lineFeedRule
            // 
            _lineFeedRule.BorderStyle = BorderStyle.None;
            _lineFeedRule.Enabled = false;
            _lineFeedRule.Location = new Point(624, 2);
            _lineFeedRule.Name = "_lineFeedRule";
            _lineFeedRule.Size = new Size(24, 23);
            _lineFeedRule.TabIndex = 0;
            _lineFeedRule.TabStop = false;
            _lineFeedRule.Click += new EventHandler(LineFeedRule);
            _lineFeedRule.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _lineFeedRule.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _lineFeedRule.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _logSuspend
            // 
            _logSuspend.BorderStyle = BorderStyle.None;
            _logSuspend.Checked = false;
            _logSuspend.Enabled = false;
            _logSuspend.Location = new Point(650, 2);
            _logSuspend.Name = "_logSuspend";
            _logSuspend.Size = new Size(24, 23);
            _logSuspend.TabIndex = 0;
            _logSuspend.TabStop = false;
            _logSuspend.Click += new EventHandler(ToggleLogSwitch);
            _logSuspend.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _logSuspend.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _logSuspend.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _commentLog
            // 
            _commentLog.BorderStyle = BorderStyle.None;
            _commentLog.Enabled = false;
            _commentLog.Location = new Point(672, 2);
            _commentLog.Name = "_commentLog";
            _commentLog.Size = new Size(24, 23);
            _commentLog.TabIndex = 0;
            _commentLog.TabStop = false;
            _commentLog.Click += new EventHandler(CommentLog);
            _commentLog.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _commentLog.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _commentLog.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // _serverInfo
            // 
            _serverInfo.BorderStyle = BorderStyle.None;
            _serverInfo.Enabled = false;
            _serverInfo.Location = new Point(704, 2);
            _serverInfo.Name = "_serverInfo";
            _serverInfo.Size = new Size(24, 23);
            _serverInfo.TabIndex = 0;
            _serverInfo.TabStop = false;
            _serverInfo.Click += new EventHandler(ShowServerInfo);
            _serverInfo.MouseEnter += new EventHandler(OnMouseEnterToButton);
            _serverInfo.MouseHover += new EventHandler(OnMouseHoverOnButton);
            _serverInfo.MouseLeave += new EventHandler(OnMouseLeaveFromButton);
            // 
            // GDialogBar
            // 
            Controls.AddRange(new Control[] {
                                                                          _newConnection,
                                                                          _newSerialConnection,
                                                                          _newCygwinConnection,
                                                                          _newSFUConnection,
                                                                          _openShortcut,
                                                                          _saveShortcut,
                                                                          _singleStyle,
                                                                          _divHorizontalStyle,
                                                                          _divVerticalStyle,
                                                                          _divHorizontal3Style,
                                                                          _divVertical3Style,
                                                                          _newLineLabel,
                                                                          _newLineOption,
                                                                          _encodingLabel,
                                                                          _encodingBox,
                                                                          _lineFeedRule,
                                                                          _commentLog,
                                                                          _logSuspend,
                                                                          _localEcho,
                                                                          _serverInfo});
            Name = "GDialogBar";
            Size = new Size(664, 24);
            TabStop = false;
            ResumeLayout(false);

        }
        #endregion

        private void LoadImages()
        {
            _openShortcut.Image = IconList.LoadIcon(IconList.ICON_OPEN);
            _newConnection.Image = IconList.LoadIcon(IconList.ICON_NEWCONNECTION);
            _newSerialConnection.Image = IconList.LoadIcon(IconList.ICON_SERIAL);
            _newCygwinConnection.Image = IconList.LoadIcon(IconList.ICON_CYGWIN);
            _newSFUConnection.Image = IconList.LoadIcon(IconList.ICON_SFU);
            _saveShortcut.Image = IconList.LoadIcon(IconList.ICON_SAVE);
            _singleStyle.Image = IconList.LoadIcon(IconList.ICON_SINGLE);
            _divHorizontalStyle.Image = IconList.LoadIcon(IconList.ICON_DIVHORIZONTAL);
            _divVerticalStyle.Image = IconList.LoadIcon(IconList.ICON_DIVVERTICAL);
            _divHorizontal3Style.Image = IconList.LoadIcon(IconList.ICON_DIVHORIZONTAL3);
            _divVertical3Style.Image = IconList.LoadIcon(IconList.ICON_DIVVERTICAL3);
            _localEcho.Image = IconList.LoadIcon(IconList.ICON_LOCALECHO);
            _lineFeedRule.Image = IconList.LoadIcon(IconList.ICON_LINEFEED);
            _logSuspend.Image = IconList.LoadIcon(IconList.ICON_SUSPENDLOG);
            _commentLog.Image = IconList.LoadIcon(IconList.ICON_COMMENTLOG);
            _serverInfo.Image = IconList.LoadIcon(IconList.ICON_INFO);
        }

        public ToggleButton SuspendLogButton
        {
            get
            {
                return _logSuspend;
            }
        }
        public ToggleButton LocalEchoButton
        {
            get
            {
                return _localEcho;
            }
        }
        public GButton LineFeedRuleButton
        {
            get
            {
                return _lineFeedRule;
            }
        }

        public ComboBox NewLineBox
        {
            get
            {
                return _newLineOption;
            }
        }

        protected override void OnGotFocus(EventArgs args)
        {
            base.OnGotFocus(args);
            //GApp.GlobalCommandTarget.SetFocusToActiveConnection();
            //Debug.WriteLine("DialogBar gotfocus");
        }

        protected override void OnPaint(PaintEventArgs arg)
        {
            base.OnPaint(arg);
            //上に区切り線を引く
            Graphics g = arg.Graphics;
            Pen p = new Pen(Color.FromKnownColor(KnownColor.WindowFrame));
            g.DrawLine(p, 0, 0, Width, 0);
            p = new Pen(Color.FromKnownColor(KnownColor.Window));
            g.DrawLine(p, 0, 1, Width, 1);

            //ツールバーの区切り目
            const int margin = 3;
            p = new Pen(Color.FromKnownColor(KnownColor.ControlDark));
            g.DrawLine(p, 108, margin, 108, Height - margin);
            g.DrawLine(p, 162, margin, 162, Height - margin);
            g.DrawLine(p, 292, margin, 292, Height - margin);
            g.DrawLine(p, 696, margin, 696, Height - margin);
        }

        private void OpenNewConnection(object sender, EventArgs e)
        {
            GApp.GlobalCommandTarget.NewConnectionWithDialog(null);
        }
        private void OpenNewSerialConnection(object sender, EventArgs e)
        {
            GApp.GlobalCommandTarget.NewSerialConnectionWithDialog(null);
        }
        private void OpenNewCygwinConnection(object sender, EventArgs e)
        {
            GApp.GlobalCommandTarget.NewCygwinConnectionWithDialog(null);
        }
        private void OpenNewSFUConnection(object sender, EventArgs e)
        {
            GApp.GlobalCommandTarget.NewSFUConnectionWithDialog(null);
        }

        private void OpenShortCut(object sender, EventArgs e)
        {
            GApp.GlobalCommandTarget.OpenShortCutWithDialog();
        }
        private void SaveShortCut(object sender, EventArgs e)
        {
            ContainerConnectionCommandTarget t = GApp.GetConnectionCommandTarget();
            t.SaveShortCut();
            t.Focus();
        }
        private void ChangeNewLine(object sender, EventArgs e)
        {
            if (_blockEventHandler) return;
            NewLine nl = (NewLine)_newLineOption.SelectedIndex;
            ContainerConnectionCommandTarget t = GApp.GetConnectionCommandTarget();
            t.SetTransmitNewLine(nl);
            t.Focus();
        }
        private void ChangeEncoding(object sender, EventArgs e)
        {
            if (_blockEventHandler) return;
            EncodingProfile enc = EncodingProfile.Get((EncodingType)_encodingBox.SelectedIndex);
            ContainerConnectionCommandTarget t = GApp.GetConnectionCommandTarget();
            t.SetEncoding(enc);
            t.Focus();
        }
        private void ToggleLocalEcho(object sender, EventArgs e)
        {
            if (_blockEventHandler) return;
            ContainerConnectionCommandTarget t = GApp.GetConnectionCommandTarget();
            t.SetLocalEcho(!t.Connection.Param.LocalEcho);
            t.Focus();
        }
        private void LineFeedRule(object sender, EventArgs e)
        {
            if (_blockEventHandler) return;
            ContainerConnectionCommandTarget t = GApp.GetConnectionCommandTarget();
            t.ShowLineFeedRuleDialog();
            t.Focus();
        }
        private void ToggleLogSwitch(object sender, EventArgs e)
        {
            if (_blockEventHandler) return;
            ContainerConnectionCommandTarget t = GApp.GetConnectionCommandTarget();
            t.SetLogSuspended(!t.Connection.LogSuspended);
            t.Focus();
        }
        private void ToggleSingleStyle(object sender, EventArgs e)
        {
            if (_blockEventHandler) return;
            if (GApp.GlobalCommandTarget.SetFrameStyle(GFrameStyle.Single) == CommandResult.Ignored)
                _singleStyle.Checked = true;
        }
        private void ToggleDivHorizontalStyle(object sender, EventArgs e)
        {
            if (_blockEventHandler) return;
            if (GApp.GlobalCommandTarget.SetFrameStyle(GFrameStyle.DivHorizontal) == CommandResult.Ignored)
                _divHorizontalStyle.Checked = true;
        }
        private void ToggleDivVerticalStyle(object sender, EventArgs e)
        {
            if (_blockEventHandler) return;
            if (GApp.GlobalCommandTarget.SetFrameStyle(GFrameStyle.DivVertical) == CommandResult.Ignored)
                _divVerticalStyle.Checked = true;
        }
        private void ToggleDivHorizontal3Style(object sender, EventArgs e)
        {
            if (_blockEventHandler) return;
            if (GApp.GlobalCommandTarget.SetFrameStyle(GFrameStyle.DivHorizontal3) == CommandResult.Ignored)
                _divHorizontal3Style.Checked = true;
        }
        private void ToggleDivVertical3Style(object sender, EventArgs e)
        {
            if (_blockEventHandler) return;
            if (GApp.GlobalCommandTarget.SetFrameStyle(GFrameStyle.DivVertical3) == CommandResult.Ignored)
                _divVertical3Style.Checked = true;
        }


        public void EnableTerminalUI(bool enabled, TerminalConnection con)
        {
            _blockEventHandler = true;
            _saveShortcut.Enabled = enabled;
            _newLineOption.Enabled = enabled && !con.IsClosed;
            _logSuspend.Enabled = enabled && !con.IsClosed && con.TextLogger.IsActive;
            _localEcho.Enabled = enabled && !con.IsClosed;
            _lineFeedRule.Enabled = enabled && !con.IsClosed;
            _encodingBox.Enabled = enabled && !con.IsClosed;
            _serverInfo.Enabled = enabled;
            _commentLog.Enabled = enabled && !con.IsClosed && con.TextLogger.IsActive;
            if (enabled)
            {
                _newLineOption.SelectedIndex = (int)con.Param.TransmitNL;
                _encodingBox.SelectedIndex = (int)con.Param.EncodingProfile.Type;
                _logSuspend.Checked = con.LogSuspended;
                _localEcho.Checked = con.Param.LocalEcho;
            }
            _blockEventHandler = false;
            Invalidate(true);
        }
        public void ApplyOptions(ContainerOptions opt)
        {
            GFrameStyle f = opt.FrameStyle;
            _singleStyle.Checked = f == GFrameStyle.Single;
            _divHorizontalStyle.Checked = f == GFrameStyle.DivHorizontal;
            _divVerticalStyle.Checked = f == GFrameStyle.DivVertical;
            _divHorizontal3Style.Checked = f == GFrameStyle.DivHorizontal3;
            _divVertical3Style.Checked = f == GFrameStyle.DivVertical3;
            Invalidate(true);
        }


        private void OnMouseEnterToButton(object sender, EventArgs args)
        {
            if (!_toolTipInitialized) InitToolTip();

            GStatusBar sb = GApp.Frame.StatusBar;
            if (sender == _openShortcut)
                sb.SetStatusBarText("Caption.ToolBar._openShortcut");
            else if (sender == _newConnection)
                sb.SetStatusBarText("Caption.ToolBar._newConnection");
            else if (sender == _newSerialConnection)
                sb.SetStatusBarText("Caption.ToolBar._newSerialConnection");
            else if (sender == _newCygwinConnection)
                sb.SetStatusBarText("Caption.ToolBar._newCygwinConnection");
            else if (sender == _newSFUConnection)
                sb.SetStatusBarText("Caption.ToolBar._newSFUConnection");
            else if (sender == _saveShortcut)
                sb.SetStatusBarText("Caption.ToolBar._saveShortcut");
            else if (sender == _singleStyle)
                sb.SetStatusBarText("Caption.ToolBar._singleStyle");
            else if (sender == _divHorizontalStyle)
                sb.SetStatusBarText("Caption.ToolBar._divHorizontalStyle");
            else if (sender == _divVerticalStyle)
                sb.SetStatusBarText("Caption.ToolBar._divVerticalStyle");
            else if (sender == _divHorizontal3Style)
                sb.SetStatusBarText("Caption.ToolBar._divHorizontal3Style");
            else if (sender == _divVertical3Style)
                sb.SetStatusBarText("Caption.ToolBar._divVertical3Style");
            else if (sender == _newLineOption)
                sb.SetStatusBarText("Caption.ToolBar._newLineOption");
            else if (sender == _lineFeedRule)
                sb.SetStatusBarText("Caption.ToolBar._lineFeedRule");
            else if (sender == _encodingBox)
                sb.SetStatusBarText("Caption.ToolBar._encodingBox");
            else if (sender == _logSuspend)
                sb.SetStatusBarText("Caption.ToolBar._logSuspend");
            else if (sender == _commentLog)
                sb.SetStatusBarText("Caption.ToolBar._commentLog");
            else if (sender == _localEcho)
                sb.SetStatusBarText("Caption.ToolBar._localEcho");
            else if (sender == _serverInfo)
                sb.SetStatusBarText("Caption.ToolBar._serverInfo");
            else
                Debug.WriteLine("Unexpected toolbar object");
        }

        private void InitToolTip()
        {
            if (_toolTip != null) _toolTip.RemoveAll();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(_openShortcut, "ToolTip.ToolBar._openShortcut");
            tt.SetToolTip(_saveShortcut, "ToolTip.ToolBar._saveShortcut");
            tt.SetToolTip(_singleStyle, "ToolTip.ToolBar._singleStyle");
            tt.SetToolTip(_divHorizontalStyle, "ToolTip.ToolBar._divHorizontalStyle");
            tt.SetToolTip(_divVerticalStyle, "ToolTip.ToolBar._divVerticalStyle");
            tt.SetToolTip(_divHorizontal3Style, "ToolTip.ToolBar._divHorizontal3Style");
            tt.SetToolTip(_divVertical3Style, "ToolTip.ToolBar._divVertical3Style");
            tt.SetToolTip(_newConnection, "ToolTip.ToolBar._newConnection");
            tt.SetToolTip(_newSerialConnection, "ToolTip.ToolBar._newSerialConnection");
            tt.SetToolTip(_newCygwinConnection, "ToolTip.ToolBar._newCygwinConnection");
            tt.SetToolTip(_newSFUConnection, "ToolTip.ToolBar._newSFUConnection");
            tt.SetToolTip(_newLineLabel, "ToolTip.ToolBar._newLineLabel");
            tt.SetToolTip(_newLineOption, "ToolTip.ToolBar._newLineLabel");
            tt.SetToolTip(_encodingLabel, "ToolTip.ToolBar._encodingLabel");
            tt.SetToolTip(_encodingBox, "ToolTip.ToolBar._encodingBox");
            tt.SetToolTip(_localEcho, "ToolTip.ToolBar._localEcho");
            tt.SetToolTip(_lineFeedRule, "ToolTip.ToolBar._lineFeedRule");
            tt.SetToolTip(_logSuspend, "ToolTip.ToolBar._logSuspend");
            tt.SetToolTip(_commentLog, "ToolTip.ToolBar._commentLog");
            tt.SetToolTip(_serverInfo, "ToolTip.ToolBar._serverInfo");

            _toolTip = tt;
            _toolTipInitialized = true;
        }

        private void OnMouseLeaveFromButton(object sender, EventArgs args)
        {
            GApp.Frame.StatusBar.ClearStatusBarText();
        }
        private void OnMouseHoverOnButton(object sender, EventArgs args)
        {

        }

        private void CommentLog(object sender, EventArgs e)
        {
            GApp.GetConnectionCommandTarget().CommentLog();
        }
        private void ShowServerInfo(object sender, EventArgs e)
        {
            GApp.GetConnectionCommandTarget().ShowServerInfo();
        }
    }
}
