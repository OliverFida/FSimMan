using OF.Base.Objects;
using OF.Base.ViewModel;

namespace OF.Base.Wpf.UiFunctions
{
    public class DialogViewModel : ViewModelBase
    {
        #region Properties
        private IViewModel? _currentViewModel;
        public IViewModel? CurrentViewModel
        {
            get => _currentViewModel;
            private set => SetProperty(ref _currentViewModel, value);
        }

        private DialogWindowButtonLayout _buttonLayout;
        public DialogWindowButtonLayout ButtonLayout
        {
            get => _buttonLayout;
            private set { if (SetProperty(ref _buttonLayout, value)) UpdateVisibility(); }
        }
        #endregion

        #region Events
        public event EventHandler<DialogWindowClosingEventArgs>? DialogWindowClosing;
        #endregion

        #region Commands
        public Command CancelCommand { get; }
        private void CancelDelegate()
        {
            OnDialogWindowClosed(DialogWindowButton.Cancel);
        }
        public bool IsCancelVisible { get => ButtonLayout == DialogWindowButtonLayout.OkCancel; }

        public Command OkCommand { get; }
        private void OkDelegate()
        {
            OnDialogWindowClosed(DialogWindowButton.Ok);
        }
        public bool IsOkVisible { get => ButtonLayout is DialogWindowButtonLayout.Ok or DialogWindowButtonLayout.OkCancel; }

        public Command NoCommand { get; }
        private void NoDelegate()
        {
            OnDialogWindowClosed(DialogWindowButton.No);
        }
        public bool IsNoVisible { get => ButtonLayout == DialogWindowButtonLayout.YesNo; }

        public Command YesCommand { get; }
        private void YesDelegate()
        {
            OnDialogWindowClosed(DialogWindowButton.Yes);
        }
        public bool IsYesVisible { get => ButtonLayout == DialogWindowButtonLayout.YesNo; }
        #endregion

        #region Constructor
        public DialogViewModel(IViewModel viewModel, DialogWindowButtonLayout buttonLayout) : base(true)
        {
            CurrentViewModel = viewModel;
            ButtonLayout = buttonLayout;

            CancelCommand = new Command(this, target => ((DialogViewModel)target).CancelDelegate());
            OkCommand = new Command(this, target => ((DialogViewModel)target).OkDelegate());
            NoCommand = new Command(this, target => ((DialogViewModel)target).NoDelegate());
            YesCommand = new Command(this, target => ((DialogViewModel)target).YesDelegate());
        }
        #endregion

        #region Methods PRIVATE
        private void UpdateVisibility()
        {
            OnPropertyChanged(nameof(IsCancelVisible));
            OnPropertyChanged(nameof(IsOkVisible));
            OnPropertyChanged(nameof(IsNoVisible));
            OnPropertyChanged(nameof(IsYesVisible));
        }

        private void OnDialogWindowClosed(DialogWindowButton button)
        {
            DialogWindowClosing?.Invoke(null, new DialogWindowClosingEventArgs(button));
        }
        #endregion
    }
}
