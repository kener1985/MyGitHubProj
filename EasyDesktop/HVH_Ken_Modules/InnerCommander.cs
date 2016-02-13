namespace HVH_Ken_Modules
{
    internal class ExtCommandLineBrokenException : System.Exception
    {
        public ExtCommandLineBrokenException() { }
        public ExtCommandLineBrokenException(string message)
            : base(message)
        {
        }
    }
    /// <summary>
    /// 
    /// </summary>
    internal interface IInnerCommander
    {
        void Invoke(string CurCmd, CommanderData Context);
        string Name { get; }
        bool Match(string bCmd);

    }
    /// <summary>
    /// 默认命令
    /// </summary>
    internal class DefaultInnerCommand : IInnerCommander
    {
        private string m_sName;
        public DefaultInnerCommand()
        {
            m_sName = string.Empty;//默认命令名为空
        }
        public string Name
        {
            get { return m_sName; }
        }
        public bool Match(string bCmd)
        {
            return true;
        }
        public void Invoke(string CurCmd, CommanderData Context)
        {
            //默认情况下只运行程序配置的命令
            try
            {
                ProgFinder.FindToRun(CurCmd,false);
            }
            catch (ExtCommandLineBrokenException)//忽略扩展命令执行异常
            { }

        }
    }

    /// <summary>
    /// 确认命令
    /// </summary>
    internal class ConfirmInnerCommand : IInnerCommander
    {
        private string m_sName;
        public ConfirmInnerCommand()
        {
            m_sName = "$check";//内部命令名必须以$开头
        }
        public string Name
        {
            get { return m_sName; }
        }
        public bool Match(string bCmd)
        {
            return bCmd.Equals(m_sName.ToLower());
        }
        public void Invoke(string CurCmd, CommanderData Context)
        {
            if (!Context.Description.Equals(string.Empty))
                
                if (GlobalVar.Tip.Question("我要执行 " + Context.Description + " 咯", "待确认...")
               == System.Windows.Forms.DialogResult.No)
                    throw new ExtCommandLineBrokenException();
        }
    }
    /// <summary>
    /// 当前程序作为某个程序参数命令
    /// </summary>
    internal class AsParamInnerCommand : IInnerCommander
    {
        private string m_sName;
        public AsParamInnerCommand()
        {
            m_sName = "$paramas";//内部命令名必须以$开头
        }
        public string Name
        {
            get { return m_sName; }
        }
        public bool Match(string bCmd)
        {
            int iPos = bCmd.IndexOf('[');
            if (iPos != -1)
                return bCmd.Substring(0, iPos).Equals(m_sName.ToLower());
            else return false;
        }
        public void Invoke(string CurCmd, CommanderData Context)
        {
            string sCmd = Context.ExtCmds;
            //取出命令
            if (!sCmd.Contains("[") || !sCmd.Contains("]"))
                throw new ExtCommandLineBrokenException();
            int iStart = sCmd.IndexOf('[');
            int iEnd = sCmd.LastIndexOf(']');
            string prog = sCmd.Substring(iStart + 1, iEnd - iStart - 1);
            //查找参数
            string path = FindPathByCmd(prog);
            Context.Args += " " + Context.ParsedCmd + " " + Context.Args;
            Context.ParsedCmd = path;
        }
        private string FindPathByCmd(string cmd)
        {
            string sPath = "";
            //if (GlobalVar.Instanse.DataBuffer.ContainsKey(cmd) &&
            //        GlobalVar.Instanse.DataBuffer[cmd] != null)
            //    sPath = GlobalVar.Instanse.DataBuffer[cmd].Path;
            GlobalVar.Helper.AddCustomParam("cmd", cmd);
            sPath = GlobalVar.Helper.ExcuteForUnique<string>("select path from programs where shortcut=@cmd");

            return sPath;
        }
    }
    /// <summary>
    /// 追加参数命令
    /// </summary>
    internal class AddParamCommand : IInnerCommander
    {
        private string m_sName;
        public AddParamCommand()
        {
            m_sName = "$addparam";//内部命令名必须以$开头
        }
        public string Name
        {
            get { return m_sName; }
        }
        public bool Match(string bCmd)
        {
            return bCmd.Equals(m_sName.ToLower());
        }
        public void Invoke(string CurCmd, CommanderData Context)
        {
            AppParamFrm frm = new AppParamFrm(Context);

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string param = frm.Param;
                Context.Args += " " + param;
            }
        }
    }

    /// <summary>
    /// 关闭主程序命令
    /// </summary>
    internal class CloseCommand : IInnerCommander
    {
        private string m_sName;
        public CloseCommand()
        {
            m_sName = "$close";//内部命令名必须以$开头
        }
        public string Name
        {
            get { return m_sName; }
        }
        public bool Match(string bCmd)
        {
            return bCmd.Equals(m_sName.ToLower());
        }
        public void Invoke(string CurCmd, CommanderData Context)
        {
            // System.Environment.
            //if (GlobalVar.MainForm != null)
            //    GlobalVar.MainForm.Dispose();
            MessageObj obj = new MessageObj("dispose", null);
            GlobalVar.Instanse.MsgPool.PushMessage(obj);

        }
    }
    /// <summary>
    /// 命令参数
    /// </summary>
    public class CommanderData
    {
        private string m_sCmd;//前端传过来的快捷命令
        private string m_sParsedCmd;//解析后的完整执行命令
        private string m_sArgs;
        private string m_sExtCmds;
        private string m_sDescription;//命令描述，用于提示
        private bool m_bRunDirectly;//是否直接运行
        public CommanderData()
        {
            m_sParsedCmd = string.Empty;
            m_sArgs = string.Empty;
            m_sExtCmds = string.Empty;
            m_bRunDirectly = false;
        }
        public string Cmd
        {
            get { return m_sCmd; }
            set { m_sCmd = value; }
        }
        public string ParsedCmd
        {
            get { return m_sParsedCmd; }
            set { m_sParsedCmd = value; }
        }
        public string Args
        {
            get { return m_sArgs; }
            set { m_sArgs = value; }
        }
        public string ExtCmds
        {
            get { return m_sExtCmds; }
            set { m_sExtCmds = value; }
        }
        public string Description
        {
            set { m_sDescription = value; }
            get { return m_sDescription; }
        }
        public bool IsRunDirectly
        {
            get { return this.m_bRunDirectly; }
            set { this.m_bRunDirectly = value; }
        }
    }
    internal class Invoker
    {
        private static System.Collections.Generic.Dictionary<string, IInnerCommander> m_Commanders;
        public Invoker()
        {
            m_Commanders = new System.Collections.Generic.Dictionary<string, IInnerCommander>();
            IInnerCommander cmder = new DefaultInnerCommand();
            m_Commanders.Add(cmder.Name, cmder);
            cmder = new ConfirmInnerCommand();
            m_Commanders.Add(cmder.Name, cmder);
            cmder = new AsParamInnerCommand();
            m_Commanders.Add(cmder.Name, cmder);
            cmder = new AddParamCommand();
            m_Commanders.Add(cmder.Name, cmder);
            cmder = new CloseCommand();
            m_Commanders.Add(cmder.Name, cmder);
        }
        public void Invoke(string CurCmd, CommanderData Context)
        {
            IInnerCommander cmder = null;
            int iPos = 0;
            string Cmd = CurCmd;
            iPos = CurCmd.IndexOf('[');
            if (iPos != -1)//带参数
                Cmd = CurCmd.Substring(0, iPos);
            if (m_Commanders.ContainsKey(Cmd))
                cmder = m_Commanders[Cmd];
            else
                cmder = m_Commanders[string.Empty];
            cmder.Invoke(CurCmd, Context);
        }
    }
}
