/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: GStatusBar.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.Drawing;
using System.Windows.Forms;
//using System.Timers;


namespace Poderosa.Forms
{
    /// <summary>
    /// ステータスバー
    /// フレームのデザインモード表示のかねあいで、StatusBarからの派生ではなくラッパになっている。
    /// </summary>
    internal class GStatusBar
    {
        private StatusBar _statusBar;
        private Timer _belltimer;
        private Timer _statusBarTextTimer;

        private Icon _bell;
        private Icon _empty;

        public GStatusBar(StatusBar sb)
        {
            _statusBar = sb;
            _bell = GIcons.GetBellIcon();
            _empty = null;

            BellPanel.Icon = _empty;

            //Windows.Forms.TimerはStart/Stopを別のスレッドからいじるとだめになってしまうようだ
            _belltimer = new Timer
            {
                Interval = 500
            };
            //_belltimer.AutoReset = false;
            _belltimer.Tick += CancelBellIcon;
            //_belltimer.Elapsed += new ElapsedEventHandler(CancelBellIcon);
        }

        private StatusBarPanel MessagePanel => _statusBar.Panels[0];

        private StatusBarPanel BellPanel => _statusBar.Panels[1];

        private StatusBarPanel CaretPanel => _statusBar.Panels[2];

        public void SetStatusBarText(string text)
        {
            MessagePanel.Text = text;
        }
        public void ClearStatusBarText()
        {
            MessagePanel.Text = "";
            if (_statusBarTextTimer != null)
            {
                _statusBarTextTimer.Stop();
            }
        }
        private void SetStatusBarTextTimer()
        {
            if (_statusBarTextTimer == null)
            {
                _statusBarTextTimer = new Timer
                {
                    Interval = 10000
                };
                _statusBarTextTimer.Tick += ClearStatusBarTextHandler;
            }
            _statusBarTextTimer.Start();
        }
        private void ClearStatusBarTextHandler(object sender, EventArgs args)
        {
            ClearStatusBarText();
        }

        public void IndicateFreeSelectionMode()
        {
            CaretPanel.Text = "Caption.GStatusBar.FreeSelection";
        }
        public void IndicateAutoSelectionMode()
        {
            CaretPanel.Text = "Caption.GStatusBar.AutoSelection";
        }
        public void ClearSelectionMode()
        {
            CaretPanel.Text = "";
        }

        //ベルアイコンを点灯する
        public void IndicateBell()
        {
            if (_belltimer.Enabled)
            {
                _belltimer.Stop();
            }
            else
            {
                BellPanel.Icon = _bell;
            }

            _belltimer.Start();
        }

        private void CancelBellIcon(object sender, EventArgs args)
        {
            BellPanel.Icon = _empty;
            _belltimer.Stop();
        }
    }
}
