using OF.Base.Objects;
using OF.FSimMan.Management.Exceptions;
using System.Xml;
using System.Xml.Serialization;

namespace OF.FSimMan.Utility
{
    public static class FileSerializationHelper
    {
        #region Methods PUBLIC
        public static T DeserializeConfigFile<T>(string fileName) where T : DataObject
        {
            string filePath = GetConfigFilePath(fileName);
            return DeserializeFile<T>(filePath);
        }

        public static T DeserializeFile<T>(string filePath) where T : DataObject
        {
            return DeserializeAnyFile<T>(filePath);
        }

        public static T DeserializeAnyFile<T>(string filePath) where T : class
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

                    reader.Close();
                }
            }
            catch (Exception ex) when (ex is not OfException)
            {
                return Activator.CreateInstance<T>();
            }

            if (data == null) throw new InvalidFileException(fileName);
            return data;
        }

        public static T Deserialize<T>(ref Stream stream) where T : DataObject
        {
            return DeserializeAny<T>(ref stream);
        }

        public static T DeserializeAny<T>(ref Stream stream) where T : class
        {
            T? data;
            try
            {
                if (stream.CanSeek && stream.Position != 0) stream.Position = 0;
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    try
                    {
                        data = serializer.Deserialize(reader) as T;
                    }
                    catch (InvalidOperationException ex) when (ex.InnerException is XmlException)
                    {
                        throw new InvalidStreamException();
                    }
                }
            }
            catch (Exception ex) when (ex is not OfException)
            {
                return Activator.CreateInstance<T>();
            }

            if (data == null) throw new InvalidStreamException();
            return data;
        }

        public static void SerializeConfigFile<T>(string fileName, T data) where T : DataObject
        {
            string filePath = GetConfigFilePath(fileName);
            SerializeFile(filePath, data);
        }

        public static void SerializeFile<T>(string filePath, T data) where T : DataObject
        {
            SerializeAnyFile<T>(filePath, data);
        }

        public static void SerializeAnyFile<T>(string filePath, T data) where T : class
        {
            FileStream fileStream = File.Create(filePath);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(fileStream, data);

            fileStream.Close();
        }

        public static void Serialize<T>(ref Stream stream, T data) where T : DataObject
        {
            SerializeAny<T>(ref stream, data);
        }

        public static void SerializeAny<T>(ref Stream stream, T data) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, data);
        }
        #endregion

        #region Methods PRIVATE
        private static string GetConfigFilePath(string fileName)
        {
            return Path.Combine(CurrentApplication.CONFIG_PATH, fileName);
        }
        #endregion
    }
}
