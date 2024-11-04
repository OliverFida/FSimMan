namespace OliverFida.Base
{
    public class ActiveViewModelChangedEventArgs : EventArgs
    {
        private ViewModelBase? _activeViewModel;
        public ViewModelBase? ActiveViewModel
        {
            get => _activeViewModel;
        }

        public ActiveViewModelChangedEventArgs(ViewModelBase? activeViewModel)
        {
            _activeViewModel = activeViewModel;
        }
    }
}
