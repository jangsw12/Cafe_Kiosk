using Cafe_Kiosk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cafe_Kiosk.Models
{
    public class CartItem : ViewModelBase
    {
        public CafeMenuItem MenuItem { get; }
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { 
                _quantity = value;
                OnPropertyChanged();
            }
        }

        public int TotalPrice => MenuItem.Price * Quantity;

        public CartItem(CafeMenuItem menuItem, int quantity)
        {
            MenuItem = menuItem;
            Quantity = quantity;
        }
    }
}