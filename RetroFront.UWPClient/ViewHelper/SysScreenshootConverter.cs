using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RetroFront.UWPClient.ViewHelper
{
    public class SysScreenshootConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
                var id = value.ToString();
                if (string.IsNullOrEmpty(id))
                {
                    var converted = string.Format(@"http://localhost:34322/api/Img/GetAppBackground");
                    return converted;
                }
                else
                {
                    var converted = string.Format(@"http://localhost:34322/api/Img/GetScreenshootForSystem/{0}", value.ToString());
                    return converted;
                }
                throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var valueStr = value as String;
            var id = valueStr.Substring(valueStr.ToString().LastIndexOf("/") + 1);
            int valueInt = -1;
            int.TryParse(id,out valueInt);
            return valueInt;
            //throw new NotImplementedException();
        }
    }
}
