using System;
using System.IO;
using System.Xml.Serialization;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions.Utilty
{
    public class XmlHelper
    {
        public static string GenerateXml<T>(T obj, Type[] types)
        {
            XmlSerializer serializer = types == null ? new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T), types);

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, (T)obj);
                return sw.ToString();
            }

        }


        public static void GenerateXml<T>(T obj, string xmlPath, Type[] types)
        {
            XmlSerializer serializer = types == null ? new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T), types);
            if (File.Exists(xmlPath))
                File.Delete(xmlPath);
            using (StreamWriter sw = new StreamWriter(xmlPath, false))
            {
                serializer.Serialize(sw, (T)obj);
            }

        }

        public static T LoadFromXml<T>(string xml, Type[] types)
        {
            XmlSerializer serializer = types == null ? new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T), types);

            using (StringReader xr = new StringReader(xml))
            {
                return (T)serializer.Deserialize(xr);
            }
        }

        public static void SerializeToFile<T>(T obj, string filePath, Type[] types)
        {
            XmlSerializer serializer = types == null ? new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T), types);
            if (File.Exists(filePath))
                File.Delete(filePath);
            using (StreamWriter sr = new StreamWriter(filePath, false))
            {
                serializer.Serialize(sr, obj);
            }
        }

        public static T DeserializeFromFile<T>(string filePath, Type[] types)
        {
            XmlSerializer serializer = types == null ? new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T), types);
            //Check.Require(File.Exists(filePath), "Can not find file");
            using (StreamReader sr = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(sr);
            }
        }

        public static T DeserializeFromFile<T>(Stream stream, Type[] types)
        {
            XmlSerializer serializer = types == null ? new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T), types);
            return (T)serializer.Deserialize(stream);
        }
    }
}
