using Cafe_Kiosk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Services.Cart
{
    public interface ICartService
    {
        void AddItem(CartItem item);
        void RemoveItem(CartItem item);
        void ClearCart();
        bool HasItems();
        ObservableCollection<CartItem> GetItems();
    }
}