using OF.Base.Objects;

namespace OF.Base.ViewModel
{
    public interface IViewModel : IBindingObject, IInitializeable
    {
        public bool IsPersistant { get; }
        public bool IsAutocloseable { get; }
    }
}
