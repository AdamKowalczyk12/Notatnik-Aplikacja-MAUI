using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.Converters
{
    internal class NoteTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = value == null ? null : value.ToString();
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            if (text.Count() <= 10)
                return text;
            return $"{text.Substring(0, 10)}...";

           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
