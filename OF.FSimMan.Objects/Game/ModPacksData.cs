using OF.Base.Objects;
using System.Collections.Concurrent;
using System.Xml.Serialization;

namespace OF.FSimMan.Game
{
    [XmlRoot("ModPacks")]
    public class ModPacksData : DataObject<ModPacks>
    {
        [XmlArray(nameof(List), IsNullable = false)]
        public ModPackData[] List = [];

        public override ModPacks FromData()
        {
            return new ModPacks
            {
                _list = (from d in List select d.FromData()).ToList()
            };
        }

        public override void ToData(ModPacks value)
        {
            ConcurrentBag<ModPackData> temp = new ConcurrentBag<ModPackData>();
            Parallel.ForEach(value.List, modPack =>
            {
                ModPackData data = new ModPackData();
                data.ToData(modPack);
                temp.Add(data);
            });
            List = temp.ToArray();
        }
    }
}
