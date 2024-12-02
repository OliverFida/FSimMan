using System.Collections.ObjectModel;

namespace OF.Base.ViewModel
{
    public class ViewModelSelector : ViewModelBase, IViewModelSelector
    {
        #region Properties
        private readonly ObservableCollection<IViewModel> _openViewModels = new ObservableCollection<IViewModel>();
        public ReadOnlyObservableCollection<IViewModel> OpenViewModels
        {
            get => new ReadOnlyObservableCollection<IViewModel>(_openViewModels);
        }

        private IViewModel? _currentViewModel = null;
        public IViewModel? CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                if (SetProperty(ref _currentViewModel, value))
                {
                    CurrentViewModelChanged?.Invoke(this, EventArgs.Empty);
                    AutoCloseViewModels();
                }
            }
        }
        #endregion

        #region Events
        public event EventHandler? CurrentViewModelChanged;
        #endregion

        #region Constructor
        public ViewModelSelector() : base(true) { }
        #endregion

        #region Methods PUBLIC
        public void OpenViewModel(IViewModel viewModel)
        {
            if (!GetIsOpen(viewModel))
            {
                _openViewModels.Add(viewModel);
                OnPropertyChanged(nameof(OpenViewModels));
            }

            CurrentViewModel = viewModel;
        }

        public void CloseViewModel(IViewModel viewModel) => CloseViewModel(viewModel, false);

        public void CloseCurrentViewModel()
        {
            if (CurrentViewModel == null) return;

            CloseViewModel(CurrentViewModel);
        }
        #endregion

        #region Methods PRIVATE
        private void CloseViewModel(IViewModel viewModel, bool isAutoclose)
        {
            if (viewModel.IsPersistant) return;
            if (!GetIsOpen(viewModel)) return;

            bool isCurrent = CurrentViewModel == viewModel;

            _openViewModels.Remove(viewModel);
            OnPropertyChanged(nameof(OpenViewModels));
            if (isCurrent) OpenViewModel(OpenViewModels.Last());
        }

        private void AutoCloseViewModels()
        {
            foreach (IViewModel viewModel in OpenViewModels)
            {
                if (viewModel.IsPersistant) continue;
                if (!viewModel.IsAutocloseable) continue;
                if (viewModel.PreventAutoclose) continue;
                if (viewModel == CurrentViewModel) continue;

                CloseViewModel(viewModel, true);
                AutoCloseViewModels();
                return;
            }
        }

        private bool GetIsOpen(IViewModel viewModel)
        {
            return OpenViewModels.Contains(viewModel);
        }
        #endregion
    }
}
