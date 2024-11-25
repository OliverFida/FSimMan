using System.Collections.ObjectModel;

namespace OF.Base.ViewModel
{
    public class ViewModelSelector : ViewModelBase, IViewModelSelector
    {
        private readonly ObservableCollection<IViewModel> _openViewModels = new ObservableCollection<IViewModel>();
        public ReadOnlyObservableCollection<IViewModel> OpenViewModels => new ReadOnlyObservableCollection<IViewModel>(_openViewModels);

        #region Properties
        private IViewModel? _currentViewModel = null;
        public IViewModel? CurrentViewModel
        {
            get => _currentViewModel;
        }
        #endregion

        #region Events
        public event EventHandler<ActiveViewModelChangedEventArgs>? ActiveViewModelChangedEvent = null;
        #endregion

        #region Constructor
        public ViewModelSelector() : base(true) { }
        #endregion

        #region Methods PUBLIC
        public void OpenViewModel(IViewModel viewModel)
        {
            ExecuteOpenViewModel(viewModel);
        }

        public void CloseCurrentViewModel()
        {
            if (CurrentViewModel is null || CurrentViewModel.IsPersistant) return;

            CloseViewModel(CurrentViewModel);
        }

        public void CloseViewModel(IViewModel viewModel)
        {
            if (viewModel.IsPersistant || !_openViewModels.Contains(viewModel)) return;

            // Remove from known
            _openViewModels.Remove(viewModel);

            // Reopen last
            ExecuteOpenViewModel(_openViewModels.LastOrDefault());
        }
        #endregion

        #region Methods PRIVATE
        public void ExecuteOpenViewModel(IViewModel? viewModel)
        {
            // Already open
            if (viewModel == CurrentViewModel) return;

            // Close current
            if (CurrentViewModel is not null && !CurrentViewModel.IsPersistant) CloseCurrentViewModel();

            // Check if unknown
            if (viewModel is not null && !_openViewModels.Contains(viewModel)) _openViewModels.Add(viewModel);

            // Set current
            SetProperty(ref _currentViewModel, viewModel, nameof(CurrentViewModel));
        }
        #endregion
    }
}
