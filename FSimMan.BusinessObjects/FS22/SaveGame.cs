using OliverFida.Base;
using OliverFida.FSimMan.Exceptions.Fs;
using OliverFida.FSimMan.FS22.SaveGame;
using System.Xml;
using System.Xml.Serialization;

namespace OliverFida.FSimMan.FS22
{
    public class Fs22SaveGame : ObjectBase
    {
        private const string FILE_NAME_CAREER_SAVEGAME = "careerSavegame.xml";
        private const string FILE_NAME_FARMS = "farms.xml";

        private string _saveGamePath;

        private careerSavegame? _careerSavegame;
        private farms? _farms;

        public Fs22SaveGame(string saveGamePath)
        {
            _saveGamePath = saveGamePath;

            _careerSavegame = DeserializeGameFile<careerSavegame>(FILE_NAME_CAREER_SAVEGAME);
            _farms = DeserializeGameFile<farms>(FILE_NAME_FARMS);
        }

        private T DeserializeGameFile<T>(string fileName)
        {
            T? file;
            using (XmlReader reader = XmlReader.Create(Path.Combine(_saveGamePath, fileName)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                file = (T?)serializer.Deserialize(reader);
            }
            if (file == null) throw new InvalidFsFileException(fileName);

            return file;
        }
    }
}
