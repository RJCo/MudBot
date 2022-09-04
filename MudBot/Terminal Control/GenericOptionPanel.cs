/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: GenericOptionPanel.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Config;
using Poderosa.Toolkit;
using Poderosa.UI;
using System;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// GenericOptionPanel ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
    /// </summary>
    internal class GenericOptionPanel : OptionDialog.CategoryPanel
    {
        private Label _languageLabel;
        private ComboBox _languageBox;
        private Label _actionOnLaunchLabel;
        private ComboBox _actionOnLaunchBox;
        private Label _MRUSizeLabel;
        private TextBox _MRUSize;
        private Label _serialCountLabel;
        private TextBox _serialCount;
        private CheckBox _showToolBar;
        private CheckBox _showStatusBar;
        private CheckBox _showTabBar;
        private GroupBox _tabBarGroup;
        private Label _tabStyleLabel;
        private ComboBox _tabStyleBox;
        private CheckBox _splitterRatioBox;
        private CheckBox _askCloseOnExit;
        private CheckBox _quitAppWithLastPane;
        private Label _optionPreservePlaceLabel;
        private ComboBox _optionPreservePlace;
        private Label _optionPreservePlacePath;

        public GenericOptionPanel()
        {
            InitializeComponent();
            FillText();
        }
        private void InitializeComponent()
        {
            _actionOnLaunchLabel = new Label();
            _actionOnLaunchBox = new ComboBox();
            _MRUSizeLabel = new Label();
            _MRUSize = new TextBox();
            _serialCountLabel = new Label();
            _serialCount = new TextBox();
            _tabBarGroup = new GroupBox();
            _tabStyleLabel = new Label();
            _tabStyleBox = new ComboBox();
            _splitterRatioBox = new CheckBox();
            _showToolBar = new CheckBox();
            _showTabBar = new CheckBox();
            _showStatusBar = new CheckBox();
            _askCloseOnExit = new CheckBox();
            _quitAppWithLastPane = new CheckBox();
            _optionPreservePlaceLabel = new Label();
            _optionPreservePlace = new ComboBox();
            _optionPreservePlacePath = new Label();
            _languageLabel = new Label();
            _languageBox = new ComboBox();

            _tabBarGroup.SuspendLayout();

            Controls.AddRange(new Control[] {
                                                                                       _actionOnLaunchLabel,
                                                                                       _actionOnLaunchBox,
                                                                                       _MRUSizeLabel,
                                                                                       _MRUSize,
                                                                                       _serialCountLabel,
                                                                                       _serialCount,
                                                                                       _showToolBar,
                                                                                       _showTabBar,
                                                                                       _showStatusBar,
                                                                                       _tabBarGroup,
                                                                                       _splitterRatioBox,
                                                                                       _askCloseOnExit,
                                                                                       _quitAppWithLastPane,
                                                                                       _optionPreservePlaceLabel,
                                                                                       _optionPreservePlacePath,
                                                                                       _optionPreservePlace,
                                                                                       _languageLabel,
                                                                                       _languageBox});
            // 
            // _languageLabel
            // 
            _languageLabel.Location = new System.Drawing.Point(16, 8);
            _languageLabel.Name = "_languageLabel";
            _languageLabel.Size = new System.Drawing.Size(168, 24);
            _languageLabel.TabIndex = 0;
            _languageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _languageBox
            // 
            _languageBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _languageBox.Location = new System.Drawing.Point(208, 8);
            _languageBox.Name = "_languageBox";
            _languageBox.Size = new System.Drawing.Size(216, 20);
            _languageBox.TabIndex = 1;
            // 
            // _actionOnLaunchLabel
            // 
            _actionOnLaunchLabel.Location = new System.Drawing.Point(16, 32);
            _actionOnLaunchLabel.Name = "_actionOnLaunchLabel";
            _actionOnLaunchLabel.Size = new System.Drawing.Size(104, 23);
            _actionOnLaunchLabel.TabIndex = 2;
            _actionOnLaunchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _actionOnLaunchBox
            // 
            _actionOnLaunchBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _actionOnLaunchBox.Location = new System.Drawing.Point(208, 32);
            _actionOnLaunchBox.Name = "_actionOnLaunchBox";
            _actionOnLaunchBox.Size = new System.Drawing.Size(216, 20);
            _actionOnLaunchBox.TabIndex = 3;
            // 
            // _MRUSizeLabel
            // 
            _MRUSizeLabel.Location = new System.Drawing.Point(16, 56);
            _MRUSizeLabel.Name = "_MRUSizeLabel";
            _MRUSizeLabel.Size = new System.Drawing.Size(272, 23);
            _MRUSizeLabel.TabIndex = 4;
            _MRUSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _MRUSize
            // 
            _MRUSize.Location = new System.Drawing.Point(304, 56);
            _MRUSize.MaxLength = 2;
            _MRUSize.Name = "_MRUSize";
            _MRUSize.Size = new System.Drawing.Size(120, 19);
            _MRUSize.TabIndex = 5;
            _MRUSize.Text = "";
            // 
            // _serialCountLabel
            // 
            _serialCountLabel.Location = new System.Drawing.Point(16, 80);
            _serialCountLabel.Name = "_serialCountLabel";
            _serialCountLabel.Size = new System.Drawing.Size(272, 23);
            _serialCountLabel.TabIndex = 6;
            _serialCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _serialCount
            // 
            _serialCount.Location = new System.Drawing.Point(304, 80);
            _serialCount.MaxLength = 2;
            _serialCount.Name = "_serialCount";
            _serialCount.Size = new System.Drawing.Size(120, 19);
            _serialCount.TabIndex = 7;
            _serialCount.Text = "";
            // 
            // _showTabBar
            // 
            _showTabBar.Location = new System.Drawing.Point(24, 107);
            _showTabBar.Name = "_showTabBar";
            _showTabBar.FlatStyle = FlatStyle.System;
            _showTabBar.Size = new System.Drawing.Size(136, 23);
            _showTabBar.TabIndex = 8;
            _showTabBar.CheckedChanged += new EventHandler(OnShowTabBarCheckedChanged);
            // 
            // _tabBarGroup
            // 
            _tabBarGroup.Controls.AddRange(new Control[] {
                                                                                       _tabStyleLabel,
                                                                                       _tabStyleBox});
            _tabBarGroup.Location = new System.Drawing.Point(16, 112);
            _tabBarGroup.Name = "_tabBarGroup";
            _tabBarGroup.FlatStyle = FlatStyle.System;
            _tabBarGroup.Size = new System.Drawing.Size(408, 40);
            _tabBarGroup.TabIndex = 9;
            _tabBarGroup.TabStop = false;
            // 
            // _tabStyleLabel
            // 
            _tabStyleLabel.Location = new System.Drawing.Point(16, 14);
            _tabStyleLabel.Name = "_tabStyleLabel";
            _tabStyleLabel.Size = new System.Drawing.Size(104, 24);
            _tabStyleLabel.TabIndex = 10;
            _tabStyleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tabStyleBox
            // 
            _tabStyleBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _tabStyleBox.Location = new System.Drawing.Point(192, 15);
            _tabStyleBox.Name = "_tabStyleBox";
            _tabStyleBox.Size = new System.Drawing.Size(196, 20);
            _tabStyleBox.TabIndex = 11;
            // 
            // _splitterRatioBox
            // 
            _splitterRatioBox.Location = new System.Drawing.Point(24, 160);
            _splitterRatioBox.Name = "_splitterRatioBox";
            _splitterRatioBox.FlatStyle = FlatStyle.System;
            _splitterRatioBox.Size = new System.Drawing.Size(320, 20);
            _splitterRatioBox.TabIndex = 12;
            // 
            // _showToolBar
            // 
            _showToolBar.Location = new System.Drawing.Point(24, 184);
            _showToolBar.Name = "_showToolBar";
            _showToolBar.FlatStyle = FlatStyle.System;
            _showToolBar.Size = new System.Drawing.Size(168, 23);
            _showToolBar.TabIndex = 13;
            // 
            // _showStatusBar
            // 
            _showStatusBar.Location = new System.Drawing.Point(224, 184);
            _showStatusBar.Name = "_showStatusBar";
            _showStatusBar.FlatStyle = FlatStyle.System;
            _showStatusBar.Size = new System.Drawing.Size(168, 23);
            _showStatusBar.TabIndex = 14;
            // 
            // _askCloseOnExit
            // 
            _askCloseOnExit.Location = new System.Drawing.Point(24, 220);
            _askCloseOnExit.Name = "_askCloseOnExit";
            _askCloseOnExit.FlatStyle = FlatStyle.System;
            _askCloseOnExit.Size = new System.Drawing.Size(296, 23);
            _askCloseOnExit.TabIndex = 15;
            // 
            // _quitAppWithLastPane
            // 
            _quitAppWithLastPane.Location = new System.Drawing.Point(24, 244);
            _quitAppWithLastPane.Name = "_quitAppWithLastPane";
            _quitAppWithLastPane.FlatStyle = FlatStyle.System;
            _quitAppWithLastPane.Size = new System.Drawing.Size(296, 23);
            _quitAppWithLastPane.TabIndex = 16;
            // 
            // _optionPreservePlaceLabel
            // 
            _optionPreservePlaceLabel.Location = new System.Drawing.Point(16, 268);
            _optionPreservePlaceLabel.Name = "_optionPreservePlaceLabel";
            _optionPreservePlaceLabel.Size = new System.Drawing.Size(208, 24);
            _optionPreservePlaceLabel.TabIndex = 17;
            _optionPreservePlaceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _optionPreservePlaceBox
            // 
            _optionPreservePlace.DropDownStyle = ComboBoxStyle.DropDownList;
            _optionPreservePlace.Location = new System.Drawing.Point(224, 268);
            _optionPreservePlace.Name = "_optionPreservePlaceBox";
            _optionPreservePlace.Size = new System.Drawing.Size(200, 20);
            _optionPreservePlace.TabIndex = 18;
            _optionPreservePlace.SelectedIndexChanged += new EventHandler(OnOptionPreservePlaceChanged);
            // 
            // _optionPreservePlacePath
            // 
            _optionPreservePlacePath.Location = new System.Drawing.Point(16, 292);
            _optionPreservePlacePath.BorderStyle = BorderStyle.FixedSingle;
            _optionPreservePlacePath.Name = "_optionPreservePlacePath";
            _optionPreservePlacePath.Size = new System.Drawing.Size(408, 36);
            _optionPreservePlacePath.TabIndex = 19;
            _optionPreservePlacePath.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            BackColor = ThemeUtil.TabPaneBackColor;
            _tabBarGroup.ResumeLayout();
        }
        private void FillText()
        {
            _actionOnLaunchLabel.Text = "Form.OptionDialog._actionOnLaunchLabel";
            _MRUSizeLabel.Text = "Form.OptionDialog._MRUSizeLabel";
            _serialCountLabel.Text = "Form.OptionDialog._serialCountLabel";
            _showTabBar.Text = "Form.OptionDialog._showTabBar";
            _tabStyleLabel.Text = "Form.OptionDialog._tabStyleLabel";
            _splitterRatioBox.Text = "Form.OptionDialog._splitterRatioBox";
            _showToolBar.Text = "Form.OptionDialog._showToolBar";
            _showStatusBar.Text = "Form.OptionDialog._showStatusBar";
            _askCloseOnExit.Text = "Form.OptionDialog._askCloseOnExit";
            _quitAppWithLastPane.Text = "Form.OptionDialog._quitAppWithLastPane";
            _optionPreservePlaceLabel.Text = "Form.OptionDialog._optionPreservePlaceLabel";
            _languageLabel.Text = "Form.OptionDialog._languageLabel";

            _tabStyleBox.Items.AddRange(EnumDescAttribute.For(typeof(TabBarStyle)).DescriptionCollection());
            _optionPreservePlace.Items.AddRange(EnumDescAttribute.For(typeof(OptionPreservePlace)).DescriptionCollection());
            _languageBox.Items.AddRange(EnumDescAttribute.For(typeof(Language)).DescriptionCollection());
        }
        public override void InitUI(ContainerOptions options)
        {
            _MRUSize.Text = options.MRUSize.ToString();
            _serialCount.Text = options.SerialCount.ToString();
            _actionOnLaunchBox.Items.Add("Caption.OptionDialog.ActionOnLaunch.Nothing");
            _actionOnLaunchBox.Items.Add("Caption.OptionDialog.ActionOnLaunch.NewConnection");
            for (int i = 0; i < GApp.MacroManager.ModuleCount; i++)
                _actionOnLaunchBox.Items.Add("Caption.OptionDialog.ActionOnLaunch.Macro" + GApp.MacroManager.GetModule(i).Title);
            _actionOnLaunchBox.SelectedIndex = ToActionOnLaunchIndex(options.ActionOnLaunch);
            _showToolBar.Checked = options.ShowToolBar;
            _showTabBar.Checked = options.ShowTabBar;
            _showStatusBar.Checked = options.ShowStatusBar;
            _splitterRatioBox.Checked = options.SplitterPreservesRatio;
            _tabStyleBox.SelectedIndex = (int)options.TabBarStyle;
            _askCloseOnExit.Checked = options.AskCloseOnExit;
            _quitAppWithLastPane.Checked = options.QuitAppWithLastPane;
            _optionPreservePlace.SelectedIndex = (int)options.OptionPreservePlace;
            _languageBox.SelectedIndex = (int)options.Language;
        }
        public override bool Commit(ContainerOptions options)
        {
            string itemname = null;
            bool successful = false;
            try
            {
                options.ActionOnLaunch = ToActionOnLaunchCID(_actionOnLaunchBox.SelectedIndex);
                itemname = "Caption.OptionDialog.MRUCount";
                options.MRUSize = Int32.Parse(_MRUSize.Text);
                itemname = "Caption.OptionDialog.SerialPortCount";
                options.SerialCount = Int32.Parse(_serialCount.Text);

                options.ShowTabBar = _showTabBar.Checked;
                options.ShowToolBar = _showToolBar.Checked;
                options.SplitterPreservesRatio = _splitterRatioBox.Checked;
                options.TabBarStyle = (TabBarStyle)_tabStyleBox.SelectedIndex;
                options.ShowStatusBar = _showStatusBar.Checked;
                options.AskCloseOnExit = _askCloseOnExit.Checked;
                options.QuitAppWithLastPane = _quitAppWithLastPane.Checked;
                if (GApp.Options.OptionPreservePlace != (OptionPreservePlace)_optionPreservePlace.SelectedIndex && !GApp.IsRegistryWritable)
                {
                    GUtil.Warning(this, "Message.OptionDialog.RegistryWriteAuthWarning");
                    return false;
                }
                options.OptionPreservePlace = (OptionPreservePlace)_optionPreservePlace.SelectedIndex;
                options.Language = (Language)_languageBox.SelectedIndex;
                if (options.Language == Language.Japanese && GApp.Options.EnvLanguage == Language.English)
                {
                    if (GUtil.AskUserYesNo(this, "Message.OptionDialog.AskJapaneseFont") == DialogResult.No)
                        return false;
                }

                successful = true;
            }
            catch (FormatException)
            {
                GUtil.Warning(this, String.Format("Message.OptionDialog.InvalidItem", itemname));
            }
            catch (InvalidOptionException ex)
            {
                GUtil.Warning(this, ex.Message);
            }

            return successful;
        }

        private void OnShowTabBarCheckedChanged(object sender, EventArgs args)
        {
            _tabStyleBox.Enabled = _showTabBar.Checked;
            _splitterRatioBox.Enabled = _showTabBar.Checked;
        }

        private void OnOptionPreservePlaceChanged(object sender, EventArgs e)
        {
            AdjustOptionFileLocation((OptionPreservePlace)_optionPreservePlace.SelectedIndex);
        }
        private void AdjustOptionFileLocation(OptionPreservePlace p)
        {
            _optionPreservePlacePath.Text = GApp.GetOptionDirectory(p);
        }

        private static int ToActionOnLaunchIndex(CID action)
        {
            if (action == CID.NOP)
                return 0;
            else if (action == CID.NewConnection)
                return 1;
            else //CID.ExecMacro
                return 2 + (int)(action - CID.ExecMacro);
        }
        private static CID ToActionOnLaunchCID(int n)
        {
            if (n == 0)
                return CID.NOP;
            else if (n == 1)
                return CID.NewConnection;
            else
                return CID.ExecMacro + (n - 2);
        }
    }
}
