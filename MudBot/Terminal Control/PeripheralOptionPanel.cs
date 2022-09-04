/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: PeripheralOptionPanel.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using Poderosa.Config;
using Poderosa.Toolkit;
using Poderosa.UI;
using System;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// PeripheralOptionPanel の概要の説明です。
    /// </summary>
    internal class PeripheralOptionPanel : OptionDialog.CategoryPanel
    {
        private Label _leftAltKeyLabel;
        private ComboBox _leftAltKeyAction;
        private Label _rightAltKeyLabel;
        private ComboBox _rightAltKeyAction;
        private Label _rightButtonActionLabel;
        private ComboBox _rightButtonAction;
        private CheckBox _autoCopyByLeftButton;
        private Label _additionalWordElementLabel;
        private TextBox _additionalWordElementBox;
        private CheckBox _send0x7FByDel;
        private Label _wheelAmountLabel;
        private TextBox _wheelAmount;
        private Label _localBufferScrollModifierLabel;
        private ComboBox _localBufferScrollModifierBox;

        public PeripheralOptionPanel()
        {
            InitializeComponent();
            FillText();
        }
        private void InitializeComponent()
        {
            _leftAltKeyLabel = new Label();
            _leftAltKeyAction = new ComboBox();
            _rightAltKeyLabel = new Label();
            _rightAltKeyAction = new ComboBox();
            _send0x7FByDel = new CheckBox();
            _autoCopyByLeftButton = new CheckBox();
            _rightButtonActionLabel = new Label();
            _rightButtonAction = new ComboBox();
            _wheelAmountLabel = new Label();
            _wheelAmount = new TextBox();
            _additionalWordElementLabel = new Label();
            _additionalWordElementBox = new TextBox();
            _localBufferScrollModifierLabel = new Label();
            _localBufferScrollModifierBox = new ComboBox();

            Controls.AddRange(new Control[] {
                                                                                                _leftAltKeyLabel,
                                                                                                _leftAltKeyAction,
                                                                                                _rightAltKeyLabel,
                                                                                                _rightAltKeyAction,
                                                                                                _send0x7FByDel,
                                                                                                _autoCopyByLeftButton,
                                                                                                _additionalWordElementLabel,
                                                                                                _additionalWordElementBox,
                                                                                                _rightButtonActionLabel,
                                                                                                _rightButtonAction,
                                                                                                _wheelAmountLabel,
                                                                                                _wheelAmount,
                                                                                                _localBufferScrollModifierLabel,
                                                                                                _localBufferScrollModifierBox});
            // 
            // _leftAltKeyLabel
            // 
            _leftAltKeyLabel.Location = new System.Drawing.Point(24, 12);
            _leftAltKeyLabel.Name = "_leftAltKeyLabel";
            _leftAltKeyLabel.Size = new System.Drawing.Size(160, 23);
            _leftAltKeyLabel.TabIndex = 0;
            _leftAltKeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _leftAltKey
            // 
            _leftAltKeyAction.DropDownStyle = ComboBoxStyle.DropDownList;
            _leftAltKeyAction.Location = new System.Drawing.Point(264, 12);
            _leftAltKeyAction.Name = "_leftAltKey";
            _leftAltKeyAction.Size = new System.Drawing.Size(152, 19);
            _leftAltKeyAction.TabIndex = 1;
            // 
            // _rightAltKeyLabel
            // 
            _rightAltKeyLabel.Location = new System.Drawing.Point(24, 36);
            _rightAltKeyLabel.Name = "_rightAltKeyLabel";
            _rightAltKeyLabel.Size = new System.Drawing.Size(160, 23);
            _rightAltKeyLabel.TabIndex = 2;
            _rightAltKeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _rightAltKey
            // 
            _rightAltKeyAction.DropDownStyle = ComboBoxStyle.DropDownList;
            _rightAltKeyAction.Location = new System.Drawing.Point(264, 36);
            _rightAltKeyAction.Name = "_rightAltKey";
            _rightAltKeyAction.Size = new System.Drawing.Size(152, 19);
            _rightAltKeyAction.TabIndex = 3;
            // 
            // _send0x7FByDel
            // 
            _send0x7FByDel.Location = new System.Drawing.Point(24, 60);
            _send0x7FByDel.Name = "_send0x7FByDel";
            _send0x7FByDel.FlatStyle = FlatStyle.System;
            _send0x7FByDel.Size = new System.Drawing.Size(192, 20);
            _send0x7FByDel.TabIndex = 4;
            // 
            // _autoCopyByLeftButton
            // 
            _autoCopyByLeftButton.Location = new System.Drawing.Point(24, 84);
            _autoCopyByLeftButton.Name = "_autoCopyByLeftButton";
            _autoCopyByLeftButton.FlatStyle = FlatStyle.System;
            _autoCopyByLeftButton.Size = new System.Drawing.Size(288, 20);
            _autoCopyByLeftButton.TabIndex = 5;
            // 
            // _additionalWordElementLabel
            // 
            _additionalWordElementLabel.Location = new System.Drawing.Point(24, 108);
            _additionalWordElementLabel.Size = new System.Drawing.Size(192, 23);
            _additionalWordElementLabel.TabIndex = 6;
            _additionalWordElementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _additionalWordElementBox
            // 
            _additionalWordElementBox.Location = new System.Drawing.Point(264, 108);
            _additionalWordElementBox.Size = new System.Drawing.Size(152, 23);
            _additionalWordElementBox.TabIndex = 7;
            // 
            // _rightButtonActionLabel
            // 
            _rightButtonActionLabel.Location = new System.Drawing.Point(24, 132);
            _rightButtonActionLabel.Name = "_rightButtonActionLabel";
            _rightButtonActionLabel.Size = new System.Drawing.Size(160, 23);
            _rightButtonActionLabel.TabIndex = 8;
            _rightButtonActionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _rightButtonAction
            // 
            _rightButtonAction.DropDownStyle = ComboBoxStyle.DropDownList;
            _rightButtonAction.Location = new System.Drawing.Point(264, 132);
            _rightButtonAction.Name = "_rightButtonAction";
            _rightButtonAction.Size = new System.Drawing.Size(152, 19);
            _rightButtonAction.TabIndex = 9;
            // 
            // _wheelAmountLabel
            // 
            _wheelAmountLabel.Location = new System.Drawing.Point(24, 156);
            _wheelAmountLabel.Name = "_wheelAmountLabel";
            _wheelAmountLabel.Size = new System.Drawing.Size(176, 23);
            _wheelAmountLabel.TabIndex = 10;
            _wheelAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _wheelAmount
            // 
            _wheelAmount.Location = new System.Drawing.Point(264, 156);
            _wheelAmount.Name = "_wheelAmount";
            _wheelAmount.Size = new System.Drawing.Size(152, 19);
            _wheelAmount.TabIndex = 11;
            _wheelAmount.MaxLength = 2;
            // 
            // _localBufferScrollModifierLabel
            // 
            _localBufferScrollModifierLabel.Location = new System.Drawing.Point(24, 180);
            _localBufferScrollModifierLabel.Name = "_localBufferScrollModifierLabel";
            _localBufferScrollModifierLabel.Size = new System.Drawing.Size(240, 23);
            _localBufferScrollModifierLabel.TabIndex = 12;
            _localBufferScrollModifierLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _localBufferScrollModifierBox
            // 
            _localBufferScrollModifierBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _localBufferScrollModifierBox.Location = new System.Drawing.Point(264, 180);
            _localBufferScrollModifierBox.Name = "_localBufferScrollModifierBox";
            _localBufferScrollModifierBox.Size = new System.Drawing.Size(152, 19);
            _localBufferScrollModifierBox.TabIndex = 13;

            BackColor = ThemeUtil.TabPaneBackColor;
        }
        private void FillText()
        {
            _leftAltKeyLabel.Text = "Form.OptionDialog._leftAltKeyLabel";
            _rightAltKeyLabel.Text = "Form.OptionDialog._rightAltKeyLabel";
            _send0x7FByDel.Text = "Form.OptionDialog._send0x7FByDel";
            _autoCopyByLeftButton.Text = "Form.OptionDialog._autoCopyByLeftButton";
            _rightButtonActionLabel.Text = "Form.OptionDialog._rightButtonActionLabel";
            _wheelAmountLabel.Text = "Form.OptionDialog._wheelAmountLabel";
            _additionalWordElementLabel.Text = "Form.OptionDialog._additionalWordElementLabel";
            _localBufferScrollModifierLabel.Text = "Form.OptionDialog._localBufferScrollModifierLabel";

            _leftAltKeyAction.Items.AddRange(EnumDescAttribute.For(typeof(AltKeyAction)).DescriptionCollection());
            _rightAltKeyAction.Items.AddRange(EnumDescAttribute.For(typeof(AltKeyAction)).DescriptionCollection());
            _rightButtonAction.Items.AddRange(EnumDescAttribute.For(typeof(RightButtonAction)).DescriptionCollection());
            _localBufferScrollModifierBox.Items.AddRange(new object[] { "Control", "Shift" });
        }
        public override void InitUI(ContainerOptions options)
        {
            _leftAltKeyAction.SelectedIndex = (int)options.LeftAltKey;
            _rightAltKeyAction.SelectedIndex = (int)options.RightAltKey;
            _send0x7FByDel.Checked = options.Send0x7FByDel;
            _autoCopyByLeftButton.Checked = options.AutoCopyByLeftButton;
            _rightButtonAction.SelectedIndex = (int)options.RightButtonAction;
            _wheelAmount.Text = options.WheelAmount.ToString();
            _additionalWordElementBox.Text = options.AdditionalWordElement;
            _localBufferScrollModifierBox.SelectedIndex = LocalBufferScrollModifierIndex(options.LocalBufferScrollModifier);
        }
        public override bool Commit(ContainerOptions options)
        {
            bool successful = false;
            string itemname = null;
            try
            {
                //Win9xでは、左右のAltの区別ができないので別々の設定にすることを禁止する
                if (Environment.OSVersion.Platform == PlatformID.Win32Windows &&
                    _leftAltKeyAction.SelectedIndex != _rightAltKeyAction.SelectedIndex)
                {
                    GUtil.Warning(this, "Message.OptionDialog.AltKeyOnWin9x");
                    return false;
                }

                options.LeftAltKey = (AltKeyAction)_leftAltKeyAction.SelectedIndex;
                options.RightAltKey = (AltKeyAction)_rightAltKeyAction.SelectedIndex;
                options.Send0x7FByDel = _send0x7FByDel.Checked;
                options.AutoCopyByLeftButton = _autoCopyByLeftButton.Checked;
                options.RightButtonAction = (RightButtonAction)_rightButtonAction.SelectedIndex;

                itemname = "Caption.OptionDialog.MousewheelAmount";
                options.WheelAmount = Int32.Parse(_wheelAmount.Text);
                options.LocalBufferScrollModifier = LocalBufferScrollModifierKey(_localBufferScrollModifierBox.SelectedIndex);

                foreach (char ch in _additionalWordElementBox.Text)
                {
                    if (ch >= 0x100)
                    {
                        GUtil.Warning(this, "Message.OptionDialog.InvalidAdditionalWordElement");
                        return false;
                    }
                }
                options.AdditionalWordElement = _additionalWordElementBox.Text;

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

        private static int LocalBufferScrollModifierIndex(Keys key)
        {
            if (key == Keys.Control)
            {
                return 0;
            }
            else if (key == Keys.Shift)
            {
                return 1;
            }
            else if (key == Keys.Alt)
            {
                return 2;
            }
            else
            {
                return -1; //never comes
            }
        }
        private static Keys LocalBufferScrollModifierKey(int index)
        {
            if (index == 0)
            {
                return Keys.Control;
            }
            else if (index == 1)
            {
                return Keys.Shift;
            }
            else if (index == 2)
            {
                return Keys.Alt;
            }
            else
            {
                return Keys.Control;
            }
        }
    }
}
