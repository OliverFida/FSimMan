namespace OF.Base.ViewModel
{
    public class ActiveViewModelChangedEventArgs : EventArgs
    {
        private IViewModel? _activeViewModel;
        public IViewModel? ActiveViewModel
        {
            get => _activeViewModel;
        }

        public ActiveViewModelChangedEventArgs(IViewModel? activeViewModel)
        {
            _activeViewModel = activeViewModel;
        }
    }
}
