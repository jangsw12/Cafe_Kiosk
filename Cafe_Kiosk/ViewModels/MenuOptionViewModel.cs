using Cafe_Kiosk.Models;
using Cafe_Kiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cafe_Kiosk.ViewModels
{
    public class MenuOptionViewModel : ViewModelBase
    {
        private readonly CafeMenuItem _selectedItem;

        public MenuOptionViewModel(CafeMenuItem selectedItem)
        {
            _selectedItem = selectedItem;
        }

        public void OpenView()
        {
            var view = new MenuOptionView
            {
                Owner = Application.Current.MainWindow
            };

            view.ShowDialog();
        }
    }
}