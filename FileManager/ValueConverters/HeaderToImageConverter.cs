﻿using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FileManager
{
    /// <summary>
    /// Converts a full path to a specific image type of a drive, folder or file
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        private readonly FileIconService fileIconService = new FileIconService();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if ((string)value == "file.png")
            //return fileIconService.GetImage((string)value);


            return new BitmapImage(new Uri($"pack://application:,,,/Images/{value}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
