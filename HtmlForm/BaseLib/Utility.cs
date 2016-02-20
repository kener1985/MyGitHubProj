using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;
using System.Windows.Forms;

namespace BaseLib
{
    public class Utility
    {
        public static void ParseQuery(string query, StrDictionary sd)
        {
            string[] pairs = query.Split(new char[] { '&' });
            foreach (string p in pairs)
            {
                string[] kv = p.Split(new char[] { '=' });
                if (kv.Length < 2)
                    continue;

                sd.Add(kv[0], System.Web.HttpUtility.UrlDecode(kv[1]));
            }
        }
        public static string ReadFile(string file)
        {
            if (System.IO.File.Exists(file) == false)
                return String.Empty;

            return System.IO.File.ReadAllText(file, Encoding.UTF8);
        }

        public static string GetQueryStr(string src)
        {
            int i = src.IndexOf('?');
            if (i != -1)
                return src.Substring(i + 1);
            else
                return String.Empty;
        }

        public static void InvokeAction(string action, string cmd, StrDictionary sd)
        {
            string module;

            bool bHint = false;
            if (String.IsNullOrEmpty(action.Trim()) == true)
            {
                action = "Default";
            }
            module = action.Replace('.', '\\') + "Action";

            AssemblyName an = new AssemblyName();
            an.CodeBase = "file:///" + GlobalVar.AppPath + "actions\\" + module + ".dll";
            try
            {
                Assembly ass = Assembly.Load(an);
                Type[] types = ass.GetExportedTypes();

                foreach (Type t in types)
                {
                    Type intf = t.GetInterface("BaseLib.IAction");

                    if (intf != null)
                    {
                        BaseLib.IAction act = ass.CreateInstance(t.FullName) as BaseLib.IAction;
                        if (act != null)
                        {
                            if (act.IsMe(cmd) == true)
                            {
                                try
                                {
                                    act.DoAction(sd);
                                }
                                catch (Exception ex)
                                {
                                    string msg = "Action:" + module + "执行" + action + "时发生内部错误\r\n";
                                    msg += ex.Message;
                                    throw new Exception(msg);
                                }

                                bHint = true;
                                break;
                            }
                        }
                    }
                }
                if (bHint == false)
                    GlobalVar.Log.LogWarm("Action未命中", "在" + module + "中没有找到" + action + "对应的Action:" + cmd);
            }
            catch (System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                System.Windows.Forms.MessageBox.Show("Action : [" + module + "] 发生错误");
                GlobalVar.Log.LogError("加载Action失败！", ex.Message);
            }
        }
        public static bool ProcessSqlError(System.Data.DataSet ds)
        {
            string except = "违反并发性: UpdateCommand 影响了预期 1 条记录中的 0 条。";
            foreach (System.Data.DataTable t in ds.Tables)
            {
                foreach (System.Data.DataRow r in t.Rows)
                {
                    if (r.HasErrors && r.RowError != except)
                        return false;
                }
            }
            return true;
        }

        public static bool ProcessSqlError(System.Data.DataTable table)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            if (table.DataSet == null)
                ds.Tables.Add(table);
            else
                ds = table.DataSet;
            return ProcessSqlError(ds);
        }

        public static DateTime MakeDate(string date, bool min)
        {

            int y = Convert.ToInt32(date.Substring(0, 4));
            int m = Convert.ToInt32(date.Substring(4, 2));
            int d = Convert.ToInt32(date.Substring(6, 2));
            DateTime dt;
            if (min)
                dt = new DateTime(y, m, d, 0, 0, 0);
            else
                dt = new DateTime(y, m, d, 23, 59, 59);

            return dt;
        }

        public static LogInfoClass getUserInfoByName(string name)
        {
            Dictionary<string, LogInfoClass> users = GlobalVar.Users;
            foreach (KeyValuePair<string, LogInfoClass> pair in users)
            {
                if (pair.Value.Name.Equals(name))
                    return pair.Value;
            }
            return null;
        }
    }
}
