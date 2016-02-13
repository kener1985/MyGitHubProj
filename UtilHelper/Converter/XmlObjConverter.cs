using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;
using System.Reflection;

namespace UtilHelper.Converter
{
    /// <summary>
    /// 对象与XML之间转换。默认为对象的字段，如果忽略某个字段，用[Ignore]标注;如果字段为类且不想串化成二进制
    /// 用[Recursion]标注;　如果想串化成二进制串，用[AsSerialize]标注。
    /// 对象可通过AddObject添加进序列化队列，反序列化可通过下标或Result进行访问
    /// 自定义序列化过程时，可实现ISerialzable接口接口，反序列化时，只需实现一个参数和ISerialzable.GetObjectData
    /// 参数表一样的构造函数便可
    /// </summary>
    public class XmlObjConverter 
    {
        private string m_Root;
        private ArrayList m_ObjBuffer;


        public XmlObjConverter(string sRootName)
        {
            m_Root = sRootName;
            m_ObjBuffer = new ArrayList();

        }
        public void AddObject(object obj)
        {
            m_ObjBuffer.Add(obj);
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
        public XmlObjConverter Xml2Objects(string sXml)
        {
            StringReader sr = new StringReader(sXml);
            XmlReaderSettings st = new XmlReaderSettings();

            st.IgnoreComments = true;
            st.CloseInput = true;
            st.CheckCharacters = false;
            st.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(sr, st);
            object obj = null;
            m_ObjBuffer.Clear();
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
                            m_ObjBuffer.Add(obj);
                        }
                        else if (flag.Equals("classSerialized"))
                        {
                            obj = X2OSingleBySerialize(reader.ReadElementString());
                            m_ObjBuffer.Add(obj);
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
           
            return this;
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
                    FieldInfo f = null;
                    try
                    {
                        f = clz.GetType().GetField(reader.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                        if (f == null)
                            continue;
                        if (Attribute.IsDefined(f, typeof(RecursionAttribute)))
                        {
                            value = X2OSingle(reader, ass);
                        }
                        else if (Attribute.IsDefined(f, typeof(AsSerializeAttribute)))
                        {
                            value = X2OSingleBySerialize(reader.ReadString());
                        }
                        else
                        {
                            sType = reader.GetAttribute(1);
                            //reader.Read();
                            value = Convert.ChangeType(reader.ReadString(), Type.GetType(sType));
                            //reader.Read();
                        }
                        f.SetValue(clz, value);
                    }catch(System.Runtime.Serialization.SerializationException)
                    {
                        System.Windows.Forms.MessageBox.Show(sClzNam + "." + f.Name + " 反序列化失败！");
                        continue; 
                    }
                    catch (Exception )
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
            if (m_ObjBuffer == null || m_ObjBuffer.Count == 0)
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
                foreach (object obj in this.m_ObjBuffer)
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
                if (writer != null)
                {
                    if(writer.WriteState != WriteState.Error)
                    {
                        writer.WriteEndElement();
                        writer.Flush();
                    }
                    writer.Close();
                }
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
                    catch(System.Runtime.Serialization.SerializationException ex )
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    continue;
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
                if (iIdx >= m_ObjBuffer.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return m_ObjBuffer[iIdx];
            }
            set
            {
                if (iIdx >= m_ObjBuffer.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                m_ObjBuffer[iIdx] = value;
            }
        }
        public object this[Type type]
        {
            get
            {
                foreach(object obj in m_ObjBuffer)
                {
                    if (obj.GetType().Equals(type))
                        return obj;
                }
                return null;
            }
        }
        public ArrayList Result
        {
            get { return this.m_ObjBuffer; }
        }

    }

}
