using OliverFida.Base;
using OliverFida.FSimMan.Exceptions.Config;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OliverFida.FSimMan.Client
{
    internal static class ConfigFileClient
    {
        #region Methods INTERNAL
        internal static T DeserializeFile<T>(string fileName) where T : DataObjectBase
        {
            T? data;
            try
            {
                using (XmlReader reader = XmlReader.Create(ResolveFilePath(fileName)))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    try
                    {
                        data = serializer.Deserialize(reader) as T;
                    }
                    catch (InvalidOperationException ex) when (ex.InnerException is XmlException)
                    {
                        throw new InvalidConfigFileException(fileName);
                    }
                }
            }
            catch (Exception ex) when (ex is not OFException)
            {
                return Activator.CreateInstance<T>();
            }

            if (data == null) throw new InvalidConfigFileException(fileName);
            return data;
        }

        internal static T Deserialize<T>(Stream stream, string fileName) where T : DataObjectBase
        {
            T? data;
            try
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    try
                    {
                        data = serializer.Deserialize(reader) as T;
                    }
                    catch (InvalidOperationException ex) when (ex.InnerException is XmlException)
                    {
                        throw new InvalidConfigFileException(fileName);
                    }
                }
            }
            catch (Exception ex) when (ex is not OFException)
            {
                return Activator.CreateInstance<T>();
            }

            if (data == null) throw new InvalidConfigFileException(fileName);
            return data;
        }

        internal static void SerializeFile<T>(string fileName, T data) where T : DataObjectBase
        {
            FileStream fileStream = File.Create(ResolveFilePath(fileName));

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(fileStream, data);

            fileStream.Close();
        }

        internal static void Serialize<T>(ref Stream stream, T data) where T : DataObjectBase
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, data);
        }
        #endregion

        #region Methods PRIVATE
        private static string ResolveFilePath(string fileName)
        {
            if (!Directory.Exists(CurrentApplication.CONFIG_PATH)) Directory.CreateDirectory(CurrentApplication.CONFIG_PATH);
            return Path.Combine(CurrentApplication.CONFIG_PATH, fileName);
        }
        #endregion
    }
}
