/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: Command.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using Poderosa.Toolkit;
using Poderosa.UI;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

namespace Poderosa.Config
{

    internal enum CID
    {
        NOP = 0,
        // File
        NewConnection,
        OpenShortcut,
        ReceiveFile,
        SendFile,
        Quit,
#if DEBUG
        EmulateLog,
#endif
        //Edit
        Copy,
        Paste,
        CopyAsLook,
        CopyToFile,
        PasteFromFile,
        ClearScreen,
        ClearBuffer,
        SelectAll,
        ToggleFreeSelectionMode,
        ToggleAutoSelectionMode,
        // terminal
        Close,
        Reproduce,
        SaveShortcut,
        ShowServerInfo,
        RenameTab,
        ToggleNewLine,
        ToggleLocalEcho,
        LineFeedRule,
        ToggleLogSuspension,
        EditRenderProfile,
        ChangeLogFile,
        CommentLog,
        ResetTerminal,
        SendBreak,
        AreYouThere,
#if DEBUG
        DumpText,
#endif
        // Tools
        OptionDialog,
        Portforwarding,
        MacroConfig,
        StopMacro,
        // Window
        CloseAll,
        CloseAllDisconnected,
        PrevPane,
        NextPane,
        MoveTabToPrev,
        MoveTabToNext,
        ExpandPane,
        ShrinkPane,
        FrameStyleSingle,
        FrameStyleDivHorizontal,
        FrameStyleDivVertical,
        FrameStyleDivHorizontal3,
        FrameStyleDivVertical3,
        MovePaneUp,
        MovePaneDown,
        MovePaneLeft,
        MovePaneRight,
        // Help
        AboutBox,
        ProductWeb,

        // Activation
        ActivateConnection0,
        ActivateConnectionLast,

        //Special
        ShowWelcomeDialog,

        // Last
        COUNT,

        // Macro
        ExecMacro = 1000,
    }

    internal class Commands : ICloneable
    {
        [EnumDesc(typeof(Category))]
        public enum Category
        {
            [EnumValue(Description = "Enum.KeyMap.Category.File")] File,
            [EnumValue(Description = "Enum.KeyMap.Category.Edit")] Edit,
            [EnumValue(Description = "Enum.KeyMap.Category.Console")] Console,
            [EnumValue(Description = "Enum.KeyMap.Category.Tool")] Tool,
            [EnumValue(Description = "Enum.KeyMap.Category.Window")] Window,
            [EnumValue(Description = "Enum.KeyMap.Category.Help")] Help,
            //メニューと対応しないもの
            [EnumValue(Description = "Enum.KeyMap.Category.Macro")] Macro,
            [EnumValue(Description = "Enum.KeyMap.Category.Fixed")] Fixed
        }

        public enum Target
        {
            Global,
            Connection
        }

        // command delegate
        public delegate CommandResult CD();

        public class Entry
        {
            private string _description;

            public Entry Clone()
            {
                return MemberwiseClone() as Entry;
            }

            public Category Category { get; }

            public CID CID { get; }

            public string Description
            {
                get
                {
                    if (Category == Category.Macro)
                    {
                        return _description;
                    }
                    else
                    {
                        return _description;
                    }
                }
            }

            public Keys Modifiers { get; set; }

            public Keys Key { get; set; }

            public Keys DefaultModifiers { get; }

            public Keys DefaultKey { get; }

            public Target Target { get; set; }

            public string KeyDisplayString => UILibUtil.KeyString(Modifiers, Key, '+');

            public bool KeyIsModified => Key != DefaultKey || Modifiers != DefaultModifiers;

            public Entry(Category cat, Target target, CID cid, string desc, Keys mod, Keys key)
            {
                Category = cat;
                Target = target;
                CID = cid;
                _description = desc;
                Modifiers = mod;
                Key = key;
                DefaultModifiers = mod;
                DefaultKey = key;
            }

        }

        public class MacroEntry : Entry
        {
            public MacroEntry(string desc, Keys mod, Keys key, int index) : base(Category.Macro, Target.Global, CID.ExecMacro, desc, mod, key)
            {
                Index = index;
            }

            public int Index { get; }
        }

        protected Entry[] _IDToEntry;
        protected Hashtable _keyToEntry;
        protected bool _keyMapIsDirty;

        protected ArrayList _entries;
        protected ArrayList _fixedEntries;
        protected ArrayList _macroEntries;

        public Commands()
        {
            _keyToEntry = new Hashtable();
            _IDToEntry = new Entry[(int)CID.COUNT];
            _entries = new ArrayList();
            _fixedEntries = new ArrayList();
            _macroEntries = new ArrayList();
            _keyMapIsDirty = true;
        }

        public void ClearKeyBinds()
        {
            foreach (Entry e in _entries)
            {
                e.Modifiers = Keys.None;
                e.Key = Keys.None;
            }

            foreach (Entry e in _macroEntries)
            {
                e.Modifiers = Keys.None;
                e.Key = Keys.None;
            }
            _keyToEntry.Clear();
        }

        public void Init()
        {
            InitCommands();
            InitFixedCommands();
        }

        public object Clone()
        {
            Commands cm = new Commands();
            IEnumerator ie = EnumEntries();
            while (ie.MoveNext())
            {
                Entry e = (Entry)ie.Current;
                Entry ne = e.Clone();
                cm.AddEntry(ne);
            }
            return cm;
        }

        protected void InitCommands()
        {
            _keyToEntry.Clear();
            _entries.Clear();
            AddKeyMap(Category.File, Target.Global, CID.NewConnection, "Command.NewConnection", Keys.Alt, Keys.N);
            AddKeyMap(Category.File, Target.Global, CID.OpenShortcut, "Command.OpenShortcut", Keys.Alt, Keys.O);
            AddKeyMap(Category.File, Target.Global, CID.ReceiveFile, "Command.ReceiveFile", Keys.None, Keys.None);
            AddKeyMap(Category.File, Target.Global, CID.SendFile, "Command.SendFile", Keys.None, Keys.None);
            AddKeyMap(Category.File, Target.Global, CID.Quit, "Command.Quit", Keys.Alt | Keys.Shift, Keys.X);
#if DEBUG
            AddKeyMap(Category.File, Target.Global, CID.EmulateLog, "Command.EmulateLog", Keys.Alt | Keys.Shift, Keys.E);
#endif
            AddKeyMap(Category.Edit, Target.Global, CID.Copy, "Command.Copy", Keys.Alt, Keys.C);
            AddKeyMap(Category.Edit, Target.Connection, CID.Paste, "Command.Paste", Keys.Alt, Keys.V);
            AddKeyMap(Category.Edit, Target.Global, CID.CopyAsLook, "Command.CopyAsLook", Keys.None, Keys.None);
            AddKeyMap(Category.Edit, Target.Global, CID.CopyToFile, "Command.CopyToFile", Keys.None, Keys.None);
            AddKeyMap(Category.Edit, Target.Connection, CID.PasteFromFile, "Command.PasteFromFile", Keys.None, Keys.None);
            AddKeyMap(Category.Edit, Target.Connection, CID.ClearScreen, "Command.ClearScreen", Keys.None, Keys.None);
            AddKeyMap(Category.Edit, Target.Connection, CID.ClearBuffer, "Command.ClearBuffer", Keys.None, Keys.None);
            AddKeyMap(Category.Edit, Target.Connection, CID.SelectAll, "Command.SelectAll", Keys.Alt, Keys.A);
            AddKeyMap(Category.Edit, Target.Global, CID.ToggleFreeSelectionMode, "Command.ToggleFreeSelectionMode", Keys.Alt, Keys.Z);
            AddKeyMap(Category.Edit, Target.Global, CID.ToggleAutoSelectionMode, "Command.ToggleAutoSelectionMode", Keys.Alt, Keys.X);
            AddKeyMap(Category.Console, Target.Connection, CID.Close, "Command.Close", Keys.Alt, Keys.W);
            AddKeyMap(Category.Console, Target.Connection, CID.Reproduce, "Command.Reproduce", Keys.Alt, Keys.R);
            AddKeyMap(Category.Console, Target.Connection, CID.SaveShortcut, "Command.SaveShortcut", Keys.Alt, Keys.S);
            AddKeyMap(Category.Console, Target.Connection, CID.ShowServerInfo, "Command.ShowServerInfo", Keys.Alt, Keys.I);
            AddKeyMap(Category.Console, Target.Connection, CID.RenameTab, "Command.RenameTab", Keys.None, Keys.None);
            AddKeyMap(Category.Console, Target.Connection, CID.ToggleNewLine, "Command.ToggleNewLine", Keys.None, Keys.None);
            AddKeyMap(Category.Console, Target.Connection, CID.ToggleLocalEcho, "Command.ToggleLocalEcho", Keys.None, Keys.None);
            AddKeyMap(Category.Console, Target.Connection, CID.LineFeedRule, "Command.LineFeedRule", Keys.None, Keys.None);
            AddKeyMap(Category.Console, Target.Connection, CID.ToggleLogSuspension, "Command.ToggleLogSuspension", Keys.None, Keys.None);
            AddKeyMap(Category.Console, Target.Connection, CID.EditRenderProfile, "Command.EditRenderProfile", Keys.None, Keys.None);
            AddKeyMap(Category.Console, Target.Connection, CID.ChangeLogFile, "Command.ChangeLogFile", Keys.None, Keys.None);
            AddKeyMap(Category.Console, Target.Connection, CID.CommentLog, "Command.CommentLog", Keys.None, Keys.None);
            AddKeyMap(Category.Console, Target.Connection, CID.ResetTerminal, "Command.ResetTerminal", Keys.None, Keys.None);
            AddKeyMap(Category.Console, Target.Connection, CID.SendBreak, "Command.SendBreak", Keys.None, Keys.None);
            AddKeyMap(Category.Console, Target.Connection, CID.AreYouThere, "Command.AreYouThere", Keys.None, Keys.None);
#if DEBUG
            AddKeyMap(Category.Console, Target.Connection, CID.DumpText, "Command.DumpText", Keys.Alt | Keys.Shift, Keys.D);
#endif
            AddKeyMap(Category.Tool, Target.Global, CID.OptionDialog, "Command.OptionDialog", Keys.Alt, Keys.T);
            AddKeyMap(Category.Tool, Target.Global, CID.Portforwarding, "Command.Portforwarding", Keys.None, Keys.None);
            AddKeyMap(Category.Tool, Target.Global, CID.MacroConfig, "Command.MacroConfig", Keys.Alt, Keys.M);
            AddKeyMap(Category.Tool, Target.Global, CID.StopMacro, "Command.StopMacro", Keys.None, Keys.None);
            AddKeyMap(Category.Window, Target.Global, CID.CloseAll, "Command.CloseAll", Keys.None, Keys.None);
            AddKeyMap(Category.Window, Target.Global, CID.CloseAllDisconnected, "Command.CloseAllDisconnected", Keys.None, Keys.None);
            AddKeyMap(Category.Window, Target.Global, CID.PrevPane, "Command.PrevPane", Keys.Control | Keys.Shift, Keys.Tab);
            AddKeyMap(Category.Window, Target.Global, CID.NextPane, "Command.NextPane", Keys.Control, Keys.Tab);
            AddKeyMap(Category.Window, Target.Global, CID.MoveTabToPrev, "Command.MoveTabToPrev", Keys.None, Keys.None);
            AddKeyMap(Category.Window, Target.Global, CID.MoveTabToNext, "Command.MoveTabToNext", Keys.None, Keys.None);
            AddKeyMap(Category.Window, Target.Global, CID.ExpandPane, "Command.ExpandPane", Keys.Alt, Keys.Add);
            AddKeyMap(Category.Window, Target.Global, CID.ShrinkPane, "Command.ShrinkPane", Keys.Alt, Keys.Subtract);
            AddKeyMap(Category.Window, Target.Global, CID.FrameStyleSingle, "Command.FrameStyleSingle", Keys.None, Keys.None);
            AddKeyMap(Category.Window, Target.Global, CID.FrameStyleDivHorizontal, "Command.FrameStyleDivHorizontal", Keys.None, Keys.None);
            AddKeyMap(Category.Window, Target.Global, CID.FrameStyleDivVertical, "Command.FrameStyleDivVertical", Keys.None, Keys.None);
            AddKeyMap(Category.Window, Target.Global, CID.FrameStyleDivHorizontal3, "Command.FrameStyleDivHorizontal3", Keys.None, Keys.None);
            AddKeyMap(Category.Window, Target.Global, CID.FrameStyleDivVertical3, "Command.FrameStyleDivVertical3", Keys.None, Keys.None);
            AddKeyMap(Category.Window, Target.Global, CID.MovePaneUp, "Command.MovePaneUp", Keys.Alt | Keys.Shift, Keys.Up);
            AddKeyMap(Category.Window, Target.Global, CID.MovePaneDown, "Command.MovePaneDown", Keys.Alt | Keys.Shift, Keys.Down);
            AddKeyMap(Category.Window, Target.Global, CID.MovePaneLeft, "Command.MovePaneLeft", Keys.Alt | Keys.Shift, Keys.Left);
            AddKeyMap(Category.Window, Target.Global, CID.MovePaneRight, "Command.MovePaneRight", Keys.Alt | Keys.Shift, Keys.Right);
            AddKeyMap(Category.Help, Target.Global, CID.AboutBox, "Command.AboutBox", Keys.None, Keys.None);
            AddKeyMap(Category.Help, Target.Global, CID.ProductWeb, "Command.ProductWeb", Keys.None, Keys.None);
            AddKeyMap(Category.Fixed, Target.Global, CID.ShowWelcomeDialog, "", Keys.None, Keys.None);
        }

        private void InitFixedCommands()
        {
            _fixedEntries.Clear();
            int count = CID.ActivateConnectionLast - CID.ActivateConnection0;
            for (int i = 0; i < count; i++)
            {
                AddKeyMap(Category.Fixed, Target.Global, CID.ActivateConnection0 + i, "", Keys.Alt, i == 9 ? Keys.D0 : Keys.D1 + i);
            }
        }

        public void ClearVariableEntries()
        {
            foreach (Entry e in _macroEntries)
            {
                if (e.Key != Keys.None)
                {
                    _keyToEntry.Remove(e.Key | e.Modifiers);
                }
            }
            _keyMapIsDirty = true;
            _macroEntries.Clear();
        }

        public Keys FindKey(CID cid)
        {
            int i = (int)cid;
            Debug.Assert(i >= 0 && i < (int)CID.COUNT);
            Entry e = _IDToEntry[i];
            return e == null ? Keys.None : e.Key | e.Modifiers;
        }

        public Entry FindEntry(Keys key)
        {
            if (_keyMapIsDirty)
            {
                FixKeyMap();
            }

            return (Entry)_keyToEntry[key];
        }

        public Entry FindEntry(CID id)
        {
            if (id >= CID.ExecMacro)
            {
                return FindMacroEntry(id - CID.ExecMacro);
            }
            else
            {
                int i = (int)id;
                Debug.Assert(i >= 0 && i < (int)CID.COUNT);
                return _IDToEntry[i];
            }
        }

        public MacroEntry FindMacroEntry(int index)
        {
            return (MacroEntry)_macroEntries[index];
        }

        public IEnumerator EnumEntries()
        {
            return new ConcatEnumerator(_entries.GetEnumerator(), new ConcatEnumerator(_fixedEntries.GetEnumerator(), _macroEntries.GetEnumerator()));
        }

        public void ModifyKey(Entry e, Keys modifiers, Keys key)
        {
            //既存のキー設定があれば置き換え
            Entry existing = FindEntry(key | modifiers);
            if (existing != null)
            {
                _keyToEntry.Remove(key | modifiers);
            }

            e.Modifiers = modifiers;
            e.Key = key;
            _keyToEntry.Add(key | modifiers, e);
        }

        public void ModifyKey(CID id, Keys modifiers, Keys key)
        {
            Entry e = FindEntry(id);
            Debug.Assert(e != null);
            ModifyKey(e, modifiers, key);
        }

        public void ModifyMacroKey(int index, Keys modifiers, Keys key)
        {
            Entry e = FindMacroEntry(index);
            ModifyKey(e, modifiers, key);
        }

        protected void AddKeyMap(Category cat, Target target, CID id, string desc, Keys mod, Keys key)
        {
            Entry e = new Entry(cat, target, id, desc, mod, key);
            AddEntry(e);
        }

        public void AddEntry(Entry e)
        {
            _keyMapIsDirty = true;

            if (e.Category == Category.Macro)
            {
                _macroEntries.Add(e);
            }
            else
            {
                _IDToEntry[(int)e.CID] = e;

                if (e.Category == Category.Fixed)
                {
                    _fixedEntries.Add(e);
                }
                else
                {
                    _entries.Add(e);
                }
            }
        }
        protected void FixKeyMap()
        {
            _keyToEntry.Clear();
            IEnumerator ie = new ConcatEnumerator(_entries.GetEnumerator(), new ConcatEnumerator(_fixedEntries.GetEnumerator(), _macroEntries.GetEnumerator()));
            while (ie.MoveNext())
            {
                Entry e = (Entry)ie.Current;
                if (e.Key != Keys.None)
                {
                    Keys k = e.Key | e.Modifiers;
                    if (!_keyToEntry.ContainsKey(k))
                    {
                        _keyToEntry.Add(k, e);
                    }
                }
            }
            _keyMapIsDirty = false;
        }

        public void Save(ConfigNode parent)
        {
            ConfigNode node = new ConfigNode("key-definition");

            foreach (Entry e in _entries)
            {
                if (e.KeyIsModified)
                {
                    node[e.CID.ToString()] = UILibUtil.KeyString(e.Modifiers, e.Key, ',');
                }
            }

            parent.AddChild(node);
        }

        public void Load(ConfigNode parent)
        {
            InitCommands();
            InitFixedCommands();

            ConfigNode node = parent.FindChildConfigNode("key-definition");
            if (node != null)
            {
                IDictionaryEnumerator ie = node.GetPairEnumerator();
                while (ie.MoveNext())
                {
                    CID id = (CID)GUtil.ParseEnum(typeof(CID), (string)ie.Key, CID.NOP);
                    if (id == CID.NOP)
                    {
                        continue;
                    }

                    string value = (string)ie.Value;
                    Entry e = FindEntry(id);
                    Keys t = GUtil.ParseKey(value.Split(','));
                    e.Modifiers = t & Keys.Modifiers;
                    e.Key = t & Keys.KeyCode;
                }
            }
        }

        public CommandResult ProcessKey(Keys k, bool macro_running)
        {
            if (_keyMapIsDirty)
            {
                FixKeyMap();
            }

            Entry e = (Entry)_keyToEntry[k];
            if (e != null)
            {
                if (macro_running)
                {
                    if (e.CID != CID.StopMacro)
                    {
                        return CommandResult.Ignored;
                    }
                    else
                    {
                        return Execute(e);
                    }
                }
                else
                {
                    return Execute(e);
                }
            }
            else
            {
                return CommandResult.NOP;
            }
        }

        private static CommandResult Execute(Entry e)
        {
            if (e.Target == Target.Global)
            {
                return GApp.GlobalCommandTarget.Exec(e);
            }
            else
            {
                ContainerConnectionCommandTarget t = GApp.GetConnectionCommandTarget();
                if (t == null)
                {
                    return CommandResult.Ignored;
                }

                return t.Exec(e.CID);
            }
        }

    }

}
