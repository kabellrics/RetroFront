using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RetroFront.UWPClient.ViewHelper
{
    public class ShellBackGroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Systeme sys)
            {
                var converted = string.Format(@"http://localhost:34322/api/Img/GetScreenshootForSystem/{0}", sys.SystemeID);
                return converted;
            }
            else if (value is GameRom rom)
            {
                var converted = string.Format(@"http://localhost:34322/api/Img/GetScreenshootForGame/{0}", rom.Id);
                return converted;
            }
            else
            {
                var converted = string.Format(@"http://localhost:34322/api/Img/GetAppBackground");
                return converted;
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var valueStr = value as String;
            var id = valueStr.Substring(valueStr.ToString().LastIndexOf("/") + 1);
            int valueInt = -1;
            int.TryParse(id, out valueInt);
            return valueInt;
            //throw new NotImplementedException();
        }
    }
}
