using OF.Base.ViewModel;
using System.Windows;

namespace OF.Base.Wpf.UiFunctions
{
    public partial class DialogWindow : Window
    {
        private DialogViewModel _viewModel;
        public DialogViewModel ViewModel { get => _viewModel; }

        public DialogWindow(IViewModel viewModel, DialogWindowButtonLayout buttonLayout)
        {
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            InitializeComponent();

            _viewModel = new DialogViewModel(viewModel, buttonLayout);
            DataContext = _viewModel;
            _viewModel.DialogWindowClosing += HandleDialogWindowClosing;
        }

        private void HandleDialogWindowClosing(object? sender, DialogWindowClosingEventArgs e)
        {
            Close();
        }
    }
}
