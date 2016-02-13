using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using HVH_Ken_Modules;
using System.Threading;
using System.Text;
using System.Data;
namespace HVH_Ken
{
    
    public partial class RunFrm : CustomIconForm,Visitable
    {
        
        enum InvokeType { Close,ShowNotice,ShowError};
        private delegate void InvokeHandler(InvokeType type,string msg);
        private readonly int KEYID_DEFAULT;
        //private readonly int KEYID_DEFAULT_CHANGE;
        private readonly int KEYID_DEFAULT_ACTIVE;
        private readonly int KEYID_DEFAULT_SHOWINFO;
        private ShowInfoFrm m_InfoFrm;
        private AdvSrcFrm m_AdvSrcFrm;
        //private readonly string TYPE_NORMAL;
        //private readonly string TYPE_RESTART;

        private InvokeHandler DoInvoke;
        /// <summary>
        /// Initializes a new instance of the <see cref="RunFrm"/> class.
        /// </summary>
        /// <param name="sStartType">启动类型，区别正常启动和重新刷新启动.</param>
        public RunFrm()
        {
            InitializeComponent();
            //this.Update();
            KEYID_DEFAULT = 100;
            //KEYID_DEFAULT_CHANGE = KEYID_DEFAULT + 1;
            KEYID_DEFAULT_ACTIVE = KEYID_DEFAULT + 2;
            KEYID_DEFAULT_SHOWINFO = KEYID_DEFAULT + 3;
            //TYPE_NORMAL = "NORMAL";
            //TYPE_RESTART = "RESTART";
            //m_sStartType = sStartType;
            DoInvoke = HandleInvoke;
        }
        private void HandleInvoke(InvokeType type,string msg)
        {
            switch (type)
            {
                case InvokeType.Close:
                    this.Close();
            	break;
                case InvokeType.ShowNotice:
                    {
                        NoticeForm frm = new NoticeForm();
                        frm.NoticeInfo = msg;
                        frm.Show();
                        //ShowWindow(frm.Handle, 4);
                        //frm.Notice();
                        break;
                    }
                case InvokeType.ShowError:
                    {
                        notifyIcon.BalloonTipText = msg;
                        notifyIcon.ShowBalloonTip(1);
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// 初始化窗口位置.
        /// </summary>
        private void InitFrmPos()
        {
            if (GlobalVar.Instanse.Left == -1 || GlobalVar.Instanse.Top == -1)
            {
                Rectangle screenOffset = SystemInformation.WorkingArea;
                this.Top = screenOffset.Height - this.Height;
                this.Left = screenOffset.Width - this.Width;
            }
            else
            {
                this.Left = GlobalVar.Instanse.Left;
                this.Top = GlobalVar.Instanse.Top;
            }
        }
        private void RunFrm_Load(object sender, EventArgs e)
        {
            
            
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources", System.Reflection.Assembly.GetExecutingAssembly());

            string sIconPath = HVH_Ken_Modules.GlobalVar.PROGRAM_ROOT_PATH + @"\default.ico";
            try
            {
                notifyIcon.Icon = new System.Drawing.Icon(sIconPath);

                pbLogo.Image = new Bitmap(@"logo.gif");
            }
            catch (Exception) { }
            if (IsProcRunning())
            {
                this.notifyIcon.Visible = false;
                GlobalVar.Tip.Info("程序正在运行！");
                this.Close();
                return;
            }
            //if(
            HVH_Ken_Modules.GlobalVar.Instanse.Init();
            HVH_Ken_Modules.GlobalVar.Instanse.MainVisitor = this;
            //bIsSetFrmShown = false;
            //初始化窗口位置
            
            this.Height = 23;
            InitFrmPos();
            //tbCmd.BackColor = Color.AliceBlue;
            plMain.BackColor = tbCmd.BackColor;
            pbLogo.BackColor = tbCmd.BackColor;
            string basePath = GlobalVar.PROGRAM_ROOT_PATH;

            //注册系统热键
            if (!RegHotKey())
            {
                GlobalVar.Tip.Error("注册系统热键错误!");
                return;
            }

            //初始化自动完成列表
            InitAutoCmpl();
            //运行启动程序
            RunWhenStart();
            //监听消息池
            ListenMsgPool();
            //初始化热键
            HotKeyMgrFrm.HotKeyHandle = this.Handle;
            HotKeyMgrFrm.InitHotKeys();
            //整理资源
            System.GC.Collect();
        }

        private void ListenMsgPool()
        {
            ThreadPool.QueueUserWorkItem(MsgLintener);
        }
        private void MsgLintener(object obj)
        {
            MessageObj msg;
            MessagePool pool = GlobalVar.Instanse.MsgPool;
            while (true)
            {
                try
                {
                    msg = pool.PopMessage();
                    
                    if (msg != null)
                    {
                        if (msg.Name.ToLower().Equals("dispose"))
                        {
                            this.Invoke(DoInvoke,new object[]{InvokeType.Close,String.Empty});
                        }
                        else if(msg.Name.Equals("$notice$"))//通知
                        {
                            if (GlobalVar.Instanse.StopNotice)
                                continue;
                            this.Invoke(DoInvoke, new object[] { InvokeType.ShowNotice ,msg.Param});
                        }else
                        {
                            DoLog(msg);
                            ProgFinder.FindToRun(msg.Name,false);
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    //File.WriteAllText(@"C:\threaderror.txt","MsgLintener " + ex.Message);
                    GlobalVar.Tip.Error(ex.Message);
                    continue;
                }
            }
        }

        private void DoLog(MessageObj msg)
        {
            DateTime dt = DateTime.Now;
            StringBuilder sb = new StringBuilder();

            sb.Append(dt.ToLongDateString())
                .Append(" (").Append(dt.DayOfWeek.ToString()).Append(")")
                .Append("\t").Append(dt.ToLongTimeString())
                .Append(" 运行命令：")
                .Append('[').Append(msg.Name).Append(']')
                .Append("\r\n");

            File.AppendAllText(GlobalVar.PROGRAM_ROOT_PATH + @"\tasklog.txt", sb.ToString());
        }
        public void SetFocus()
        {
            this.Activate();
            tbCmd.Focus();
            tbCmd.SelectAll();
        }
        /// <summary>
        /// 解析 tbCmd 的值，执行相应的程序
        /// </summary>
        private bool TryRun(string sKeyWord)
        {
            
            try
            {
                
                if (!sKeyWord.Equals(String.Empty))
                {
                    ProgFinder.FindToRun(sKeyWord.ToLower(), false);
                    tbCmd.Clear();
                    tbCmd.AutoCompleteCustomSource.Add(sKeyWord);
                    SetTipMsg("在此输入运行命令\r\n双击可查看错误信息");
                    //GlobalVar.Helper.Show(sKeyWord,"Main After");
                }
                else
                { //打开目录
                    ProgFinder.FindToRun("explorer /e,", false);
                }

            }
            catch (Exception ex)
            {
                SetTipMsg(ex.Message);
                return false;
            }
            return true;
        }

        private void SetTipMsg(string sMsg)
        {
            tbCmd.Focus();
            tbCmd.SelectAll();
            toolTip.SetToolTip(tbCmd, sMsg);
        }

        private bool IsProcRunning()
        {
            string proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);
            if (processes.Length > 1)
            {
                return true;
            }
            return false;
        }
        #region 初始化程序配置信息
        /// <summary>
        /// 初始化程序.
        /// </summary>
        /// <returns></returns>
        private bool RegHotKey()
        {
            bool b = false;
            try
            {
                //RegisterHotKey(this.Handle.ToInt32(), KEYID_DEFAULT_CHANGE, GlobalVar.Instanse.HOT_KEY_MODIFIER, GlobalVar.Instanse.HOT_KEY_CHANGE);//窗口切换热键
                if (GlobalVar.Instanse.HOT_KEY_MODIFIER == 0 ||
                    GlobalVar.Instanse.HOT_KEY_ACTIVE == 0 ||
                    GlobalVar.Instanse.HOT_KEY_SHOWINFO == 0)
                    return true;
                b = RegisterHotKey(this.Handle, KEYID_DEFAULT_ACTIVE, GlobalVar.Instanse.HOT_KEY_MODIFIER, GlobalVar.Instanse.HOT_KEY_ACTIVE);//激活窗口
                b = RegisterHotKey(this.Handle, KEYID_DEFAULT_SHOWINFO, GlobalVar.Instanse.HOT_KEY_MODIFIER, GlobalVar.Instanse.HOT_KEY_SHOWINFO);//显示配置程序信息
            }
            catch (Exception)
            {
                return false;

            }
            return b;

        }
        /// <summary>
        /// 运行随程序启动的程序.
        /// </summary>
        private void RunWhenStart()
        {
            //foreach (System.Collections.Generic.KeyValuePair<string, ConfigData> pair in GlobalVar.Instanse.DataBuffer)
            //{
            //    ConfigData data = pair.Value;
            //    if (data.IsAutRun)
            //    {
            //        try
            //        {
            //            ProgFinder.FindToRun(data.Shortcut, false);
            //        }
            //        catch (Exception)
            //        {
            //        }
            //    }
            //}
        }
        #endregion

        #region 系统热键
        [DllImport("user32.dll", EntryPoint = "RegisterHotKey")]
        public static extern bool RegisterHotKey(
        IntPtr hwnd,
        int id,
        uint fsModifiers,
        uint vk);
        [DllImport("user32.dll", EntryPoint = "UnregisterHotKey")]
        public static extern bool UnregisterHotKey(
        IntPtr hwnd,
        int id);
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private static extern int ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetCapture")]
        private static extern IntPtr SetCapture(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "ShowWindow")]
        private static extern bool ShowWindow(IntPtr hWnd,int iCmd);
        private const int WM_SysCommand = 0x0112;
        private const int SC_MOVE = 0xF012;
        protected override void WndProc(ref Message m)
        {
            //WM_HOTKEY消息
            switch (m.Msg)
            {
                case 0x0312:
                    {
                        //热键：显示信息
                        if (m.WParam.ToInt32().Equals(KEYID_DEFAULT_SHOWINFO))
                        {
                            //GlobalVar.InfoFrm.Visible = !GlobalVar.InfoFrm.Visible;
                            if (m_InfoFrm == null)
                            {
                                m_InfoFrm = new ShowInfoFrm();
                                m_InfoFrm.ShowInTaskbar = false;
                            }
                           
                            m_InfoFrm.Visible = !m_InfoFrm.Visible;
                        }
                        else//热键：切换窗口
                        {
                            //切换窗口
                            if (m.WParam.ToInt32().Equals(KEYID_DEFAULT_ACTIVE))
                            {
                                this.Show();
                                this.WindowState = FormWindowState.Normal;
                                this.Height = 22;//有时窗口高度会变化，可能是BUG
                                
                                this.SetFocus();
                            }
                        }
                        if(m.WParam.ToInt32() >= 103)
                        {
                            if(HotKeyMgrFrm.HotKeys.ContainsKey(m.WParam.ToInt32()))
                                TryRun(HotKeyMgrFrm.HotKeys[m.WParam.ToInt32()]);
                        }
                    } break;
            }

            base.WndProc(ref m);
        }
        #endregion
        #region 事件响应程序

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            string sKeyWord = tbCmd.Text.Trim();
            switch (e.KeyData)
            {
                case (Keys.Enter | Keys.Control)://按下Ctrl + 回车，进行用户搜索
                    {
                        if (sKeyWord.Equals(String.Empty))
                        {
                            if (this.m_AdvSrcFrm == null)
                                this.m_AdvSrcFrm = new AdvSrcFrm();
                            this.m_AdvSrcFrm.Show();
                            this.m_AdvSrcFrm.Activate();
                        }
                        else
                        {
                            try
                            {
                                ProgFinder.StartSerachEngine(sKeyWord, GlobalVar.Instanse.DEFAULT_SEARCH_ENGINE_URL);
                                tbCmd.Clear();
                                tbCmd.AutoCompleteCustomSource.Add(sKeyWord);
                                this.SetTipMsg("在此输入运行命令\r\n双击可查看错误信息");
                            }
                            catch (Exception)
                            {
                                SetTipMsg("找不到合适的搜索引擎: [ " + GlobalVar.Instanse.DEFAULT_SEARCH_ENGINE_URL + " ]\r\n或关键字占位符为空");
                            }
                        }
                        break;
                    }
                case Keys.Enter://按下回车,打开程序
                    {
                        this.WindowState = FormWindowState.Minimized;
                        this.Hide();
                        TryRun(tbCmd.Text.Trim());
                        break;
                    }
                case Keys.Escape:
                    {
                        tbCmd.Clear();
                        this.WindowState = FormWindowState.Minimized;
                        this.Hide();

                        break;
                    }
                default:
                    {
                        //处理其它 键
                        ProcOther(e);
                        break;
                    }
            }

        }
        private void ProcOther(KeyEventArgs e)
        {

        }

        private void itemDatBak_Click(object sender, EventArgs e)
        {
            //using (FolderBrowserDialog fbdOpenDir = new FolderBrowserDialog())
            //{
            //    if (fbdOpenDir.ShowDialog() != DialogResult.OK)
            //        return;
            //    //string path = fbdOpenDir.SelectedPath + GlobalVar.DatFilNam;
            //    //XmlConfigAccessor accessor = new XmlConfigAccessor(path);
            //    //if (accessor.Write(GlobalVar.Instanse.DataBuffer))
            //    //else
            //    //    MessageBox.Show("备份失败,我已经很努力了");
            //}
        }

        private void itemDatImp_Click(object sender, EventArgs e)
        {
            //FileStream stream;
            //using (OpenFileDialog openFileDialog = new OpenFileDialog())
            //{
            //    openFileDialog.FileName = "";
            //    openFileDialog.Multiselect = false;
            //    openFileDialog.Filter = "DB files (*.db)|*.db";
            //    string path = null;

            //    if (MessageBox.Show("导入数据会替换原来数据,是否继续?", "当心!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        return;

            //    //选择文件
            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        stream = openFileDialog.OpenFile() as FileStream;
            //        using (stream)
            //        {
            //            path = stream.Name;
            //        }
            //    }
            //    else
            //        return;
            //    try
            //    {
            //        string fullpath = GlobalVar.PROGRAM_ROOT_PATH + GlobalVar.DatFilNam;
            //        //删除原文件
            //        File.Delete(fullpath);
            //        //复制文件
            //        File.Copy(path, fullpath);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("导入失败,我已经很努力了");
            //        return;
            //    }
            //    MessageBox.Show("导入成功,需重启程序");
            //}
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool Save()
        {
            while (true)
            {
                if (GlobalVar.Instanse.Save())
                    return true;
                else
                {
                    if (GlobalVar.Tip.Question("保存失败了!", "我已经努力保存了,是否再试一次？") == DialogResult.No)
                        return false;
                }
            }
        }

        #endregion

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new SrcEngEdt().ShowDialog();
        }
        /// <summary>
        /// Mouses the move impl.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void MouseMoveImpl(object sender, MouseEventArgs e)
        {
            //if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
            //    return;
            //ReleaseCapture();
            //SendMessage(this.Handle, WM_SysCommand, SC_MOVE, 0);
            //GlobalVar.Instanse.NeedToSaveCfg = true;
            //GlobalVar.Instanse.Top = this.Top;
            //GlobalVar.Instanse.Left = this.Left;
        }

        /// <summary>
        /// 初始化自动完成列表.
        /// </summary>
        private void InitAutoCmpl()
        {
            AutoCompleteStringCollection src = tbCmd.AutoCompleteCustomSource;
            DataTable table = new DataTable("programs");
            GlobalVar.Helper.AddSelect("programs", "shortcut");
            GlobalVar.Helper.Fill(ref table);
            src.Clear();
            this.SuspendLayout();
            string[] sSrc = new string[table.Rows.Count];
            int i = 0;
            foreach (DataRow row in table.Rows)
            {
                sSrc[i++] = (row.Field<string>("shortcut"));
            }
            src.AddRange(sSrc);
            this.ResumeLayout();
        }

        private void RunFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            GlobalVar.Instanse.Clear();
            Save();
            UnregisterHotKey(this.Handle, KEYID_DEFAULT_ACTIVE);
            UnregisterHotKey(this.Handle, KEYID_DEFAULT_SHOWINFO);
            foreach (System.Collections.Generic.KeyValuePair<int, string> p in HotKeyMgrFrm.HotKeys)
            {
                UnregisterHotKey(this.Handle, p.Key);
            }
            if (GlobalVar.Connection.State == System.Data.ConnectionState.Open)
            {
                GlobalVar.Connection.Close();
            }
            GlobalVar.Helper.Dispose();
            this.Dispose();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void tbCmd_TextChanged(object sender, EventArgs e)
        {
            toolTip.SetToolTip(tbCmd, "在此输入运行命令\r\n双击可查看错误信息");
        }

    

        private void RunFrm_Deactivate(object sender, EventArgs e)
        {
            if (this.Disposing)
                return;

            if (tbCmd.Text.Trim() == String.Empty)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }
        private void CfgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVar.Instanse.IsSetFrmShown == false)
            {
                SettingFrm frm = new SettingFrm();
                GlobalVar.Instanse.IsSetFrmShown = true;
                frm.ShowDialog();
            }
        }

        private void EditEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecOprFrm frm;
            Random random = new Random();
            int iActType = random.Next(2);
            if (iActType == 0)
                frm = new RecOprFrm(HVH_Ken_Modules.GlobalVar.ActionType.Modify);
            else
                frm = new RecOprFrm(HVH_Ken_Modules.GlobalVar.ActionType.Add);
            frm.ShowDialog();
            
            if(frm.HasChange)
                InitAutoCmpl();
            
        }

        private void tbCmd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string sErrInfos;
            string[] errCmds;
            if (GlobalVar.PopErrorInfos(out sErrInfos, out errCmds) == true)
            {
                //MessageBox.Show(sErrInfos, "Error Infomations", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetTipMsg(sErrInfos);
                //去掉发生错误的命令
                //MessageBox.Show(errCmds.Length.ToString());
                foreach (string errCmd in errCmds)
                {
                    tbCmd.AutoCompleteCustomSource.Remove(errCmd);
                }
            }
            else
            {
                SetTipMsg("无错误信息^_^");
            }
        }
        
        private void pbLogo_Click(object sender, EventArgs e)
        {

        }
        
        private void itemNotice_Click(object sender, EventArgs e)
        {
            NoticeMgrFrm frm = new NoticeMgrFrm();
            frm.ShowDialog();
            frm.Close();
        }

      
        #region Visitable 成员

        public void Visit(object obj)
        {
            Invoke(DoInvoke, new object[] { InvokeType.ShowError,obj.ToString()});

        }

        #endregion

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void tsOptimize_Click(object sender, EventArgs e)
        {
            string file = GlobalVar.PROGRAM_ROOT_PATH + @"\info.db3";
            long s1, s2;
            FileInfo fi = new FileInfo(file);
            s1 = fi.Length;
            GlobalVar.Helper.ExcuteForUnique("vacuum");
            fi = new FileInfo(file);
            s2 = fi.Length;
            GlobalVar.Tip.Info("压缩字节数：" +  (s1-s2).ToString());
        }

        private void tbCmd_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(KeyboardHelper.CodeToString(e.));
            //return;
        }

        private void tsmHotKey_Click(object sender, EventArgs e)
        {
            HotKeyMgrFrm frm = new HotKeyMgrFrm();
            frm.ShowDialog();
        }

        private void pbLogo_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;
            ReleaseCapture();
            SendMessage(this.Handle, WM_SysCommand, SC_MOVE, 0);
            GlobalVar.Instanse.NeedToSaveCfg = true;
            GlobalVar.Instanse.Top = this.Top;
            GlobalVar.Instanse.Left = this.Left;
        }
    }
}
