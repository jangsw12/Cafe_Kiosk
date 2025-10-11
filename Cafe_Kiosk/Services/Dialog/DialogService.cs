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
            var paymentWindow = new PaymentView()
            {
                Width = Application.Current.MainWindow.ActualWidth * 0.75,
                Height = Application.Current.MainWindow.ActualHeight * 0.75,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            paymentWindow.Left = Application.Current.MainWindow.Left +
                                   (Application.Current.MainWindow.Width - paymentWindow.Width) / 2;
            paymentWindow.Top = Application.Current.MainWindow.Top +
                                   (Application.Current.MainWindow.Height - paymentWindow.Height) / 2;

            paymentWindow.DataContext = App.Current.Services.GetRequiredService<PaymentViewModel>();
            paymentWindow.ShowDialog();
        }
    }
}