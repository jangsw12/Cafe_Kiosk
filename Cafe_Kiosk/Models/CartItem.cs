using Cafe_Kiosk.Models.Enums;
using Cafe_Kiosk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cafe_Kiosk.Models
{
    public class CartItem : ViewModelBase
    {
        // Properties
        public CafeMenuItem MenuItem { get; }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { 
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        private CoffeeSize _selectedCoffeeSize;

        public CoffeeSize SelectedCoffeeSize
        {
            get { return _selectedCoffeeSize; }
            set {
                if (_selectedCoffeeSize != value)
                {
                    _selectedCoffeeSize = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        private IceAmount _selectedIceAmount;

        public IceAmount SelectedIceAmount
        {
            get { return _selectedIceAmount; }
            set {
                if (_selectedIceAmount != value)
                {
                    _selectedIceAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        // Syrup Property
        // .... 작성 예정

        public int TotalPrice
        {
            get
            {
                int basePrice = MenuItem.Price;

                // Size
                int sizeExtra = SelectedCoffeeSize switch
                {
                    CoffeeSize.Small => 0,
                    CoffeeSize.Tall => 500,
                    CoffeeSize.Large => 1000,
                    _ => 0
                };

                // Syrup
                // .... 작성 예정


                return (basePrice + sizeExtra/* + Syrup*/) * Quantity;
            }
        }

        // Constructor
        public CartItem(CafeMenuItem menuItem, int quantity)
        {
            MenuItem = menuItem;
            Quantity = quantity;

            // Initally Default Value
            SelectedCoffeeSize = CoffeeSize.Small;
            SelectedIceAmount = IceAmount.Regular;
            // Syrup 초기값 설정
        }
    }
}