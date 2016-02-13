using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseLib
{
    public class FormBrowser : System.Windows.Forms.WebBrowser
    {
        //由于可能打开多个（继承自）MainForm，因此该id不在于GlobalVar中
        //private string _PageId;
        public string PageId { get; set; }
        private bool _bInited;
        private string currentFrameName;
        //private string _RequestId;//
        public FormBrowser()
        {
            this.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wb_Navigating);
            this.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
            _bInited = false;
            string p = Application.StartupPath;
            int idx = p.LastIndexOf('\\');
            if (idx != p.Length-1)
                p += @"\";
            GlobalVar.AppPath =p;
            this.AllowWebBrowserDrop = false;
            this.currentFrameName = null;
        }

        void wb_BeforeNavigate2(object pDisp, ref object URL, ref object Flags, ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel)
        {
            //_RequestId = "";
            StrDictionary sd = new StrDictionary();
            byte[] b = PostData as byte[];
            string surl = URL as string;
            int flag = (int)Flags;
            if (TargetFrameName != null)
                currentFrameName = TargetFrameName.ToString();
            if (b == null)//post 或 get时，该值不为0
                return;

            //sd.Add("pageid", _PageId);
            int i = surl.IndexOf('?');
            if (GlobalVar.Container == null)
                GlobalVar.Container = GetCurrentContainer(TargetFrameName.ToString());
            try
            {
                if (i != -1)
                    Utility.ParseQuery(surl.Substring(i + 1), sd);

                string action = GlobalVar.Container.InvokeScript("GetAction") as string;
                if (sd.ContainsKey("action"))
                    action = sd["action"];

                if (String.IsNullOrEmpty(action))
                    action = "Default";
                
                if (b != null)
                {
                    string post = Encoding.UTF8.GetString(b).Trim(new char[]{'\0'});

                    Utility.ParseQuery(post, sd);
                    //GlobalVar.Actions["post"].DoAction(url, sd);
                    GlobalVar.Log.LogDebug("InvokeAction [post]", surl);
                    Utility.InvokeAction(action, "post", sd);
                    Cancel = true;
                }
                else if (flag != 0) //GET方法 
                {
                    Utility.InvokeAction(action,"get", sd);
                    GlobalVar.Log.LogDebug("InvokeAction [get]", surl);
                    Cancel = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            
        }

        /** 初始都是open命令打开的容器 **/
        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            

            //if (e.Url.OriginalString != _RequestId)
            //    return;
            
            //_RequestId = ""; 
            HtmlDocument doc = GlobalVar.Container;
            //页面初始化
            if (doc == null && this.Document != null)
            {
                string param = "";
                int idx = e.Url.OriginalString.IndexOf('?');
                if (idx != -1)
                    param = e.Url.OriginalString.Substring(idx);

                this.Document.InvokeScript("InitPage", new object[] { param });

            }
            if (doc == null || doc.Url.Scheme == "res")
            {
                FormBrowser fb = sender as FormBrowser;
                if (fb != null && fb.Document != null)
                {
                    fb.Document.ContextMenuShowing += new HtmlElementEventHandler(Document_ContextMenuShowing);
                }
                
                return;
            }
            //屏蔽鼠标右键
            doc.ContextMenuShowing += new HtmlElementEventHandler(Document_ContextMenuShowing);
            StrDictionary sd = new StrDictionary();

            try
            {

                Utility.ParseQuery(Utility.GetQueryStr(e.Url.OriginalString), sd);
                
                //加载javascript脚本   
                //bool noError = true;
                //noError = ScriptUtility.LoadJsFile(doc, "baselib,basefunc");
                mshtml.IHTMLDocument2 d = doc.DomDocument as mshtml.IHTMLDocument2;
                
                //容器不允许出现脚本，主要考虑代码安全(尚待改进，脚本还可以在元素的事件中执行)
                //if (!noError || d.scripts.length != 0)
                //{
                   // MessageBox.Show("脚本异常");
                    //这里可以跳到错误页面
                    //this.Navigate("about:blank");
                    //this.DocumentText = "";
                //}

                //if (sd.ContainsKey("script"))
                //    ScriptUtility.LoadJsFile(doc, sd["script"]);


                //加载css文件，必须在脚本加载完毕后加载
                //if (sd.ContainsKey("css"))
                //{
                //    string[] csses = sd["css"].Split(new char[] { ',' });
                //    foreach (string css in csses)
                //    {
                //        string path = GlobalVar.AppPath + css + ".css";
                //        if (System.IO.File.Exists(path))
                //            doc.InvokeScript("LoadCssFile", new object[] { path });
                //    }
                //}
                //回调脚本函数
                if (sd.ContainsKey("invoke") && sd["invoke"] != "")
                {
                    CallInFrame(sd);
                }
                //当前页面缓存用
                CacheManager.ClearPageCache(PageId);
                PageId = DateTime.Now.Ticks.ToString();
                GlobalVar.Container = null;
         

#if DEBUG
                DebugPageGenerator dpg = new DebugPageGenerator(this);
                dpg.Generate(sd);
               
#else
                ScriptUtility.ExecScript(this.Document,"window.alert = function(){}");
#endif
                GC.Collect();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void CallInFrame(StrDictionary sd)
        {
            try
            {
                HtmlDocument doc = GlobalVar.Container; 
                StringBuilder sz = new StringBuilder();
                foreach (KeyValuePair<string, string> kv in sd)
                {
                    sz.Append(kv.Key).Append('=').Append(kv.Value).Append('&');
                    
                }
                doc.InvokeScript(sd["invoke"], new object[] { sz.ToString().Substring(0,sz.Length-1) });
            }catch(Exception )
            {
                MessageBox.Show("frame 无效!");
            }
        }
        void Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
        {
#if NO_RBUTTON
            HtmlElement he = this.Document.GetElementFromPoint(e.MousePosition);
            if (he.TagName.ToLower() != "input")
                e.ReturnValue = false;
#endif
        }

        private void wb_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //if (String.IsNullOrEmpty(_RequestId) == false && _RequestId != e.Url.OriginalString)
            //    return;

            if (_bInited == false)
            {
                SHDocVw.WebBrowser shwb = (SHDocVw.WebBrowser)this.ActiveXInstance;
                shwb.BeforeNavigate2 += new SHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(wb_BeforeNavigate2);
                _bInited = true;
            }
            //MessageBox.Show(e.Url.ToString());
            GlobalVar.Container = GetCurrentContainer(e.TargetFrameName);
            if (e.Url.Scheme.ToLower().Equals("receipt") == true)
            {
                e.Cancel = true;
                StrDictionary sd = new StrDictionary();
                try
                {
                    Utility.ParseQuery(Utility.GetQueryStr(e.Url.OriginalString), sd);
                    //sd.Add("pageid", _PageId);
                    GlobalVar.Log.LogDebug("InvokeAction", e.Url.OriginalString);
                    string action = e.Url.Host;
                    
                    string cmd = e.Url.LocalPath;
                    if(String.IsNullOrEmpty(cmd))
                    {
                        GlobalVar.Log.LogError("invalid action!");
                        return;
                    }
                    Utility.InvokeAction(action,cmd.Trim(new char[]{'/','\\'}), sd);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }else
            {
                //_RequestId = e.Url.OriginalString;
            }
           

            
        }
        private HtmlDocument GetCurrentContainer(string framename)
        {
            HtmlWindowCollection hwc = this.Document == null ? null : this.Document.Window.Frames;
           
            HtmlDocument doc = this.Document;
            if (String.IsNullOrEmpty(framename.Trim()) ||
                hwc == null ||
                hwc.Count == 0 
                )
                return this.Document;

            try
            {
                doc = hwc[framename].Document;
            }
            catch (Exception)
            {
                doc = this.Document;
            }
            return doc;
        }
        void Document_MouseOver(object sender, HtmlElementEventArgs e)
        {
            MessageBox.Show("over");
        }

        void Window_Load(object sender, HtmlElementEventArgs e)
        {
            MessageBox.Show("load");
        }

        void FormBrowser_Load(object sender, HtmlElementEventArgs e)
        {
            MessageBox.Show("load");
        }
    }
}
