using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cafe_Kiosk.ViewModels
{
    public class MainViewModel
    {
        public ICommand TestClickCommand { get; set; }

        public MainViewModel()
        {
            TestClickCommand = new RelayCommand<object>(TestClick);
        }

        private void TestClick(object _)
        {
            MessageBox.Show("프로그램 시작");
        }
    }
}