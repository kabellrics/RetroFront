using Microsoft.Toolkit.Mvvm.ComponentModel;
using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.ViewModels.Modals
{
    public class ImgScrapeChoiceViewModelcs : ObservableObject
    {
        private String _title;
        public String Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
            }
        }
        private DisplayGame _currentgame;
        public DisplayGame CurrentGame
        {
            get => _currentgame;
            set
            {
                SetProperty(ref _currentgame, value);
            }
        }
        private ScraperType _typeimg;
        public ScraperType TypeImg
        {
            get => _typeimg;
            set
            {
                SetProperty(ref _typeimg, value);
            }
        }
        private int _imgheight;
        public int ImgHeight
        {
            get => _imgheight;
            set
            {
                SetProperty(ref _imgheight, value);
            }
        }
        private int _imgwidht;
        public int ImgWidth
        {
            get => _imgwidht;
            set
            {
                SetProperty(ref _imgwidht, value);
            }
        }
        private bool _showigdb;
        public bool ShowIGDB
        {
            get => _showigdb;
            set
            {
                SetProperty(ref _showigdb, value);
            }
        }
        private bool _showSGDB;
        public bool ShowSGDB
        {
            get => _showSGDB;
            set
            {
                SetProperty(ref _showSGDB, value);
            }
        }
        private bool _showSCSP;
        public bool ShowSCSP
        {
            get => _showSCSP;
            set
            {
                SetProperty(ref _showSCSP, value);
            }
        }
    }
}
