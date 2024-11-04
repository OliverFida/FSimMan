using OliverFida.Base;
using System.Windows;

namespace OliverFida.FSimMan.UI
{
    public partial class DialogWindow : Window
    {
        private ViewModelBase? _viewModel;
        public ViewModelBase? ViewModel
        {
            get => _viewModel;
            private set => SetProperty(ref _viewModel, value);
        }

        private DialogWindowButtonLayout _buttons;
        public DialogWindowButtonLayout Buttons
        {
            get => _buttons;
            private set { if (SetProperty(ref _buttons, value)) UpdateVisibility(); }
        }

        public DialogWindow(ViewModelBase viewModel, DialogWindowButtonLayout buttons)
        {
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            InitializeComponent();

            ViewModel = viewModel;
            Buttons = buttons;
        }

        public event EventHandler<DialogWindowClosedEventArgs>? DialogWindowClosed;
        private void OnDialogWindowClosed(DialogWindowButton button)
        {
            DialogWindowClosed?.Invoke(null, new DialogWindowClosedEventArgs(button));
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            OnDialogWindowClosed(DialogWindowButton.Cancel);
        }
        public bool IsCancelVisible
        {
            get => Buttons == DialogWindowButtonLayout.OkCancel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            OnDialogWindowClosed(DialogWindowButton.Ok);
        }
        public bool IsOkVisible
        {
            get => Buttons == DialogWindowButtonLayout.Ok || Buttons == DialogWindowButtonLayout.OkCancel;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            OnDialogWindowClosed(DialogWindowButton.No);
        }
        public bool IsNoVisible
        {
            get => Buttons == DialogWindowButtonLayout.YesNo;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            OnDialogWindowClosed(DialogWindowButton.Yes);
        }
        public bool IsYesVisible
        {
            get => Buttons == DialogWindowButtonLayout.YesNo;
        }

        private void UpdateVisibility()
        {
            OnPropertyChanged(nameof(IsCancelVisible));
            OnPropertyChanged(nameof(IsOkVisible));
            OnPropertyChanged(nameof(IsNoVisible));
            OnPropertyChanged(nameof(IsYesVisible));
        }
    }
}
