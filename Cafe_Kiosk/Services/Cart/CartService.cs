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
        private readonly ObservableCollection<CartItem> _items = new();

        public void AddItem(CartItem item)
        {
            _items.Add(item);
        }

        public ObservableCollection<CartItem> GetItems()
        {
            return _items;
        }
    }
}