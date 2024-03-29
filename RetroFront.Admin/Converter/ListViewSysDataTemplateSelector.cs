﻿using RetroFront.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RetroFront.Admin.Converter
{
    public class ListViewSysDataTemplateSelector:DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement elemnt = container as FrameworkElement;
            SystemeViewModel sys = item as SystemeViewModel;
            if (sys.HasLogo)
            {
                return elemnt.FindResource("SystemeLogoTemplate") as DataTemplate;
            }
            else
            {
                return elemnt.FindResource("SystemeBasicTemplate") as DataTemplate;
            }
        }
    }
    public class ListViewGameDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement elemnt = container as FrameworkElement;
            GameViewModel game = item as GameViewModel;
            if(game.Game.Fanart != null)
            {
                if (File.Exists(game.Game.Fanart) || File.Exists(game.Game.Fanart.Replace(".jpg", ".png")))
                {
                    if (game.Game.IsFavorite)
                    {
                        return elemnt.FindResource("GameFanartFavoriteTemplate") as DataTemplate;
                    }
                    else
                    {
                        return elemnt.FindResource("GameFanartUnFavoriteTemplate") as DataTemplate; 
                    }
                }
            }
            return elemnt.FindResource("GameBasicTemplate") as DataTemplate;
        }
    }
}
