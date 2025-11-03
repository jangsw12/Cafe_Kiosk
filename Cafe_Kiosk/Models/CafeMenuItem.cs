using Cafe_Kiosk.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cafe_Kiosk.Models
{
    public class CafeMenuItem : ViewModelBase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name = string.Empty;

        public string Name
        {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _price;

        public int Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private string _category = string.Empty;

        public string Category
        {
            get { return _category; }
            set { 
                _category = value;
                OnPropertyChanged();
            }
        }

        private string _image = string.Empty;

        public string Image
        {
            get { return _image; }
            set
            { 
                _image = value;
                OnPropertyChanged();
            }
        }
    }
}