namespace OF.FSimMan.ViewModel.Base
{
    public abstract class RememberableViewModelBase : OF.Base.ViewModel.ViewModelBase, IRememberableViewModel
    {
        public RememberableViewModelBase() : base() { }
        public RememberableViewModelBase(bool isPersistant) : base(isPersistant) { }
    }
}
