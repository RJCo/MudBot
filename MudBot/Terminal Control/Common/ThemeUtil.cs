/*
 Copyright (c) 2005 Poderosa Project, All Rights Reserved.
 This file is a part of the Granados SSH Client Library that is subject to
 the license included in the distributed package.
 You may not use this file except in compliance with the license.

 $Id: ThemeUtil.cs,v 1.2 2005/04/20 09:06:03 okajima Exp $
*/
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Poderosa.UI
{
    //Look & Feelを変更するための仕組み
    public class ThemeUtil
    {
        public enum Theme
        {
            Unspecified,
            Luna
        }

        public static Theme CurrentTheme { get; private set; }

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int GetCurrentThemeName(char[] filename, int filenamelen, char[] colorbuff, int colornamelen, char[] sizebuff, int sizebufflen);

        private static void SpecifyThemeUnderWinXP()
        {
            try
            {
                char[] fn = new char[256];
                char[] cb = new char[256];
                char[] sz = new char[256];
                int r = GetCurrentThemeName(fn, 256, cb, 256, sz, 256);
                if (r == 0)
                {
                    string theme_name = new string(fn);
                    if (theme_name.IndexOf("Luna") != -1)
                    {
                        CurrentTheme = Theme.Luna;
                    }
                }
                //Debug.WriteLine(String.Format("FN={0} Color={1} Size={2}", new string(fn), new string(cb), new string(sz)));
            }
            catch (Exception)
            {
            }
        }

        public static void Init()
        {
            Application.EnableVisualStyles();
            CurrentTheme = Theme.Unspecified;
            OperatingSystem os = Environment.OSVersion;
            if (os.Platform == PlatformID.Win32NT && os.Version.CompareTo(new Version(5, 1)) >= 0)
            {
                SpecifyThemeUnderWinXP();
            }
        }

        public static Color TabPaneBackColor
        {
            get
            {
                if (CurrentTheme == Theme.Luna)
                {
                    return Color.FromKnownColor(KnownColor.ControlLightLight);
                }
                else
                {
                    return Color.FromKnownColor(KnownColor.Control);
                }
            }
        }
    }
}
