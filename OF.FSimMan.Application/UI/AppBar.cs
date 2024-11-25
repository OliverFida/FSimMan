using System.Windows.Controls;

namespace OF.FSimMan.UI
{
    internal partial class AppBar : UserControl
    {
        private AppBarViewModel _viewModel = new AppBarViewModel();

        public AppBar()
        {
            DataContext = _viewModel;
        }
    }
}