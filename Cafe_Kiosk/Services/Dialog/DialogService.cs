using Cafe_Kiosk.Models;
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
        public void ShowMenuOptionDialog(CafeMenuItem menuItem)
        {
            var _menuOptionView = new MenuOptionView()
            {
                Width = Application.Current.MainWindow.ActualWidth * 0.75,
                Height = Application.Current.MainWindow.ActualHeight * 0.75,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            _menuOptionView.Left = Application.Current.MainWindow.Left +
                                   (Application.Current.MainWindow.Width - _menuOptionView.Width) / 2;
            _menuOptionView.Top = Application.Current.MainWindow.Top + 
                                   (Application.Current.MainWindow.Height - _menuOptionView.Height) / 2;

            _menuOptionView.DataContext = new MenuOptionViewModel(menuItem);
            _menuOptionView.ShowDialog();
        }
    }
}