using RetroFront.UWPClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPClient.ViewHelper
{
    public class FlipRotateTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PlateformeListTemplate { get; set; }
        public DataTemplate GameListTemplate { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is FlipRotateElement element)
            {
                if (element.Type == "games")
                    return GameListTemplate;
                if (element.Type == "plateforme")
                    return PlateformeListTemplate;

                return base.SelectTemplateCore(item);
            }
            else
                return GameListTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}
