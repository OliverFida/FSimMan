using System.Windows;
using System.Windows.Controls;

namespace OliverFida.FSimMan.UI
{
    internal class InputGroup : Control
    {
        public string HeadingText
        {
            get { return (string)GetValue(HeadingTextProperty); }
            set { SetValue(HeadingTextProperty, value); }
        }
        public static readonly DependencyProperty HeadingTextProperty = DependencyProperty.Register(nameof(HeadingText), typeof(string), typeof(InputGroup), new PropertyMetadata(string.Empty));
    }
}
