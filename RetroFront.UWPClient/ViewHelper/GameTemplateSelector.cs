using RetroFront.UWPClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RetroFront.UWPClient.ViewHelper
{
    public class GameTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BoxWall { get; set; }
        public DataTemplate BannerWall { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is RomDisplay)
            {
                var displayType = (RomDisplay)item;
                if (displayType == RomDisplay._0)
                    return BoxWall;
                if (displayType == RomDisplay._1)
                    return BannerWall;
                //if (displayType == HomeDisplay._2)
                //    return HubView;

                return base.SelectTemplateCore(item);
            }
            else
                return BoxWall;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}
