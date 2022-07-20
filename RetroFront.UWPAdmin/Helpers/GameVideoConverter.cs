using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.UI.Xaml.Data;

namespace RetroFront.UWPAdmin.Helpers
{
    public class GameVideoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return MediaSource.CreateFromUri(new Uri((string)value));
            //var converted = new Uri(string.Format(@"http://localhost:34322/api/Img/GetVideoForGame/{0}", value.ToString()));
            //return converted;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var valueStr = value as MediaSource;
            var id = valueStr.Uri.ToString();
            return id;
            //throw new NotImplementedException();
        }
    }
}
