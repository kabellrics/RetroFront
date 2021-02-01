using Microsoft.Extensions.DependencyInjection;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RetroFront.Converter
{

    public class NavigationService : INavigationService
    {
        #region Fields
        private readonly Dictionary<string, Uri> _pagesByKey;
        private readonly List<string> _historic;
        private string _currentPageKey;
        #endregion
        #region Properties                                              
        public string CurrentPageKey
        {
            get
            {
                return _currentPageKey;
            }

            private set
            {
                if (_currentPageKey == value)
                {
                    return;
                }

                _currentPageKey = value;
                OnPropertyChanged("CurrentPageKey");
            }
        }
        public object Parameter { get; private set; }
        #endregion
        #region Ctors and Methods
        public NavigationService()
        {
            _pagesByKey = new Dictionary<string, Uri>();
            _historic = new List<string>();
        }
        public void GoBack()
        {
            if (_historic.Count > 1)
            {
                _historic.RemoveAt(_historic.Count - 1);
                NavigateTo(_historic.Last());
            }
        }
        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null, string.Empty);
        }
        public void NavigateTo(string pageKey, string frameName)
        {
            NavigateTo(pageKey, null, frameName);
        }
        public virtual void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(string.Format("No such page: {0} ", pageKey), "pageKey");
                }

                //if (string.IsNullOrEmpty(frameName) || string.IsNullOrWhiteSpace(frameName))
                var frameName = "NavigationFrame";
                var frame = GetDescendantFromName(Application.Current.MainWindow, frameName) as Frame;

                if (frame != null)
                {
                    frame.Source = _pagesByKey[pageKey];
                }
                Parameter = parameter;
                _historic.Add(pageKey);
                CurrentPageKey = pageKey;
            }
        }
        public virtual void NavigateTo(string pageKey, object parameter, string frameName)
        {
            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(string.Format("No such page: {0} ", pageKey), "pageKey");
                }
                if (string.IsNullOrEmpty(frameName) || string.IsNullOrWhiteSpace(frameName))
                    frameName = "NavigationFrame";
                var frame = GetDescendantFromName(Application.Current.MainWindow, frameName) as Frame;

                if (frame != null)
                {
                    frame.Source = _pagesByKey[pageKey];
                }
                Parameter = parameter;
                _historic.Add(pageKey);
                CurrentPageKey = pageKey;
            }
        }
        public void Configure(string key, Uri pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    _pagesByKey[key] = pageType;
                }
                else
                {
                    _pagesByKey.Add(key, pageType);
                }
            }
        }

        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                var frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
