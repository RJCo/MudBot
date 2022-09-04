/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: EditEscapeSequenceColor.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using Poderosa.Terminal;
using Poderosa.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.Forms
{
    /// <summary>
    /// EditEscapeSequenceColor �̊T�v�̐����ł��B
    /// </summary>
    internal sealed class EditEscapeSequenceColor : Form
    {
        private ColorButton _backColorBox;
        private ColorButton _foreColorBox;
        private ColorButton[] _colorBoxes;

        private Button _setDefaultButton;
        private Button _okButton;
        private Button _cancelButton;
        /// <summary>
        /// �K�v�ȃf�U�C�i�ϐ��ł��B
        /// </summary>
        private Container components = null;

        public EscapesequenceColorSet Result { get; }

        public Color GForeColor { get; set; }

        public Color GBackColor { get; set; }

        public EditEscapeSequenceColor(Color back, Color fore, EscapesequenceColorSet cs)
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();

            //
            // TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
            //
            _colorBoxes = new ColorButton[8];
            GBackColor = back;
            GForeColor = fore;
            Result = (EscapesequenceColorSet)cs.Clone();
            int ti = 0;

            int y = 8;
            AddBackColorUI(y, ref ti);
            y += 24;

            for (int i = -1; i < 8; i++)
            {
                AddUI(i, y, ref ti); //-1�̓f�t�H���g�F�ݒ�
                y += 24;
            }

            y += 8;
            _setDefaultButton = new Button
            {
                Left = 106,
                Width = 144
            };
            _setDefaultButton.Click += OnSetDefault;
            _setDefaultButton.Text = "Set to Default";
            _setDefaultButton.Top = y;
            _setDefaultButton.TabIndex = ti++;
            _setDefaultButton.FlatStyle = FlatStyle.System;
            Controls.Add(_setDefaultButton);

            y += 32;
            _okButton.Text = "OK";
            _okButton.Top = y;
            _cancelButton.Text = "Cancel";
            _cancelButton.Top = y;
            Text = "Color settings for escape sequences";

            ClientSize = new Size(ClientSize.Width, y + 32);
        }

        /// <summary>
        /// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
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

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
        /// </summary>
        private void InitializeComponent()
        {
            _okButton = new Button();
            _cancelButton = new Button();
            SuspendLayout();
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.FlatStyle = FlatStyle.System;
            _okButton.Location = new Point(88, 232);
            _okButton.Name = "_okButton";
            _okButton.Click += OnOK;
            _okButton.TabIndex = 0;
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.FlatStyle = FlatStyle.System;
            _cancelButton.Location = new Point(176, 232);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.TabIndex = 1;
            // 
            // EditEscapeSequenceColor
            // 
            AcceptButton = _okButton;
            AutoScaleBaseSize = new Size(5, 12);
            CancelButton = _cancelButton;
            ClientSize = new Size(258, 266);
            Controls.Add(_cancelButton);
            Controls.Add(_okButton);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditEscapeSequenceColor";
            ShowInTaskbar = false;
            ResumeLayout(false);
        }
        #endregion

        private void AddBackColorUI(int y, ref int tabindex)
        {
            Label num = new Label
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Back",
                Left = 8,
                Width = 48,
                Top = y,
                Height = 24,
                TabIndex = tabindex++
            };

            ColorButton col = new ColorButton
            {
                SelectedColor = GBackColor,
                Left = 122,
                Width = 128,
                Top = y + 2
            };
            col.ColorChanged += OnNewBackColor;
            col.TabIndex = tabindex++;
            _backColorBox = col;

            Controls.Add(num);
            Controls.Add(col);
        }

        private void AddUI(int index, int y, ref int tabindex)
        {
            Label num = new Label
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Text = index == -1 ? "Default" : index.ToString(),
                Left = 8,
                Width = 48,
                Top = y,
                Height = 24,
                TabIndex = tabindex++
            };

            Label sample = new Label
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Text = GetIndexDesc(index),
                Left = 64,
                Width = 48,
                Top = y,
                Height = 24,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = GBackColor,
                ForeColor = index == -1 ? GForeColor : Result[index],
                TabIndex = tabindex++
            };

            ColorButton col = new ColorButton
            {
                SelectedColor = index == -1 ? GForeColor : Result[index],
                Left = 122,
                Width = 128,
                Top = y + 2
            };
            col.ColorChanged += OnNewColor;
            col.Tag = sample;
            col.TabIndex = tabindex++;
            if (index == -1)
            {
                _foreColorBox = col;
            }
            else
            {
                _colorBoxes[index] = col;
            }

            Controls.Add(num);
            Controls.Add(sample);
            Controls.Add(col);
        }

        private void OnNewColor(object sender, Color arg)
        {
            Control col = (Control)sender;
            ((Label)col.Tag).ForeColor = arg;
        }
        private void OnNewBackColor(object sender, Color arg)
        {
            ((Label)_foreColorBox.Tag).BackColor = arg;
            for (int i = 0; i < _colorBoxes.Length; i++)
            {
                ((Label)_colorBoxes[i].Tag).BackColor = arg;
            }
            Invalidate(true);
        }

        private void OnSetDefault(object sender, EventArgs args)
        {
            for (int i = 0; i < _colorBoxes.Length; i++)
            {
                Color c = EscapesequenceColorSet.GetDefaultColor(i);
                _colorBoxes[i].SelectedColor = c;
                _colorBoxes[i].Invalidate();
                ((Label)_colorBoxes[i].Tag).ForeColor = c;
            }
        }
        private void OnOK(object sender, EventArgs args)
        {
            GBackColor = _backColorBox.SelectedColor;
            GForeColor = _foreColorBox.SelectedColor;
            for (int i = 0; i < _colorBoxes.Length; i++)
            {
                Color c = _colorBoxes[i].SelectedColor;
                Result[i] = c;
            }
        }

        private static string GetIndexDesc(int index)
        {
            char[] t = new char[3];
            t[0] = (index & 4) != 0 ? '1' : '0';
            t[1] = (index & 2) != 0 ? '1' : '0';
            t[2] = (index & 1) != 0 ? '1' : '0';
            return new string(t);
        }
    }
}
