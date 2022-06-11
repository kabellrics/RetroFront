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
    public class HomeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate GameOSView { get; set; }
        public DataTemplate FlixView { get; set; }
        public DataTemplate HubView { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is HomeDisplay)
            {
                var displayType = (HomeDisplay)item;
                if (displayType == HomeDisplay._0)
                    return GameOSView;
                if (displayType == HomeDisplay._1)
                    return FlixView;
                if (displayType == HomeDisplay._2)
                    return HubView;

                return base.SelectTemplateCore(item); 
            }else
                return GameOSView;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}
