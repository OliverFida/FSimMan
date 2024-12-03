using OF.Base.Objects;
using System.Windows;
using System.Windows.Input;

namespace OF.Base.Wpf
{
    public class Button : System.Windows.Controls.Button
    {
        #region Properties
        public Command CommandDoubleClick
        {
            get { return (Command)GetValue(CommandDoubleClickProperty); }
            set { SetValue(CommandDoubleClickProperty, value); }
        }
        public static readonly DependencyProperty CommandDoubleClickProperty = DependencyProperty.Register(nameof(CommandDoubleClick), typeof(Command), typeof(Button));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(Button));
        #endregion

        #region Constructor
        public Button()
        {
            MouseDoubleClick += OnMouseDoubleClick;
        }
        #endregion

        #region Methods PRIVATE
        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CommandDoubleClick?.CanExecute(null) == true)
            {
                CommandDoubleClick.Execute(null);
                e.Handled = true;
            }
        }
        #endregion
    }
}
