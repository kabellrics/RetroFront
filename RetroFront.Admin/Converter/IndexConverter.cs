using RetroFront.Admin.Dialogs.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace RetroFront.Admin.Converter
{
    public class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((SteamGridSearchViewModel)value).ResultImgs.Select((x, i) => i).ToList();
            //return ((IEnumerable)value).OfType<object>().Select((x, i) => i).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
