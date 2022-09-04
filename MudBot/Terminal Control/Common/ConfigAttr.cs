/*
 Copyright (c) 2005 Poderosa Project, All Rights Reserved.
 This file is a part of the Granados SSH Client Library that is subject to
 the license included in the distributed package.
 You may not use this file except in compliance with the license.

 $Id: ConfigAttr.cs,v 1.2 2005/04/20 09:06:03 okajima Exp $
*/
using System;
using System.Reflection;
using System.Text;

namespace Poderosa.Config
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public abstract class ConfigElementAttribute : Attribute
    {
        protected string _externalName;
        protected FieldInfo _fieldInfo;

        public FieldInfo FieldInfo
        {
            get => _fieldInfo;
            set
            {
                _fieldInfo = value;
                _externalName = ToExternalName(_fieldInfo.Name);
            }
        }
        public string ExternalName => _externalName;

        public static string ToExternalName(string value)
        {
            //if the field name starts with '_', strip it off
            if (value[0] == '_')
            {
                return value.Substring(1);
            }
            else
            {
                return value;
            }
        }

        public abstract void ExportTo(object holder, ConfigNode node);
        public abstract void ImportFrom(object holder, ConfigNode node);
        public abstract void Reset(object holder);
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConfigIntElementAttribute : ConfigElementAttribute
    {
        public int Initial { get; set; }

        public override void ExportTo(object holder, ConfigNode node)
        {
            int value = (int)_fieldInfo.GetValue(holder);
            if (value != Initial)
            {
                node[_externalName] = value.ToString();
            }
        }
        public override void ImportFrom(object holder, ConfigNode node)
        {
            _fieldInfo.SetValue(holder, ParseInt(node[_externalName], Initial));
        }
        public override void Reset(object holder)
        {
            _fieldInfo.SetValue(holder, Initial);
        }

        public static int ParseInt(string value, int defaultvalue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultvalue;
            }

            return int.TryParse(value, out int goodParse) 
                ? goodParse 
                : defaultvalue;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConfigBoolElementAttribute : ConfigElementAttribute
    {
        public bool Initial { get; set; }

        public override void ExportTo(object holder, ConfigNode node)
        {
            bool value = (bool)_fieldInfo.GetValue(holder);
            if (value != Initial)
            {
                node[_externalName] = value.ToString();
            }
        }
        public override void ImportFrom(object holder, ConfigNode node)
        {
            _fieldInfo.SetValue(holder, ParseBool(node[_externalName], Initial));
        }
        public override void Reset(object holder)
        {
            _fieldInfo.SetValue(holder, Initial);
        }
        public static bool ParseBool(string value, bool defaultvalue)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return defaultvalue;
                }

                return bool.Parse(value);
            }
            catch (Exception)
            {
                return defaultvalue;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConfigFloatElementAttribute : ConfigElementAttribute
    {
        public float Initial { get; set; }

        public override void ExportTo(object holder, ConfigNode node)
        {
            float value = (float)_fieldInfo.GetValue(holder);
            if (value != Initial)
            {
                node[_externalName] = value.ToString();
            }
        }
        public override void ImportFrom(object holder, ConfigNode node)
        {
            _fieldInfo.SetValue(holder, ParseFloat(node[_externalName], Initial));
        }
        public override void Reset(object holder)
        {
            _fieldInfo.SetValue(holder, Initial);
        }
        public static float ParseFloat(string value, float defaultvalue)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return defaultvalue;
                }
                else
                {
                    return Single.Parse(value);
                }
            }
            catch (Exception)
            {
                return defaultvalue;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConfigStringElementAttribute : ConfigElementAttribute
    {
        public string Initial { get; set; }

        public override void ExportTo(object holder, ConfigNode node)
        {
            string value = _fieldInfo.GetValue(holder) as string;
            if (value != Initial)
            {
                node[_externalName] = value == null ? "" : value;
            }
        }
        public override void ImportFrom(object holder, ConfigNode node)
        {
            string t = node[_externalName];
            _fieldInfo.SetValue(holder, t == null ? Initial : t);
        }
        public override void Reset(object holder)
        {
            _fieldInfo.SetValue(holder, Initial);
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConfigStringArrayElementAttribute : ConfigElementAttribute
    {
        public string[] Initial { get; set; }

        public override void ExportTo(object holder, ConfigNode node)
        {
            string[] t = (string[])_fieldInfo.GetValue(holder);
            StringBuilder bld = new StringBuilder();
            foreach (string a in t)
            {
                if (bld.Length > 0)
                {
                    bld.Append(',');
                }

                bld.Append(a);
            }
            node[_externalName] = bld.ToString();
        }
        public override void ImportFrom(object holder, ConfigNode node)
        {
            string t = node[_externalName];
            _fieldInfo.SetValue(holder, t == null ? Initial : t.Split(','));
        }
        public override void Reset(object holder)
        {
            _fieldInfo.SetValue(holder, Initial);
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConfigEnumElementAttribute : ConfigElementAttribute
    {
        private ValueType _initial;

        public ConfigEnumElementAttribute(Type t)
        {
            Type = t;
        }

        public int InitialAsInt
        {
            get => (int)_initial;
            set => _initial = value;
        }
        public Type Type { get; }

        public override void ExportTo(object holder, ConfigNode node)
        {
            ValueType value = (ValueType)_fieldInfo.GetValue(holder);
            if (value != _initial)
            {
                node[_externalName] = value.ToString();
            }
        }
        public override void ImportFrom(object holder, ConfigNode node)
        {
            object v = ParseEnum(Type, node[_externalName], _initial);
            _fieldInfo.SetValue(holder, v);
        }
        public override void Reset(object holder)
        {
            _fieldInfo.SetValue(holder, Enum.ToObject(Type, (int)_initial));
        }

        public static ValueType ParseEnum(Type enumtype, string t, ValueType defaultvalue)
        {
            try
            {
                if (string.IsNullOrEmpty(t))
                {
                    return (ValueType)Enum.ToObject(enumtype, (int)defaultvalue);
                }
                else
                {
                    return (ValueType)Enum.Parse(enumtype, t, false);
                }
            }
            catch (FormatException)
            {
                return (ValueType)Enum.ToObject(enumtype, (int)defaultvalue);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConfigFlagElementAttribute : ConfigElementAttribute
    {
        public ConfigFlagElementAttribute(Type t)
        {
            Type = t;
        }

        public int Initial { get; set; }

        public int Max { get; set; }

        public Type Type { get; }

        public override void ExportTo(object holder, ConfigNode node)
        {
            int value = (int)_fieldInfo.GetValue(holder);
            StringBuilder bld = new StringBuilder();
            for (int i = 1; i <= Max; i <<= 1)
            {
                if ((i & value) != 0)
                {
                    if (bld.Length > 0)
                    {
                        bld.Append(',');
                    }

                    bld.Append(Enum.GetName(Type, i));
                }
            }
            node[_externalName] = bld.ToString();
        }
        public override void ImportFrom(object holder, ConfigNode node)
        {
            string value = node[_externalName];
            if (value == null)
            {
                _fieldInfo.SetValue(holder, Enum.ToObject(Type, Initial));
            }
            else
            {
                int r = 0;
                foreach (string t in value.Split(','))
                {
                    r |= (int)Enum.Parse(Type, t, false);
                }

                _fieldInfo.SetValue(holder, Enum.ToObject(Type, r));
            }
        }
        public override void Reset(object holder)
        {
            _fieldInfo.SetValue(holder, Enum.ToObject(Type, Initial));
        }
    }


}
