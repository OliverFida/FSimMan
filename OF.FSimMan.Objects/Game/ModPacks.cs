using OF.Base.Objects;

namespace OF.FSimMan.Game
{
    public class ModPacks : EditableObject
    {
        internal List<ModPack> _list = new List<ModPack>();
        public List<ModPack> List
        {
            get => (from p in _list orderby p.Title ascending select p).ToList();
        }
    }
}
