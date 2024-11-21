using System.Collections.ObjectModel;

namespace OF.Base.ViewModel
{
    public class ViewModelSelector : ViewModelBase, IViewModelSelector
    {
        private ObservableCollection<IViewModel> _viewModels = new ObservableCollection<IViewModel>();

        private IViewModel? _activeViewModel = null;
        public IViewModel? ActiveViewModel
        {
            get => _activeViewModel;
        }

        public event EventHandler<ActiveViewModelChangedEventArgs>? ActiveViewModelChangedEvent = null;
        protected virtual void OnActiveViewModelChanged()
        {
            ActiveViewModelChangedEvent?.Invoke(this, new ActiveViewModelChangedEventArgs(ActiveViewModel));
        }

        public void SetActiveViewModel(IViewModel viewModel)
        {
            if (!_viewModels.Contains(viewModel)) _viewModels.Add(viewModel);

            _activeViewModel = viewModel;
            OnPropertyChanged(nameof(ActiveViewModel));
            OnActiveViewModelChanged();
        }

        public void CloseViewModel(IViewModel viewModel)
        {
            if (_viewModels.Contains(viewModel)) _viewModels.Remove(viewModel);
            SetActiveViewModel(_viewModels.Last());
        }

        public void CloseCurrentViewModel()
        {
            if (ActiveViewModel == null) return;

            CloseViewModel(ActiveViewModel);
        }
    }
}
