﻿using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OF.Base.Wpf.Converter
{
    public class StringImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage fallbackImage = new BitmapImage(new Uri("/UI/Resources/Images/Missing-Image.png", UriKind.Relative));

            try
            {
                string sourceString = (string)value;

                if (string.IsNullOrWhiteSpace(sourceString)) return fallbackImage;
                //return new BitmapImage(new Uri(sourceString, UriKind.RelativeOrAbsolute));

                BitmapImage bitmapImage = new BitmapImage();
                using (FileStream fs = File.OpenRead(sourceString))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = fs;
                    bitmapImage.EndInit();
                }

                return bitmapImage;
            }
            catch
            {
                return fallbackImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}