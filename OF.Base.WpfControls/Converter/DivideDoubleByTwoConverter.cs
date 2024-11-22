using System.Globalization;
using System.Windows.Data;

namespace OF.Base.Wpf.Converter
{
    internal class DivideDoubleByTwoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double input = (double)value;
            return input / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack not implemented");
        }
    }
}
