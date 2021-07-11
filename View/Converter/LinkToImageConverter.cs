using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;

namespace Booklist.View.Converter
{
    public class LinkToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) || value == null)
            {
                return new BitmapImage(new Uri("https://cdn.vox-cdn.com/thumbor/5VQLfvl2smTJ1uxXH2JyDj9U0sI=/0x0:2040x1360/920x613/filters:focal(868x1009:1194x1335):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/54627067/rwarren_170504_1668_0001.0.jpg"));
            }
            string data = value.ToString();
            if (data.Equals("") || !isValidLink(data))
            {
                return new BitmapImage(new Uri("https://cdn.vox-cdn.com/thumbor/5VQLfvl2smTJ1uxXH2JyDj9U0sI=/0x0:2040x1360/920x613/filters:focal(868x1009:1194x1335):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/54627067/rwarren_170504_1668_0001.0.jpg"));
            }
            return new BitmapImage(new Uri(data));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool isValidLink(string url)
        {
            url = url.ToLower();
            return url.StartsWith("http://") || url.StartsWith("https://");
        }
    }
}
