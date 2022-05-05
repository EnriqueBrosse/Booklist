using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Booklist.View.Converter
{
    class LinkToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = value.ToString();
            path = path.Replace("https://", "");
            path = path.Replace("www.", "");
            string[] split = path.Split('/');
            //if (path.Contains("amazon"))
            //{
            //    return "Amazon";
            //}
            //else if (path.Contains("bol"))
            //{
            //    return "bol";
            //}
            return split[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
