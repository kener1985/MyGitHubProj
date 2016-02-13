using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace BaseLib
{

    //[System.Runtime.Serialization.DataContract]
    //public class Package 
    //{

    //    [System.Runtime.Serialization.DataMember(Name = "data")]
    //    public Ini ini { get; set; }
    //    public Package()
    //    {
    //        ini = new Ini();
    //    }
    //}
    public class StrDictionary : Dictionary<string,string>
    {
        public new string this[string key]
        {
            get{
                if (base.ContainsKey(key))
                    return base[key];
                return String.Empty;}
            set{base[key] = value;}
        }
    }
    [System.Runtime.Serialization.DataContract]
    public class SectionItem
    {
        public SectionItem()
        {
            rows = new List<StrDictionary>();
        }
        [System.Runtime.Serialization.DataMember(Name = "rows")]
        public List<StrDictionary> rows{get;set;}
        [System.Runtime.Serialization.DataMember(Name = "total")]
        public int total { get { return rows.Count; } }
    }
    [System.Runtime.Serialization.DataContract]
    public class Package : Dictionary<string, SectionItem>
    {
        public override string ToString()
        {
            StringBuilder sz = new StringBuilder(256);
            foreach (KeyValuePair<string,SectionItem> p in this)
            {
                sz.Append('[').Append(p.Key).AppendLine("]");
                SectionItem item = p.Value;
                foreach (StrDictionary sd in item.rows)
                {
                     foreach (KeyValuePair<string,string> p1 in sd)
                     {
                         sz.Append(p1.Key).Append('=').Append(p1.Value).Append("; ");
                     }
                     sz.AppendLine();
                }
            }
            return sz.ToString();
        }
    }
}
