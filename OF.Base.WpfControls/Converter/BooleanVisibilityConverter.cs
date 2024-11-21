using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OF.Base.Wpf.Converter
{
    public class BooleanVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool booVal = (bool)value;
            Visibility vis = Visibility.Collapsed;
            if (booVal) vis = Visibility.Visible;
            return vis;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack not implemented");
        }
    }
}
