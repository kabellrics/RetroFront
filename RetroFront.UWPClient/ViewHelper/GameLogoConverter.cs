using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RetroFront.UWPClient.ViewHelper
{
    public class GameLogoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var converted = string.Format(@"http://localhost:34322/api/Img/GetLogoForGame/{0}", value.ToString());
            return converted;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var valueStr = value as String;
            var id = valueStr.Substring(valueStr.ToString().LastIndexOf("/") + 1);
            var valueInt = int.Parse(id);
            return valueInt;
            //throw new NotImplementedException();
        }
    }
}
