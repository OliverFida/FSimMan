namespace OF.FSimMan.ViewModel.Base
{
    public abstract class RememberableViewModelBase : OF.Base.ViewModel.ViewModelBase, IRememberableViewModel
    {
        public RememberableViewModelBase(string title) : base(title) { }
        public RememberableViewModelBase(string title, bool isPersistant) : base(title, isPersistant) { }
    }
}
