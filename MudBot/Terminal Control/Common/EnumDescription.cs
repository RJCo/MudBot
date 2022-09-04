/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: EnumDescription.cs,v 1.2 2005/04/20 08:45:46 okajima Exp $
*/
using System;
using System.Collections;
using System.Reflection;

namespace Poderosa.Toolkit
{

    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumDescAttribute : Attribute
    {
        private string[] _descriptions;
        private Hashtable _descToValue;
        private string[] _names;
        private Hashtable _nameToValue;

        public EnumDescAttribute(Type t)
        {
            Init(t);
        }

        public void Init(Type t)
        {
            MemberInfo[] ms = t.GetMembers();
            _descToValue = new Hashtable(ms.Length);
            _nameToValue = new Hashtable(ms.Length);

            ArrayList descriptions = new ArrayList(ms.Length);
            ArrayList names = new ArrayList(ms.Length);

            int expected = 0;
            foreach (MemberInfo mi in ms)
            {
                FieldInfo fi = mi as FieldInfo;
                if (fi != null && fi.IsStatic && fi.IsPublic)
                {
                    int intVal = (int)fi.GetValue(null);
                    if (intVal != expected)
                    {
                        throw new Exception("unexpected enum value order");
                    }

                    EnumValueAttribute a = (EnumValueAttribute)(fi.GetCustomAttributes(typeof(EnumValueAttribute), false)[0]);

                    string desc = a.Description;
                    descriptions.Add(desc);
                    _descToValue[desc] = intVal;

                    string name = fi.Name;
                    names.Add(name);
                    _nameToValue[name] = intVal;

                    expected++;
                }
            }

            _descriptions = (string[])descriptions.ToArray(typeof(string));
            _names = (string[])names.ToArray(typeof(string));
        }

        public virtual string GetDescription(ValueType i)
        {
            return LoadString(_descriptions[(int)i]);
        }

        public virtual ValueType FromDescription(string v, ValueType d)
        {
            if (v == null)
            {
                return d;
            }

            IDictionaryEnumerator ie = _descToValue.GetEnumerator();
            while (ie.MoveNext())
            {
                if (v == LoadString((string)ie.Key))
                {
                    return (ValueType)ie.Value;
                }
            }
            return d;
        }
        public virtual string GetName(ValueType i)
        {
            return _names[(int)i];
        }
        public virtual ValueType FromName(string v)
        {
            return (ValueType)_nameToValue[v];
        }
        public virtual ValueType FromName(string v, ValueType d)
        {
            if (v == null)
            {
                return d;
            }

            var t = (ValueType)_nameToValue[v];
            return t ?? d;
        }

        public virtual string[] DescriptionCollection()
        {
            string[] r = new string[_descriptions.Length];
            for (int i = 0; i < r.Length; i++)
            {
                r[i] = LoadString(_descriptions[i]);
            }

            return r;
        }

        private static string LoadString(string id)
        {
            return id;
        }

        private static readonly Hashtable TypeToAttr = new Hashtable();
        public static EnumDescAttribute For(Type type)
        {
            if (!(TypeToAttr[type] is EnumDescAttribute a))
            {
                a = (EnumDescAttribute)(type.GetCustomAttributes(typeof(EnumDescAttribute), false)[0]);
                TypeToAttr.Add(type, a);
            }
            return a;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class EnumValueAttribute : Attribute
    {
        public string Description { get; set; }
    }

}