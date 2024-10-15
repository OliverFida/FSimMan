using System.Windows;

namespace OliverFida.Controls
{
    public class OFWindow : Window {
        public OFWindow()
        {
            DataContext = this;
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OFWindow), new FrameworkPropertyMetadata(typeof(OFWindow)));
        }
    }
}
