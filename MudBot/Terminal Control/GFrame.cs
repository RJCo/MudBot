/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: GFrame.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Communication;
using Poderosa.Config;
using Poderosa.Connection;
using Poderosa.ConnectionParam;
using Poderosa.MacroEnv;
using Poderosa.Toolkit;
using Poderosa.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Poderosa.Forms
{

    internal class GFrame : Form
    {
        public MenuStrip Menu;

        #region Poderosa Fields
        private Hashtable _windowMenuItemMap;
        private Hashtable _MRUMenuToParameter;
        private InitialAction _initialAction;
        internal bool _firstflag;
        public MultiPaneControl _multiPaneControl;

        private ContextMenuStrip _contextMenu;

        private MenuStrip _menu;
        private StatusBar _statusBar;
        private StatusBarPanel _textStatusBarPanel;
        private StatusBarPanel _bellIndicateStatusBarPanel;
        private StatusBarPanel _caretPosStatusBarPanel;

        private GMenuItem _menuBarFile1;
        private GMenuItem _menuBarFile2;
        private GMenuItem _menuReceiveFile;
        private GMenuItem _menuSendFile;
        private GMenuItem _menuBarBeforeMRU;
        private GMenuItem _menuBarAfterMRU;
        private GMenuItem _menuQuit;
        private GMenuItem _menuBarConsole2;
        private GMainMenuItem _menuFile;
        private GMenuItem _menuOpenShortcut;
        private GMenuItem _menuNewConnection;
        private GMainMenuItem _menuTool;
        private GMenuItem _menuSaveShortcut;
        private GMenuItem _menuBarTool1;
        private GMenuItem _menuMacro;
        private GMenuItem _menuBarTool2;
        private GMenuItem _menuOption;
        private GMenuItem _menuMacroConfig;
        private GMenuItem _menuStopMacro;
        private GMenuItem _menuBarMacro;
        private GMainMenuItem _menuConsole;
        private GMenuItem _menuNewLine;
        private GMenuItem _menuNewLine_CR;
        private GMenuItem _menuNewLine_LF;
        private GMenuItem _menuNewLine_CRLF;
        private GMenuItem _menuLineFeedRule;
        private GMenuItem _menuLocalEcho;
        private GMenuItem _menuSendSpecial;
        private GMenuItem _menuSendBreak;
        private GMenuItem _menuAreYouThere;
        private GMenuItem _menuResetTerminal;
        private GMenuItem _menuEncoding;
        private GMenuItem _menuSuspendLog;
        private GMenuItem _menuCommentLog;
        private GMenuItem _menuServerInfo;
        private GMainMenuItem _menuWindow;
        private GMainMenuItem _menuHelp;
        private GMenuItem _menuAboutBox;
        private GMenuItem _menuProductWeb;
        private GMenuItem _menuChangeLog;
        private GMenuItem _menuBarConsole3;
        private GMenuItem _menuConsoleClose;
        private GMenuItem _menuConsoleReproduce;
        private GMenuItem _menuRenameTab;
        private GMenuItem _menuBarConsole1;
        private GMenuItem _menuPane;
        private GMenuItem _menuTab;
        private GMenuItem _menuMovePane;
        private GMenuItem _menuBarWindow3;
        private GMenuItem _menuMovePaneUp;
        private GMenuItem _menuMovePaneDown;
        private GMenuItem _menuMovePaneLeft;
        private GMenuItem _menuMovePaneRight;
        private GMenuItem _menuCloseAll;
        private GMenuItem _menuCloseAllDisconnected;
        private GMenuItem _menuBarWindow1;
        private GMenuItem _menuPrevTab;
        private GMenuItem _menuNextTab;
        private GMenuItem _menuMoveTabToPrev;
        private GMenuItem _menuMoveTabToNext;
        private GMenuItem _menuExpandPane;
        private GMenuItem _menuShrinkPane;
        private GMainMenuItem _menuEdit;
        private GMenuItem _menuCopy;
        private GMenuItem _menuPaste;
        private GMenuItem _menuCopyToFile;
        private GMenuItem _menuPasteFromFile;
        private GMenuItem _menuSelectAll;
        private GMenuItem _menuBarEdit1;
        private GMenuItem _menuBarEdit2;
        private GMenuItem _menuBarEdit3;
        private GMenuItem _menuClearScreen;
        private GMenuItem _menuClearBuffer;
        private GMenuItem _menuFreeSelectionMode;
        private GMenuItem _menuAutoSelectionMode;
        private GMenuItem _menuBarWindow2;
        private GMenuItem _menuFrameStyle;
        private GMenuItem _menuFrameStyleSingle;
        private GMenuItem _menuFrameStyleDivHorizontal;
        private GMenuItem _menuFrameStyleDivVertical;
        private GMenuItem _menuFrameStyleDivHorizontal3;
        private GMenuItem _menuFrameStyleDivVertical3;
        private GMenuItem _menuEditRenderProfile;
        private GMenuItem _menuCopyAsLook;
        private GMenuItem _menuLaunchPortforwarding;
        #endregion
        public GFrame(InitialAction act)
        {
            #region Poderosa Constructors
            _initialAction = act;
            _windowMenuItemMap = new Hashtable();
            _MRUMenuToParameter = new Hashtable();
            _firstflag = true;
            //
            // Windows
            //

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            Icon = GApp.Options.GuevaraMode ? GIcons.GetOldGuevaraIcon() : GIcons.GetAppIcon();
            InitMenuText();

            //システムからエンコーディングを列挙してメニューをセット
            foreach (string e in EnumDescAttribute.For(typeof(EncodingType)).DescriptionCollection())
            {
                GMenuItem m = new GMenuItem
                {
                    Text = e
                };
                m.Click += OnChangeEncoding;
                _menuEncoding.DropDownItems.Add(m);
            }

            TabBar = new TabBar
            {
                Dock = DockStyle.Top,
                Height = 25
            };

            ApplyOptions(null, GApp.Options);
            ApplyHotKeys();

            StatusBar = new GStatusBar(_statusBar);
            AdjustTitle(null);
            #endregion

        }

        #region
        public void ReloadIcon()
        {
            Icon = GApp.Options.GuevaraMode ? GIcons.GetOldGuevaraIcon() : GIcons.GetAppIcon();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GFrame));
            _menu = new MenuStrip();
            _menuFile = new GMainMenuItem();
            _menuNewConnection = new GMenuItem();
            _menuBarFile1 = new GMenuItem();
            _menuOpenShortcut = new GMenuItem();
            _menuSaveShortcut = new GMenuItem();
            _menuBarFile2 = new GMenuItem();
            _menuReceiveFile = new GMenuItem();
            _menuSendFile = new GMenuItem();
            _menuBarBeforeMRU = new GMenuItem();
            _menuBarAfterMRU = new GMenuItem();
            _menuQuit = new GMenuItem();
            _menuEdit = new GMainMenuItem();
            _menuCopy = new GMenuItem();
            _menuPaste = new GMenuItem();
            _menuBarEdit1 = new GMenuItem();
            _menuCopyAsLook = new GMenuItem();
            _menuCopyToFile = new GMenuItem();
            _menuPasteFromFile = new GMenuItem();
            _menuBarEdit2 = new GMenuItem();
            _menuClearScreen = new GMenuItem();
            _menuClearBuffer = new GMenuItem();
            _menuBarEdit3 = new GMenuItem();
            _menuSelectAll = new GMenuItem();
            _menuFreeSelectionMode = new GMenuItem();
            _menuAutoSelectionMode = new GMenuItem();
            _menuConsole = new GMainMenuItem();
            _menuConsoleClose = new GMenuItem();
            _menuRenameTab = new GMenuItem();
            _menuConsoleReproduce = new GMenuItem();
            _menuBarConsole1 = new GMenuItem();
            _menuNewLine = new GMenuItem();
            _menuNewLine_CR = new GMenuItem();
            _menuNewLine_LF = new GMenuItem();
            _menuNewLine_CRLF = new GMenuItem();
            _menuLocalEcho = new GMenuItem();
            _menuLineFeedRule = new GMenuItem();
            _menuSendSpecial = new GMenuItem();
            _menuSendBreak = new GMenuItem();
            _menuAreYouThere = new GMenuItem();
            _menuResetTerminal = new GMenuItem();
            _menuBarConsole2 = new GMenuItem();
            _menuSuspendLog = new GMenuItem();
            _menuCommentLog = new GMenuItem();
            _menuChangeLog = new GMenuItem();
            _menuBarConsole3 = new GMenuItem();
            _menuServerInfo = new GMenuItem();
            _menuTool = new GMainMenuItem();
            _menuBarTool1 = new GMenuItem();
            _menuMacro = new GMenuItem();
            _menuBarTool2 = new GMenuItem();
            _menuOption = new GMenuItem();
            _menuMacroConfig = new GMenuItem();
            _menuStopMacro = new GMenuItem();
            _menuBarMacro = new GMenuItem();
            _menuWindow = new GMainMenuItem();
            _menuFrameStyle = new GMenuItem();
            _menuFrameStyleSingle = new GMenuItem();
            _menuFrameStyleDivHorizontal = new GMenuItem();
            _menuFrameStyleDivVertical = new GMenuItem();
            _menuFrameStyleDivHorizontal3 = new GMenuItem();
            _menuFrameStyleDivVertical3 = new GMenuItem();
            _menuPane = new GMenuItem();
            _menuMovePane = new GMenuItem();
            _menuMovePaneUp = new GMenuItem();
            _menuMovePaneDown = new GMenuItem();
            _menuMovePaneLeft = new GMenuItem();
            _menuMovePaneRight = new GMenuItem();
            _menuExpandPane = new GMenuItem();
            _menuShrinkPane = new GMenuItem();
            _menuBarWindow1 = new GMenuItem();
            _menuTab = new GMenuItem();
            _menuPrevTab = new GMenuItem();
            _menuNextTab = new GMenuItem();
            _menuMoveTabToPrev = new GMenuItem();
            _menuMoveTabToNext = new GMenuItem();
            _menuBarWindow2 = new GMenuItem();
            _menuCloseAll = new GMenuItem();
            _menuCloseAllDisconnected = new GMenuItem();
            _menuBarWindow3 = new GMenuItem();
            _menuHelp = new GMainMenuItem();
            _menuAboutBox = new GMenuItem();
            _menuProductWeb = new GMenuItem();
            _statusBar = new StatusBar();
            _textStatusBarPanel = new StatusBarPanel();
            _bellIndicateStatusBarPanel = new StatusBarPanel();
            _caretPosStatusBarPanel = new StatusBarPanel();
            _menuEncoding = new GMenuItem();
            _menuEditRenderProfile = new GMenuItem();
            _menuLaunchPortforwarding = new GMenuItem();
            ((ISupportInitialize)(_textStatusBarPanel)).BeginInit();
            ((ISupportInitialize)(_bellIndicateStatusBarPanel)).BeginInit();
            ((ISupportInitialize)(_caretPosStatusBarPanel)).BeginInit();
            SuspendLayout();
            // 
            // _menu
            // 
            _menu.Items.AddRange(new ToolStripMenuItem[] {
                _menuFile,
                _menuEdit,
                _menuConsole,
                _menuTool,
                _menuWindow,
                _menuHelp
            });
            // 
            // _menuFile
            // 
            _menuFile.MergeIndex = 0;
            _menuFile.DropDownItems.AddRange(new ToolStripMenuItem[]
            {
                _menuNewConnection,
                _menuBarFile1,
                _menuOpenShortcut,
                _menuSaveShortcut,
                _menuBarFile2,
                _menuReceiveFile,
                _menuSendFile,
                _menuBarBeforeMRU,
                _menuBarAfterMRU,
                _menuQuit
            });
            // 
            // _menuNewConnection
            // 
            _menuNewConnection.MergeIndex = 0;
            _menuNewConnection.Click += new EventHandler(OnMenu);
            _menuNewConnection.CID = (int)CID.NewConnection;
            // 
            // _menuBarFile1
            // 
            _menuBarFile1.MergeIndex = 4;
            _menuBarFile1.Text = "-";
            // 
            // _menuOpenShortcut
            // 
            _menuOpenShortcut.MergeIndex = 5;
            _menuOpenShortcut.Click += new EventHandler(OnMenu);
            _menuOpenShortcut.CID = (int)CID.OpenShortcut;
            // 
            // _menuSaveShortcut
            // 
            _menuSaveShortcut.Enabled = false;
            _menuSaveShortcut.MergeIndex = 6;
            _menuSaveShortcut.Click += new EventHandler(OnMenu);
            _menuSaveShortcut.CID = (int)CID.SaveShortcut;
            // 
            // _menuBarFile2
            // 
            _menuBarFile2.MergeIndex = 7;
            _menuBarFile2.Text = "-";
            // 
            // _menuReceiveFile
            // 
            _menuReceiveFile.Enabled = false;
            _menuReceiveFile.MergeIndex = 8;
            _menuReceiveFile.Click += new EventHandler(OnMenu);
            _menuReceiveFile.CID = (int)CID.ReceiveFile;
            // 
            // _menuSendFile
            // 
            _menuSendFile.Enabled = false;
            _menuSendFile.MergeIndex = 9;
            _menuSendFile.Click += new EventHandler(OnMenu);
            _menuSendFile.CID = (int)CID.SendFile;
            // 
            // _menuBarBeforeMRU
            // 
            _menuBarBeforeMRU.MergeIndex = 10;
            _menuBarBeforeMRU.Text = "-";
            // 
            // _menuBarAfterMRU
            // 
            _menuBarAfterMRU.MergeIndex = 11;
            _menuBarAfterMRU.Text = "-";
            // 
            // _menuQuit
            // 
            _menuQuit.MergeIndex = 12;
            _menuQuit.Click += new EventHandler(OnMenu);
            _menuQuit.CID = (int)CID.Quit;
            // 
            // _menuEdit
            // 
            _menuEdit.MergeIndex = 1;
            _menuEdit.DropDownItems.AddRange(new ToolStripMenuItem[] {
                _menuCopy,
                _menuPaste,
                _menuBarEdit1,
                _menuCopyAsLook,
                _menuCopyToFile,
                _menuPasteFromFile,
                _menuBarEdit2,
                _menuClearScreen,
                _menuClearBuffer,
                _menuBarEdit3,
                _menuSelectAll,
                _menuFreeSelectionMode,
                _menuAutoSelectionMode
            });

            // RJC _menuEdit.Popup += new EventHandler(AdjustEditMenu);
            // 
            // _menuCopy
            // 
            _menuCopy.Enabled = false;
            _menuCopy.MergeIndex = 0;
            _menuCopy.Click += new EventHandler(OnMenu);
            _menuCopy.CID = (int)CID.Copy;
            // 
            // _menuPaste
            // 
            _menuPaste.Enabled = false;
            _menuPaste.MergeIndex = 1;
            _menuPaste.Click += new EventHandler(OnMenu);
            _menuPaste.CID = (int)CID.Paste;
            // 
            // _menuBarEdit1
            // 
            _menuBarEdit1.MergeIndex = 2;
            _menuBarEdit1.Text = "-";
            // 
            // _menuCopyAsLook
            // 
            _menuCopyAsLook.Enabled = false;
            _menuCopyAsLook.MergeIndex = 3;
            _menuCopyAsLook.Click += new EventHandler(OnMenu);
            _menuCopyAsLook.CID = (int)CID.CopyAsLook;
            // 
            // _menuCopyToFile
            // 
            _menuCopyToFile.Enabled = false;
            _menuCopyToFile.MergeIndex = 4;
            _menuCopyToFile.Click += new EventHandler(OnMenu);
            _menuCopyToFile.CID = (int)CID.CopyToFile;
            // 
            // _menuPasteFromFile
            // 
            _menuPasteFromFile.Enabled = false;
            _menuPasteFromFile.MergeIndex = 5;
            _menuPasteFromFile.Click += new EventHandler(OnMenu);
            _menuPasteFromFile.CID = (int)CID.PasteFromFile;
            // 
            // _menuBarEdit2
            // 
            _menuBarEdit2.MergeIndex = 6;
            _menuBarEdit2.Text = "-";
            // 
            // _menuClearScreen
            // 
            _menuClearScreen.MergeIndex = 7;
            _menuClearScreen.Click += new EventHandler(OnMenu);
            _menuClearScreen.CID = (int)CID.ClearScreen;
            // 
            // _menuClearBuffer
            // 
            _menuClearBuffer.MergeIndex = 8;
            _menuClearBuffer.Click += new EventHandler(OnMenu);
            _menuClearBuffer.CID = (int)CID.ClearBuffer;
            // 
            // _menuBarEdit3
            // 
            _menuBarEdit3.MergeIndex = 9;
            _menuBarEdit3.Text = "-";

            // 
            // _menuSelectAll
            // 
            _menuSelectAll.MergeIndex = 10;
            _menuSelectAll.Click += new EventHandler(OnMenu);
            _menuSelectAll.CID = (int)CID.SelectAll;
            // 
            // _menuFreeSelectionMode
            // 
            _menuFreeSelectionMode.MergeIndex = 11;
            _menuFreeSelectionMode.Click += new EventHandler(OnMenu);
            _menuFreeSelectionMode.CID = (int)CID.ToggleFreeSelectionMode;
            // 
            // _menuAutoSelectionMode
            // 
            _menuAutoSelectionMode.MergeIndex = 12;
            _menuAutoSelectionMode.Click += new EventHandler(OnMenu);
            _menuAutoSelectionMode.CID = (int)CID.ToggleAutoSelectionMode;
            // 
            // _menuConsole
            // 
            _menuConsole.MergeIndex = 2;
            _menuConsole.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                         _menuConsoleClose,
                                                                                         _menuConsoleReproduce,
                                                                                         _menuBarConsole1,
                                                                                         _menuNewLine,
                                                                                         _menuLineFeedRule,
                                                                                         _menuEncoding,
                                                                                         _menuLocalEcho,
                                                                                         _menuSendSpecial,
                                                                                         _menuBarConsole2,
                                                                                         _menuSuspendLog,
                                                                                         _menuCommentLog,
                                                                                         _menuChangeLog,
                                                                                         _menuBarConsole3,
                                                                                         _menuEditRenderProfile,
                                                                                         _menuServerInfo,
                                                                                         _menuRenameTab});



            // RJC _menuConsole.Popup += new EventHandler(AdjustConsoleMenu);

            // 
            // _menuConsoleClose
            // 
            _menuConsoleClose.Enabled = false;
            _menuConsoleClose.MergeIndex = 0;
            _menuConsoleClose.Click += new EventHandler(OnMenu);
            _menuConsoleClose.CID = (int)CID.Close;
            // 
            // _menuConsoleReproduce
            // 
            _menuConsoleReproduce.Enabled = false;
            _menuConsoleReproduce.MergeIndex = 1;
            _menuConsoleReproduce.Click += new EventHandler(OnMenu);
            _menuConsoleReproduce.CID = (int)CID.Reproduce;
            // 
            // _menuBarConsole1
            // 
            _menuBarConsole1.MergeIndex = 2;
            _menuBarConsole1.Text = "-";
            // 
            // _menuNewLine
            // 
            _menuNewLine.Enabled = false;
            _menuNewLine.MergeIndex = 3;
            _menuNewLine.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                         _menuNewLine_CR,
                                                                                         _menuNewLine_LF,
                                                                                         _menuNewLine_CRLF});




            // 
            // _menuNewLine_CR
            // 
            _menuNewLine_CR.MergeIndex = 0;
            _menuNewLine_CR.Text = "CR";
            _menuNewLine_CR.Click += new EventHandler(OnChangeNewLine);
            // 
            // _menuNewLine_LF
            // 
            _menuNewLine_LF.MergeIndex = 1;
            _menuNewLine_LF.Text = "LF";
            _menuNewLine_LF.Click += new EventHandler(OnChangeNewLine);
            // 
            // _menuNewLine_CRLF
            // 
            _menuNewLine_CRLF.MergeIndex = 2;
            _menuNewLine_CRLF.Text = "CR+LF";
            _menuNewLine_CRLF.Click += new EventHandler(OnChangeNewLine);
            // 
            // _menuEncoding
            // 
            _menuEncoding.Enabled = false;
            _menuEncoding.MergeIndex = 4;
            // 
            // _menuLineFeedRule
            // 
            _menuLineFeedRule.Enabled = false;
            _menuLineFeedRule.MergeIndex = 5;
            _menuLineFeedRule.Click += new EventHandler(OnMenu);
            _menuLineFeedRule.CID = (int)CID.LineFeedRule;
            // 
            // _menuLocalEcho
            // 
            _menuLocalEcho.Enabled = false;
            _menuLocalEcho.MergeIndex = 6;
            _menuLocalEcho.Click += new EventHandler(OnMenu);
            _menuLocalEcho.CID = (int)CID.ToggleLocalEcho;
            // 
            // _menuSendSpecial
            // 
            _menuSendSpecial.Enabled = false;
            _menuSendSpecial.MergeIndex = 7;
            _menuSendSpecial.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                         _menuSendBreak,
                                                                                         _menuAreYouThere,
                                                                                         _menuResetTerminal
            });




            // 
            // _menuSendBreak
            // 
            _menuSendBreak.MergeIndex = 0;
            _menuSendBreak.Click += new EventHandler(OnMenu);
            _menuSendBreak.CID = (int)CID.SendBreak;
            // 
            // _menuAreYouThere
            // 
            _menuAreYouThere.MergeIndex = 1;
            _menuAreYouThere.Click += new EventHandler(OnMenu);
            _menuAreYouThere.CID = (int)CID.AreYouThere;

            // 
            // _menuResetTerminal
            // 
            _menuResetTerminal.MergeIndex = 3;
            _menuResetTerminal.Click += new EventHandler(OnMenu);
            _menuResetTerminal.CID = (int)CID.ResetTerminal;
            // 
            // _menuBarConsole2
            // 
            _menuBarConsole2.MergeIndex = 8;
            _menuBarConsole2.Text = "-";
            // 
            // _menuSuspendLog
            // 
            _menuSuspendLog.Enabled = false;
            _menuSuspendLog.MergeIndex = 9;
            _menuSuspendLog.Click += new EventHandler(OnMenu);
            _menuSuspendLog.CID = (int)CID.ToggleLogSuspension;
            // 
            // _menuCommentLog
            // 
            _menuCommentLog.Enabled = false;
            _menuCommentLog.MergeIndex = 10;
            _menuCommentLog.Click += new EventHandler(OnMenu);
            _menuCommentLog.CID = (int)CID.CommentLog;
            // 
            // _menuChangeLog
            // 
            _menuChangeLog.Enabled = false;
            _menuChangeLog.MergeIndex = 11;
            _menuChangeLog.Click += new EventHandler(OnMenu);
            _menuChangeLog.CID = (int)CID.ChangeLogFile;
            // 
            // _menuBarConsole3
            // 
            _menuBarConsole3.MergeIndex = 12;
            _menuBarConsole3.Text = "-";
            // 
            // _menuEditRenderProfile
            // 
            _menuEditRenderProfile.Enabled = false;
            _menuEditRenderProfile.MergeIndex = 13;
            _menuEditRenderProfile.Click += new EventHandler(OnMenu);
            _menuEditRenderProfile.CID = (int)CID.EditRenderProfile;
            // 
            // _menuServerInfo
            // 
            _menuServerInfo.Enabled = false;
            _menuServerInfo.MergeIndex = 14;
            _menuServerInfo.Click += new EventHandler(OnMenu);
            _menuServerInfo.CID = (int)CID.ShowServerInfo;
            // 
            // _menuRenameTab
            // 
            _menuRenameTab.Enabled = false;
            _menuRenameTab.MergeIndex = 15;
            _menuRenameTab.Click += new EventHandler(OnMenu);
            _menuRenameTab.CID = (int)CID.RenameTab;
            // 
            // _menuTool
            // 
            _menuTool.MergeIndex = 3;
            _menuTool.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                      _menuLaunchPortforwarding,
                                                                                      _menuBarTool1,
                                                                                      _menuMacro,
                                                                                      _menuBarTool2,
                                                                                      _menuOption});

            // 
            // _menuBarTool1
            // 
            _menuBarTool1.MergeIndex = 3;
            _menuBarTool1.Text = "-";
            // 
            // _menuMacro
            // 
            _menuMacro.MergeIndex = 4;
            _menuMacro.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                        _menuMacroConfig,
                                                                                        _menuStopMacro,
                                                                                        _menuBarMacro});
            // 
            // _menuBarTool2
            // 
            _menuBarTool2.MergeIndex = 5;
            _menuBarTool2.Text = "-";
            // 
            // _menuOption
            // 
            _menuOption.MergeIndex = 6;
            _menuOption.Click += new EventHandler(OnMenu);
            _menuOption.CID = (int)CID.OptionDialog;
            // 
            // _menuMacroConfig
            // 
            _menuMacroConfig.MergeIndex = 0;
            _menuMacroConfig.Click += new EventHandler(OnMenu);
            _menuMacroConfig.CID = (int)CID.MacroConfig;
            // 
            // _menuStopMacro
            // 
            _menuStopMacro.MergeIndex = 1;
            _menuStopMacro.Enabled = false;
            _menuStopMacro.Click += new EventHandler(OnMenu);
            _menuStopMacro.CID = (int)CID.StopMacro;
            // 
            // _menuBarMacro
            // 
            _menuBarMacro.MergeIndex = 2;
            _menuBarMacro.Text = "-";
            // 
            // _menuWindow
            // 
            // RJC _menuWindow.Popup += new EventHandler(OnWindowMenuClicked);
            _menuWindow.MergeIndex = 4;
            _menuWindow.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                        _menuFrameStyle,
                                                                                        _menuBarWindow1,
                                                                                        _menuPane,
                                                                                        _menuTab,
                                                                                        _menuBarWindow2,
                                                                                        _menuCloseAll,
                                                                                        _menuCloseAllDisconnected,
                                                                                        _menuBarWindow3});




            // 
            // _menuFrameStyle
            // 
            _menuFrameStyle.MergeIndex = 0;
            _menuFrameStyle.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                            _menuFrameStyleSingle,
                                                                                            _menuFrameStyleDivHorizontal,
                                                                                            _menuFrameStyleDivVertical,
                                                                                            _menuFrameStyleDivHorizontal3,
                                                                                            _menuFrameStyleDivVertical3});




            // 
            // _menuFrameStyleSingle
            // 
            _menuFrameStyleSingle.MergeIndex = 0;
            _menuFrameStyleSingle.Click += new EventHandler(OnMenu);
            _menuFrameStyleSingle.CID = (int)CID.FrameStyleSingle;
            // 
            // _menuFrameStyleDivHorizontal
            // 
            _menuFrameStyleDivHorizontal.MergeIndex = 1;
            _menuFrameStyleDivHorizontal.Click += new EventHandler(OnMenu);
            _menuFrameStyleDivHorizontal.CID = (int)CID.FrameStyleDivHorizontal;
            // 
            // _menuFrameStyleDivVertical
            // 
            _menuFrameStyleDivVertical.MergeIndex = 2;
            _menuFrameStyleDivVertical.Click += new EventHandler(OnMenu);
            _menuFrameStyleDivVertical.CID = (int)CID.FrameStyleDivVertical;
            // 
            // _menuFrameStyleDivHorizontal3
            // 
            _menuFrameStyleDivHorizontal3.MergeIndex = 3;
            _menuFrameStyleDivHorizontal3.Click += new EventHandler(OnMenu);
            _menuFrameStyleDivHorizontal3.CID = (int)CID.FrameStyleDivHorizontal3;
            // 
            // _menuFrameStyleDivVertical3
            // 
            _menuFrameStyleDivVertical3.MergeIndex = 4;
            _menuFrameStyleDivVertical3.Click += new EventHandler(OnMenu);
            _menuFrameStyleDivVertical3.CID = (int)CID.FrameStyleDivVertical3;
            // 
            // _menuBarWindow1
            // 
            _menuBarWindow1.MergeIndex = 1;
            _menuBarWindow1.Text = "-";
            // 
            // _menuPane
            // 
            _menuPane.MergeIndex = 2;
            _menuPane.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                            _menuMovePane,
                                                                                            _menuExpandPane,
                                                                                            _menuShrinkPane});




            // 
            // _menuMovePane
            // 
            _menuMovePane.MergeIndex = 0;
            _menuMovePane.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                          _menuMovePaneUp,
                                                                                          _menuMovePaneDown,
                                                                                          _menuMovePaneLeft,
                                                                                          _menuMovePaneRight});




            // 
            // _menuMovePaneUp
            // 
            _menuMovePaneUp.MergeIndex = 0;
            _menuMovePaneUp.Click += new EventHandler(OnMenu);
            _menuMovePaneUp.CID = (int)CID.MovePaneUp;
            // 
            // _menuMovePaneDown
            // 
            _menuMovePaneDown.MergeIndex = 1;
            _menuMovePaneDown.Click += new EventHandler(OnMenu);
            _menuMovePaneDown.CID = (int)CID.MovePaneDown;
            // 
            // _menuMovePaneLeft
            // 
            _menuMovePaneLeft.MergeIndex = 2;
            _menuMovePaneLeft.Click += new EventHandler(OnMenu);
            _menuMovePaneLeft.CID = (int)CID.MovePaneLeft;
            // 
            // _menuMovePaneRight
            // 
            _menuMovePaneRight.MergeIndex = 3;
            _menuMovePaneRight.Click += new EventHandler(OnMenu);
            _menuMovePaneRight.CID = (int)CID.MovePaneRight;
            // 
            // _menuExpandPane
            // 
            _menuExpandPane.MergeIndex = 1;
            _menuExpandPane.Click += new EventHandler(OnMenu);
            _menuExpandPane.CID = (int)CID.ExpandPane;
            // 
            // _menuShrinkPane
            // 
            _menuShrinkPane.MergeIndex = 2;
            _menuShrinkPane.Click += new EventHandler(OnMenu);
            _menuShrinkPane.CID = (int)CID.ShrinkPane;
            // 
            // _menuTab
            // 
            _menuTab.MergeIndex = 3;
            _menuTab.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                      _menuPrevTab,
                                                                                      _menuNextTab,
                                                                                      _menuMoveTabToPrev,
                                                                                      _menuMoveTabToNext});



            // 
            // _menuPrevTab
            // 
            _menuPrevTab.MergeIndex = 0;
            _menuPrevTab.Click += new EventHandler(OnMenu);
            _menuPrevTab.CID = (int)CID.PrevPane;
            // 
            // _menuNextTab
            // 
            _menuNextTab.MergeIndex = 1;
            _menuNextTab.Click += new EventHandler(OnMenu);
            _menuNextTab.CID = (int)CID.NextPane;
            // 
            // _menuMoveTabToPrev
            // 
            _menuMoveTabToPrev.MergeIndex = 2;
            _menuMoveTabToPrev.Click += new EventHandler(OnMenu);
            _menuMoveTabToPrev.CID = (int)CID.MoveTabToPrev;
            // 
            // _menuMoveTabToNext
            // 
            _menuMoveTabToNext.MergeIndex = 3;
            _menuMoveTabToNext.Click += new EventHandler(OnMenu);
            _menuMoveTabToNext.CID = (int)CID.MoveTabToNext;
            // 
            // _menuBarWindow2
            // 
            _menuBarWindow2.MergeIndex = 4;
            _menuBarWindow2.Text = "-";
            // 
            // _menuCloseAll
            // 
            _menuCloseAll.MergeIndex = 5;
            _menuCloseAll.Click += new EventHandler(OnMenu);
            _menuCloseAll.CID = (int)CID.CloseAll;
            // 
            // _menuCloseAllDisconnected
            // 
            _menuCloseAllDisconnected.MergeIndex = 6;
            _menuCloseAllDisconnected.Click += new EventHandler(OnMenu);
            _menuCloseAllDisconnected.CID = (int)CID.CloseAllDisconnected;
            // 
            // _menuBarWindow3
            // 
            _menuBarWindow3.MergeIndex = 7;
            _menuBarWindow3.Text = "-";
            // 
            // _menuHelp
            // 
            _menuHelp.MergeIndex = 5;
            _menuHelp.DropDownItems.AddRange(new ToolStripMenuItem[] {
                                                                                      _menuAboutBox,_menuProductWeb});


            // 
            // _menuAboutBox
            // 
            _menuAboutBox.MergeIndex = 0;
            _menuAboutBox.Click += new EventHandler(OnMenu);
            _menuAboutBox.CID = (int)CID.AboutBox;
            // 
            // _menuProductWeb
            // 
            _menuProductWeb.MergeIndex = 1;
            _menuProductWeb.Click += new EventHandler(OnMenu);
            _menuProductWeb.CID = (int)CID.ProductWeb;
            // 
            // _statusBar
            // 
            _statusBar.Location = new Point(0, 689);
            _statusBar.Dock = DockStyle.Bottom;
            _statusBar.Name = "_statusBar";
            _statusBar.Panels.AddRange(new StatusBarPanel[] {
                                                                                          _textStatusBarPanel,
                                                                                          _bellIndicateStatusBarPanel,
                                                                                          _caretPosStatusBarPanel});
            _statusBar.ShowPanels = true;
            _statusBar.Size = new Size(600, 24);
            _statusBar.TabIndex = 5;
            // 
            // _textStatusBarPanel
            // 
            _textStatusBarPanel.BorderStyle = StatusBarPanelBorderStyle.None;
            _textStatusBarPanel.Width = 443;
            // 
            // _bellIndicateStatusBarPanel
            // 
            _bellIndicateStatusBarPanel.Alignment = HorizontalAlignment.Right;
            _bellIndicateStatusBarPanel.Width = 31;

            // 
            // _caretPosStatusBarPanel
            // 
            _caretPosStatusBarPanel.Alignment = HorizontalAlignment.Center;
            _caretPosStatusBarPanel.Width = 120;

            _statusBar.SuspendLayout(); //このAutoSizeセットが極めて重いことが判明。これで軽くなるか？
            _textStatusBarPanel.AutoSize = StatusBarPanelAutoSize.Spring;
            _bellIndicateStatusBarPanel.AutoSize = StatusBarPanelAutoSize.None;
            _caretPosStatusBarPanel.AutoSize = StatusBarPanelAutoSize.None;
            _statusBar.ResumeLayout(false);

            // 
            // _multiPaneControl
            // 
            _multiPaneControl = new MultiPaneControl
            {
                Dock = DockStyle.Fill
            };
            // 
            // GFrame
            // 
            AllowDrop = true;
            FormBorderStyle = FormBorderStyle.Sizable;
            AutoScaleBaseSize = new Size(5, 12);
            Controls.AddRange(new Control[] {
                                                                          _multiPaneControl,
                                                                          });
            Name = "GFrame";
            Text = "Poderosa";
            Menu = _menu;
            StartPosition = FormStartPosition.Manual;
            ((ISupportInitialize)(_textStatusBarPanel)).EndInit();
            ((ISupportInitialize)(_bellIndicateStatusBarPanel)).EndInit();
            ((ISupportInitialize)(_caretPosStatusBarPanel)).EndInit();
            ResumeLayout(false);

        }
        #endregion

        #region 
        private void InitMenuText()
        {
            #region Poderosa string bullshit
            _menuFile.Text = "Menu._menuFile";
            _menuNewConnection.Text = "Menu._menuNewConnection";
            _menuOpenShortcut.Text = "Menu._menuOpenShortcut";
            _menuSaveShortcut.Text = "Menu._menuSaveShortcut";
            _menuReceiveFile.Text = "Menu._menuReceiveFile";
            _menuSendFile.Text = "Menu._menuSendFile";
            _menuQuit.Text = "Menu._menuQuit";
            _menuEdit.Text = "Menu._menuEdit";
            _menuCopy.Text = "Menu._menuCopy";
            _menuPaste.Text = "Menu._menuPaste";
            _menuCopyAsLook.Text = "Menu._menuCopyAsLook";
            _menuCopyToFile.Text = "Menu._menuCopyToFile";
            _menuPasteFromFile.Text = "Menu._menuPasteFromFile";
            _menuClearScreen.Text = "Menu._menuClearScreen";
            _menuClearBuffer.Text = "Menu._menuClearBuffer";
            _menuSelectAll.Text = "Menu._menuSelectAll";
            _menuFreeSelectionMode.Text = "Menu._menuFreeSelectionMode";
            _menuAutoSelectionMode.Text = "Menu._menuAutoSelectionMode";
            _menuConsole.Text = "Menu._menuConsole";
            _menuConsoleClose.Text = "Menu._menuConsoleClose";
            _menuConsoleReproduce.Text = "Menu._menuConsoleReproduce";
            _menuNewLine.Text = "Menu._menuNewLine";
            _menuEncoding.Text = "Menu._menuEncoding";
            _menuLineFeedRule.Text = "Menu._menuLineFeedRule";
            _menuLocalEcho.Text = "Menu._menuLocalEcho";
            _menuSendSpecial.Text = "Menu._menuSendSpecial";
            _menuSendBreak.Text = "Menu._menuSendBreak";
            _menuAreYouThere.Text = "Menu._menuAreYouThere";
            _menuResetTerminal.Text = "Menu._menuResetTerminal";
            _menuSuspendLog.Text = "Menu._menuSuspendLog";
            _menuCommentLog.Text = "Menu._menuCommentLog";
            _menuChangeLog.Text = "Menu._menuChangeLog";
            _menuEditRenderProfile.Text = "Menu._menuEditRenderProfile";
            _menuServerInfo.Text = "Menu._menuServerInfo";
            _menuRenameTab.Text = "Menu._menuRenameTab";
            _menuTool.Text = "Menu._menuTool";
            _menuLaunchPortforwarding.Text = "Menu._menuLaunchPortforwarding";
            _menuMacro.Text = "Menu._menuMacro";
            _menuOption.Text = "Menu._menuOption";
            _menuMacroConfig.Text = "Menu._menuMacroConfig";
            _menuStopMacro.Text = "Menu._menuStopMacro";
            _menuWindow.Text = "Menu._menuWindow";
            _menuFrameStyle.Text = "Menu._menuFrameStyle";
            _menuFrameStyleSingle.Text = "Menu._menuFrameStyleSingle";
            _menuFrameStyleDivHorizontal.Text = "Menu._menuFrameStyleDivHorizontal";
            _menuFrameStyleDivVertical.Text = "Menu._menuFrameStyleDivVertical";
            _menuFrameStyleDivHorizontal3.Text = "Menu._menuFrameStyleDivHorizontal3";
            _menuFrameStyleDivVertical3.Text = "Menu._menuFrameStyleDivVertical3";
            _menuPane.Text = "Menu._menuPane";
            _menuMovePane.Text = "Menu._menuMovePane";
            _menuMovePaneUp.Text = "Menu._menuMovePaneUp";
            _menuMovePaneDown.Text = "Menu._menuMovePaneDown";
            _menuMovePaneLeft.Text = "Menu._menuMovePaneLeft";
            _menuMovePaneRight.Text = "Menu._menuMovePaneRight";
            _menuExpandPane.Text = "Menu._menuExpandPane";
            _menuShrinkPane.Text = "Menu._menuShrinkPane";
            _menuTab.Text = "Menu._menuTab";
            _menuPrevTab.Text = "Menu._menuPrevTab";
            _menuNextTab.Text = "Menu._menuNextTab";
            _menuMoveTabToPrev.Text = "Menu._menuMoveTabToPrev";
            _menuMoveTabToNext.Text = "Menu._menuMoveTabToNext";
            _menuCloseAll.Text = "Menu._menuCloseAll";
            _menuCloseAllDisconnected.Text = "Menu._menuCloseAllDisconnected";
            _menuHelp.Text = "Menu._menuHelp";
            _menuAboutBox.Text = "Menu._menuAboutBox";
            _menuProductWeb.Text = "Menu._menuProductWeb";
            #endregion
        }
        public void ReloadLanguage(Language l)
        {
            InitMenuText();
            MenuStrip mm = new MenuStrip();
            while (_menu.Items.Count > 0)
            {
                mm.Items.Add(_menu.Items[0]);
            }
            _menu = mm;
            Menu = mm;
            InitContextMenu();
            AdjustMRUMenu();
            if (ToolBar != null)
            {
                ToolBar.ReloadLanguage(l);
            }

            AdjustTitle(GEnv.Connections.ActiveTag);
            AdjustMacroMenu();

            if (XModemDialog != null)
            {
                XModemDialog.ReloadLanguage();
            }
        }
        private void CreateToolBar()
        {
            ToolBar = new GDialogBar
            {
                Dock = DockStyle.Top,
                Height = 27
            };
        }
        private void InitContextMenu()
        {

            _contextMenu = new ContextMenuStrip();

            GMenuItem copy = new GMenuItem
            {
                Text = "Menu._menuCopy",
                ShortcutKey = GApp.Options.Commands.FindKey(CID.Copy),
                CID = (int)CID.Copy,
            };
            copy.Click += OnMenu;

            GMenuItem paste = new GMenuItem
            {
                Text = "Menu._menuPaste",
                ShortcutKey = GApp.Options.Commands.FindKey(CID.Paste),
                CID = (int)CID.Paste,
            };
            paste.Click += OnMenu;


            GMenuItem bar = new GMenuItem
            {
                Text = "-"
            };

            _contextMenu.Items.Add(copy);
            _contextMenu.Items.Add(paste);
            _contextMenu.Items.Add(bar);

            foreach (ToolStripMenuItem child in _menuConsole.DropDownItems)
            {
                ToolStripMenuItem mi = child;  // RJC .Clone();
                _contextMenu.Items.Add(mi);
                CloneMenuCommand(child, mi);
            }
        }
        private void CloneMenuCommand(ToolStripMenuItem src, ToolStripItem dest)
        {
            ////CloneMenuされた子メニューも含んでいるかもしれない
            //int index = 0;
            //foreach (ToolStripMenuItem child in src.DropDownItems)
            //    CloneMenuCommand(child, dest.OwnerItem[index++]);
        }
        public GDialogBar ToolBar { get; private set; }

        public MultiPaneControl PaneContainer => _multiPaneControl;
        public XModemDialog XModemDialog { get; set; }

        #endregion

        #region properties
        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                if (_contextMenu == null)
                {
                    InitContextMenu();
                }

                return _contextMenu;
            }
        }
        public GStatusBar StatusBar { get; }

        public TabBar TabBar { get; }

        public ToolStripMenuItem MenuNewLineCR => _menuNewLine_CR;

        public ToolStripMenuItem MenuNewLineLF => _menuNewLine_LF;

        public ToolStripMenuItem MenuNewLineCRLF => _menuNewLine_CRLF;

        public ToolStripMenuItem MenuLocalEcho => _menuLocalEcho;

        public ToolStripMenuItem MenuSuspendLog => _menuSuspendLog;

        public ToolStripMenuItem MenuMacroStop => _menuStopMacro;

        #endregion

        #region
        public void AdjustTitle(ConnectionTag tag)
        {
            string title = "";
            if (tag != null)
            {
                title = tag.FormatFrameText() + " - ";
            }

            if (GApp.MacroManager.MacroIsRunning)
            {
                title += String.Format("Caption.GFrame.MacroIsRunning", GApp.MacroManager.CurrentMacro.Title);
            }
            title += "Poderosa";

            Text = title;
        }
        public void AddConnection(ConnectionTag ct)
        {
            //この順序には要注意
            TabBar.AddTab(ct);
            AddWindowMenu(ct);
        }
        internal void ActivateConnection(ConnectionTag ct)
        {
            if (ct != null)
            {
                if (TabBar != null)
                {
                    TabBar.SetActiveTab(ct);
                }

                AdjustTerminalUI(true, ct);
                _multiPaneControl.ActivateConnection(ct);
                AdjustTitle(ct);
                if (XModemDialog != null && !XModemDialog.Executing)
                {
                    XModemDialog.ConnectionTag = ct;
                }
            }
            else
            {
                AdjustTerminalUI(false, null);
                AdjustTitle(null);
                if (XModemDialog != null)
                {
                    XModemDialog.Close();
                }
            }

            IDictionaryEnumerator e = _windowMenuItemMap.GetEnumerator();
            while (e.MoveNext())
            {
                ((ToolStripMenuItem)e.Key).Checked = (ct == e.Value);
            }
        }
        public void RefreshConnection(ConnectionTag ct)
        {
            if (ct != null)
            {
                TabBar.RefreshConnection(ct);
                TabBar.ArrangeButtons();
            }

            if (GEnv.Connections.Count == 0)
            {
                AdjustTerminalUI(false, null);
                AdjustTitle(null);
            }
            else if (ct == GEnv.Connections.ActiveTag)
            {
                TabBar.SetActiveTab(ct);
                AdjustTitle(ct);
            }

            //Windowメニューの調整
            IDictionaryEnumerator e = _windowMenuItemMap.GetEnumerator();
            while (e.MoveNext())
            {
                if (ct == e.Value)
                {
                    ((GMenuItem)e.Key).Text = ct.FormatTabText();
                    break;
                }
            }
        }
        public void RemoveConnection(ConnectionTag ct)
        {
            TabBar.RemoveTab(ct);
            RemoveWindowMenu(ct);
        }
        public void ReplaceConnection(ConnectionTag prev, ConnectionTag next)
        {
            IDictionaryEnumerator e = _windowMenuItemMap.GetEnumerator();
            while (e.MoveNext())
            {
                if (prev == e.Value)
                {
                    object k = e.Key;
                    _windowMenuItemMap.Remove(k);
                    _windowMenuItemMap.Add(k, next);
                    break;
                }
            }
        }
        public void RemoveAllConnections()
        {
            TabBar.Clear();
            ClearWindowMenu();
            AdjustTitle(null);
            _multiPaneControl.RemoveAllConnections();
        }
        public void AdjustTerminalUI(bool enabled, ConnectionTag ct)
        {
            TerminalConnection con = ct == null ? null : ct.Connection;
            if (ToolBar != null)
            {
                ToolBar.EnableTerminalUI(enabled, con);
            }

            bool e = GEnv.Connections.Count > 0;
            _menuCloseAll.Enabled = e;
            _menuMovePane.Enabled = e;
            _menuNextTab.Enabled = e;
            _menuPrevTab.Enabled = e;


            _menuSaveShortcut.Enabled = (ct != null);
            _menuSendFile.Enabled = (ct != null);
            _menuReceiveFile.Enabled = (ct != null);
            AdjustConsoleMenu(_menuConsole.DropDownItems, enabled, con, 0);
        }
        internal void AdjustContextMenu(bool enabled, TerminalConnection con)
        {
            ToolStripItemCollection col = ContextMenuStrip.Items;
            col[0].Enabled = !GEnv.TextSelection.IsEmpty && GEnv.TextSelection.Owner.Connection == con;
            col[1].Enabled = !con.IsClosed && CanPaste();
            AdjustConsoleMenu(col, enabled, con, 3); //コピー、ペースト、区切り線の先がコンソールメニュー
        }
        internal TerminalConnection CommandTargetConnection { get; set; }

        private void AdjustConsoleMenu(object sender, EventArgs args)
        {
            AdjustConsoleMenu(_menuConsole.DropDownItems, GEnv.Connections.ActiveTag != null, GEnv.Connections.ActiveConnection, 0);
        }
        private void AdjustConsoleMenu(ToolStripItemCollection target, bool enabled, TerminalConnection con, int baseIndex)
        {
            target[baseIndex + _menuServerInfo.MergeIndex].Enabled = enabled;
            target[baseIndex + _menuLocalEcho.MergeIndex].Enabled = enabled && !con.IsClosed;
            target[baseIndex + _menuLineFeedRule.MergeIndex].Enabled = enabled && !con.IsClosed;
            target[baseIndex + _menuEncoding.MergeIndex].Enabled = enabled && !con.IsClosed;
            target[baseIndex + _menuConsoleClose.MergeIndex].Enabled = enabled;
            //複製と再接続は動作はほとんど一緒
            target[baseIndex + _menuConsoleReproduce.MergeIndex].Enabled = enabled;
            target[baseIndex + _menuConsoleReproduce.MergeIndex].Text = (con != null && con.IsClosed ? "Menu._menuConsoleRevive" : "Menu._menuConsoleReproduce");
            target[baseIndex + _menuSendSpecial.MergeIndex].Enabled = enabled && !con.IsClosed;
            target[baseIndex + _menuNewLine.MergeIndex].Enabled = enabled && !con.IsClosed;
            target[baseIndex + _menuCommentLog.MergeIndex].Enabled = enabled && !con.IsClosed && con.TextLogger.IsActive;
            target[baseIndex + _menuSuspendLog.MergeIndex].Enabled = enabled && !con.IsClosed && con.TextLogger.IsActive;
            target[baseIndex + _menuChangeLog.MergeIndex].Enabled = enabled && !con.IsClosed;
            target[baseIndex + _menuEditRenderProfile.MergeIndex].Enabled = enabled;
            target[baseIndex + _menuRenameTab.MergeIndex].Enabled = enabled && !con.IsClosed;

            // RJC
            //ToolStripItemCollection nls = target[baseIndex + _menuSendSpecial.MergeIndex].MenuItems;
            //nls[_menuSerialConfig.MergeIndex].Enabled = enabled && (con is SerialTerminalConnection);
            //nls[_menuResetTerminal.MergeIndex].Enabled = enabled && !con.IsClosed;

            if (enabled)
            {
                // RJC
                //target[baseIndex + _menuLocalEcho.MergeIndex].Checked = con.Param.LocalEcho;
                //target[baseIndex + _menuSuspendLog.MergeIndex].Checked = con.LogSuspended;

                //nls = target[baseIndex + _menuNewLine.MergeIndex];
                //nls[_menuNewLine_CR.MergeIndex].Checked = (con.Param.TransmitNL == NewLine.CR);
                //nls[_menuNewLine_LF.MergeIndex].Checked = (con.Param.TransmitNL == NewLine.LF);
                //nls[_menuNewLine_CRLF.MergeIndex].Checked = (con.Param.TransmitNL == NewLine.CRLF);

                //nls = target[baseIndex + _menuEncoding.MergeIndex].MenuItems;
                //for (int i = 0; i < nls.Count; i++)
                //{
                //    nls[i].Checked = (i == (int)con.Param.EncodingProfile.Type);
                //}
            }
        }
        public void AdjustMacroMenu()
        {
            int n = _menuBarMacro.MergeIndex + 1;
            while (n < _menuMacro.DropDownItems.Count)
            {
                _menuMacro.DropDownItems.RemoveAt(n); //バー以降を全消去
            }

            foreach (MacroModule mod in GApp.MacroManager.Modules)
            {
                GMenuItem mi = new GMenuItem
                {
                    Text = mod.Title,
                    ShortcutKey = mod.ShortCut
                };
                mi.Click += OnExecMacro;
                _menuMacro.DropDownItems.Add(mi);
            }
        }
        private void OnExecMacro(object sender, EventArgs args)
        {
            GMenuItem mi = (GMenuItem)sender;
            int i = mi.MergeIndex - (_menuBarMacro.MergeIndex + 1);
            GApp.MacroManager.Execute(this, GApp.MacroManager.GetModule(i));
        }
        private void AddWindowMenu(ConnectionTag ct)
        {
            GMenuItem mi = new GMenuItem
            {
                Text = ct.FormatTabText(),
                Checked = true
            };
            //このショートカットは固定で、カスタマイズ不可
            if (_windowMenuItemMap.Count <= 8)
            {
                mi.ShortcutKey = Keys.Alt | (Keys)((int)Keys.D1 + _windowMenuItemMap.Count);
            }
            else if (_windowMenuItemMap.Count == 9)
            {
                mi.ShortcutKey = Keys.Alt | Keys.D0;
            }

            foreach (ToolStripMenuItem m in _windowMenuItemMap.Keys)
            {
                m.Checked = false;
            }

            _windowMenuItemMap.Add(mi, ct);
            mi.Click += OnWindowItemMenuClicked;
            _menuWindow.DropDownItems.Add(mi);
        }
        private void RemoveWindowMenu(ConnectionTag ct)
        {
            IDictionaryEnumerator e = _windowMenuItemMap.GetEnumerator();
            while (e.MoveNext())
            {
                if (ct == e.Value)
                {
                    _menuWindow.DropDownItems.Remove((ToolStripMenuItem)e.Key);
                    _windowMenuItemMap.Remove(e.Key);
                    break;
                }
            }

            for (int i = _menuBarWindow3.MergeIndex + 1; i < _menuWindow.DropDownItems.Count; i++)
            {
                GMenuItem mi = (GMenuItem)_menuWindow.DropDownItems[i];
                int n = i - (_menuBarWindow3.MergeIndex + 1);
                if (n <= 8)
                {
                    mi.ShortcutKey = Keys.Alt | (Keys)((int)Keys.D1 + n);
                }
                else if (n == 9)
                {
                    mi.ShortcutKey = Keys.Alt | Keys.D0;
                }
                else
                {
                    mi.ShortcutKey = Keys.None;
                }
            }
        }
        public void ReorderWindowMenu(int index, int newindex, ConnectionTag active_tag)
        {
            GMenuItem mi1 = (GMenuItem)_menuWindow.DropDownItems[TagIndexToWindowMenuItemIndex(index)];
            Keys mi1_key = mi1.ShortcutKey;
            GMenuItem mi2 = (GMenuItem)_menuWindow.DropDownItems[TagIndexToWindowMenuItemIndex(newindex)];
            mi1.ShortcutKey = mi2.ShortcutKey;
            mi2.ShortcutKey = mi1_key;

            _menuWindow.DropDownItems.Remove(mi1);
            _menuWindow.DropDownItems.Insert(TagIndexToWindowMenuItemIndex(newindex), mi1);
            if (TabBar != null)
            {
                TabBar.ReorderButton(index, newindex, active_tag);
            }
        }
        private int TagIndexToWindowMenuItemIndex(int index)
        {
            return _menuBarWindow3.MergeIndex + 1 + index;
        }
        private void ClearWindowMenu()
        {
            int i = _menuBarWindow3.MergeIndex + 1;
            while (_menuWindow.DropDownItems.Count > i)
            {
                _menuWindow.DropDownItems.RemoveAt(i);
            }
            _windowMenuItemMap.Clear();
        }
        public void AdjustMRUMenu()
        {
            //まず既存MRUメニューのクリア
            _MRUMenuToParameter.Clear();
            int i = _menuBarBeforeMRU.MergeIndex + 1;
            ToolStripItemCollection mi = _menuFile.DropDownItems;
            while (_menuBarAfterMRU.MergeIndex > i)
            {
                mi.RemoveAt(i);
            }

            //リストからセット
            int count = GApp.Options.MRUSize;
            i = 0;
            foreach (TerminalParam p in GApp.ConnectionHistory)
            {
                GMenuItem mru = new GMenuItem();
                _MRUMenuToParameter[mru] = p;
                string text = p.Caption;
                if (text == null || text.Length == 0)
                {
                    text = p.ShortDescription;
                }

                mru.Text = i <= 8 ?
                    String.Format("&{0} {1}", i + 1, text) :
                    String.Format("{0} {1}", i + 1, text);
                mru.Click += OnMRUMenuClicked;
                mi.Insert(_menuBarBeforeMRU.MergeIndex + i + 1, mru);

                if (++i == count)
                {
                    break;
                }
            }

            _menuBarAfterMRU.Visible = (i > 0); //１つもないときはバーが連続してしまい見苦しい
        }
        public void ApplyOptions(ContainerOptions prev, ContainerOptions opt)
        {

            _contextMenu = null;
            _menuMovePaneUp.Enabled = _menuMovePaneDown.Enabled = (opt.FrameStyle == GFrameStyle.DivHorizontal || opt.FrameStyle == GFrameStyle.DivHorizontal3);
            _menuMovePaneLeft.Enabled = _menuMovePaneRight.Enabled = (opt.FrameStyle == GFrameStyle.DivVertical || opt.FrameStyle == GFrameStyle.DivVertical3);
            _menuFrameStyleSingle.Checked = opt.FrameStyle == GFrameStyle.Single;
            _menuFrameStyleDivHorizontal.Checked = opt.FrameStyle == GFrameStyle.DivHorizontal;
            _menuFrameStyleDivVertical.Checked = opt.FrameStyle == GFrameStyle.DivVertical;
            _menuFrameStyleDivHorizontal3.Checked = opt.FrameStyle == GFrameStyle.DivHorizontal3;
            _menuFrameStyleDivVertical3.Checked = opt.FrameStyle == GFrameStyle.DivVertical3;
            _menuExpandPane.Enabled = opt.FrameStyle != GFrameStyle.Single;
            _menuShrinkPane.Enabled = opt.FrameStyle != GFrameStyle.Single;

            if (prev != null && prev.FrameStyle != opt.FrameStyle) //起動直後(prev==null)だとまだレイアウトがされていないのでInitUIは実行できない
            {
                _multiPaneControl.InitUI(prev, opt);
            }

            bool toolbar = prev != null && prev.ShowToolBar;
            bool tabbar = prev != null && prev.ShowTabBar;
            bool statusbar = prev != null && prev.ShowStatusBar;

            SuspendLayout();
            _multiPaneControl.ApplyOptions(opt);
            TabBar.ApplyOptions(opt);

            if (!tabbar && opt.ShowTabBar)
            {
                Controls.Add(TabBar);
                Controls.SetChildIndex(TabBar, 1); //index 0は_multiPaneControl固定
            }
            else if (tabbar && !opt.ShowTabBar)
            {
                Controls.Remove(TabBar);
            }

            if (!toolbar && opt.ShowToolBar)
            {
                if (ToolBar == null)
                {
                    CreateToolBar();
                }

                Controls.Add(ToolBar);
                Controls.SetChildIndex(ToolBar, opt.ShowTabBar ? 2 : 1);
            }
            else if (toolbar && !opt.ShowToolBar)
            {
                if (ToolBar != null)
                {
                    Controls.Remove(ToolBar);
                }
            }
            if (opt.ShowToolBar)
            {
                ToolBar.ApplyOptions(opt);
            }

            if (!statusbar && opt.ShowStatusBar)
            {
                Controls.Add(_statusBar);
                Controls.SetChildIndex(_statusBar, Controls.Count - 1);
            }
            else if (statusbar && !opt.ShowStatusBar)
            {
                Controls.Remove(_statusBar);
            }
            ResumeLayout(true);
        }
        public void ApplyHotKeys()
        {
            ApplyHotKeys(GApp.Options.Commands);
        }
        public void ApplyHotKeys(Commands cmds)
        {
            ApplyHotKeys(cmds, _menu.Items);
            AdjustMacroMenu();
        }
        private void ApplyHotKeys(Commands km, ToolStripItemCollection items)
        {
            foreach (GMenuItemBase mib in items)
            {
                GMenuItem mi = mib as GMenuItem;
                if (mi != null)
                {
                    CID cid = (CID)mi.CID;
                    mi.ShortcutKey = km.FindKey(cid);

                }
                if (mib.DropDownItems.Count > 0)
                {
                    ApplyHotKeys(km, mib.DropDownItems);
                }
            }
        }
        #endregion
        #region overrides
        protected override void OnActivated(EventArgs a)
        {
            base.OnActivated(a);
            if (_firstflag)
            {
                _firstflag = false; //以降は初回のみ実行

                _multiPaneControl.InitUI(null, GApp.Options); //サイズがフィックスしないとこれは実行できない

                //起動時にショートカットを開くには_multiPaneControl.InitUIが先でなければならず、
                //これは自身のウィンドウサイズが必要なのでOnLoadでは早すぎる
                if (_initialAction.ShortcutFile != null)
                {
                    if (File.Exists(_initialAction.ShortcutFile))
                    {
                        GApp.GlobalCommandTarget.OpenShortCut(_initialAction.ShortcutFile);
                    }
                    else
                    {
                        GUtil.Warning(this, String.Format("Message.GFrame.FailedToOpen", _initialAction.ShortcutFile));
                    }
                }
            }
            else
            {
                //GApp.GlobalCommandTarget.SetFocusToActiveConnection();
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GEnv.InterThreadUIService.MainFrameHandle = Handle; //ここでハンドルをセットし、IWin32Windowを他のスレッドがいじらないようにする

            foreach (string m in _initialAction.Messages)
            {
                GUtil.Warning(this, m);
            }

            CID cid = GApp.Options.ShowWelcomeDialog ? CID.ShowWelcomeDialog : GApp.Options.ActionOnLaunch;
            if (cid != CID.NOP)
            {
                GApp.GlobalCommandTarget.DelayedExec(cid);
            }
        }

        protected override void OnSizeChanged(EventArgs args)
        {
            base.OnSizeChanged(args);
            TabBar.ArrangeButtons();
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            if (GApp.GlobalCommandTarget.CloseAll() == CommandResult.Cancelled)
            {
                args.Cancel = true;
            }
            else
            {
                GApp.Options.FramePosition = DesktopBounds;
                GApp.Options.FrameState = WindowState;
                base.OnClosing(args);
            }
        }

        internal void OnDragEnterInternal(DragEventArgs a)
        {
            OnDragEnterBody(a);
        }
        internal void OnDragDropInternal(DragEventArgs a)
        {
            OnDragDropBody(a);
        }

        protected override void OnDragEnter(DragEventArgs a)
        {
            base.OnDragEnter(a);
            OnDragEnterBody(a);
        }
        protected override void OnDragDrop(DragEventArgs a)
        {
            base.OnDragDrop(a);
            OnDragDropBody(a);
        }
        private void OnDragEnterBody(DragEventArgs a)
        {
            if (a.Data.GetDataPresent("FileContents") || a.Data.GetDataPresent("FileDrop"))
            {
                a.Effect = DragDropEffects.Link;
            }
            else
            {
                a.Effect = DragDropEffects.None;
            }
        }
        private void OnDragDropBody(DragEventArgs a)
        {
            string[] fmts = a.Data.GetFormats();

            if (a.Data.GetDataPresent("FileDrop"))
            {
                string[] files = (string[])a.Data.GetData("FileDrop", true);
                //Debug.WriteLine("files="+files.Length);
                GApp.GlobalCommandTarget.DelayedOpenShortcut(files[0]);
            }
        }

        protected override bool IsInputKey(Keys key)
        {
            //Debug.WriteLine("Frame IsInputKey "+key);
            return false;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //Debug.WriteLine("Frame ProcessDialogKey " + keyData);
            CommandResult r = GApp.Options.Commands.ProcessKey(keyData, GApp.MacroManager.MacroIsRunning);
            return r != CommandResult.NOP;
        }
        #endregion
        #region menu event handlers
        private void AdjustEditMenu(object sender, EventArgs e)
        {
            _menuCopy.Enabled = _menuCopyAsLook.Enabled = _menuCopyToFile.Enabled = !GEnv.TextSelection.IsEmpty;
            ConnectionTag tag = GEnv.Connections.ActiveTag;
            bool enable = tag != null;
            IPoderosaTerminalPane p = tag == null ? null : tag.AttachedPane;
            _menuPaste.Enabled = CanPaste() && enable && tag.ModalTerminalTask == null;
            _menuPasteFromFile.Enabled = enable;
            _menuClearBuffer.Enabled = enable;
            _menuClearScreen.Enabled = enable;
            _menuSelectAll.Enabled = enable;
            _menuFreeSelectionMode.Enabled = enable;
            _menuFreeSelectionMode.Checked = enable && (p != null && p.InFreeSelectionMode);
            _menuAutoSelectionMode.Enabled = enable;
            _menuAutoSelectionMode.Checked = enable && (p != null && p.InAutoSelectionMode);
        }

        private void OnMenu(object sender, EventArgs args)
        {
            CID cmd = (CID)(((GMenuItem)sender).CID);
            Commands.Entry e = GApp.Options.Commands.FindEntry(cmd);
            if (e == null)
            {
                Debug.WriteLine("Command Entry Not Found: " + cmd);
            }
            else
            {
                if (GApp.MacroManager.MacroIsRunning && e.CID != CID.StopMacro)
                {
                    return;
                }

                if (e.Target == Commands.Target.Global)
                {
                    GApp.GlobalCommandTarget.Exec(cmd);
                }
                else
                {
                    bool context_menu = ((ToolStripMenuItem)sender).Owner is ContextMenuStrip;
                    ContainerConnectionCommandTarget t = GApp.GetConnectionCommandTarget(context_menu ? CommandTargetConnection : GEnv.Connections.ActiveConnection);
                    if (t == null)
                    {
                        return; //アクティブなコネクションがなければ無視
                    }

                    t.Exec(cmd);
                }
            }
        }

        private int GetIndex(object sender)
        {
            if (sender is ToolStripMenuItem item)
            {
                return (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
            }

            return -1;
        }

        private void OnChangeEncoding(object sender, EventArgs args)
        {
            ContainerConnectionCommandTarget t = GApp.GetConnectionCommandTarget(CommandTargetConnection == null ? GEnv.Connections.ActiveConnection : CommandTargetConnection);
            t.SetEncoding(EncodingProfile.Get((EncodingType)GetIndex(sender)));
        }

        private void OnChangeNewLine(object sender, EventArgs args)
        {
            ContainerConnectionCommandTarget t = GApp.GetConnectionCommandTarget(CommandTargetConnection == null ? GEnv.Connections.ActiveConnection : CommandTargetConnection);
            int i = GetIndex(sender);
            Debug.Assert(0 <= i && i <= 2);
            t.SetTransmitNewLine((NewLine)i);
        }

        private void OnMovePane(object sender, EventArgs args)
        {
            Keys key;
            if (sender == _menuMovePaneUp)
            {
                key = Keys.Up;
            }
            else if (sender == _menuMovePaneDown)
            {
                key = Keys.Down;
            }
            else if (sender == _menuMovePaneLeft)
            {
                key = Keys.Left;
            }
            else /*if(sender==_menuMovePaneRight)*/
            {
                key = Keys.Right;
            }

            GApp.GlobalCommandTarget.MoveActivePane(key);
        }

        private void OnMRUMenuClicked(object sender, EventArgs args)
        {
            TerminalParam p = (TerminalParam)_MRUMenuToParameter[sender];
            p = (TerminalParam)p.Clone();
            p.FeedLogOption();
            GApp.GlobalCommandTarget.NewConnection(p);
        }

        private void OnWindowMenuClicked(object sender, EventArgs args)
        {
            bool e = GEnv.Connections.Count > 0;
            _menuCloseAll.Enabled = e;
            _menuCloseAllDisconnected.Enabled = e;
            _menuMovePane.Enabled = e;
            _menuTab.Enabled = e;
            _menuNextTab.Enabled = e;
            _menuPrevTab.Enabled = e;
            _menuMoveTabToPrev.Enabled = e;
            _menuMoveTabToNext.Enabled = e;
        }
        private void OnWindowItemMenuClicked(object sender, EventArgs args)
        {
            ConnectionTag ct = (ConnectionTag)_windowMenuItemMap[sender];
            GApp.GlobalCommandTarget.ActivateConnection(ct.Connection);
        }

        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            //ショートカットを開いたときの処理
            if (msg.Msg == Win32.WM_COPYDATA)
            {
                unsafe
                {
                    Win32.COPYDATASTRUCT* p = (Win32.COPYDATASTRUCT*)msg.LParam.ToPointer();
                    if (p != null && p->dwData == InterProcessService.OPEN_SHORTCUT)
                    {
                        string fn = new String((char*)p->lpData);
                        msg.Result = new IntPtr(InterProcessService.OPEN_SHORTCUT_OK);
                        GApp.GlobalCommandTarget.OpenShortCut(fn);
                    }
                }
            }
            else if (msg.Msg == GConst.WMG_UIMESSAGE)
            {
                //受信スレッドからのUI処理要求
                GApp.InterThreadUIService.ProcessMessage(msg.WParam, msg.LParam);
            }
            else if (msg.Msg == GConst.WMG_DELAYED_COMMAND)
            {
                GApp.GlobalCommandTarget.DoDelayedExec();
            }
        }

        internal void CenteringDialog(Form frm)
        {
            frm.Left = Left + (Width - frm.Width) / 2;
            frm.Top = Top + (Height - frm.Height) / 2;
        }

        private static bool CanPaste()
        {
            try
            {
                IDataObject data = Clipboard.GetDataObject();
                return data != null && data.GetDataPresent("Text");
            }
            catch (Exception)
            { //クリップボードをロックしているアプリケーションがあるなどのときは例外になってしまう
                return false;
            }
        }
        #endregion
    }
}
