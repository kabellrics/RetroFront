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
    public class PlateformeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FlipView { get; set; }
        public DataTemplate CarrousselLogoView { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is SysDisplay)
            {
                var displayType = (SysDisplay)item;
                if (displayType == SysDisplay._0)
                    return FlipView;
                if (displayType == SysDisplay._1)
                    return CarrousselLogoView;
                //if (displayType == HomeDisplay._2)
                //    return HubView;

                return base.SelectTemplateCore(item);
            }
            else
                return FlipView;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}
