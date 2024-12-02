using System.Collections.ObjectModel;

namespace OF.Base.ViewModel
{
    public interface IViewModelSelector : IViewModel
    {
        public ReadOnlyObservableCollection<IViewModel> OpenViewModels { get; }
        public IViewModel? CurrentViewModel { get; }

        public event EventHandler? CurrentViewModelChanged;

        public void OpenViewModel(IViewModel viewModel);
        public void CloseViewModel(IViewModel viewModel);
        public void CloseCurrentViewModel();
    }
}
