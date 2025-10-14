using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.ViewModels;
using Cafe_Kiosk.ViewModels.Payment;
using Cafe_Kiosk.Views;
using Cafe_Kiosk.Views.Payment;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Cafe_Kiosk.Services.Dialog
{
    public class DialogService : IDialogService
    {
        // Properties
        private Window _menuOptionWindow;
        private Window _paymentWindow;
        private readonly ICartService _cartService;

        // Constructor
        public DialogService(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Methods
        public void ShowMenuOptionDialog(CafeMenuItem menuItem)
        {
            _menuOptionWindow = new MenuOptionView()
            {
                Width = Application.Current.MainWindow.ActualWidth * 0.75,
                Height = Application.Current.MainWindow.ActualHeight * 0.75,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            _menuOptionWindow.Left = Application.Current.MainWindow.Left +
                                   (Application.Current.MainWindow.Width - _menuOptionWindow.Width) / 2;
            _menuOptionWindow.Top = Application.Current.MainWindow.Top + 
                                   (Application.Current.MainWindow.Height - _menuOptionWindow.Height) / 2;

            _menuOptionWindow.DataContext = new MenuOptionViewModel(menuItem, this, _cartService);
            _menuOptionWindow.ShowDialog();
        }

        public void CloseMenuOptionDialog()
        {
            _menuOptionWindow?.Close();
            _menuOptionWindow = null;
        }

        public bool ShowConfirmation(string message, string title)
        {
            var dialog = new ConfirmDialog(message)
            {
                Title = title,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            dialog.ShowDialog();

            return dialog.Result;
        }

        public void ShowPaymentDialog()
        {
            _paymentWindow = new PaymentView()
            {
                Width = Application.Current.MainWindow.ActualWidth * 0.75,
                Height = Application.Current.MainWindow.ActualHeight * 0.75,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            _paymentWindow.Left = Application.Current.MainWindow.Left +
                                   (Application.Current.MainWindow.Width - _paymentWindow.Width) / 2;
            _paymentWindow.Top = Application.Current.MainWindow.Top +
                                   (Application.Current.MainWindow.Height - _paymentWindow.Height) / 2;

            _paymentWindow.DataContext = App.Current.Services.GetRequiredService<PaymentViewModel>();
            _paymentWindow.ShowDialog();
        }

        public void ClosePaymentDialog()
        {
            _paymentWindow?.Close();
            _paymentWindow = null;
        }
    }
}