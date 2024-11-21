namespace OF.Base.ViewModel
{
    public interface IViewModelSelector : IViewModel
    {
        public IViewModel? ActiveViewModel { get; }

        public event EventHandler<ActiveViewModelChangedEventArgs>? ActiveViewModelChangedEvent;

        public void SetActiveViewModel(IViewModel viewModel);
        public void CloseViewModel(IViewModel viewModel);
        public void CloseCurrentViewModel();
    }
}
