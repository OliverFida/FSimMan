using OF.FSimMan.Updater.ViewModels;
using System.Windows;

namespace OF.FSimMan.Updater
{
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel.Instance;
        }
        #endregion
    }
}