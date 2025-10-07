using Cafe_Kiosk.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cafe_Kiosk.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // Properties
        public MenuViewModel _menuViewModel { get; }
        public CartViewModel _cartViewModel { get; }

        // Constructor
        public MainViewModel(MenuViewModel menuViewModel, CartViewModel cartViewModel)
        {
            _menuViewModel = menuViewModel;
            _cartViewModel = cartViewModel;
        }
    }
}