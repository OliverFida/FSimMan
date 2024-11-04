using System.Windows;
using System.Windows.Input;

namespace OliverFida.FSimMan.UI
{
    internal class Button : System.Windows.Controls.Button
    {
        public ICommand CommandDoubleClick
        {
            get { return (ICommand)GetValue(CommandDoubleClickProperty); }
            set { SetValue(CommandDoubleClickProperty, value); }
        }
        public static readonly DependencyProperty CommandDoubleClickProperty = DependencyProperty.Register(nameof(CommandDoubleClick), typeof(ICommand), typeof(Button));
        private void OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CommandDoubleClick?.CanExecute(null) == true)
            {
                CommandDoubleClick.Execute(null);
                e.Handled = true;
            }
        }

        public Button()
        {
            PreviewMouseDoubleClick += OnPreviewMouseDoubleClick;
        }
    }
}
