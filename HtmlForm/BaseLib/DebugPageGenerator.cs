using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib
{
    public class DebugPageGenerator
    {
        private FormBrowser _wb;
        public DebugPageGenerator(FormBrowser wb)
        {
            this._wb = wb;
        }
        public void Generate(StrDictionary sd)
        {
            string file = GlobalVar.AppPath + "__debug_html.html";
            char[] sep = { ',' };
            string[] js = (sd["script"] + ",common").Split(sep);
            string[] css = sd["css"].Split(sep);
            string body = this._wb.Document.Body.OuterHtml;
            
            StringBuilder sz = new StringBuilder(256);
            sz.AppendLine("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");

            sz.AppendLine("<HTML>");
            sz.AppendLine(" <HEAD>");
            sz.AppendLine("  <TITLE> Debug page </TITLE>\r\n");
            AddCss(sz, css);
            AddScript(sz, js);
            sz.AppendLine(body);
            sz.AppendLine("</HTML>");
            System.IO.File.WriteAllText(file, sz.ToString(), Encoding.UTF8);
        }
        private void AddScript(StringBuilder sz, string[] js)
        {
            foreach (string s in js)
            {
                string path = GlobalVar.AppPath + s + ".js";
                sz.Append("  <SCRIPT LANGUAGE=\"JavaScript\" src=\"");
                sz.Append(path);

                sz.AppendLine("\"></SCRIPT>");
            }
        }
        private void AddCss(StringBuilder sz, string[] css)
        {
            foreach (string s in css)
            {

                string path = GlobalVar.AppPath + s + ".css";
                sz.Append("  <link  rel=\"stylesheet\" type=\"text/css\" href=\"");
                sz.Append(path);

                sz.AppendLine("\"></link>");
            }
        }
    }

}
