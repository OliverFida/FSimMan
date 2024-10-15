using OliverFida.FSimMan.UI;

namespace OliverFida.FSimMan
{
    public partial class MainWindow : OFWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static string AppTitle
        {
            get => CurrentApplication.AppTitle;
        }
    }
}