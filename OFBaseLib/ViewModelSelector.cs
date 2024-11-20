using System.Collections.ObjectModel;

namespace OliverFida.Base
{
    public class ViewModelSelector : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _viewModels = new ObservableCollection<ViewModelBase>();

        private ViewModelBase? _activeViewModel = null;
        public ViewModelBase? ActiveViewModel
        {
            get => _activeViewModel;
        }

        public event EventHandler<ActiveViewModelChangedEventArgs>? ActiveViewModelChangedEvent = null;
        protected virtual void OnActiveViewModelChanged()
        {
            ActiveViewModelChangedEvent?.Invoke(this, new ActiveViewModelChangedEventArgs(ActiveViewModel));
        }

        public void SetActiveViewModel(ViewModelBase viewModel)
        {
            if (!_viewModels.Contains(viewModel)) _viewModels.Add(viewModel);

            _activeViewModel = viewModel;
            OnPropertyChanged(nameof(ActiveViewModel));
            OnActiveViewModelChanged();
        }

        public void CloseViewModel(ViewModelBase viewModel)
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
