using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace BaseLib
{
    public class ScriptUtility
    {
        public static bool LoadJsFile(HtmlDocument doc, string js)
        {
            bool noErr = true;
            string file = String.Empty;
            try
            {
                string[] files = js.Split(new char[] { ',' });
                foreach (string f in files)
                {
                    file = GlobalVar.AppPath + "res\\" + f + ".js";
                    
                    string s = Utility.ReadFile(file);
                    noErr &= !String.IsNullOrEmpty(s);

                    if(String.IsNullOrEmpty(s))
                        GlobalVar.Log.LogError("取得文件内空为空！",file);

                    ExecScript(doc, s);

                }
                doc.Write("<script language='javascript'>alert('hello');</script>");
                
            }
            
            catch (System.Exception )
            {
                MessageBox.Show("load [" + file + "] error");
                return false;
            }
            return noErr;
        }
        public static object  ExecScript(HtmlDocument doc, string js)
        {
            object ret = null;
            if (String.IsNullOrEmpty(js) == false)
            {
                mshtml.IHTMLDocument2 d = doc.DomDocument as mshtml.IHTMLDocument2;
                mshtml.IHTMLWindow2 win = d.parentWindow as mshtml.IHTMLWindow2;
                ret = win.execScript(js, "javascript");
                
            }

            return ret;
        }
        

        
    }
}