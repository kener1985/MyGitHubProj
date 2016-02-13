using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Resources;
using UtilHelper.Database;
using System.Data;
namespace HVH_Ken_Modules
{
    public class XmlConfigAccessor
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private string m_Path;
        private string m_Key;
        public XmlConfigAccessor(string path)
        {
            this.m_Path = path;
            m_Key = GlobalVar.Instanse.SecurityKey;//密钥
        }
        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ConfigData> Read()
        {
            Dictionary<string, ConfigData> datas = null;
            XmlReader reader = null;
            ConfigData data = null;
            SecurityOpr so = null;
            StringReader sr = null;
            try
            {
                datas = new Dictionary<string, ConfigData>();
                so = new SecurityOpr(m_Key);
                //从加密文件中读取出数据，并进行解密
                string buf = so.ReadFromFile(m_Path);
                if (buf.Equals(String.Empty))
                    return datas;
                //去掉XML文件结尾的一些无效的字符，因为在解密过程中，从内存取出的数据为原始字节，
                //因此字节串的长度应该等于２的指数，否则系统会自动在字节串结尾加空字节
                int pos = buf.LastIndexOf('>');
                if (pos != -1)
                    sr = new StringReader(buf.Substring(0, pos + 1));
                else
                    sr = new StringReader(buf);
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.CheckCharacters = false;
                settings.CloseInput = true;
                reader = XmlReader.Create(sr, settings);
                while (reader.Read())
                {
                    if (reader.Name.Equals("program") && reader.IsStartElement())
                    {
                        data = new ConfigData();
                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                Type type = typeof(ConfigData);
                                FieldInfo finfo = type.GetField(reader.Name, BindingFlags.Instance | BindingFlags.NonPublic);

                                if (finfo != null)
                                {
                                    object value = Convert.ChangeType(reader.Value, finfo.FieldType);
                                    finfo.SetValue(data, value);
                                }
                            }
                        }
                    }
                    else if (reader.Name.Equals("path") && reader.IsStartElement())
                    {
                        reader.Read();
                        data.Path = reader.Value.Trim();
                        reader.Read();
                    }
                    else if (reader.Name.Equals("shortcut") && reader.IsStartElement())
                    {
                        reader.Read();
                        data.Shortcut = reader.Value.Trim();
                        reader.Read();
                    }
                    else if ((reader.Name.Equals("notice") || reader.Name.Equals("taskitem"))
                                && reader.IsStartElement())
                    {
                        ITrigerable item;
                        Type type;
                        string name = reader.Name;
                        if (name.Equals("taskitem"))
                        {
                            item = new TaskItem();
                            type = typeof(TaskItem);
                        }
                        else
                        {
                            item = new Notice();
                            type = typeof(Notice);
                        }

                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {

                                FieldInfo finfo = type.GetField(reader.Name, BindingFlags.Instance | BindingFlags.NonPublic);

                                if (finfo != null)
                                {
                                    object value = Convert.ChangeType(reader.Value, finfo.FieldType);
                                    finfo.SetValue(item, value);
                                }
                            }
                        }
                        //多一份拷贝，否则在修改运行任务的时候，扫描对象和修改对象为同一个，有可能会造成冲突
                        //虽然冲突没有什么大问题，现在的概率也较小，但这样不太好。
                        //if (name.Equals("taskitem"))
                        //    data.TaskItem = item.Clone() as TaskItem;

                        GlobalVar.Instanse.Trigers.Add(item);
                    }
                    else if (reader.Name.Equals("program") && !reader.IsStartElement())
                    {
                        datas.Add(data.Shortcut, data);
                    }
                }
            }
            catch (FileNotFoundException)
            {

            }
            catch (System.Xml.XmlException)
            {
                GlobalVar.Tip.Error("XML文档格式错误");
            }
            catch (Exception ex)
            {

                GlobalVar.Tip.Error(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            GlobalVar.Helper.AddInsert("programs", "shortcut,path,is_auto_run,title");
                DataTable dt = new DataTable("programs");
                GlobalVar.Helper.MakeSchemaFromObj(dt, typeof(ConfigData));

                foreach (KeyValuePair<string, ConfigData> pair in datas)
                {

                    ConfigData d = pair.Value;
                    GlobalVar.Helper.FillDataFromObj(dt, d);
                }
                GlobalVar.Helper.Update(dt);
            return datas;
        }

        /// <summary>
        /// Writes the specified buf.
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <returns></returns>
        public bool Write(Dictionary<string, ConfigData> buf)
        {
            XmlWriter writer = null;
            StringWriter sw = null;
            SecurityOpr so = new SecurityOpr(m_Key);
            try
            {
                XmlWriterSettings setting = new XmlWriterSettings();
                setting.Encoding = Encoding.UTF8;
                setting.CloseOutput = true;
                setting.CheckCharacters = false;
                StringBuilder sb = new StringBuilder();
                sw = new StringWriter(sb);
                //setting.Indent = true;
                //writer = XmlWriter.Create(sw, setting);

                //setting.Encoding = Encoding.UTF8;
                //writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                //writer.WriteStartElement("pr ograms");
                GlobalVar.Helper.AddInsert("programs", "shortcut,path,is_auto_run,title");
                DataTable dt = new DataTable("programs");
                GlobalVar.Helper.MakeSchemaFromObj(dt, typeof(ConfigData));

                foreach (KeyValuePair<string, ConfigData> pair in buf)
                {

                    ConfigData data = pair.Value;
                    GlobalVar.Helper.FillDataFromObj(dt, data);
                    continue;
                    Type type = typeof(ConfigData);

                    writer.WriteStartElement("program");
                    //写属性
                    WriteAttributes(writer, type, data);
                    //FieldInfo[] fis = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                    //foreach (FieldInfo fi in fis)
                    //{
                    //    if (fi.Name.StartsWith("_"))//以_开头的为Attribute
                    //    {
                    //        value = fi.GetValue(data).ToString();
                    //        writer.WriteAttributeString(fi.Name, value);
                    //    }
                    //}
                    //path
                    writer.WriteStartElement("path");
                    writer.WriteString(data.Path);
                    writer.WriteEndElement();
                    //shortcup
                    writer.WriteStartElement("shortcut");
                    writer.WriteString(data.Shortcut);
                    writer.WriteEndElement();
                    //taskitem
                    //if (data.TaskItem != null)
                    //{
                    //    writer.WriteStartElement("taskitem");
                    //    WriteAttributes(writer, typeof(TaskItem), data.TaskItem);
                    //    writer.WriteEndElement();//end taskitem
                    //}
                    writer.WriteEndElement();//end program
                }
                
                GlobalVar.Helper.Update(dt);
                //notice
                //writer.WriteStartElement("notices");
                //foreach (ITrigerable item in GlobalVar.Instanse.Trigers)
                //{
                //    if (item is Notice)
                //    {
                //        writer.WriteStartElement("notice");
                //        WriteAttributes(writer, typeof(Notice), item);
                //        writer.WriteEndElement();//end notice
                //    }
                //}
                //writer.WriteEndElement();//end notices

                //writer.WriteEndElement();//end programs
                //writer.Flush();
                ////加密并保存到文件
                //so.WriteToFile(m_Path, sb.ToString());
            }
            catch (Exception ex)
            {
                GlobalVar.Tip.Error(ex.Message);
                //throw ex;
                return false;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

            return true;
        }
        private void WriteAttributes(XmlWriter writer, Type type, object obj)
        {

            FieldInfo[] fis = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo fi in fis)
            {
                if (fi.Name.StartsWith("_"))
                {
                    string value = fi.GetValue(obj).ToString();
                    writer.WriteAttributeString(fi.Name, value);
                }
            }

        }
    }

    /// <summary>
    /// 资源文件操作
    /// </summary>
    public class ResxCfgAccessor
    {
        private string m_StrPath;
        private Dictionary<string, object> m_cfgDatas;
        private ResXResourceWriter m_Writer = null;
        ResXResourceReader m_Reader = null;
        public ResxCfgAccessor(string path)
        {
            this.m_StrPath = path;
            m_cfgDatas = new Dictionary<string, object>();
        }
        /// <summary>
        /// 根据strKey键值获得配置值.
        /// </summary>
        /// <param name="strKey">The STR key.</param>
        /// <returns></returns>
        public object GetResource(string strKey)
        {
            object valObj = null;
            if (m_cfgDatas == null)
                return null;
            try
            {
                valObj = this.m_cfgDatas[strKey];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
            return valObj;
        }
        public void SetResource(string strKey, object obj)
        {
            if (m_cfgDatas == null)
                return;
            m_cfgDatas[strKey] = obj;
        }


        /// <summary>
        /// 加载配置数据.如果加载失败，将抛异常
        /// </summary>
        public void LoadConfig()
        {

            SecurityOpr secOpr = new SecurityOpr(GlobalVar.Instanse.SecurityKey);
            string Datas = secOpr.ReadFromFile(m_StrPath);
            StringReader reader = new StringReader(Datas);
            using (m_Reader = new ResXResourceReader(reader))
            {
                try
                {
                    foreach (DictionaryEntry item in m_Reader)
                        m_cfgDatas[item.Key.ToString()] = item.Value;
                }
                catch (FileNotFoundException)
                { }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            //finally
            //{
            //    if (m_Reader != null)
            //        m_Reader.Close();
            //}

        }

        /// <summary>
        /// 保存配置数据.如果加载失败，将抛异常
        /// </summary>
        public void Save()
        {
            SecurityOpr secOpr = null;
            StringBuilder sb = null;
            StringWriter stringWriter = null;
            try
            {
                secOpr = new SecurityOpr(GlobalVar.Instanse.SecurityKey);
                sb = new StringBuilder();
                stringWriter = new StringWriter(sb);
                m_Writer = new ResXResourceWriter(stringWriter);
                foreach (KeyValuePair<string, object> item in m_cfgDatas)
                    m_Writer.AddResource(item.Key, item.Value);
                m_Writer.Generate();//将数据写入字符串流

                //将数据进行加密写入文件
                secOpr.WriteToFile(m_StrPath, sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (m_Writer != null)
                    m_Writer.Close();
                if (stringWriter != null)
                    stringWriter.Close();
            }
        }
        public bool IsFieldExist(string sFieldKey)
        {
            bool bExist;
            bExist = m_cfgDatas.ContainsKey(sFieldKey) && m_cfgDatas[sFieldKey] != null;
            return bExist;

        }

    }
}
