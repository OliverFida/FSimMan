using OliverFida.FSimMan.ViewModels.UI;
using System.Windows.Controls;

namespace OliverFida.FSimMan.UI
{
    internal partial class AppBar : UserControl
    {
        public AppBar()
        {
            DataContext = new AppBarViewModel();
        }
    }
}
