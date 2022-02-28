using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    public class BaseAerovoice
    {


        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static string UTF8ByteArrayToString(byte[] characters)
        {
           UTF8Encoding encoding = new UTF8Encoding();
           string constructedString = encoding.GetString(characters);
           return (constructedString);
        } 

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        private static byte[] StringToUTF8ByteArray(string pXmlString)
        {
           UTF8Encoding encoding = new UTF8Encoding();
           byte[] byteArray = encoding.GetBytes(pXmlString);
           return byteArray;
        } 

        /// <summary>
        /// Serialize an object into an XML string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T obj)
        {
           try
           {
              string xmlString = null;
              MemoryStream memoryStream = new MemoryStream();
              XmlSerializer xs = new XmlSerializer(typeof(T));
              XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
              xs.Serialize(xmlTextWriter, obj);
              memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
              xmlString = UTF8ByteArrayToString(memoryStream.ToArray());      return xmlString.Trim();
           }
           catch (Exception ex)
           {
              return string.Empty;
           }
        } 

        /// <summary>
        /// Reconstruct an object from an XML string
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string xml)
        {
           XmlSerializer xs = new XmlSerializer(typeof(T));
           MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(xml));
           XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
           return (T)xs.Deserialize(memoryStream);
        }


    }
}
