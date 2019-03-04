using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FileManager
{
    /// <summary>
    /// Converts a full path to a specific image type of a drive, folder or file
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class PathToImageConverter : IValueConverter
    {
        private readonly FileIconService fileIconService = new FileIconService();

        public static PathToImageConverter Instance { get; } = new PathToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return fileIconService.GetImage((string)value);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
