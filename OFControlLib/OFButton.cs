using System.Windows;
using System.Windows.Controls;

namespace OliverFida.Controls
{
    public class OFButton : Button
    {
        public OFButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OFButton), new FrameworkPropertyMetadata(typeof(OFButton)));
        }
    }
}
