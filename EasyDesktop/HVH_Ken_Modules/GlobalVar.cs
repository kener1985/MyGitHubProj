using System;
using System.Threading;
using UtilHelper.Log;
using UtilHelper.Converter;
using System.Data;

namespace HVH_Ken_Modules
{
    internal struct ErrorInfo
    {
        public string Cmd;
        public string Info;
        public ErrorInfo(string cmd, string info)
        {
            this.Cmd = cmd;
            this.Info = info;
        }
    };
    public class GlobalVar
    {
        [Ignore]
        private LogHelper<SynTextLog> _Loger;
        public enum ActionType { Add, Modify };
        private static GlobalVar g_GlobalVar = null;
        private static System.Data.SQLite.SQLiteConnection _con;
        private static  UtilHelper.Database.SQLiteHelper _help;
        public static UtilHelper.Tip.MB Tip = new UtilHelper.Tip.MB("助手提醒您");
        [Ignore]
        private Thread TaskThread;
        //错误信息
        private static System.Collections.Generic.Stack<ErrorInfo> g_ErrInfos;
        public static System.Data.SQLite.SQLiteConnection Connection
        {
            get { return _con; }
        }
        public static  UtilHelper.Database.SQLiteHelper Helper
        {
            get { return _help; }
        }
        public static GlobalVar Instanse
        {
            get
            {
                if (g_GlobalVar == null)
                {
                    _con = new System.Data.SQLite.SQLiteConnection(@"data source=info.db3");
                    _con.SetPassword(strSecurityKey);
                    _con.Open();
                    _help = new UtilHelper.Database.SQLiteHelper(_con);
                    SecurityOpr so = new SecurityOpr(strSecurityKey);
                    string sXml = so.ReadFromFile(PROGRAM_ROOT_PATH + CfgFilNam);
                    if (sXml != String.Empty)
                    {
                        int iPos = sXml.LastIndexOf('>');
                        if (iPos != -1)
                            sXml = sXml.Substring(0, iPos + 1);
                        XmlObjConverter x2o = new XmlObjConverter("Commander");
                        System.Collections.ArrayList list = x2o.Xml2Objects(sXml).Result;
                        if (list.Count != 0)
                        {
                            g_GlobalVar = list[0] as GlobalVar;
                        }
                    }
                    else
                    {
                        g_GlobalVar = new GlobalVar();
                    }
                }
                g_GlobalVar.SrcEngInfo = new System.Collections.Specialized.OrderedDictionary();
                return g_GlobalVar;
            }
            set { g_GlobalVar = value; }
        }

        static public bool PopErrorInfos(out string sErrInfos, out string[] cmds)
        {
            bool hasErr = false;
            int idx = 0;
            lock (g_ErrInfos)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                cmds = new string[g_ErrInfos.Count];
                while (g_ErrInfos.Count > 0)
                {
                    ErrorInfo info = g_ErrInfos.Pop();
                    sb.Append("[ ").Append(info.Cmd).Append(" ] : ").Append(info.Info).Append("\r\n");
                    cmds[idx++] = info.Cmd;
                    hasErr = true;
                }
                sErrInfos = sb.ToString();
                return hasErr;
            }

        }
        static internal void PushErrorInfo(ErrorInfo singleInfo)
        {
            lock (g_ErrInfos)
            {
                g_ErrInfos.Push(singleInfo);
            }
        }
        
        /// <summary>
        /// 程序配置数据
        /// </summary>
        [Ignore]
        private System.Collections.Generic.Dictionary<string, ConfigData> dataBuffer;
        /// <summary>
        /// 搜索引擎信息
        /// </summary>
        [AsSerialize]
        private System.Collections.Specialized.OrderedDictionary srcEngInfo;
        [Ignore]
        private System.Collections.Generic.List<ITrigerable> _Trigers;

        //热键设置
        public uint HOT_KEY_MODIFIER;
        //public uint HOT_KEY_CHANGE;
        public uint HOT_KEY_ACTIVE;
        public uint HOT_KEY_SHOWINFO;
        //public System.Windows.Forms.Form MainForm;
        //全局参数

        public static string PROGRAM_ROOT_PATH;
        private static string strSecurityKey; //密钥
        private static string strCfgFilNam;//配置数据文件名
        //private static string strDatFilNam;//程序配置数据文件名

        static GlobalVar()
        {
            g_ErrInfos = new System.Collections.Generic.Stack<ErrorInfo>();
            PROGRAM_ROOT_PATH = System.Windows.Forms.Application.StartupPath;
            strCfgFilNam = @"\config.db";
            
            //strDatFilNam = @"\datas.db";
            //将密钥定入代码中是不安全的，但明文中没有第三信息，只为忽悠
            strSecurityKey = "kener";
        }
        [Ignore]
        private bool bNeedToSaveCfg;//配置是否要保存
        [Ignore]
        private bool bNeedToSaveData;//配置是否要保存
        [Ignore]
        private bool bNeedToSaveNotice;//通知是否要保存
        [Ignore]
        private bool bIsSetFrmShown;//SettingFrm显示状态,防止多次点击生成多个设置窗口
        private bool bStopNotice;//是否停止通知

        //[Ignore]
        //public bool bLoaded;//是否已加载

        public string KEY_WORD_FLAG;//搜索关键字标志
        public string DEFAULT_SEARCH_ENGINE_URL;//搜索引擎标志

        //窗口初始位置
        private int INT_MAIN_FORM_TOP;
        private int INT_MAIN_FORM_LEFT;
        //皮肤
        private string sSkinFile;
        [Ignore]
        private System.Drawing.Color clStyleColor;
        //最高显示
        private bool bTopMost;

        //主界面访问器
        [Ignore]
        private Visitable _MainVisitor;
        public Visitable MainVisitor
        {
            get { return _MainVisitor; }
            set { _MainVisitor = value; }
        }
        public bool StopNotice
        {
            get { return bStopNotice; }
            set{bStopNotice = value;}
        }
        public string SkinFile
        {
            get { return sSkinFile; }
            set{sSkinFile = value;}
        }
        //消息池
        public MessagePool MsgPool
        {
            get { return MessagePool.GetInstanse(); }
        }
        public System.Collections.Generic.List<ITrigerable> Trigers
        {
            get
            {
                if (_Trigers == null)
                    _Trigers = new System.Collections.Generic.List<ITrigerable>();
                return _Trigers;
            }
            set { _Trigers = value; }
        }
        public bool IsSetFrmShown
        {
            get { return bIsSetFrmShown; }
            set { bIsSetFrmShown = value; }
        }
        public bool NeedToSaveCfg
        {
            get { return bNeedToSaveCfg; }
            set { bNeedToSaveCfg = value; }
        }

        public bool NeedToSaveData
        {
            get { return bNeedToSaveData; }
            set { bNeedToSaveData = value; }
        }
        public bool NeedToSaveNotice
        {
            get { return bNeedToSaveNotice; }
            set { bNeedToSaveNotice = value; }
        }
        //public System.Collections.Generic.Dictionary<string, ConfigData> DataBuffer
        //{
        //    get
        //    {
        //        if (dataBuffer == null)
        //            LoadData();
        //        return dataBuffer;
        //    }

        //}
        public System.Collections.Specialized.OrderedDictionary SrcEngInfo
        {
            get { return srcEngInfo; }
            set { srcEngInfo = value; }
        }
       
        public static string CfgFilNam
        {
            get { return strCfgFilNam; }
            //set { infoFrm = value; }
        }
        public int Top
        {
            get { return INT_MAIN_FORM_TOP; }
            set { INT_MAIN_FORM_TOP = value; }
        }
        public int Left
        {
            get { return INT_MAIN_FORM_LEFT; }
            set { INT_MAIN_FORM_LEFT = value; }
        }

        public System.Drawing.Color StyleColor
        {
            get { return clStyleColor; }
            set { clStyleColor = value; }
        }
        public string SecurityKey
        {
            get { return strSecurityKey; }
        }
        public bool TopMost
        {
            set { bTopMost = value; }
            get { return bTopMost; }
        }
        
        public bool Init()
        {
            try
            {
                //初始化程序配置数据
                bNeedToSaveCfg = false;
                bIsSetFrmShown = false;
                //sSkinFile = @"skin\PageColor2.ssk";
                //clStyleColor = System.Drawing.SystemColors.Window;

                //初始化通知
                DataSet ds = new DataSet();
                Helper.AddSelect("taskitem", "shortcut,condition,type,fid");
                Helper.AddSelect("notices", "id,info,dur_times,times,condition,is_temp");

                Helper.Fill(ds, "taskitem,notices");

                foreach (DataRow row in ds.Tables["taskitem"].Rows)
                {
                    TaskItem item = new TaskItem();
                    Helper.Row2DbObj(row, item);
                    Trigers.Add(item);
                }
                foreach (DataRow row in ds.Tables["notices"].Rows)
                {
                    Notice item = new Notice();
                    Helper.Row2DbObj(row, item);
                    Trigers.Add(item);
                }

                if (Trigers.Count > 0)
                {
                    TaskThread = new Thread(TaskCallback);
                    //TaskThread.Priority = ThreadPriority.Highest;
                    TaskThread.Start();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
       /************************************************************************/
       /* return null : 命令不存在; Count==0,用户没有选择
       /************************************************************************/
        public System.Collections.Generic.List<ConfigData> FindCommand(string shortcut)
        {
            
            GlobalVar.Helper.AddSelect("programs", "id,shortcut,path,title,is_auto_run,hints","shortcut");
            GlobalVar.Helper.AddCustomParam("shortcut", shortcut);
            DataTable table = new DataTable("programs");
            System.Collections.Generic.List<ConfigData> list = new System.Collections.Generic.List<ConfigData>();
            GlobalVar.Helper.Fill(ref table);
            if (table.Rows.Count == 0)
                return null;
            
            foreach (DataRow row in table.Rows)
            {
                ConfigData cd = new ConfigData();
                cd.Row = row;
                GlobalVar.Helper.Row2DbObj(row, cd);
                list.Add(cd);                
            }
            if(list.Count > 1)
            {
                SelectFrm frm = new SelectFrm(list);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    frm.UpdateSelectedHint();
                }
                else
                {
                    list.Clear();
                    return list;
                }
                
                //进行选择
            }
            else
            {
                DataRow row = list[0].Row;
                int hint = row.Field<int>("hints");
                row.SetField<int>("hints", ++hint);
            }
            Helper.AddUpdate("programs", "hints", "id");
            Helper.Update(table);
            return list;
        }
        //private void LoadData()
        //{
        //    //XmlConfigAccessor xmlAcr = new XmlConfigAccessor(PROGRAM_ROOT_PATH + strDatFilNam);
        //    //xmlAcr.Read();
        //    //如果任务列表不为空，开启计时器定时监听
            
        //}
        private void TaskCallback()
        {
            while (Thread.CurrentThread.ThreadState == ThreadState.Running)
            {
                try
                {
                    //防止时间过长，导致Timer产生多个线程访问任务列表
                    //LogInfo("触发队列查询线程：锁定触发队列");
                    lock (GlobalVar.Instanse.Trigers)
                    {
                        //LogInfo("触发队列查询线程：进行触发队列查询");
                        foreach (ITrigerable item in Trigers)
                        {
                            if (item.CanBeTriger())
                            {
                                MessageObj msg;
                                item.CreateMessage(out msg);
                                MsgPool.PushMessage(msg);
                            }

                        }
                    }
                    Thread.Sleep(1000);
                }
                catch (System.Exception)
                {

                }
            }
        }
        public void Clear()
        {
            try
            {
                TaskThread.Abort();
            }
            catch (System.Exception )
            {

            }

        }

        /// <summary>
        /// 保存数据.判断GlobalVar的bNeedToSave状态，若为true，则保存操作，否则不做任何操作
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            bool bRtn = true;
            XmlConfigAccessor xmlAcr = null;
            string sXml;
            try
            {
                if (bNeedToSaveCfg)
                {
                    XmlObjConverter o2x = new XmlObjConverter("Commander");
                    o2x.AddObject(GlobalVar.Instanse);
                    sXml = o2x.Objects2Xml();
                    SecurityOpr so = new SecurityOpr(strSecurityKey);
                    so.WriteToFile(PROGRAM_ROOT_PATH + CfgFilNam, sXml);
                    bNeedToSaveCfg = false;
                }
                //if (bNeedToSaveData || bNeedToSaveNotice)
                //{
                //    //xmlAcr = new XmlConfigAccessor(PROGRAM_ROOT_PATH + strDatFilNam);
                //    //bRtn = xmlAcr.Write(DataBuffer);
                //    //bNeedToSaveCfg = !bRtn;
                //}
                return bRtn;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public GlobalVar()
        {
            _Loger = LogHelper<SynTextLog>.Me;
            _Loger.IsSync = true;
        }
        public void LogInfo(string info)
        {
            _Loger.LogInfo(info);
        }
    }
    class SynTextLog : ILogWriter
    {

        #region ILogWriter 成员

        public void LogCustom(LogObject logObj)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            string remark = logObj.When.ToString() + " [id : " + id.ToString() + "]";
            LogObject obj = new LogObject(logObj.Level, logObj.Msg, remark);
            System.IO.File.AppendAllText("threadstate.log", obj.ToString("\t"), System.Text.Encoding.UTF8);
        }

        #endregion
    }
}
