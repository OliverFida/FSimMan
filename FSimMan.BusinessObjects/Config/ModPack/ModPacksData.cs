using OliverFida.Base;
using System.Xml.Serialization;

namespace OliverFida.FSimMan.Config.ModPack
{
    [XmlRoot("ModPacks")]
    public class ModPacksData : DataObjectBase<ModPacks>
    {
        [XmlArray(nameof(List), IsNullable = false)]
        [XmlArrayItem("ModPack")]
        public ModPackData[] List = [];

        public override ModPacks FromData()
        {
            ModPacks temp = new ModPacks();
            if (List != null) temp._list = (from d in List select d.FromData()).ToList();

            return temp;
        }

        public override void ToData(ModPacks value)
        {
            {
                List<ModPackData> temp = new List<ModPackData>();
                foreach (ModPack modPack in value.List)
                {
                    ModPackData data = new ModPackData();
                    data.ToData(modPack);
                    temp.Add(data);
                }
                List = temp.ToArray();
            }
        }
    }
}
