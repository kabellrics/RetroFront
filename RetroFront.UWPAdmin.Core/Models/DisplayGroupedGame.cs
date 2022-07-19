using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RetroFront.UWPAdmin.Core.Models
{
    public class DisplayGroupedGame : DisplayBase
    {
        public DisplayGroupedGame(string name,IEnumerable<DisplayGame> list) : base()
        {
            Name = name;
            Items = new ObservableCollection<DisplayGame>();
            foreach (var item in list)
                Items.Add(item);
        }
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }
        private ObservableCollection<DisplayGame> _items;

        public ObservableCollection<DisplayGame> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
            }
        }
    }
}
