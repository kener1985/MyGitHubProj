using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BaseLib
{
    [System.Runtime.Serialization.DataContract]
    public class EasyUITable
    {
        private System.Data.DataTable _Table;
        public EasyUITable()
        {
            _Table = null;
        }
        [System.Runtime.Serialization.DataMember(Name = "rows")]
        public System.Data.DataTable Table
        {
            get
            {
                if (_Table == null)
                    _Table = new System.Data.DataTable();

                return _Table;
            }
            set
            {
                _Table = value;
            }
        
        }
        [System.Runtime.Serialization.DataMember(Name = "total")]
        public int Total
        {
            get
            {
                if (Table == null)
                    return 0;
                return Table.Rows.Count;
            }
        }
        public bool Parse(string json)
        {
            EasyUITable temp = JsonConvert.DeserializeObject<EasyUITable>(json);
            this._Table = temp.Table;

            return (this._Table != null);
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class EasyUIDataSet : Dictionary<string,EasyUITable>
    {
        public bool Parse(string json)
        {
            
            EasyUIDataSet temp = JsonConvert.DeserializeObject<EasyUIDataSet>(json);
            foreach (KeyValuePair<string,EasyUITable> p in temp)
            {
                p.Value.Table.TableName = p.Key;
                this.Add(p.Key, p.Value);
            }
            return this.Count == 0;
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
