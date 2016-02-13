using System;
using System.Collections.Specialized;
using System.Collections;

namespace HVH_Ken_Modules
{
    public class ProgFinder
    {
        //private static StringDictionary m_SpcSigMap;//URL特殊字符映射
        private static readonly char KONGGE ;
        private static readonly char DOT ;
        private static readonly char XIEGAN ;
        private static readonly char MAOHAO;
        private static readonly char INNERSTART;
        private static readonly char INNEREND;

        static ProgFinder()
        {
            KONGGE = ' ';
            DOT = '.';
            XIEGAN = '\\';
            MAOHAO = ':';
            INNERSTART = '{';
            INNEREND = '}';
        }
        /// <summary>
        /// 根据shortcut查找程序.
        /// 传进来的shortcut应该通过Trim,否则不能正常解析
        /// 开启新线程解析命令并运行
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        /// <param name="bPassParse">if set to <c>true</c> 直接运行，不进行解析.</param>
        public static void FindToRun(string shortcut,bool bRunDirectly)
        {
            CommanderData cmdObj = new CommanderData();
            cmdObj.Cmd = shortcut;
            cmdObj.IsRunDirectly = bRunDirectly;
            
            System.Threading.ThreadPool.QueueUserWorkItem(StartFindThread, cmdObj);
        }

        /// <summary>
        /// 命令解析执行线程方法.
        /// </summary>
        /// <param name="obj">命令对象.</param>
        private static void StartFindThread(object obj)
        {
            CommanderData cmdObj = obj as CommanderData;
            bool bParsed = false;//命令被解析标志
            if (cmdObj == null)
                return;
            
            try
            {
                if (cmdObj.IsRunDirectly == true)
                {
                    //运行时用ParsedCmd
                    cmdObj.ParsedCmd = cmdObj.Cmd;
                    ProgExecuter.Execute(cmdObj);
                }
                else
                {
                    ////大多数情况下都没有空格，如果含有空格，说明可能有参数，该判断为提高性能用
                    //int iPos = cmdObj.Cmd.IndexOf(" ");
                    //if (iPos != -1)
                    //{
                    //    //解析输入命令、参数
                    //    ParseCommand(cmdObj.Cmd, cmdObj);
                    //    //有空格且为完整路径直接执行程序，Cmd无用；如果为快捷命令，则进行提取
                    //    cmdObj.Cmd = cmdObj.ParsedCmd.Substring(0, iPos);
                    //    cmdObj.Description = cmdObj.Cmd;

                    //    bParsed = true;
                    //}
                    //查找程序
                    System.Collections.Generic.List<ConfigData> datas = GlobalVar.Instanse.FindCommand(cmdObj.Cmd);
                    
                    if(datas == null)
                    {
                        datas = new System.Collections.Generic.List<ConfigData>();
                        ConfigData data = new ConfigData();
                        data.Path = cmdObj.Cmd;
                        data.Title = cmdObj.Description;
                        data.Shortcut = cmdObj.Cmd;
                        datas.Add(data);
                    }
                    else if (datas.Count == 0)
                    {
                        return;
                    }
                    foreach(ConfigData data in datas)
                    {
                        //如果上面已经调用过，还需要将data.Path中的参数解析出来
                        ParseCommand(data.Path, cmdObj);
                        //bParsed = true;
                        cmdObj.Description = data.Title;
                    
                    //if (bParsed == false)
                    //{
                    //    //解析输入命令、参数
                    //    ParseCommand(cmdObj.Cmd, cmdObj);
                    //}
                    //这里的判断有待改进
                    if (cmdObj.ParsedCmd != string.Empty &&
                        cmdObj.ParsedCmd[0] == INNERSTART &&
                        cmdObj.ParsedCmd[cmdObj.ParsedCmd.Length - 1] == INNEREND)
                    {
                        string sExtCmd = ParseExtCmd(cmdObj.ParsedCmd);
                        ProgExecuter.ExecExtCmd(sExtCmd, cmdObj);
                    }
                    else
                        ProgExecuter.Execute(cmdObj);
                    }
                }
            }
            catch
                (ExtCommandLineBrokenException)
            { }
            catch (Exception ex)
            {
                ErrorInfo ei = new ErrorInfo(cmdObj.Cmd, ex.Message);
                GlobalVar.PushErrorInfo(ei);
                GlobalVar.Instanse.MainVisitor.Visit(ex.Message);
            }
            
        }
        //private bool FindCommand(out ConfigData cd)
        //{
        //    GlobalVar.Helper.AddSelect("programs", "");
        //    return true;
        //}
        /// <summary>
        /// 根据配置，解析用户输入的命令成为系统可识别的命令和参数.
        /// </summary>
        /// <param name="shortcut">用户输入的快捷命令.</param>
        /// <param name="cmd">系统可识别的命令.</param>
        /// <param name="args">参数.</param>
        private static void ParseCommand(string shortcut, CommanderData cmdObj)
        {
            string sCmd;
            string sArgs;
            string sExtCmd;
            //获取命令和参数间隔的空格位置
            int iPos = GetParamPos(shortcut);
            if (iPos == -1)
            {
                sCmd = shortcut;
                sArgs = String.Empty;
                sExtCmd = String.Empty;
            }
            else
            {
                sCmd = shortcut.Substring(0, iPos);
                sArgs = shortcut.Substring(++iPos, shortcut.Length - iPos);
                int iIc = sArgs.IndexOf(INNERSTART);
                if (iIc != -1)
                {
                    sExtCmd = ParseExtCmd(sArgs.Substring(iIc).Trim());
                    sArgs = sArgs.Substring(0, iIc);
                }
                else
                    sExtCmd = String.Empty;
            }
            //构造命令对象
            cmdObj.Args += " " + sArgs;//用 += 是因为参数可能来自用户输入和配置
            cmdObj.ParsedCmd = sCmd;
            cmdObj.ExtCmds = sExtCmd;
        }
        
        /// <summary>
        /// 解析形为{cmd}的命令格式,将里面的命令字符 cmd 提取出来.
        /// </summary>
        /// <param name="sFullCmd">完整的命令参数.</param>
        /// <returns></returns>
        private static string ParseExtCmd(string sFullCmd)
        {
            if (sFullCmd.Length < 2 || !sFullCmd[0].Equals(INNERSTART) || !sFullCmd[sFullCmd.Length - 1].Equals(INNEREND))
                throw new ArgumentException("这个命令我解析不了,是不是有空格呀!?");

            return sFullCmd.Substring(1, sFullCmd.Length - 2).Trim();
        }
        /// <summary>
        /// 打启搜索,必须捕捉异常.
        /// </summary>
        /// <param name="strKeyword">The STR keyword.</param>
        /// <param name="strUrl">带参数的搜索引擎URL.</param>
        public static void StartSerachEngine(string strKeyword, string strUrl)
        {
            byte[] keybt = System.Text.Encoding.Default.GetBytes(strKeyword);
            //一个byte转换成三个字符
            System.Text.StringBuilder sb = new System.Text.StringBuilder(keybt.Length * 3);
            foreach (byte b in keybt)
            {
                sb.Append('%').Append(b.ToString("X2"));
            }
            //由于各搜索引擎对中文的转码格式不一致，因此不考虑对关键字进行转码
            //strTmp = ChangeToUniCode(strTmp);
            //运行搜索引擎
            FindToRun(strUrl.Replace(GlobalVar.Instanse.KEY_WORD_FLAG, sb.ToString()),false);
        }
        /*
        /// <summary>
        /// 将字符串中的汉字转十六进制.
        /// </summary>
        /// <param name="sContent">Content of the s.</param>
        /// <returns></returns>
        public static string ChangeToUniCode(string sContent)
        {
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("gb2312");
            byte[] bytes = chs.GetBytes(sContent);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i == bytes.Length - 1)
                {
                    str += (char)bytes[i];
                    break;
                }
                byte bHigh = bytes[i];
                byte bLow = bytes[i + 1];
                if (
                    i != bytes.Length - 1 &&
                    IsChinese(bHigh,bLow)
                    )
                {
                    //为汉字
                    str +=   string.Format("%{0:X}%{1:X}", (byte)bHigh,(byte)bLow);
                    i++;
                }
                else
                    str += (char)bytes[i];
            }

            return str;
        }
        private static bool IsChinese(byte bHigh, byte bLow)
        {
            bool bRtn = ((bHigh >= 0xB0 && bHigh <= 0xF7) && //gb2312
                     (bLow >= 0xA0 && bLow <= 0xFE)) ||
                     ((bHigh >= 0x81 && bHigh <= 0xFE) && //gbK
                     (bLow >= 0x40 && bLow <= 0xFE)) ||
                     ((bHigh >= 0x81 && bHigh <= 0xFE) && //big5
                     (bLow >= 0x40 && bLow <= 0x7E) || (bLow >= 0xA1 && bLow <= 0xFE));
            return bRtn;
        }
         * */
        /// <summary>
        /// Gets the param start pos.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <returns></returns>
        private static int GetParamPos(string shortcut)
        {
            int iPos = -1;
            int iLstDot = 0;

            //多数情况下都没有参数，该判断为提高性能
            if (shortcut.IndexOf(KONGGE) == -1)
                return -1;

            //以下解析可用责任链模式代替
            if (shortcut.Length >= 2)
            {
                //为本地路径或FTP,该标志下才有可能出现扩展名标志
                if (shortcut[1].Equals(MAOHAO))
                {
                    //不要用LastIndexOf,因为参数有可能为程序完整路径
                    iLstDot = shortcut.IndexOf(DOT);
                    if (iLstDot == -1)//为文件夹路径
                        iPos = -1;
                    else //为程序完整路径
                        iPos = shortcut.IndexOf(KONGGE, iLstDot);
                }
                else if (shortcut[0].Equals(XIEGAN) && shortcut[1].Equals(XIEGAN))
                {
                    //FTP路径不支持参数
                    iPos = -1;
                }
                else//为配置命令
                    iPos = shortcut.IndexOf(KONGGE);
            }
            else
                iPos = -1;
            return iPos;
        }
    }
}
