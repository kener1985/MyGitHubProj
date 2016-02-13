using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace UtilHelper.Converter
{
    public class JSonXmlConverter
    {
        /// <summary>
        /// Obj2s the json.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string Obj2Json(object obj,Encoding encoding)
        {
            string sJson = String.Empty;

            DataContractJsonSerializer js = new DataContractJsonSerializer(obj.GetType());
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            js.WriteObject(ms, obj);
            sJson = encoding.GetString(ms.ToArray());
                
            return sJson;
        }
        /// <summary>
        /// Json2s the obj.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static T Json2Obj<T>(string json, Encoding encoding) where T : class, new()
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
            byte[] bz = encoding.GetBytes(json);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bz);

            return js.ReadObject(ms) as T;
        }
        /// <summary>
        /// Obj2s the json.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string Obj2Json(object obj)
        {
            return Obj2Json(obj,Encoding.UTF8);
        }
        
        public static T Json2Obj<T>(string json) where T : class, new()
        {
            return Json2Obj<T>(json,Encoding.UTF8);
        }
        /// <summary>
        /// Json2s the XML.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string Json2Xml(string json, Encoding encoding)
        {
            System.Xml.XmlDictionaryReader reader =
              JsonReaderWriterFactory.CreateJsonReader(encoding.GetBytes(json),
              System.Xml.XmlDictionaryReaderQuotas.Max);
            reader.Read();

            return reader.ReadOuterXml();
        }
        /// <summary>
        /// Json2s the XML.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public static string Json2Xml(string json)
        {
            return Json2Xml(json,Encoding.UTF8);
        }
        /// <summary>
        /// XML2s the json.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static string Xml2Json(string xml,Encoding encoding)
        {
            //throw new NotImplementedException("Not Implemented");
            string json = String.Empty;
            XmlReaderSettings xrs = new XmlReaderSettings();
            xrs.CloseInput = true;
            xrs.IgnoreComments = true;
            xrs.IgnoreWhitespace = true;
            xrs.IgnoreProcessingInstructions = true;
            System.IO.StringReader sr = new System.IO.StringReader(xml);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            XmlDictionaryWriter writer =
             JsonReaderWriterFactory.CreateJsonWriter(ms, encoding, true);
            XmlReader xr = XmlReader.Create(sr, xrs);
            try
            {
                if (xr.ReadToFollowing("root") == false)
                    throw new NotSupportedException("Not the valid format");
                //写对象属性
                writer.WriteStartElement("root");
                if (xr.AttributeCount != 1)
                    throw new NotSupportedException("Not the valid format");
                xr.MoveToAttribute(0);
                writer.WriteAttributeString(xr.Name, xr.GetAttribute(0));
                while (xr.Read())
                {
                    switch (xr.NodeType)
                    {
                        case XmlNodeType.Element:
                            {
                                writer.WriteStartElement(xr.Name);
                                if (xr.AttributeCount != 1)
                                    throw new NotSupportedException("Not the valid format");

                                string attr = xr.GetAttribute(0);
                                xr.MoveToAttribute(0);
                                writer.WriteAttributeString(xr.Name, attr);
                                break;
                            }
                        case XmlNodeType.Text:
                        case XmlNodeType.CDATA:
                            {
                                string s = xr.ReadContentAsString();
                                writer.WriteString(s);
                                //内容节点后，必结束当前元素
                                writer.WriteEndElement();
                                break;
                            }
                        case XmlNodeType.EndElement:
                            {
                                writer.WriteEndElement();
                                break;
                            }
                    }
                }
                //writer.WriteEndElement();//结束 root
                writer.Flush();
                json = encoding.GetString(ms.ToArray());
            }
            catch (NotSupportedException ex)
            {
                throw ex;
            }
            catch (System.Exception)
            {
                //Tip.MB tip = new Tip.MB();
                //tip.Error(ex.Message);
            }
            finally
            {
                xr.Close();
                writer.Close();
            }          

            return json;
        }
        public static string Xml2Json(string xml)
        {
            return Xml2Json(xml,Encoding.UTF8);
        }
    }
}
