/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: TerminalOptionPanel.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Config;
using Poderosa.ConnectionParam;
using Poderosa.Toolkit;
using Poderosa.UI;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    internal class TerminalOptionPanel : OptionDialog.CategoryPanel
    {
        private Label _badCharLabel;
        private ComboBox _badCharBox;
        private Label _bufferSizeLabel;
        private TextBox _bufferSize;
        private Label _disconnectNotificationLabel;
        private ComboBox _disconnectNotification;
        private CheckBox _closeOnDisconnect;
        private CheckBox _beepOnBellChar;
        private CheckBox _adjustsTabTitleToWindowTitle;
        private CheckBox _allowsScrollInAppMode;
        private CheckBox _keepAliveCheck;
        private TextBox _keepAliveIntervalBox;
        private Label _keepAliveLabel;
        private GroupBox _defaultLogGroup;
        private CheckBox _autoLogCheckBox;
        private Label _defaultLogTypeLabel;
        private ComboBox _defaultLogTypeBox;
        private Label _defaultLogDirectoryLabel;
        private TextBox _defaultLogDirectory;
        private Button _dirSelect;

        public TerminalOptionPanel()
        {
            InitializeComponent();
            FillText();
        }
        private void InitializeComponent()
        {
            _badCharLabel = new Label();
            _badCharBox = new ComboBox();
            _bufferSizeLabel = new Label();
            _bufferSize = new TextBox();
            _disconnectNotificationLabel = new Label();
            _disconnectNotification = new ComboBox();
            _closeOnDisconnect = new CheckBox();
            _beepOnBellChar = new CheckBox();
            _adjustsTabTitleToWindowTitle = new CheckBox();
            _allowsScrollInAppMode = new CheckBox();
            _keepAliveCheck = new CheckBox();
            _keepAliveIntervalBox = new TextBox();
            _keepAliveLabel = new Label();
            _defaultLogGroup = new GroupBox();
            _defaultLogTypeLabel = new Label();
            _defaultLogTypeBox = new ComboBox();
            _defaultLogDirectoryLabel = new Label();
            _defaultLogDirectory = new TextBox();
            _dirSelect = new Button();
            _autoLogCheckBox = new CheckBox();

            _defaultLogGroup.SuspendLayout();

            Controls.AddRange(new Control[] {
                                                                          _badCharLabel,
                                                                          _badCharBox,
                                                                          _bufferSizeLabel,
                                                                          _bufferSize,
                                                                          _disconnectNotificationLabel,
                                                                          _disconnectNotification,
                                                                          _closeOnDisconnect,
                                                                          _beepOnBellChar,
                                                                          _adjustsTabTitleToWindowTitle,
                                                                          _allowsScrollInAppMode,
                                                                          _autoLogCheckBox,
                                                                          _keepAliveCheck,
                                                                          _keepAliveIntervalBox,
                                                                          _keepAliveLabel,
                                                                          _defaultLogGroup});
            // 
            // _badCharLabel
            // 
            _badCharLabel.Location = new Point(24, 8);
            _badCharLabel.Name = "_badCharLabel";
            _badCharLabel.Size = new Size(160, 23);
            _badCharLabel.TabIndex = 0;
            _badCharLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _badCharBox
            // 
            _badCharBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _badCharBox.Location = new Point(200, 8);
            _badCharBox.Name = "_badCharBox";
            _badCharBox.Size = new Size(152, 20);
            _badCharBox.TabIndex = 1;
            // 
            // _bufferSizeLabel
            // 
            _bufferSizeLabel.Location = new Point(24, 32);
            _bufferSizeLabel.Name = "_bufferSizeLabel";
            _bufferSizeLabel.Size = new Size(96, 23);
            _bufferSizeLabel.TabIndex = 2;
            _bufferSizeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _bufferSize
            // 
            _bufferSize.Location = new Point(200, 32);
            _bufferSize.Name = "_bufferSize";
            _bufferSize.Size = new Size(72, 19);
            _bufferSize.TabIndex = 3;
            _bufferSize.TextAlign = HorizontalAlignment.Left;
            _bufferSize.MaxLength = 4;
            _bufferSize.Text = "";
            // 
            // _disconnectNotificationLabel
            // 
            _disconnectNotificationLabel.Location = new Point(24, 56);
            _disconnectNotificationLabel.Name = "_disconnectNotificationLabel";
            _disconnectNotificationLabel.Size = new Size(160, 23);
            _disconnectNotificationLabel.TabIndex = 4;
            _disconnectNotificationLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _disconnectNotification
            // 
            _disconnectNotification.DropDownStyle = ComboBoxStyle.DropDownList;
            _disconnectNotification.Location = new Point(200, 56);
            _disconnectNotification.Name = "_disconnectNotification";
            _disconnectNotification.Size = new Size(152, 19);
            _disconnectNotification.TabIndex = 5;
            // 
            // _closeOnDisconnect
            // 
            _closeOnDisconnect.Location = new Point(24, 80);
            _closeOnDisconnect.Name = "_closeOnDisconnect";
            _closeOnDisconnect.FlatStyle = FlatStyle.System;
            _closeOnDisconnect.Size = new Size(192, 20);
            _closeOnDisconnect.TabIndex = 6;
            // 
            // _beepOnBellChar
            // 
            _beepOnBellChar.Location = new Point(24, 102);
            _beepOnBellChar.Name = "_beepOnBellChar";
            _beepOnBellChar.FlatStyle = FlatStyle.System;
            _beepOnBellChar.Size = new Size(288, 20);
            _beepOnBellChar.TabIndex = 7;
            // 
            // _adjustsTabTitleToWindowTitle
            // 
            _adjustsTabTitleToWindowTitle.Location = new Point(24, 126);
            _adjustsTabTitleToWindowTitle.Name = "_adjustsTabTitleToWindowTitle";
            _adjustsTabTitleToWindowTitle.FlatStyle = FlatStyle.System;
            _adjustsTabTitleToWindowTitle.Size = new Size(336, 20);
            _adjustsTabTitleToWindowTitle.TabIndex = 8;
            // 
            // _allowsScrollInAppMode
            // 
            _allowsScrollInAppMode.Location = new Point(24, 150);
            _allowsScrollInAppMode.Name = "_allowsScrollInAppMode";
            _allowsScrollInAppMode.FlatStyle = FlatStyle.System;
            _allowsScrollInAppMode.Size = new Size(288, 20);
            _allowsScrollInAppMode.TabIndex = 9;
            // 
            // _keepAliveCheck
            // 
            _keepAliveCheck.Location = new Point(24, 176);
            _keepAliveCheck.Name = "_keepAliveCheck";
            _keepAliveCheck.FlatStyle = FlatStyle.System;
            _keepAliveCheck.Size = new Size(244, 20);
            _keepAliveCheck.TabIndex = 10;
            _keepAliveCheck.CheckedChanged += new EventHandler(OnKeepAliveCheckChanged);
            // 
            // _keepAliveIntervalBox
            // 
            _keepAliveIntervalBox.Location = new Point(276, 176);
            _keepAliveIntervalBox.Name = "_keepAliveIntervalBox";
            _keepAliveIntervalBox.Size = new Size(40, 20);
            _keepAliveIntervalBox.TabIndex = 11;
            _keepAliveIntervalBox.MaxLength = 2;
            _keepAliveIntervalBox.TextAlign = HorizontalAlignment.Right;
            // 
            // _keepAliveLabel
            // 
            _keepAliveLabel.Location = new Point(316, 176);
            _keepAliveLabel.Name = "_keepAliveLabel";
            _keepAliveLabel.Size = new Size(50, 20);
            _keepAliveLabel.TabIndex = 12;
            _keepAliveLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _defaultLogGroup
            // 
            _defaultLogGroup.Controls.AddRange(new Control[] {
                                                                                           _defaultLogTypeLabel,
                                                                                           _defaultLogTypeBox,
                                                                                           _defaultLogDirectoryLabel,
                                                                                           _defaultLogDirectory,
                                                                                           _dirSelect});
            _defaultLogGroup.Location = new Point(16, 204);
            _defaultLogGroup.Name = "_defaultLogGroup";
            _defaultLogGroup.FlatStyle = FlatStyle.System;
            _defaultLogGroup.Size = new Size(392, 76);
            _defaultLogGroup.TabIndex = 14;
            _defaultLogGroup.TabStop = false;
            // 
            // _defaultLogTypeLabel
            // 
            _defaultLogTypeLabel.Location = new Point(8, 20);
            _defaultLogTypeLabel.Name = "_defaultLogTypeLabel";
            _defaultLogTypeLabel.Size = new Size(96, 23);
            _defaultLogTypeLabel.TabIndex = 15;
            _defaultLogTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _defaultLogTypeBox
            // 
            _defaultLogTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _defaultLogTypeBox.Location = new Point(128, 20);
            _defaultLogTypeBox.Name = "_defaultLogTypeBox";
            _defaultLogTypeBox.Size = new Size(104, 20);
            _defaultLogTypeBox.TabIndex = 16;
            // 
            // _defaultLogDirectoryLabel
            // 
            _defaultLogDirectoryLabel.Location = new Point(8, 48);
            _defaultLogDirectoryLabel.Name = "_defaultLogDirectoryLabel";
            _defaultLogDirectoryLabel.Size = new Size(112, 23);
            _defaultLogDirectoryLabel.TabIndex = 17;
            _defaultLogDirectoryLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _defaultLogDirectory
            // 
            _defaultLogDirectory.Location = new Point(128, 48);
            _defaultLogDirectory.Name = "_defaultLogDirectory";
            _defaultLogDirectory.Size = new Size(176, 19);
            _defaultLogDirectory.TabIndex = 18;
            _defaultLogDirectory.Text = "";
            // 
            // _dirSelect
            // 
            _dirSelect.Location = new Point(312, 48);
            _dirSelect.Name = "_dirSelect";
            _dirSelect.FlatStyle = FlatStyle.System;
            _dirSelect.Size = new Size(19, 19);
            _dirSelect.TabIndex = 19;
            _dirSelect.Text = "...";
            _dirSelect.Click += new EventHandler(OnSelectLogDirectory);
            // 
            // _autoLogCheckBox
            // 
            _autoLogCheckBox.Location = new Point(24, 200);
            _autoLogCheckBox.Name = "_autoLogCheckBox";
            _autoLogCheckBox.FlatStyle = FlatStyle.System;
            _autoLogCheckBox.Size = new Size(200, 24);
            _autoLogCheckBox.TabIndex = 13;
            _autoLogCheckBox.Checked = true;
            _autoLogCheckBox.CheckedChanged += new EventHandler(OnAutoLogCheckBoxClick);

            BackColor = ThemeUtil.TabPaneBackColor;
            _defaultLogGroup.ResumeLayout();

        }
        private void FillText()
        {
            _badCharLabel.Text = "Form.OptionDialog._badCharLabel";
            _bufferSizeLabel.Text = "Form.OptionDialog._bufferSizeLabel";
            _disconnectNotificationLabel.Text = "Form.OptionDialog._disconnectNotificationLabel";
            _closeOnDisconnect.Text = "Form.OptionDialog._closeOnDisconnect";
            _beepOnBellChar.Text = "Form.OptionDialog._beepOnBellChar";
            _adjustsTabTitleToWindowTitle.Text = "Form.OptionDialog._adjustsTabTitleToWindowTitle";
            _allowsScrollInAppMode.Text = "Form.OptionDialog._allowsScrollInAppMode";
            _keepAliveCheck.Text = "Form.OptionDialog._keepAliveCheck";
            _keepAliveLabel.Text = "Form.OptionDialog._keepAliveLabel";
            _defaultLogTypeLabel.Text = "Form.OptionDialog._defaultLogTypeLabel";
            _defaultLogDirectoryLabel.Text = "Form.OptionDialog._defaultLogDirectoryLabel";
            _autoLogCheckBox.Text = "Form.OptionDialog._autoLogCheckBox";

            _badCharBox.Items.AddRange(EnumDescAttribute.For(typeof(WarningOption)).DescriptionCollection());
            _disconnectNotification.Items.AddRange(EnumDescAttribute.For(typeof(DisconnectNotification)).DescriptionCollection());
            _defaultLogTypeBox.Items.AddRange(new object[] {
                                                               EnumDescAttribute.For(typeof(LogType)).GetDescription(LogType.Default),
                                                               EnumDescAttribute.For(typeof(LogType)).GetDescription(LogType.Binary),
                                                               EnumDescAttribute.For(typeof(LogType)).GetDescription(LogType.Xml)});
        }
        public override void InitUI(ContainerOptions options)
        {
            _bufferSize.Text = options.TerminalBufferSize.ToString();
            _closeOnDisconnect.Checked = options.CloseOnDisconnect;
            _disconnectNotification.SelectedIndex = (int)options.DisconnectNotification;
            _beepOnBellChar.Checked = options.BeepOnBellChar;
            _badCharBox.SelectedIndex = (int)options.WarningOption;
            _adjustsTabTitleToWindowTitle.Checked = options.AdjustsTabTitleToWindowTitle;
            _allowsScrollInAppMode.Checked = options.AllowsScrollInAppMode;
            _keepAliveCheck.Checked = options.KeepAliveInterval != 0;
            _keepAliveIntervalBox.Text = _keepAliveCheck.Checked ? (options.KeepAliveInterval / 60000).ToString() : "5";
            _autoLogCheckBox.Checked = options.DefaultLogType != LogType.None;
            _defaultLogTypeBox.SelectedIndex = (int)options.DefaultLogType - 1;
            _defaultLogDirectory.Text = options.DefaultLogDirectory;
        }
        public override bool Commit(ContainerOptions options)
        {
            bool successful = false;
            string itemname = null;
            try
            {
                options.CloseOnDisconnect = _closeOnDisconnect.Checked;
                options.BeepOnBellChar = _beepOnBellChar.Checked;
                options.AdjustsTabTitleToWindowTitle = _adjustsTabTitleToWindowTitle.Checked;
                options.AllowsScrollInAppMode = _allowsScrollInAppMode.Checked;
                itemname = "Caption.OptionDialog.BufferLineCount";
                options.TerminalBufferSize = Int32.Parse(_bufferSize.Text);
                itemname = "Caption.OptionDialog.MRUCount";
                options.WarningOption = (WarningOption)_badCharBox.SelectedIndex;
                options.DisconnectNotification = (DisconnectNotification)_disconnectNotification.SelectedIndex;
                if (_keepAliveCheck.Checked)
                {
                    itemname = "Caption.OptionDialog.KeepAliveInterval";
                    options.KeepAliveInterval = Int32.Parse(_keepAliveIntervalBox.Text) * 60000;
                    if (options.KeepAliveInterval <= 0)
                    {
                        throw new FormatException();
                    }
                }
                else
                {
                    options.KeepAliveInterval = 0;
                }

                if (_autoLogCheckBox.Checked)
                {
                    if (_defaultLogDirectory.Text.Length == 0)
                    {
                        GUtil.Warning(this, "Message.OptionDialog.EmptyLogDirectory");
                        return false;
                    }
                    options.DefaultLogType = (LogType)EnumDescAttribute.For(typeof(LogType)).FromDescription(_defaultLogTypeBox.Text, LogType.None);
                    if (!Directory.Exists(_defaultLogDirectory.Text))
                    {
                        if (GUtil.AskUserYesNo(this, String.Format("Message.OptionDialog.AskCreateDirectory", _defaultLogDirectory.Text)) == DialogResult.Yes)
                        {
                            Directory.CreateDirectory(_defaultLogDirectory.Text);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    options.DefaultLogDirectory = _defaultLogDirectory.Text;
                }
                else
                {
                    options.DefaultLogType = LogType.None;
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

        private void OnSelectLogDirectory(object sender, EventArgs e)
        {
            /*
			 * よくみるとフォルダ選択UIは.NET1.1で追加されたようだ。CPはお勤めご苦労。
			CP.Windows.Forms.ShellFolderBrowser br = new CP.Windows.Forms.ShellFolderBrowser();
			br.Title = "Caption.OptionDialog.DefaultLogDirectory";
			if(br.ShowDialog(this)) {
				_defaultLogDirectory.Text = br.FolderPath;
			}
			*/
            FolderBrowserDialog dlg = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                Description = "Caption.OptionDialog.DefaultLogDirectory"
            };
            if (_defaultLogDirectory.Text.Length > 0 && Directory.Exists(_defaultLogDirectory.Text))
            {
                dlg.SelectedPath = _defaultLogDirectory.Text;
            }

            if (GCUtil.ShowModalDialog(FindForm(), dlg) == DialogResult.OK)
            {
                _defaultLogDirectory.Text = dlg.SelectedPath;
            }
        }
        private void OnAutoLogCheckBoxClick(object sender, EventArgs args)
        {
            bool e = _autoLogCheckBox.Checked;
            _defaultLogTypeBox.Enabled = e;
            if (_defaultLogTypeBox.SelectedIndex == -1)
            {
                _defaultLogTypeBox.SelectedIndex = 0;
            }

            _defaultLogDirectory.Enabled = e;
            _dirSelect.Enabled = e;
        }
        private void OnKeepAliveCheckChanged(object sender, EventArgs args)
        {
            _keepAliveIntervalBox.Enabled = _keepAliveCheck.Checked;
        }
    }
}
