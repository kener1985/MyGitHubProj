using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BaseLib;
using System.Data;
using UtilHelper.Http;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace HtmlForm
{
    class SyncDataHandler
    {
        public static bool SyncData(System.Windows.Forms.Form form)
        {
            MainForm frm = form as MainForm;
            //string msg = String.Empty;
            try
            {
                StrDictionary cfg = ReadConfig();
                string handler = cfg["server"];
                string db = cfg["dbname"];
                cfg.Remove("server");
                cfg.Remove("dbname");
                string errtbl = String.Empty;
                foreach (KeyValuePair<string, string> kvp in cfg)
                {
                    GlobalVar.DBHelper.AddSelectWithLimit(kvp.Key, kvp.Value, "syncflag!='0'");
                    DataTable tbl = new DataTable(kvp.Key);
                    //tbl.Init("products","id,name");

                    GlobalVar.DBHelper.Fill(ref tbl);
                    if (tbl.Rows.Count == 0)
                        continue;

                    EasyUITable etb = new EasyUITable();
                    etb.Table = tbl;

                    HttpComm comm = new HttpComm(handler);

                    string json = etb.ToJson();
                    //string es = System.Web.HttpUtility.UrlEncode(json);
                    Dictionary<string, string> ps = new Dictionary<string, string>();
                    ps["data"] = json;
                    ps["table"] = kvp.Key;
                    ps["fields"] = kvp.Value;
                    string s = comm.PostRequest(ps);

                    if(String.IsNullOrEmpty(s))
                    {
                        //frm.Invoke(new MainForm.SyncResultHandler(frm.ShowSyncResult), "同步数据失败，请检查日志");
                        GlobalVar.Log.LogError("网络异常", "SyncHandler#1");
                        return false;
                    }
                    if (s[0] == '0')
                    {
                        errtbl += kvp.Key + ",";
                        GlobalVar.Log.LogError(s);
                    }
                    else
                        GlobalVar.DBHelper.ExcuteSQL("update " + kvp.Key + " set syncflag='0' where syncflag!='0'");
                }

                
                    if (String.IsNullOrEmpty(errtbl) == false)
                    {
                        GlobalVar.Log.LogError("Tables:" + errtbl,"SyncErrorTable");
                        return false;
                    }
            }
            catch (System.Exception ex)
            {
                GlobalVar.Log.LogError(ex.Message, "SyncHandler#2");
                return false;
            }

            return true;
            //frm.Invoke(new MainForm.SyncResultHandler(frm.ShowSyncResult), msg);

        }
        static StrDictionary ReadConfig()
        {
            XmlReaderSettings s = new XmlReaderSettings();
            s.CloseInput = true;
            s.IgnoreComments = true;
            s.IgnoreWhitespace = true;
            
            XmlReader r = XmlReader.Create("config.xml", s);
            StrDictionary config = new StrDictionary();

            try
            {
                while (r.Read())
                {
                    if (r.NodeType == XmlNodeType.Element)
                    {
                        if (r.Name == "config")
                        {
                            if (r.HasAttributes)
                            {
                                config.Add("server", r.GetAttribute("server"));
                                config.Add("dbname", r.GetAttribute("dbname"));
                            }

                        }
                        else if (r.Name == "table")
                        {
                            
                            if (r.HasAttributes)
                            {
                                config.Add(r.GetAttribute("name"), r.ReadString());
                                //r.MoveToElement();
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.Message);
                GlobalVar.Log.LogError(ex.Message, "SyncThread-ReadConfig");
            }
            finally
            {
                if (r != null)
                    r.Close();
            }
            return config;
        }

    }
}
