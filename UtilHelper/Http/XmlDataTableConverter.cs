using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Collections.Specialized;
using UtilHelper.HttpExtention;
using System.Collections;
namespace UtilHelper.Http
{
    /// <summary>
    /// xml 报文和DataTable之间的互转，保留Modified、Added和Deleted状态的记录，
    /// 一般应用于远程数据访问。
    /// 
    /// </summary>
    public class DataTransfer
    {

        public DataTransfer()
        {
        }
        public enum ActionType { Query, Update, Info, QueryResponse };
        /// <summary>
        /// Toes the XML.
        /// </summary>
        /// <param name="tbl">The TBL.</param>
        /// <param name="schema">The schema.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public string AllToPackage(DataTable table, string ids, ActionType type)
        {
            List<string> fields;
            string schema;
            GetSchema(table, out fields, out schema);
            PackageProperty prop = new PackageProperty();
            prop.Schema = schema;
            prop.IdFields = ids;
            prop.Action = type;
            prop.TableName = table.TableName;
            return ToPackage(table, prop);
        }
        public string QueryToPackage(DataTable table, string schema, string ids)
        {
            PackageProperty prop = new PackageProperty();
            prop.Schema = schema;
            prop.IdFields = ids;
            prop.Action = ActionType.Query;
            prop.TableName = table.TableName;
            return ToPackage(table, prop);
        }
        public string QueryResponseToPackage(DataTable table, string schema, string ids)
        {
            PackageProperty prop = new PackageProperty();
            prop.Schema = schema;
            prop.IdFields = ids;
            prop.Action = ActionType.QueryResponse;
            prop.TableName = table.TableName;
            return ToPackage(table, prop);
        }
        public string UpdateToPackage(DataTable table, string schema, string ids)
        {
            PackageProperty prop = new PackageProperty();
            prop.Schema = schema;
            prop.IdFields = ids;
            prop.Action = ActionType.Update;
            prop.TableName = table.TableName;
            return ToPackage(table, prop);
        }
        public string InfoToPackage(StringDictionary info)
        {
            DataTable table = new DataTable();
            PackageProperty prop = new PackageProperty();
            prop.Action = ActionType.Info;
            DataRow row = table.NewRow();
            //转换成DataTable
            foreach (string key in info.Keys)
            {
                DataColumn dc = new DataColumn(key);
                table.Columns.Add(dc);
                row[dc] = info[key];
            }
            
            table.Rows.Add(row);
            return AllToPackage(table, String.Empty, prop.Action);
        }
        public string ToPackage(DataTable table, PackageProperty prop)
        {
            string schema = prop.Schema;
            string id = prop.IdFields;
            ActionType action = prop.Action;
            prop.TableName = table.TableName;

            StringBuilder sz = new StringBuilder();
            XmlWriterSettings s = new XmlWriterSettings();
            s.Encoding = Encoding.Default;
            s.ConformanceLevel = ConformanceLevel.Fragment;
#if DEBUG
            s.Indent = true;
#endif
            s.CheckCharacters = false;
            using (XmlWriter writer = XmlWriter.Create(sz, s))
            {
                writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                writer.WriteStartElement("package");
                //写报文头
                if (prop.Header.Count != 0)
                {
                    writer.WriteStartElement("header");
                    foreach (DictionaryEntry entry in prop.Header)
                    {
                        writer.WriteStartElement("headeritem");
                        writer.WriteAttributeString("key", entry.Key.ToString());
                        writer.WriteCData(entry.Value.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }

                //写数据体
                writer.WriteStartElement("table");

                if (action.Equals(ActionType.Update) && String.IsNullOrEmpty(id.Trim()))
                    throw new ArgumentException("the id param can not Null");

                string[] fields = schema.Split(',');
                string[] ids = id.Split(',');

                foreach (DataRow row in table.Rows)
                {
                    //Detached 的记录不会被送往服务器
                    if (row.RowState == DataRowState.Detached ||
                        //如果向服务器更新数据，去掉状态为Unchanged的记录
                        (action == ActionType.Update && row.RowState == DataRowState.Unchanged) ||
                        //如果向服务器查询数据，去掉状态为Deleted的记录
                        (action == ActionType.Query && row.RowState == DataRowState.Deleted)
                        )
                        continue;
                    writer.WriteStartElement("row");
                    writer.WriteAttributeString("state", row.RowState.ToString());
                    //删除的行只传ID字段，新增和更新的行才传所有的字段

                    string[] tmpfs = fields;
                    if (row.RowState == DataRowState.Deleted)
                        tmpfs = ids;

                    foreach (string f in tmpfs)
                    {
                        if (table.Columns.Contains(f) && row.IsNull(f) == false)
                        {
                            writer.WriteStartElement("field");
                            writer.WriteAttributeString("key", f);
                            //writer.WriteString(r[f].ToString());
                            writer.WriteCData(row[f].ToString());
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();//end row
                }

                writer.WriteEndElement();//end table
                writer.WriteEndElement();//end package
                //writer.WriteEndDocument();
                writer.Flush();
            }
            return sz.ToString();
        }
        private void GetSchema(DataTable table, out List<string> fields, out string schema)
        {
            fields = new List<string>();
            StringBuilder sz = new StringBuilder(64);
            foreach (DataColumn dc in table.Columns)
            {
                fields.Add(dc.ColumnName);
                sz.Append(',').Append(dc.ColumnName);
            }
            if (fields.Count > 0)
            {
                //去掉前面的 , 号
                schema = sz.ToString().Substring(1);
                return;
            }
            schema = String.Empty;
        }

        /// <summary>
        /// Toes the data table.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public PackageProperty PackageToDataTable(string xml, out DataTable tbl)
        {
            tbl = new DataTable();
            PackageProperty prop = new PackageProperty();
            System.IO.StringReader sr = new System.IO.StringReader(xml);
            XmlReaderSettings s = new XmlReaderSettings();
            DataRow row = null;
            string state = string.Empty;
            s.IgnoreWhitespace = true;
            ActionType type = ActionType.Info;

            XmlReader reader = XmlReader.Create(sr, s);

            while (reader.Read())
            {
                //报文头
                if (reader.Name.Equals("header") && reader.IsStartElement())
                {
                    string x = reader.ReadOuterXml();
                    prop.LoadFromXml(x);
                }

                if (reader.Name.Equals("field") && reader.IsStartElement())
                {
                    string key = reader.GetAttribute("key");
                    if (tbl.Columns.Contains(key))
                    {
                        row[key] = reader.ReadString();
                    }
                }else if (reader.Name.Equals("row"))
                {
                    if (reader.IsStartElement() == true)
                    {
                        row = tbl.NewRow();
                        state = reader.GetAttribute("state");
                    }
                    else
                    {
                        tbl.Rows.Add(row);
                        //默认状态为Added
                        if (state.Equals(DataRowState.Modified.ToString()))
                        {
                            row.AcceptChanges();
                            row.SetModified();
                        }
                    }
                }
            }
            //更新数据操作需保持其原来的记录状态
            if (type != ActionType.Update)
                tbl.AcceptChanges();
            return prop;
        }
        /// <summary>
        /// 将服务器返回的xml报文解封成Dictionay数据结构.
        /// 一般服务器返回对请求的应答，都为单记录数据，用Dictionary方便使用
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public StringDictionary PackageToDic(string xml)
        {
            DataTable table;
            StringDictionary dic = null;
            PackageToDataTable(xml, out table);
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                dic = row.ToDictionary();
            }
            return dic;
        }
    }
}