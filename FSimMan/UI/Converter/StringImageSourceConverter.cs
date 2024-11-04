using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OliverFida.FSimMan.UI.Converter
{
    class StringImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sourceString = (string)value;

            if (string.IsNullOrWhiteSpace(sourceString)) return new BitmapImage(new Uri("/UI/Resources/Images/Missing-Image.png", UriKind.Relative));
            return new BitmapImage(new Uri(sourceString, UriKind.RelativeOrAbsolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
