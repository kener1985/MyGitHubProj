using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace UtilHelper.StringRegrex
{
    public class SynbolReplaceHelper
    {
        private static StringDictionary m_dSingle;
        private static List<StringDictionary> m_lMulti;
        private static StringDictionary m_Context;//当前上下文值容器
        
        private static Regex reg ;
        private SynbolReplaceHelper(){}
        static SynbolReplaceHelper()
        {
            string pattern = @"(\$\w+\$)|(?<full><multi>(?<value>(.|\r|\n)*)</multi>)";
            reg = new Regex(pattern, RegexOptions.IgnoreCase);
            m_dSingle = null;
            m_lMulti = null;
            m_Context = null;
        }
        public static string Replace(string sOri, StringDictionary values)
        {
            m_Context = values;
            m_dSingle = values;
            return reg.Replace(sOri, ProcessSingle);
        }
        public static string Replace(string sOri, StringDictionary values, List<StringDictionary> rows)
        {
            m_lMulti = rows;
            return Replace(sOri, values);
        }
        /// <summary>
        /// Processes the value.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <returns></returns>
        private static string ProcessSingle(Match m)
        {
            if (String.IsNullOrEmpty(m.Groups["full"].Value) == false)
            { 
                //处理多行记录,去掉多行记录标记<multi></multi>
                return ProcessMulti(m.Groups["value"].Value, m_lMulti);
            }
            
            string sign = m.Value;
            int iLen = sign.Length;

            if (iLen < 2)
                return string.Empty;

            string key = sign.Substring(1, iLen - 2);
            if (m_Context == null || 
                m_Context.ContainsKey(key) == false)
                return string.Empty;

            return m_Context[key];
            
        }
        private static string ProcessMulti(string rec, List<StringDictionary> values)
        {
            StringBuilder sb = new StringBuilder();
            foreach (StringDictionary row in values)
            {
                m_Context = row;//当前上下文为列表中的该行
                sb.Append(reg.Replace(rec, ProcessSingle));
            }
            m_Context = m_dSingle;//默认上下文
            return sb.ToString();
        }
        
    }
    public class HttpUnescape
    {
        private Regex reg ;
        private Encoding _Encoding ;
        public HttpUnescape(Encoding encoding)
        {
            string pattern = @"%u[0-9a-f]{4}";
            reg = new Regex(pattern, RegexOptions.IgnoreCase);
            _Encoding = encoding;
        }
        public string Escape(string sOri)
        {
            return reg.Replace(sOri,MatchOne);
        }
        private string MatchOne(Match m)
        {
            byte[] b = new byte[2];
            b[0] = Convert.ToByte(m.Value.Substring(4, 2), 16);
            b[1] = Convert.ToByte(m.Value.Substring(2, 2), 16);

            return _Encoding.GetString(b);
        }
        
    }
}
