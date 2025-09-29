using Cafe_Kiosk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cafe_Kiosk.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly ObservableCollection<CafeMenuItem> _items = new();

        public void AddItem(CafeMenuItem item)
        {
            _items.Add(item);
        }

        public ObservableCollection<CafeMenuItem> GetItems()
        {
            return _items;
        }
    }
}