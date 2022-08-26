using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Models
{
    public class DisplayBase : ObservableObject
    {
        private bool _hasChanged;

        public bool HasChanged
        {
            get => _hasChanged;
            set
            {
                SetProperty(ref _hasChanged, value);
            }
        }
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }
        protected void ChangeStatus() { HasChanged = true; }
        public DisplayBase()
        {
            HasChanged = false;
        }
    }
}
