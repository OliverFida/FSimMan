using System.ComponentModel;

namespace OliverFida.Base
{
    public interface IEditableCollection : IEditableObject
    {
        bool IsModified { get; }
    }
}
