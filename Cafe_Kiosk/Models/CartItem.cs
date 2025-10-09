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

        private ShotCount _selectedShotCount;

        public ShotCount SelectedShotCount
        {
            get { return _selectedShotCount; }
            set
            {
                if (_selectedShotCount != value)
                {
                    _selectedShotCount = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        private Temperature _selectedTemperature;

        public Temperature SelectedTemperature
        {
            get { return _selectedTemperature; }
            set
            {
                if (_selectedTemperature != value)
                {
                    _selectedTemperature = value;
                    OnPropertyChanged();
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

                // Shot
                int shotExtra = SelectedShotCount switch
                {
                    ShotCount.Zero => 0,
                    ShotCount.One => 500,
                    ShotCount.Two => 1000,
                    _ => 0
                };

                return (basePrice + sizeExtra + shotExtra) * Quantity;
            }
        }

        // Display Properties
        public string DisplayCoffeeSize =>
            SelectedCoffeeSize switch
            {
                CoffeeSize.Small => "스몰",
                CoffeeSize.Tall => "톨",
                CoffeeSize.Large => "라지",
                _ => string.Empty
            };

        public string DisplayShotCount =>
            SelectedShotCount switch
            {
                ShotCount.Zero => "샷 추가 없음",
                ShotCount.One => "1샷 추가",
                ShotCount.Two => "2샷 추가",
                _ => string.Empty
            };

        //public string DisplayTemperature =>
        //    SelectedTemperature switch
        //    {
        //        Temperature.Ice => "아이스",
        //        Temperature.Hot => "뜨겁게",
        //        _ => string.Empty
        //    };

        //public string DisplayIceAmount
        //{
        //    get
        //    {
        //        if (SelectedTemperature == Temperature.Hot)
        //            return string.Empty;

        //        return SelectedIceAmount switch
        //        {
        //            IceAmount.None => "얼음 없음",
        //            IceAmount.Less => "얼음 적게",
        //            IceAmount.Regular => "얼음 보통",
        //            IceAmount.More => "얼음 많이",
        //            _ => string.Empty
        //        };
        //    }
        //}

        public string DisplayTemperatureWithIce
        {
            get
            {
                var tempText = SelectedTemperature switch
                {
                    Temperature.Ice => "아이스",
                    Temperature.Hot => "뜨겁게",
                    _ => string.Empty
                };

                if (SelectedTemperature == Temperature.Hot)
                    return tempText;

                var iceText = SelectedIceAmount switch
                {
                    IceAmount.None => "얼음 없음",
                    IceAmount.Less => "얼음 적게",
                    IceAmount.Regular => "얼음 보통",
                    IceAmount.More => "얼음 많이",
                    _ => string.Empty
                };

                return $"{tempText} ({iceText})";
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
            SelectedShotCount = ShotCount.One;
            SelectedTemperature = Temperature.Ice;
        }
    }
}