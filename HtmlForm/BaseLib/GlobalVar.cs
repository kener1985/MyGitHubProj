using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UtilHelper.Log;
using UtilHelper.Database;
using System.Data;
using MySql.Data.MySqlClient;

namespace BaseLib
{
    public class LogInfoClass
    {
        public string Name { get; set; }
        public string Password{get;set;}
        public string WorkCode{get;set;}
        public string Role { get; set; }
    }
    public class GlobalVar
    {
        private static string _path;
        private static LogInfoClass _LogInfo;
        private static Dictionary<string, LogInfoClass> users = new Dictionary<string, LogInfoClass>();
        private static System.Windows.Forms.HtmlDocument _container = null;
        private static FormBrowser _browser = null;
        public static LogHelper<TxtLogWriter> Log = LogHelper<TxtLogWriter>.CreateLogger("");
        private static AbstractDbHelper _DBHelper = null;
        public static System.Windows.Forms.Form MainFrame { get; set; }
        public static Dictionary<string, LogInfoClass> Users  { get{return users;}}
        public static LogInfoClass LogInfo
        {
            get
            {
                if (_LogInfo == null)
                    _LogInfo = new LogInfoClass();
                return _LogInfo;
            }
            set { _LogInfo = value; }
        }
        public GlobalVar() {
        }
        public static AbstractDbHelper DBHelper
        {
            get
            {
                if (_DBHelper == null)
                {
                    MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                    builder.Server = "localhost";
                    builder.UserID = "jhuser";
                    builder.Password = "jhuser_Good";
                    builder.CharacterSet = "utf8";
                    builder.Database = "jhdb";
                    builder.Pooling = true;
                    
                    _DBHelper = new MySQLHelper(builder.ConnectionString, Log);
                    
                }
                return _DBHelper;
            }
        }
        public static string AppPath
        {
            get { return _path; }
            set
            {
                if (String.IsNullOrEmpty(_path))

                    _path = value;
            }
        }
        public static string ViewPath
        {
            get { return _path + @"views\"; }
        }
        public static string ResPath
        {
            get { return _path + @"res\"; }
        }
        public static FormBrowser MainBrowser
        {
            get { return _browser; }
            set
            {
                if (_browser == null)
                    _browser = value;
                else
                    throw new Exception("不能更改该值");
            }
        }
        public static System.Windows.Forms.HtmlDocument Container
        {
            get
            {
                return _container;
            }
            set { _container = value; }
        }

        public static void AddDateFildFromSeqnbr(DataTable tbl, bool isLong)
        {
            if (tbl.Columns.Contains("seqnbr") == false) return;
            tbl.Columns.Add("date");
            foreach (DataRow row in tbl.Rows)
            {
                long ticks = Convert.ToInt64(row["seqnbr"].ToString());
                DateTime dt = new DateTime(ticks);
                if (isLong)
                    row["date"] = dt.ToLongDateString() + " " + dt.ToLongTimeString();
                else
                    row["date"] = dt.ToLongDateString();

            }

        }
        public static void transferCodeToName(DataTable tbl,string codeField)
        {
            if (tbl == null || tbl.Columns.Contains(codeField) == false || GlobalVar.Users.Count == 0)
                return;

            tbl.Columns.Add("__name__");
            foreach (DataRow row in tbl.Rows)
            {
                string code = row.Field<string>(codeField);
                string name = code;
                if (GlobalVar.Users.ContainsKey(code))
                {
                    name = GlobalVar.Users[code].Name;
                }
                row.SetField<string>("__name__", name);
            }
        }
        static GlobalVar()
        {
            
        }

        
    }
}
