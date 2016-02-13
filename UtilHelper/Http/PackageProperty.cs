using System;
using System.Collections.Generic;

namespace UtilHelper.Http
{
    public sealed class PackageProperty
    {
        private System.Collections.Specialized.StringDictionary _Header;
        public PackageProperty()
        {
            _Header = new System.Collections.Specialized.StringDictionary();
        }
        public string TableName
        {
            get{return GetHeadItem("tablename");}
            set { SetHeadItem("tablename", value);}
        }
        public string IdFields
        {
            get { return GetHeadItem("idfields"); }
            set { SetHeadItem("idfields", value); }
        }
        public DataTransfer.ActionType Action
        {
            get {

                DataTransfer.ActionType type = DataTransfer.ActionType.Info;
                switch (GetHeadItem("action"))
                {
                    case "Info":
                        type = DataTransfer.ActionType.Info;
                	break;
                    case "Update":
                    type = DataTransfer.ActionType.Update;
                    break;
                    case "Query":
                    type = DataTransfer.ActionType.Query;
                    break;
                    case "QueryResponse":
                    type = DataTransfer.ActionType.QueryResponse;
                    break;
                }
                return type;
            }
            set { SetHeadItem("action",value.ToString()); }
        }
        public string Schema
        {
            get { return GetHeadItem("schema"); }
            set { SetHeadItem("schema", value); }
        }
        public string GetHeadItem(string key)
        {
            if(_Header.ContainsKey(key))
                return _Header[key];
            return String.Empty;
        }
        public void SetHeadItem(string key,string value)
        {
            _Header[key] = value;
        }
        public System.Collections.Specialized.StringDictionary Header
        {
            get { return _Header; }
        }
        public void LoadFromXml(string  xml)
        {
            System.Xml.XmlReaderSettings s = new System.Xml.XmlReaderSettings();
            s.IgnoreWhitespace = true;
            using (System.IO.StringReader sr = new System.IO.StringReader(xml))
            {
                using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(sr, s))
                {
                    while (reader.Read())
                    {
                        if (reader.Name.Equals("headeritem") && reader.IsStartElement())
                        {
                            string key = reader.GetAttribute("key");
                            string value = reader.ReadString();
                            _Header.Add(key, value);
                        }
                    }
                }
            }
        }
    }

}
