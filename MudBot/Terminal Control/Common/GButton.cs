/*
 * Copyright (c) 2005 Poderosa Project, All Rights Reserved.
 * $Id: GButton.cs,v 1.2 2005/04/20 08:45:46 okajima Exp $
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Poderosa.UI
{
    public enum DrawState
    {
        Normal,
        Disabled,
        Hover,
        Focused
    }

    public class GButton : UserControl
    {
        protected bool _mouseDown;
        protected bool _mouseEnter;
        protected bool _showComboStyle;
        protected bool _checked;
        protected Image _image;
        private const int COMBOAREA_WIDTH = 8;
        private BorderStyle _borderstyle;

        public new BorderStyle BorderStyle
        {
            get
            {
                return _borderstyle;
            }
            set
            {
                _borderstyle = value;
            }
        }
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
            }
        }
        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }
        public bool ShowComboStyle
        {
            get
            {
                return _showComboStyle;
            }
            set
            {
                _showComboStyle = value;
            }
        }

        public GButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            _borderstyle = BorderStyle.None;
            Debug.Assert(!InvokeRequired);
        }

        public void Reset()
        {
            _mouseDown = false;
            _mouseEnter = false;
            Cursor = Cursors.Default;
            Debug.Assert(!InvokeRequired);
            Invalidate();
        }

        public int BodyWidth
        {
            get
            {
                int w = Width;
                if (_showComboStyle) w -= COMBOAREA_WIDTH;
                return w;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawState st;
            if (_mouseDown)
                st = DrawState.Focused;
            else if (_mouseEnter)
                st = DrawState.Hover;
            else if (Enabled)
                st = DrawState.Normal;
            else
                st = DrawState.Disabled;

            DrawButtonState(pe.Graphics, st);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            try
            {
                base.OnMouseEnter(e);
                _mouseEnter = true;
                Cursor = Cursors.Hand;
                Invalidate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Debugger.Break();
            }

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            try
            {
                base.OnMouseLeave(e);
                _mouseEnter = false;
                Cursor = Cursors.Default;
                Invalidate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Debugger.Break();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                base.OnMouseDown(e);
                _mouseDown = true;
                Invalidate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Debugger.Break();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                base.OnMouseUp(e);
                _mouseDown = false;
                Invalidate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Debugger.Break();
            }
        }

        protected virtual void DrawButtonState(Graphics g, DrawState state)
        {
            DrawBackground(g, state);

            bool has_text = false;
            bool has_image = Image != null;

            int x, y;
            if (has_text && !has_image)
            {
                x = BodyWidth / 2;
                y = Height / 2;
                DrawText(g, Text, state, x, y);
            }
            else if (has_image && !has_text)
            {
                x = (BodyWidth - Image.Width) / 2;
                y = (Height - Image.Height) / 2;
                if (_checked)
                {
                    x++; y++;
                }
                DrawImage(g, state, Image, x, y);
            }
            else if (has_image && has_text)
            {
                x = 1;
                y = (Height - Image.Height) / 2;
                if (_checked)
                {
                    x++; y++;
                }
                DrawImage(g, state, Image, x, y);
                x += Image.Width + 2;
                DrawText(g, Text, state, x, y);
            }

            if (_showComboStyle)
            {
                DrawComboStyleTriangle(g, state);
            }

        }

        protected void DrawBackground(Graphics g, DrawState state)
        {
            Rectangle rc = ClientRectangle;
            if (_focusedBackgroundBrush == null) CreateBrushes();


            if (state == DrawState.Normal || state == DrawState.Disabled)
            {
                g.FillRectangle(_checked ? SystemBrushes.ControlLight : SystemBrushes.Control, rc);
                if (_checked)
                {
                    ControlPaint.DrawBorder3D(g, rc, Border3DStyle.Sunken);
                }
                else if (_borderstyle != BorderStyle.None)
                {
                    //!!g.FillRectangle(new SolidBrush(this.BackColor), rc);
                    g.DrawRectangle(state == DrawState.Disabled ? SystemPens.ControlDark : SystemPens.ControlDarkDark, rc.Left, rc.Top, rc.Width - 1, rc.Height - 1);
                }
            }
            else if (state == DrawState.Hover || state == DrawState.Focused)
            {
                if (state == DrawState.Hover)
                    g.FillRectangle(_hoverBackgroundBrush, rc);
                else
                    g.FillRectangle(_focusedBackgroundBrush, rc);
                g.DrawRectangle(SystemPens.Highlight, rc.Left, rc.Top, rc.Width - 1, rc.Height - 1);
            }
        }

        protected static void DrawImage(Graphics g, DrawState state, Image image, int x, int y)
        {
            if (state == DrawState.Normal)
                g.DrawImage(image, x, y, image.Width, image.Height);
            else if (state == DrawState.Disabled)
                ControlPaint.DrawImageDisabled(g, image, x, y, SystemColors.Control);
            else if (state == DrawState.Focused || state == DrawState.Hover)
            {
                ControlPaint.DrawImageDisabled(g, image, x + 1, y, SystemColors.Control);
                g.DrawImage(image, x - 1, y - 1, image.Width, image.Height);
            }
        }

        protected void DrawText(Graphics g, string text, DrawState state, int x, int y)
        {
            if (state == DrawState.Disabled)
                g.DrawString(text, Font, SystemBrushes.ControlDark, new Point(x, y));
            else
                g.DrawString(text, Font, SystemBrushes.ControlText, new Point(x, y));
        }

        protected void DrawComboStyleTriangle(Graphics g, DrawState state)
        {
            Pen p = state == DrawState.Disabled ? SystemPens.ControlDark : SystemPens.ControlText;
            int x = Width - COMBOAREA_WIDTH;
            int y = Height / 2;
            g.DrawLine(p, x, y - 1, x + 5, y - 1);
            g.DrawLine(p, x + 1, y, x + 4, y);
            g.DrawLine(p, x + 2, y + 1, x + 3, y + 1);
        }

        private static Brush _focusedBackgroundBrush;
        private static Brush _hoverBackgroundBrush;

        private static void CreateBrushes()
        {
            _focusedBackgroundBrush = new SolidBrush(ColorUtil.VSNetPressedColor);
            _hoverBackgroundBrush = new SolidBrush(ColorUtil.VSNetSelectionColor);
        }
    }

    public class ToggleButton : GButton
    {
        private bool _autoToggle;

        public bool AutoToggle
        {
            get
            {
                return _autoToggle;
            }
            set
            {
                _autoToggle = value;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (_autoToggle)
                _checked = !_checked;
        }
    }

    public class TabBarButton : GButton
    {
        private string _headText;
        private bool _selected;
        private static DrawUtil.RoundRectColors _selectedColors;
        private static DrawUtil.RoundRectColors _hoverColors;

        public string HeadText
        {
            get
            {
                return _headText;
            }
            set
            {
                _headText = value;
            }
        }
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }
        public TabBarButton()
        {
            BorderStyle = BorderStyle.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (_selectedColors == null) CreateColor();
            //border
            if (_selected)
                DrawUtil.DrawRoundRect(g, 0, 0, Width - 1, Height - 1, _selectedColors);
            else if (_mouseEnter)
                DrawUtil.DrawRoundRect(g, 0, 0, Width - 1, Height - 1, _hoverColors);

            DrawButtonState(g);
        }


        private void DrawButtonState(Graphics g)
        {
            int x, y;
            x = 2;
            y = (Height - Image.Height) / 2;
            DrawImage(g, DrawState.Normal, Image, x, y);
            x += Image.Width + 2;
            if (_headText != null)
            {
                g.DrawString(_headText, Font, SystemBrushes.ControlDark, x, y + 2);
                x += 11; //Should it be configurable?
            }
            DrawText(g, Text, _selected ? DrawState.Focused : DrawState.Normal, x, y + 2);
        }
        private static void CreateColor()
        {
            Color c = SystemColors.Control;
            c = Color.FromArgb((c.R + 255) / 2, (c.G + 255) / 2, (c.B + 255) / 2); //”’‚Æ‚Ì’†ŠÔ‚ð‚Æ‚é

            _selectedColors = new DrawUtil.RoundRectColors
            {
                border_color = DrawUtil.ToCOLORREF(SystemColors.ControlDarkDark),
                inner_color = DrawUtil.ToCOLORREF(c),
                outer_color = DrawUtil.ToCOLORREF(SystemColors.Control)
            };
            _selectedColors.lightlight_color = DrawUtil.MergeColor(_selectedColors.border_color, _selectedColors.outer_color);
            _selectedColors.light_color = DrawUtil.MergeColor(_selectedColors.lightlight_color, _selectedColors.border_color);

            _hoverColors = new DrawUtil.RoundRectColors
            {
                border_color = DrawUtil.ToCOLORREF(DrawUtil.DarkColor(Color.Orange)),
                inner_color = DrawUtil.ToCOLORREF(SystemColors.Control),
                outer_color = DrawUtil.ToCOLORREF(SystemColors.Control)
            };
            _hoverColors.lightlight_color = DrawUtil.MergeColor(_hoverColors.border_color, _hoverColors.outer_color);
            _hoverColors.light_color = DrawUtil.MergeColor(_hoverColors.lightlight_color, _hoverColors.border_color);
        }

    }

}
