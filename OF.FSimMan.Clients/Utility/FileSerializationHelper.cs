using OF.Base.Objects;
using OF.FSimMan.Management.Exceptions;
using System.Xml;
using System.Xml.Serialization;

namespace OF.FSimMan.Client.Utility
{
    internal static class FileSerializationHelper
    {
        public static T DeserializeConfigFile<T>(string fileName) where T : DataObject
        {
            string filePath = GetConfigFilePath(fileName);
            return DeserializeFile<T>(filePath);
        }

        public static T DeserializeFile<T>(string filePath) where T : DataObject
        {
            string fileName = Path.GetFileName(filePath);
            T? data;
            try
            {
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    try
                    {
                        data = serializer.Deserialize(reader) as T;
                    }
                    catch (InvalidOperationException ex) when (ex.InnerException is XmlException)
                    {
                        throw new InvalidFileException(fileName);
                    }
                }
            }
            catch (Exception ex) when (ex is not OfException)
            {
                return Activator.CreateInstance<T>();
            }

            if (data == null) throw new InvalidFileException(fileName);
            return data;
        }

        public static void SerializeConfigFile<T>(string fileName, T data) where T : DataObject
        {
            string filePath = GetConfigFilePath(fileName);
            SerializeFile<T>(filePath, data);
        }

        public static void SerializeFile<T>(string filePath, T data) where T : DataObject
        {
            FileStream fileStream = File.Create(filePath);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(fileStream, data);

            fileStream.Close();
        }

        public static void Serialize<T>(ref Stream stream, T data) where T : DataObject
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, data);
        }

        public static string GetConfigFilePath(string fileName)
        {
            if (!Directory.Exists(CurrentApplication.CONFIG_PATH)) Directory.CreateDirectory(CurrentApplication.CONFIG_PATH);
            return Path.Combine(CurrentApplication.CONFIG_PATH, fileName);
        }
    }
}
