using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace UtilHelper.Http
{
    public class HttpComm
    {
        private string _Host;
        public HttpComm(string host)
        {
            _Host = host;
        }
        public string PostRequest(Dictionary<string,string> querystring)
        {
            return PostRequest(querystring, Encoding.UTF8);
        }
        public string PostRequest(Dictionary<string, string> querystring, Encoding enc)
        {
            //System.Web.HttpUtility.HtmlEncode()
            
            string resStr = String.Empty;
            Stream sRes = null;
            StreamReader sr = null;
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            try
            {
                req = (HttpWebRequest)HttpWebRequest.Create(_Host);
                StringBuilder sz = new StringBuilder();
                foreach (KeyValuePair<string,string> kvp in querystring)
                {
                    sz.Append(kvp.Key).Append('=').Append(System.Web.HttpUtility.UrlEncode(kvp.Value, enc)).Append("&");
                }
                
                byte[] bz = enc.GetBytes(sz.ToString());

                req.Method = "Post";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = bz.Length;
                using (Stream sReq = req.GetRequestStream())
                {
                    sReq.Write(bz, 0, bz.Length);
                    sReq.Flush();
                }

                res = (HttpWebResponse)req.GetResponse();
                if (res.StatusCode != HttpStatusCode.OK)
                    throw new WebException("通讯错误");

                sRes = res.GetResponseStream();
                sr = new StreamReader(sRes);
                resStr = sr.ReadToEnd();
            }
            catch (System.Exception)
            {
                
            }
            finally
            {
                if(sr != null)
                    sr.Close();
                if(res != null)
                    res.Close();
            }
            
            return resStr;
        }
    }
}
