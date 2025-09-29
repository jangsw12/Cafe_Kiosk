using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.ViewModels;
using Cafe_Kiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cafe_Kiosk.Services.Dialog
{
    public class DialogService : IDialogService
    {
        private Window _window;
        private readonly ICartService _cartService;

        public DialogService(ICartService cartService)
        {
            _cartService = cartService;
        }

        public void ShowMenuOptionDialog(CafeMenuItem menuItem)
        {
            _window = new MenuOptionView()
            {
                Width = Application.Current.MainWindow.ActualWidth * 0.75,
                Height = Application.Current.MainWindow.ActualHeight * 0.75,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            _window.Left = Application.Current.MainWindow.Left +
                                   (Application.Current.MainWindow.Width - _window.Width) / 2;
            _window.Top = Application.Current.MainWindow.Top + 
                                   (Application.Current.MainWindow.Height - _window.Height) / 2;

            _window.DataContext = new MenuOptionViewModel(menuItem, this, _cartService);
            _window.ShowDialog();
        }

        public void CloseMenuOptionDialog()
        {
            _window?.Close();
            _window = null;
        }
    }
}