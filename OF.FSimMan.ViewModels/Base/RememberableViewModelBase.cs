namespace OF.FSimMan.ViewModel.Base
{
    public abstract class RememberableViewModelBase : CLS.Core.ViewModel.ViewModelBase, IRememberableViewModel
    {
        public RememberableViewModelBase(string title) : base(title) { }
        public RememberableViewModelBase(string title, bool isPersistant) : base(title, isPersistant) { }
    }
}
