/*
 Copyright (c) 2005 Poderosa Project, All Rights Reserved.

 $Id: ConfigColorAttr.cs,v 1.2 2005/04/20 09:06:04 okajima Exp $
*/
using System;
using System.Diagnostics;
using System.Drawing;

namespace Poderosa.Config
{
    //Colorを直接Attributeの定義に使うことはできない！
    public enum LateBindColors
    {
        Empty,
        Window,
        WindowText
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConfigColorElementAttribute : ConfigElementAttribute
    {
        public LateBindColors Initial { get; set; }

        private static Color ToColor(LateBindColors value)
        {
            switch (value)
            {
                case LateBindColors.Empty:
                    return Color.Empty;
                case LateBindColors.Window:
                    return SystemColors.Window;
                case LateBindColors.WindowText:
                    return SystemColors.WindowText;
            }
            Debug.Assert(false, "should not reach here");
            return Color.Empty;
        }

        public override void ExportTo(object holder, ConfigNode node)
        {
            Color value = (Color)_fieldInfo.GetValue(holder);
            if (value != ToColor(Initial))
            {
                node[_externalName] = value.Name;
            }
        }
        public override void ImportFrom(object holder, ConfigNode node)
        {
            _fieldInfo.SetValue(holder, GUtil.ParseColor(node[_externalName], ToColor(Initial)));
        }
        public override void Reset(object holder)
        {
            _fieldInfo.SetValue(holder, ToColor(Initial));
        }
    }
}
