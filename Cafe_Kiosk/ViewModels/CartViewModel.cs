using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cafe_Kiosk.ViewModels
{
    public class CartViewModel : ViewModelBase
    {
		// Properties
        private readonly ICartService _cartService;

		public ObservableCollection<CafeMenuItem> CartItems => _cartService.GetItems();

		// Constructor
        public CartViewModel(ICartService cartService)
        {
            _cartService = cartService;
        }
    }
}