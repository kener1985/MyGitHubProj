using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace HVH_Ken_Modules
{
    public class ProgExecuter
    {

        private ProgExecuter() { }//防止被实例化
        private static readonly string COMMAND_SEPRATOR;
        //private static System.Diagnostics.ProcessStartInfo info;
        private static Invoker m_invoker = null;
        static ProgExecuter()
        {
            m_invoker = new Invoker();
            COMMAND_SEPRATOR = ",";
        }
        /// <summary>
        /// 初始化进程信息.
        /// </summary>
        private static void InitProInfo(System.Diagnostics.ProcessStartInfo info, string sCmd, string sArgs)
        {
            info.UseShellExecute = true;
            info.LoadUserProfile = false;
            //设置工作目录，否则有些程序运行不了
            if (System.IO.Path.HasExtension(sCmd))
                info.WorkingDirectory = System.IO.Path.GetDirectoryName(sCmd);
            else
            {
                string sSysWkDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                int i = sSysWkDir.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
                sSysWkDir = sSysWkDir.Substring(0,i);
                info.WorkingDirectory = sSysWkDir;
            }
            info.FileName = sCmd;
            info.Arguments = sArgs;
        }
        /// <summary>
        /// 直接执行护展命令
        /// </summary>
        /// <param name="sExtCmds">The s ext CMDS.</param>
        public static void ExecExtCmd(string sExtCmds, CommanderData Context)
        {
            StringCollection scBefore, scAfter;
            ParseBACmd(sExtCmds, out scBefore, out scAfter);
            //先执行
            foreach (string bCmd in scBefore)
                MatchForInvoke(bCmd, Context);

            //后执行
            foreach (string aCmd in scAfter)
                MatchForInvoke(aCmd, Context);

        }
        /// <summary>
        /// 调用命令.
        /// </summary>
        /// <param name="cmdObj">The CMD obj.</param>
        public static void Execute(CommanderData cmdObj)
        {
            StringCollection scBefore, scAfter;
            ParseBACmd(cmdObj.ExtCmds, out scBefore, out scAfter);

            //查找系统环境变量或直接打开文件夹
            if (scBefore.Contains(cmdObj.Cmd) || scAfter.Contains(cmdObj.Cmd))
            {
                throw new ExtCommandLineBrokenException("扩展命令集不能包含自身");
            }
            try
            {
                //前执行
                foreach (string bCmd in scBefore)
                    MatchForInvoke(bCmd, cmdObj);

                Run(cmdObj.ParsedCmd, cmdObj.Args);

                //后执行
                foreach (string aCmd in scAfter)
                    MatchForInvoke(aCmd, cmdObj);
            }
            catch (ExtCommandLineBrokenException)
            {
            }
        }
        /// <summary>
        /// 解析前、后执行命令.
        /// </summary>
        /// <param name="sExtCmd">The s ext CMD.</param>
        /// <param name="aBefore">A before.</param>
        /// <param name="aAfter">A after.</param>
        private static void ParseBACmd(string sExtCmd, out StringCollection aBefore, out  StringCollection aAfter)
        {
            string[] aSep = new string[] { COMMAND_SEPRATOR };
            string[] aCmds = sExtCmd.Split(aSep, StringSplitOptions.RemoveEmptyEntries);
            aBefore = new StringCollection();
            aAfter = new StringCollection();
            foreach (string cmd in aCmds)
            {
                if (cmd[0] == '+')//前执行
                    aBefore.Add(cmd.Substring(1).Trim());
                else
                    aAfter.Add(cmd.Trim());//默认为后执行
            }
        }
        private static void MatchForInvoke(string FullCmd, CommanderData Context)
        {
            m_invoker.Invoke(FullCmd, Context);
        }

        /// <summary>
        /// 运行命令.
        /// </summary>
        /// <param name="sCmd">The s CMD.</param>
        /// <param name="sArgs">The s args.</param>
        private static void Run(string sCmd, string sArgs)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo info;
                info = new System.Diagnostics.ProcessStartInfo();
               
                InitProInfo(info, sCmd, sArgs);
                System.Diagnostics.Process.Start(info);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
