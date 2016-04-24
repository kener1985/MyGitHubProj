using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Web;
using System.Collections.Specialized;
using BaseLib;
using System.Reflection;
using System.Xml;
using UtilHelper.Database;
using System.IO;
using System.Diagnostics;

 

namespace HtmlForm
{
    public partial class MainForm : Form
    {
        public Form LogFrm {get;set;}
        public MainForm()
        {
            InitializeComponent();
        }

        
        /************************************************************************/
        /*  schema: 
         *      open    打开一个容器，展示工作通过initview查找并在该容器中展示
         *      initview 打开一个视图，并在当前容器显示。容器可能是一个空页面，只有
         *      query   将业务数据发往后台处理
         *      
         *  [host] : url的主机部分，描述Action所在位置，以 . 号分隔，默认为DefaultAction
         *  [localpath] : 容器位置
         *  script   : 容器所需的js文件，如果存在则会被加载，common，默认都会被加载
         *  invoke : 回调脚本函数名，如果有，则会被调用
         *  css  ：容器所需css文件，如果存在则会被加载
         *  secid：会话ID，如果有，则会在回调函数中返回
         *  
        /************************************************************************/
        private void MainForm_Load(object sender, EventArgs e)
        {
            GlobalVar.MainBrowser = wb;
            
            //wb.Navigate("test://?count=0");
           //wb.Navigate(@"open:///default/?script=calendar,default&invoke=MyCallback&css=default");
            //Utility.DoAction(wb,"open", "/default", "script=calendar,default&invoke=MyCallback&css=default");
            
            InitMenu();
        }

        private void InitMenu()
        {
            //启动url
            string startup = String.Empty;
            XmlReaderSettings s = new XmlReaderSettings();
            s.CloseInput = true;
            s.IgnoreComments = true;
            s.IgnoreWhitespace = true;
            XmlReader r = XmlReader.Create("menu.xml",s);
            ToolStripMenuItem item;
            Stack<ToolStripMenuItem> items = new Stack<ToolStripMenuItem>();
            try
            {
                while (r.Read())
                {
                    switch (r.NodeType)
                    {
                        case XmlNodeType.Element:
                            {
                                StrDictionary sd;
                                ParseAttr(r, out sd);
                                item = new ToolStripMenuItem();
                                item.Text = sd["name"];
                                if(r.Name == "ItemGroup")
                                {
                                    
                                    if (r.Depth == 1)
                                    {
                                        ms.Items.Add(item);
                                    }
                                    else
                                    {
                                        items.Peek().DropDownItems.Add(item);
                                    }
                                    items.Push(item);
                                }
                                else if (r.Name == "Item")
                                {
                                    item.Tag = sd;
                                    item.Click +=new EventHandler(item_Click);
                                    items.Peek().DropDownItems.Add(item);
                                }else if (r.Name == "menu")
                                {
                                    StringBuilder sz = new StringBuilder();
                                    sz.Append(GlobalVar.ViewPath).Append(sd["startup"])
                                        .Append(".html?").Append(sd["query"]);
                                    startup = sz.ToString();
                                    //GlobalVar.Container = sd["locatebase"];
                                }
                                break;
                            }
                        case XmlNodeType.EndElement:
                            {
                                if (r.Name == "ItemGroup")
                                {
                                    items.Pop();
                                }
                                break;
                            }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }finally
            {
                if (r != null)
                    r.Close();
            }
           // MessageBox.Show(startup);
            if(String.IsNullOrEmpty(startup) == false)
            {
                wb.Navigate(startup);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null)
                return;
            StrDictionary sd = item.Tag as StrDictionary;
            if (sd == null)
                return;
            //wb.Navigate(@"open:///default/?script=calendar,default&invoke=MyCallback&css=default");
         
            if(sd.ContainsKey("page") && !sd["page"].Equals(String.Empty))
            {
                StringBuilder sz = new StringBuilder();
                sz.Append(GlobalVar.ViewPath).Append(sd["page"])
                    .Append(".html?").Append(sd["query"]);
                wb.Navigate(sz.ToString());
            }
        }
        private void ParseAttr(XmlReader r,out StrDictionary sd)
        {
            sd = new StrDictionary();
            StringBuilder sz = new  StringBuilder();
            while(r.MoveToNextAttribute())
            {
                //r.GetAttribute()
                //r.AttributeCount

                    sd.Add(r.Name, r.Value);
            }

            r.MoveToElement();
            
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string temppath = GlobalVar.AppPath + "temp";
            if(Directory.Exists(temppath))
                Directory.Delete(temppath, true);
            this.LogFrm.Close();
        }

        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            this.LogFrm.Visible = !this.Visible;
            StringBuilder sz = new StringBuilder();

            sz.Append("当前登录用户：").Append(GlobalVar.LogInfo.Name)
                .Append(" | ").Append("登录时间：").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            statusBar.Text = sz.ToString();
        }
        public void FreshPage()
        {
            wb.Refresh();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DirectoryInfo theFolder = new DirectoryInfo(GlobalVar.AppPath + @"backups");
            FileInfo[] files = theFolder.GetFiles("*.dat");
            if (files.Length >= 3)
            {
                List<string> dats = new List<string>();
                //dats.AddRange(new string[] { "20150112", "20150114", "20150116", "20150113", "20150110" });
                foreach (FileInfo fi in files)
                {
                    dats.Add(fi.FullName);
                }
                StringComparer sc = StringComparer.Ordinal;
                dats.Sort(sc);
                for(int i=0;i<(dats.Count -3);++i)
                    File.Delete(dats[i]);
            }
           
            string app = GlobalVar.AppPath + "backup.bat";
            Process p = new Process();
            ProcessStartInfo info = p.StartInfo;
            info.FileName = app;
            info.WorkingDirectory = GlobalVar.AppPath;
            info.UseShellExecute = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            if (MessageBox.Show("是否退出系统?", "温馨提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            //备份数据
            //GlobalVar.MainBrowser.Document.InvokeScript("SyncDataState", new object[] { "1"});
            //System.Threading.Thread.Sleep(5000);
            //statusBar.Text = "数据同步中，请稍候...";

            // bool rst = SyncDataHandler.SyncData(this);
            //if (rst == false)
            //{
            //    GlobalVar.MainBrowser.Document.InvokeScript("SyncDataState", new object[] { "0" });
            //   MessageBox.Show("数据同步失败，请检查日志", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

       
    }
}
