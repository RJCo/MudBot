/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: CommandOptionPanel.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using Poderosa.Config;
using Poderosa.Toolkit;
using Poderosa.UI;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    internal class CommandOptionPanel : OptionDialog.CategoryPanel
    {
        private Commands _commands;

        private ListView _keyConfigList;
        private ColumnHeader _commandCategoryHeader;
        private ColumnHeader _commandNameHeader;
        private ColumnHeader _commandConfigHeader;
        private Button _resetKeyConfigButton;
        private Button _clearKeyConfigButton;
        private GroupBox _commandConfigGroup;
        private Label _commandNameLabel;
        private Label _commandName;
        private Label _currentConfigLabel;
        private Label _currentCommand;
        private Label _newAllocationLabel;
        private HotKey _hotKey;
        private Button _allocateKeyButton;

        public CommandOptionPanel()
        {
            InitializeComponent();
            FillText();
        }
        private void InitializeComponent()
        {
            _keyConfigList = new ListView();
            _commandCategoryHeader = new ColumnHeader();
            _commandNameHeader = new ColumnHeader();
            _commandConfigHeader = new ColumnHeader();
            _resetKeyConfigButton = new Button();
            _clearKeyConfigButton = new Button();
            _commandConfigGroup = new GroupBox();
            _commandNameLabel = new Label();
            _commandName = new Label();
            _currentConfigLabel = new Label();
            _currentCommand = new Label();
            _hotKey = new HotKey();
            _newAllocationLabel = new Label();
            _allocateKeyButton = new Button();

            _commandConfigGroup.SuspendLayout();

            Controls.AddRange(new Control[] {
                                                                                       _keyConfigList,
                                                                                       _resetKeyConfigButton,
                                                                                       _clearKeyConfigButton,
                                                                                       _commandConfigGroup});
            // 
            // _keyConfigList
            // 
            _keyConfigList.Columns.AddRange(new ColumnHeader[] {
                                                                                             _commandCategoryHeader,
                                                                                             _commandNameHeader,
                                                                                             _commandConfigHeader});
            _keyConfigList.FullRowSelect = true;
            _keyConfigList.GridLines = true;
            _keyConfigList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            _keyConfigList.MultiSelect = false;
            _keyConfigList.Name = "_keyConfigList";
            _keyConfigList.Size = new System.Drawing.Size(432, 172);
            _keyConfigList.TabIndex = 0;
            _keyConfigList.View = View.Details;
            _keyConfigList.SelectedIndexChanged += new EventHandler(OnKeyMapItemActivated);
            // 
            // _commandCategoryHeader
            // 
            _commandCategoryHeader.Width = 80;
            // 
            // _commandNameHeader
            // 
            _commandNameHeader.Width = 188;
            // 
            // _commandConfigHeader
            // 
            _commandConfigHeader.Width = 136;
            // 
            // _resetKeyConfigButton
            // 
            _resetKeyConfigButton.Location = new System.Drawing.Point(216, 172);
            _resetKeyConfigButton.Name = "_resetKeyConfigButton";
            _resetKeyConfigButton.FlatStyle = FlatStyle.System;
            _resetKeyConfigButton.Size = new System.Drawing.Size(104, 23);
            _resetKeyConfigButton.TabIndex = 1;
            _resetKeyConfigButton.Click += new EventHandler(OnResetKeyConfig);
            // 
            // _clearKeyConfigButton
            // 
            _clearKeyConfigButton.Location = new System.Drawing.Point(336, 172);
            _clearKeyConfigButton.Name = "_clearKeyConfigButton";
            _clearKeyConfigButton.FlatStyle = FlatStyle.System;
            _clearKeyConfigButton.Size = new System.Drawing.Size(88, 23);
            _clearKeyConfigButton.TabIndex = 2;
            _clearKeyConfigButton.Click += new EventHandler(OnClearKeyConfig);
            // 
            // _commandConfigGroup
            // 
            _commandConfigGroup.Controls.AddRange(new Control[] {
                                                                                              _commandNameLabel,
                                                                                              _commandName,
                                                                                              _currentConfigLabel,
                                                                                              _currentCommand,
                                                                                              _hotKey,
                                                                                              _newAllocationLabel,
                                                                                              _allocateKeyButton});
            _commandConfigGroup.Location = new System.Drawing.Point(8, 196);
            _commandConfigGroup.Name = "_commandConfigGroup";
            _commandConfigGroup.FlatStyle = FlatStyle.System;
            _commandConfigGroup.Size = new System.Drawing.Size(416, 96);
            _commandConfigGroup.TabIndex = 3;
            _commandConfigGroup.TabStop = false;
            // 
            // _commandNameLabel
            // 
            _commandNameLabel.Location = new System.Drawing.Point(8, 16);
            _commandNameLabel.Name = "_commandNameLabel";
            _commandNameLabel.Size = new System.Drawing.Size(88, 23);
            _commandNameLabel.TabIndex = 4;
            _commandNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _commandName
            // 
            _commandName.Location = new System.Drawing.Point(112, 16);
            _commandName.Name = "_commandName";
            _commandName.Size = new System.Drawing.Size(248, 23);
            _commandName.TabIndex = 5;
            _commandName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _currentConfigLabel
            // 
            _currentConfigLabel.Location = new System.Drawing.Point(8, 40);
            _currentConfigLabel.Name = "_currentConfigLabel";
            _currentConfigLabel.Size = new System.Drawing.Size(88, 23);
            _currentConfigLabel.TabIndex = 6;
            _currentConfigLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _currentCommand
            // 
            _currentCommand.Location = new System.Drawing.Point(112, 40);
            _currentCommand.Name = "_currentCommand";
            _currentCommand.Size = new System.Drawing.Size(248, 23);
            _currentCommand.TabIndex = 7;
            _currentCommand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _hotKey
            // 
            _hotKey.DebugTextBox = null;
            _hotKey.ImeMode = ImeMode.Disable;
            _hotKey.Key = Keys.None;
            _hotKey.Location = new System.Drawing.Point(112, 64);
            _hotKey.Name = "_hotKey";
            _hotKey.Size = new System.Drawing.Size(168, 19);
            _hotKey.TabIndex = 8;
            _hotKey.Text = "";
            // 
            // _newAllocationLabel
            // 
            _newAllocationLabel.Location = new System.Drawing.Point(8, 64);
            _newAllocationLabel.Name = "_newAllocationLabel";
            _newAllocationLabel.Size = new System.Drawing.Size(88, 23);
            _newAllocationLabel.TabIndex = 9;
            _newAllocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _allocateKeyButton
            // 
            _allocateKeyButton.Enabled = false;
            _allocateKeyButton.Location = new System.Drawing.Point(288, 64);
            _allocateKeyButton.Name = "_allocateKeyButton";
            _allocateKeyButton.FlatStyle = FlatStyle.System;
            _allocateKeyButton.Size = new System.Drawing.Size(75, 24);
            _allocateKeyButton.TabIndex = 10;
            _allocateKeyButton.Click += new EventHandler(OnAllocateKey);

            BackColor = ThemeUtil.TabPaneBackColor;
            _commandConfigGroup.ResumeLayout();
        }
        private void FillText()
        {
            _commandCategoryHeader.Text = "Category";
            _commandNameHeader.Text = "Command";
            _commandConfigHeader.Text = "Key Allocation";
            _resetKeyConfigButton.Text = "Reset to Default";
            _clearKeyConfigButton.Text = "Clear All";
            _commandConfigGroup.Text = "Command Setting";
            _commandNameLabel.Text = "Command Name";
            _currentConfigLabel.Text = "Current Allocation";
            _newAllocationLabel.Text = "New Allocation";
            _allocateKeyButton.Text = "Allocate";
        }
        public override void InitUI(ContainerOptions options)
        {
            _commands = (Commands)options.Commands.Clone();
            InitKeyConfigUI();
        }
        public override bool Commit(ContainerOptions options)
        {
            options.Commands = _commands;
            return true;
        }

        private void InitKeyConfigUI()
        {
            _keyConfigList.Items.Clear();
            IEnumerator ie = _commands.EnumEntries();
            while (ie.MoveNext())
            {
                Commands.Entry e = (Commands.Entry)ie.Current;
                if (e.Category == Commands.Category.Fixed) continue;
                ListViewItem li = new ListViewItem(EnumDescAttribute.For(typeof(Commands.Category)).GetDescription(e.Category));
                li = _keyConfigList.Items.Add(li);
                li.SubItems.Add(e.Description);
                li.SubItems.Add(e.KeyDisplayString);
                li.Tag = e.CID;
            }
        }
        private void OnKeyMapItemActivated(object sender, EventArgs args)
        {
            if (_keyConfigList.SelectedItems.Count == 0) return;

            CID id = (CID)_keyConfigList.SelectedItems[0].Tag;
            Commands.Entry e = _commands.FindEntry(id);
            Debug.Assert(e != null);
            _hotKey.Key = e.Modifiers | e.Key;

            _commandName.Text = String.Format("{0} - {1}", EnumDescAttribute.For(typeof(Commands.Category)).GetDescription(e.Category), e.Description);
            _currentCommand.Text = e.KeyDisplayString;
            _allocateKeyButton.Enabled = true;
        }
        private void OnAllocateKey(object sender, EventArgs args)
        {
            if (_keyConfigList.SelectedItems.Count == 0) return;

            CID id = (CID)_keyConfigList.SelectedItems[0].Tag;
            Keys key = _hotKey.Key;
            int code = GUtil.KeyToControlCode(key);
            if (code != -1)
            {
                if (GUtil.AskUserYesNo(this, String.Format("The key {0} is used to input ASCII code {1} character. Do you give this command setting priority to the ASCII code?", _hotKey.Text, code)) == DialogResult.No)
                    return;
            }

            Commands.Entry existing = _commands.FindEntry(key);
            if (existing != null && existing.CID != id)
            {
                if (GUtil.AskUserYesNo(this, String.Format("This key is already allocated for the command \"{0}\". Do you wish to overwrite?", existing.Description)) == DialogResult.No)
                    return;

                existing.Key = Keys.None;
                existing.Modifiers = Keys.None;
                FindListViewItem(existing.CID).SubItems[2].Text = "";
            }

            Commands.Entry e = _commands.FindEntry(id);
            Debug.Assert(e != null);
            _commands.ModifyKey(e.CID, key & Keys.Modifiers, key & Keys.KeyCode);
            _keyConfigList.SelectedItems[0].SubItems[2].Text = e.KeyDisplayString;
        }

        private void OnResetKeyConfig(object sender, EventArgs args)
        {
            _commands.Init();
            InitKeyConfigUI();
        }
        private void OnClearKeyConfig(object sender, EventArgs args)
        {
            _commands.ClearKeyBinds();
            InitKeyConfigUI();
        }
        private ListViewItem FindListViewItem(CID id)
        {
            foreach (ListViewItem li in _keyConfigList.Items)
            {
                if (li.Tag.Equals(id)) return li;
            }
            return null;
        }
    }
}
