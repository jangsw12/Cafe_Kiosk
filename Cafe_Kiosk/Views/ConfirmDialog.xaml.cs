using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cafe_Kiosk.Views
{
    public partial class ConfirmDialog : Window
    {
        public bool Result { get; private set; }

        public ConfirmDialog(string message)
        {
            InitializeComponent();
            MessageText.Text = message;

            Loaded += ConfirmDialog_Loaded;
        }

        private void ConfirmDialog_Loaded(object sender, RoutedEventArgs e)
        {
            if (Owner != null)
            {
                Left = Owner.Left + (Owner.Width - ActualWidth) / 2;
                Top = Owner.Top + (Owner.Height - ActualHeight) / 2;
            }
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
        }
    }
}