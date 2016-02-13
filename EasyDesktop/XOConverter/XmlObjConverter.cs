using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;
using System.Reflection;

namespace HVH_Ken.XO
{
    public class XmlObjConverter
    {
        private string m_Root;
        private ArrayList m_Objects;


        public XmlObjConverter(string sRootName)
        {
            m_Root = sRootName;
            m_Objects = new ArrayList();

        }
        public void AddObject(object obj)
        {
            m_Objects.Add(obj);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            return base.ToString();
        }
        #region XML 转对象
        /// <summary>
        /// XML 转对象链.
        /// </summary>
        /// <param name="sXml">The s XML.</param>
        public ArrayList Xml2Objects(string sXml)
        {
            ArrayList list = new ArrayList();
            StringReader sr = new StringReader(sXml);
            XmlReaderSettings set = new XmlReaderSettings();

            set.IgnoreComments = true;
            set.CloseInput = true;
            set.CheckCharacters = false;
            XmlReader reader = XmlReader.Create(sr, set);
            object obj = null;

            try
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement() && reader.HasAttributes)
                    {
                        string flag = reader.GetAttribute(0);
                        if (flag.Equals("class"))
                        {
                            AssemblyName assName = new AssemblyName(reader.GetAttribute(1));
                            Assembly ass = Assembly.Load(assName);
                            obj = X2OSingle(reader, ass);
                            list.Add(obj);
                        }
                        else if (flag.Equals("classSerialized"))
                        {
                            obj = X2OSingleBySerialize(reader.ReadElementString());
                            list.Add(obj);
                        }
                    }
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("发生错误，数据可能被篡改！");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
           
            return list;
        }
        /// <summary>
        /// 将XML配置转成单个对象.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="sOuterXml">The s outer XML.</param>
        private object X2OSingle(XmlReader reader, Assembly ass)
        {
            string sClzNam = reader.Name;
            string sType;
            object value = null;
            object clz = ass.CreateInstance(sClzNam);
            do
            {
                reader.Read();
                if (reader.IsStartElement())
                {
                    try
                    {
                        FieldInfo f = clz.GetType().GetField(reader.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                        if (f == null)
                            continue;
                        if (Attribute.IsDefined(f, typeof(RecursionAttribute)))
                        {
                            value = X2OSingle(reader, ass);
                        }
                        else if (Attribute.IsDefined(f, typeof(AsSerializeAttribute)))
                        {
                            reader.Read();
                            value = X2OSingleBySerialize(reader.Value);
                            reader.Read();
                        }
                        else
                        {
                            sType = reader.GetAttribute(1);
                            reader.Read();
                            value = Convert.ChangeType(reader.Value, Type.GetType(sType));
                            reader.Read();
                        }
                        f.SetValue(clz, value);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            } while (reader.Name.Equals(sClzNam) == false);
            return clz;
        }
        private object X2OSingleBySerialize(string sObjSrc)
        {
            if (sObjSrc.Equals(String.Empty))
                return null;
            object obj;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
                                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            byte[] b = Encoding.GetEncoding(28591).GetBytes(sObjSrc);
            MemoryStream ms = new MemoryStream(b);

            obj = bf.Deserialize(ms);
            return obj;
        }
        #endregion
        #region 对象转 XML
        /// <summary>
        /// 将对象链转成XML文档.
        /// </summary>
        /// <returns></returns>
        public string Objects2Xml()
        {
            StringBuilder sb = new StringBuilder(1024);
            if (m_Objects == null || m_Objects.Count == 0)
                return String.Empty;
            XmlWriterSettings set = new XmlWriterSettings();
            StringWriter sw = new StringWriter(sb);
            set.Encoding = Encoding.UTF8;// Encoding.GetEncoding(28591);
            //set.Indent = true;
            set.CheckCharacters = false;//不检查特殊字符
            set.CloseOutput = true;

            XmlWriter writer = XmlWriter.Create(sw, set);
            try
            {
                writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                writer.WriteStartElement(m_Root);
                foreach (object obj in this.m_Objects)
                {
                    if (obj == null)
                        continue;
                    O2XSingle(writer, obj);
                }
            }
            catch (ArgumentException)
            {
                System.Windows.Forms.MessageBox.Show("检查XML根节点名称是否符合XML规范");
            }
            catch (Exception)
            {
                return String.Empty;
            }
            finally
            {
                writer.WriteEndElement();
                writer.Flush();
                if (writer != null)
                    writer.Close();
            }



            return sb.ToString();
        }

        private void O2XSerializeSingle(XmlWriter writer, string sName, object value)
        {
            if (value == null)
                return;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, value);
            ms.Flush();
            byte[] bytes = ms.ToArray();
            ms.Close();

            writer.WriteStartElement(sName);
            writer.WriteAttributeString("flag", "classSerialized");
            writer.WriteString(Encoding.GetEncoding(28591).GetString(bytes));
            writer.WriteEndElement();
        }
        /// <summary>
        /// 将单个对象转成XML数据.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="obj">The obj.</param>
        private void O2XSingle(XmlWriter writer, object obj)
        {
            if (obj == null)//递归时对象可能为null
                return;
            Type type = obj.GetType();
            writer.WriteStartElement(type.FullName);
            //写类
            writer.WriteAttributeString("flag", "class");
            writer.WriteAttributeString("assmbly", type.Assembly.FullName);

            //写类成员
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo f in fields)
            {

                if (Attribute.IsDefined(f, typeof(IgnoreAttribute)))
                {
                    continue;
                }
                try
                {
                    if (Attribute.IsDefined(f, typeof(AsSerializeAttribute)))//类类型
                    {
                        //如果为序列化对象，不需进行递归处理
                        O2XSerializeSingle(writer, f.Name, f.GetValue(obj));
                    }
                    else if (Attribute.IsDefined(f, typeof(RecursionAttribute)))
                    {
                        O2XSingle(writer, f.GetValue(obj));
                    }
                    else
                    {
                        object valueOjb = f.GetValue(obj);
                        if (valueOjb == null)
                            continue;
                        writer.WriteStartElement(f.Name);
                        writer.WriteAttributeString("flag", "field");
                        writer.WriteAttributeString("type", f.FieldType.ToString());
                        writer.WriteString(valueOjb.ToString());
                        writer.WriteEndElement();
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            writer.WriteEndElement();//关闭类
        }
        #endregion
        public object this[int iIdx]
        {
            get
            {
                if (iIdx >= m_Objects.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return m_Objects[iIdx];
            }
            set
            {
                if (iIdx >= m_Objects.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                m_Objects[iIdx] = value;
            }
        }
    }

}
