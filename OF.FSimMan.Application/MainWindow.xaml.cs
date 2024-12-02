using OF.FSimMan.ViewModel;
using System.Windows;

namespace OF.FSimMan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel.Instance;

            MainViewModel.Instance.UpdateCompleteEvent += HandleUpdateCompleteEvent;
        }

        private void HandleUpdateCompleteEvent(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => Application.Current.Shutdown(0));
        }
    }
}