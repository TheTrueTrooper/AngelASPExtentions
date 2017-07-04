﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelASPExtentions.AngelCSSToInlineEng
{
    enum CSSBasicType
    {
        /// <summary>
        /// This is for classes
        /// </summary>
        Class,
        /// <summary>
        /// This is just for ID's
        /// </summary>
        DomID,
        /// <summary>
        /// This Is to modify all selectors
        /// </summary>
        HTMLSelector,
        /// <summary>
        /// Custom Type extentions maybe?
        /// </summary>
        Other
    }

    class CSSObjectData : ICloneable
    {
        public string ObjectName;
        public CSSBasicType Type;
        public Dictionary<string, string> CSSDataTags = new Dictionary<string, string>();

        string this[string Index]
        {
            get
            {
                if (CSSDataTags.ContainsKey(Index))
                    return CSSDataTags[Index];
                return null;
            }
            set
            {
                if (CSSDataTags.ContainsKey(Index))
                    CSSDataTags[Index] = value;
                else
                    CSSDataTags.Add(Index, value);
            }
        }

        public static CSSObjectData operator +(CSSObjectData CSS1, CSSObjectData CSS2)
        {
            CSSObjectData Return = new CSSObjectData() { ObjectName = CSS1.ObjectName + "," + CSS2.ObjectName, Type = CSSBasicType.Other , CSSDataTags = CSS1.CSSDataTags.ToDictionary(x => x.Key, x => x.Value) };
            foreach (KeyValuePair<string, string> Pair in CSS2.CSSDataTags)
            {
                if (Return.CSSDataTags.ContainsKey(Pair.Key))
                    Return.CSSDataTags[Pair.Key] = Pair.Value;
                else
                    Return.CSSDataTags.Add(Pair.Key, Pair.Value);
            }
            return Return;
        }

        public override string ToString()
        {
            string Tags = " style=\"";

            foreach (KeyValuePair<string, string> Pair in  CSSDataTags)
            {
                Tags += Pair.Key + ":" + Pair.Value + ";";
            }
            Tags += "\"";
            return Tags;
        }

        public object Clone()
        {
            return new CSSObjectData { Type = this.Type, ObjectName = this.ObjectName, CSSDataTags = this.CSSDataTags.ToDictionary(x => x.Key, x => x.Value) };
        }
    }
}
