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
            private set => SetProperty(ref _currentViewModel, value);
        }
        #endregion

        #region Constructor
        public ViewModelSelector() : base(true) { }
        #endregion

        #region Methods PUBLIC
        public void OpenViewModel(IViewModel viewModel)
        {
            OpenViewModel(viewModel, true);
        }

        public void OpenViewModel(IViewModel viewModel, bool triggerAutoclose)
        {
            ExecuteOpenViewModel(viewModel, triggerAutoclose);
        }

        public void CloseCurrentViewModel()
        {
            if (CurrentViewModel is null || CurrentViewModel.IsPersistant) return;

            CloseViewModel(CurrentViewModel);
        }

        public void CloseViewModel(IViewModel viewModel)
        {
            CloseViewModel(viewModel, true);
        }
        #endregion

        #region Methods PRIVATE
        private void ExecuteOpenViewModel(IViewModel? viewModel, bool triggerAutoclose)
        {
            // Already open
            if (viewModel == CurrentViewModel) return;

            // Close current
            if (triggerAutoclose) ExecuteAutoclose();

            // Check if unknown
            if (viewModel is not null && !OpenViewModels.Contains(viewModel)) _openViewModels.Add(viewModel);

            // Set current
            CurrentViewModel = viewModel;
        }

        private void CloseViewModel(IViewModel viewModel, bool triggerOpenLast)
        {
            if (viewModel.IsPersistant || !OpenViewModels.Contains(viewModel)) return;

            // Remove from known
            _openViewModels.Remove(viewModel);

            // Reopen last
            if (triggerOpenLast) ExecuteOpenViewModel(OpenViewModels.LastOrDefault(), false);
        }

        private void ExecuteAutoclose()
        {
            List<IViewModel> obsoleteViewModels = (from vm in OpenViewModels where !vm.IsPersistant && vm.IsAutocloseable select vm).ToList();

            foreach (IViewModel viewModel in obsoleteViewModels)
            {
                CloseViewModel(viewModel, false);
            }
        }
        #endregion
    }
}
