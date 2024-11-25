using System.Windows;
using System.Windows.Media;

namespace OF.Base.Wpf
{
    public class ImageButton : Button
    {
        #region Properties
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(ImageButton));
        #endregion

        #region Constructor
        public ImageButton() { }
        #endregion
    }
}
