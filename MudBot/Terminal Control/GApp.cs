/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: GApp.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Microsoft.Win32;
using Poderosa.Communication;
using Poderosa.Config;
using Poderosa.Connection;
using Poderosa.Forms;
using Poderosa.MacroEnv;
using Poderosa.Terminal;
using Poderosa.Toolkit;
using Poderosa.UI;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Poderosa
{
    internal class GApp
    {
        public static GFrame _frame;
        private static PoderosaContainer _container;
        public static IntPtr _globalMutex;


        [STAThread]
        private static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            try
            {
                if (args.Length > 0)
                {
                    if (InterProcessService.SendShortCutFileNameToExistingInstance(args[0]))
                    {
                        return;
                    }
                }

                Run(args);
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
#else
				GUtil.ReportCriticalError(e);
#endif
            }
        }

        private static IntPtr CheckDuplicatedInstance()
        {
            IntPtr t = Win32.CreateEvent(IntPtr.Zero, 0, 0, "PoderosaHandle");
            if (Win32.GetLastError() == Win32.ERROR_ALREADY_EXISTS)
            {
                Win32.CloseHandle(t);
                return IntPtr.Zero;
            }
            else
            {
                return t;
            }
        }

        public static void CreateGFrame(string[] args)
        {
            InitialAction a = new InitialAction();
            _globalMutex = Win32.CreateMutex(IntPtr.Zero, 0, "PoderosaGlobalMutex");
            bool already_exists = (Win32.GetLastError() == Win32.ERROR_ALREADY_EXISTS);
            if (_globalMutex == IntPtr.Zero)
            {
                throw new Exception("Global mutex could not open");
            }

            LoadEnvironment(a);
            Init(a, args, already_exists);
            //System.Windows.Forms.Application.Run(_frame);
            //_frame.Show();

            if (!CloseWithoutSave)
            {
                SaveEnvironment();
            }

            GEnv.Terminate();

            Win32.CloseHandle(_globalMutex);
        }

        public static void Run(string[] args)
        {
            InitialAction a = new InitialAction();
            _globalMutex = Win32.CreateMutex(IntPtr.Zero, 0, "PoderosaGlobalMutex");
            bool already_exists = (Win32.GetLastError() == Win32.ERROR_ALREADY_EXISTS);
            if (_globalMutex == IntPtr.Zero)
            {
                throw new Exception("Global mutex could not open");
            }

            LoadEnvironment(a);
            Init(a, args, already_exists);
            //System.Windows.Forms.Application.Run(_frame);
            //_frame.Show();
            if (!CloseWithoutSave)
            {
                SaveEnvironment();
            }

            GEnv.Terminate();

            Win32.CloseHandle(_globalMutex);
        }

        public static void Init(InitialAction act, string[] args, bool already_exists)
        { //GFrameの作成はコンストラクタの後にしないと、GuevaraAppのメソッドをデリゲートの引数にできない。

            if (args.Length > 0)
            {
                act.ShortcutFile = args[0];
            }

            _frame = new GFrame(act);
            GlobalCommandTarget.Init(_frame);

            if (already_exists && Options.FrameState == FormWindowState.Normal)
            {
                Rectangle rect = Options.FramePosition;
                rect.Location += new Size(24, 24);
                Options.FramePosition = rect;
            }

            _frame.DesktopBounds = Options.FramePosition;
            _frame.WindowState = Options.FrameState;
            _frame.AdjustMRUMenu();

            //キャッチできなかったエラーの補足
            Application.ThreadException += OnThreadException;
        }

        public static void LoadEnvironment(InitialAction act)
        {
            ThemeUtil.Init();

            OptionPreservePlace place = GetOptionPreservePlace();
            Options = new ContainerOptions();
            ConnectionHistory = new ConnectionHistory();
            MacroManager = new MacroManager();
            _container = new PoderosaContainer();
            GlobalCommandTarget = new ContainerGlobalCommandTarget();
            InterThreadUIService = new ContainerInterThreadUIService();


            //この時点ではOSの言語設定に合ったリソースをロードする。起動直前で必要に応じてリロード
            ReloadStringResource();

            GEnv.Init(_container);
            GEnv.Options = Options;
            GEnv.GlobalCommandTarget = GlobalCommandTarget;
            GEnv.InterThreadUIService = InterThreadUIService;
            string dir = GetOptionDirectory(place);
            LoadConfigFiles(dir, act);
            Options.OptionPreservePlace = place;

            //ここまできたら言語設定をチェックし、必要なら読み直し
            if (GUtil.CurrentLanguage != Options.Language)
            {
                Thread.CurrentThread.CurrentUICulture = Options.Language == Language.Japanese ? new CultureInfo("ja") : CultureInfo.InvariantCulture;
                ReloadStringResource();
            }

        }

        private static void LoadConfigFiles(string dir, InitialAction act)
        {
            if (Win32.WaitForSingleObject(_globalMutex, 10000) != Win32.WAIT_OBJECT_0)
            {
                throw new Exception("Global mutex lock error");
            }

            try
            {
                string optionfile = dir + "options.conf";
                bool config_loaded = false;
                bool macro_loaded = false;

                TextReader reader = null;
                try
                {
                    if (File.Exists(optionfile))
                    {
                        reader = new StreamReader(File.Open(optionfile, FileMode.Open, FileAccess.Read), Encoding.Default);
                        if (VerifyConfigHeader(reader))
                        {
                            ConfigNode root = new ConfigNode("root", reader).FindChildConfigNode("poderosa");
                            if (root != null)
                            {
                                Options.Load(root);
                                config_loaded = true;
                                ConnectionHistory.Load(root);
                                MacroManager.Load(root);
                                macro_loaded = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //_errorOccurredOnBoot = true;
                    Debug.WriteLine(ex.StackTrace);
                    GUtil.WriteDebugLog(ex.StackTrace);
                    act.AddMessage("Failed to read the configuration file.\n" + ex.Message);
                }
                finally
                {
                    if (!config_loaded)
                    {
                        Options.Init();
                    }

                    if (!macro_loaded)
                    {
                        MacroManager.SetDefault();
                    }

                    if (reader != null)
                    {
                        reader.Close();
                    }
                }

                GEnv.Options = Options; //これでDefaultRenderProfileが初期化される
            }
            finally
            {
                Win32.ReleaseMutex(_globalMutex);
            }
        }

        private static void ReloadStringResource()
        {
            GEnv.ReloadStringResource();
        }

        private static void InitConfig()
        {
            Options.Init();
            MacroManager.SetDefault();
        }


        private static void SaveEnvironment()
        {

            //OptionDialogで、レジストリへの書き込み権限がないとOptionPreservePlaceは変更できないようにしてあるのでWritableなときだけ書いておけばOK
            if (IsRegistryWritable)
            {
                RegistryKey g = Registry.CurrentUser.CreateSubKey(GCConst.REGISTRY_PATH);
                g.SetValue("option-place", EnumDescAttribute.For(typeof(OptionPreservePlace)).GetName(Options.OptionPreservePlace));
            }

            if (Win32.WaitForSingleObject(_globalMutex, 10000) != Win32.WAIT_OBJECT_0)
            {
                throw new Exception("Global mutex lock error");
            }

            try
            {
                string dir = GetOptionDirectory(Options.OptionPreservePlace);
                TextWriter wr = null;
                try
                {
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    wr = new StreamWriter(dir + "options.conf", false, Encoding.Default);
                }
                catch (Exception ex)
                {
                    //GUtil.ReportCriticalError(ex);
                    GUtil.Warning(Form.ActiveForm, String.Format("Message.GApp.WriteError {1} {0}", ex.Message, dir));
                }

                if (wr != null)
                {
                    try
                    {
                        ConfigNode node = new ConfigNode("poderosa");
                        Options.Save(node);
                        ConnectionHistory.Save(node);
                        MacroManager.Save(node);

                        wr.WriteLine(GCConst.CONFIG_HEADER);
                        node.WriteTo(wr);
                        wr.Close();
                    }
                    catch (Exception ex)
                    {
                        GUtil.ReportCriticalError(ex);
                    }
                }
            }
            finally
            {
                Win32.ReleaseMutex(_globalMutex);
            }
        }
        public static string GetOptionDirectory(OptionPreservePlace p)
        {
            if (p == OptionPreservePlace.InstalledDir)
            {
                string t = AppDomain.CurrentDomain.BaseDirectory;
                if (Environment.UserName.Length > 0)
                {
                    t += Environment.UserName + "\\";
                }

                return t;
            }
            else
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\poderosa\\";
            }
        }
        public static string GetCommonLogDirectory()
        {
            if (GetOptionPreservePlace() == OptionPreservePlace.InstalledDir)
            {
                return AppDomain.CurrentDomain.BaseDirectory + "\\";
            }
            else
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\";
            }
        }
        public static string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;


        public static ConnectionHistory ConnectionHistory { get; private set; }

        public static MacroManager MacroManager { get; private set; }

        public static void UpdateOptions(ContainerOptions opt)
        {
            GEnv.Options = opt;
            _frame.ApplyOptions(Options, opt);
            ConnectionHistory.LimitCount(opt.MRUSize);
            _frame.AdjustMRUMenu();

            if (Options.Language != opt.Language)
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                ReloadStringResource();
                MacroManager.ReloadLanguage();
                _frame.ReloadLanguage(opt.Language);
            }

            //デフォルトのままであった場合には更新をかける
            RenderProfile newprof = new RenderProfile(opt);
            foreach (ConnectionTag ct in GEnv.Connections)
            {
                if (ct.RenderProfile == null && ct.AttachedPane != null)
                {
                    ct.AttachedPane.ApplyRenderProfile(newprof);
                }
            }
            GEnv.DefaultRenderProfile = newprof;
            Options = opt;
        }

        internal static ContainerInterThreadUIService InterThreadUIService { get; private set; }

        internal static GFrame Frame => _frame;
        internal static ContainerOptions Options { get; private set; }

        internal static ContainerGlobalCommandTarget GlobalCommandTarget { get; private set; }

        public static bool ClosingApp { get; set; }

        public static bool CloseWithoutSave { get; set; }


        public static bool IsRegistryWritable
        {
            get
            {
                try
                {
                    RegistryKey g = Registry.CurrentUser.CreateSubKey(GCConst.REGISTRY_PATH);
                    if (g == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private static OptionPreservePlace GetOptionPreservePlace()
        {
            RegistryKey g = Registry.CurrentUser.OpenSubKey(GCConst.REGISTRY_PATH, false);
            if (g == null)
            {
                return OptionPreservePlace.InstalledDir;
            }
            else
            {
                string v = (string)g.GetValue("option-place");
                if (v == null || v.Length == 0)
                {
                    return OptionPreservePlace.InstalledDir;
                }
                else
                {
                    return (OptionPreservePlace)Enum.Parse(typeof(OptionPreservePlace), v);
                }
            }
        }

        public static ContainerConnectionCommandTarget GetConnectionCommandTarget()
        {
            TerminalConnection con = GEnv.Connections.ActiveConnection;
            return con == null ? null : new ContainerConnectionCommandTarget(con);
        }
        public static ContainerConnectionCommandTarget GetConnectionCommandTarget(TerminalConnection con)
        {
            return new ContainerConnectionCommandTarget(con);
        }
        public static ContainerConnectionCommandTarget GetConnectionCommandTarget(ConnectionTag tag)
        {
            return new ContainerConnectionCommandTarget(tag.Connection);
        }

        public static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            GUtil.ReportThreadException(e.Exception);
            /*
			string msg = DateTime.Now.ToString() + " : " + "OnThreadException() sender=" + sender.ToString() + "\r\n  StackTrace=" + new StackTrace(true).ToString();
			Debug.WriteLine(msg);
			GUtil.WriteDebugLog(msg);
			throw e.Exception;
			*/
        }

        private static bool VerifyConfigHeader(TextReader reader)
        {
            string l = reader.ReadLine();
            return l == GCConst.CONFIG_HEADER;
        }
    }
    internal class GCConst
    {
        private static string _productWeb;
        public static string PRODUCT_WEB
        {
            get
            {
                if (_productWeb == null)
                {
                    _productWeb = "http://en.poderosa.org/";
                }

                return _productWeb;
            }
        }
        public const string REGISTRY_PATH = "Software\\Poderosa Project\\Poderosa";
        public const string CONFIG_HEADER = "Poderosa Config 3.0";
    }
    internal class PoderosaContainer : IPoderosaContainer
    {
        public void RemoveConnection(ConnectionTag ct)
        {
            GApp.Frame.RemoveConnection(ct);
        }

        public void ActivateConnection(ConnectionTag ct)
        {
            GApp.GlobalCommandTarget.ActivateConnection2(ct);
        }
        public void RefreshConnection(ConnectionTag ct)
        {
            GApp.Frame.RefreshConnection(ct);
        }

        public void OnDragDrop(DragEventArgs args)
        {
            GApp.Frame.OnDragDropInternal(args);
        }
        public void OnDragEnter(DragEventArgs args)
        {
            GApp.Frame.OnDragEnterInternal(args);
        }

        public Size TerminalSizeForNextConnection => GApp.Frame.PaneContainer.TerminalSizeForNextConnection;

        public int PositionForNextConnection => GApp.Frame.PaneContainer.PositionForNextConnection;


        public void IndicateBell()
        {
            GApp.Frame.StatusBar.IndicateBell();
        }

        public void SetStatusBarText(string text)
        {
            GApp.Frame.StatusBar.SetStatusBarText(text);
        }

        public void ShowContextMenu(Point pt, ConnectionTag ct)
        {
            //GApp.Frame.CommandTargetConnection = ct.Connection;
            //メニューのUI調整
            //GApp.Frame.AdjustContextMenu(true, ct.Connection);
            //GApp.Frame.ContextMenu.Show(GApp.Frame.PaneContainer, pt);

            //foreach (TerminalPane p in GApp.Frame._multiPaneControl._panes)
            //{
            //if (p.Visible)
            //GApp.Frame.ContextMenu.Show(p, pt);
            //}
        }

        public void SetSelectionStatus(SelectionStatus status)
        {
            if (status == SelectionStatus.Auto)
            {
                GApp.Frame.StatusBar.IndicateAutoSelectionMode();
            }
            else if (status == SelectionStatus.Free)
            {
                GApp.Frame.StatusBar.IndicateFreeSelectionMode();
            }
            else
            {
                GApp.Frame.StatusBar.ClearSelectionMode();
            }
        }

        public bool MacroIsRunning => GApp.MacroManager.MacroIsRunning;

        public CommandResult ProcessShortcutKey(Keys key)
        {
            return GApp.Options.Commands.ProcessKey(key, GApp.MacroManager.MacroIsRunning);
        }

        public Form AsForm()
        {
            return GApp.Frame;
        }


        public IntPtr Handle => GApp.Frame.Handle;

        public bool IgnoreErrors => GApp.ClosingApp;
    }
    internal class InterProcessService
    {

        public const int OPEN_SHORTCUT = 7964;
        public const int OPEN_SHORTCUT_OK = 485;

        public static bool SendShortCutFileNameToExistingInstance(string filename)
        {
            unsafe
            {
                //find target
                IntPtr hwnd = Win32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, null);
                bool success = false;
                char[] name = new char[256];
                char[] mf = new char[256];
                while (hwnd != IntPtr.Zero)
                {
                    int len = Win32.GetWindowText(hwnd, name, 256);
                    if (new string(name, 0, len).IndexOf("Poderosa") != -1)
                    { //Window Classを確認するとか何とかすべきかも、だが
                        success = TryToSend(hwnd, filename);
                        if (success)
                        {
                            break;
                        }
                    }
                    hwnd = Win32.FindWindowEx(IntPtr.Zero, hwnd, null, null);
                }


                return success;
            }
        }

        private static unsafe bool TryToSend(IntPtr hwnd, string filename)
        {
            char[] data = filename.ToCharArray();
            char* b = stackalloc char[data.Length + 1];
            for (int i = 0; i < data.Length; i++)
            {
                b[i] = data[i];
            }

            b[data.Length] = '\0';

            //string t = ReadFileName(hglobal);
            Win32.COPYDATASTRUCT cddata = new Win32.COPYDATASTRUCT
            {
                dwData = OPEN_SHORTCUT,
                cbData = (uint)(sizeof(char) * (data.Length + 1)),
                lpData = b
            };

            int lresult = Win32.SendMessage(hwnd, Win32.WM_COPYDATA, IntPtr.Zero, new IntPtr(&cddata));
            //Debug.WriteLine("TryToSend "+lresult);
            return lresult == OPEN_SHORTCUT_OK;
        }

    }
    internal class InitialAction
    {
        private ArrayList _messages;  //message boxを出すべき内容

        public InitialAction()
        {
            _messages = new ArrayList();
        }
        public void AddMessage(string msg)
        {
            _messages.Add(msg);
        }
        public string ShortcutFile { get; set; }

        public IEnumerable Messages => _messages;
    }
}
