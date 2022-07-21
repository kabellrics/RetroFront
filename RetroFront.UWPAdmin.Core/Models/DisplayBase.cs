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
        protected void ChangeStatus() { HasChanged = true; }
        public DisplayBase()
        {
            HasChanged = false;
        }
    }
}
