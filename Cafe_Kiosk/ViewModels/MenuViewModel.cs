using Cafe_Kiosk.Services;
using Cafe_Kiosk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using Cafe_Kiosk.ViewModels;
using System.Windows;
using Cafe_Kiosk.Views;
using Cafe_Kiosk.Services.MenuData;
using Cafe_Kiosk.Services.Dialog;
using Cafe_Kiosk.Commands;
using FontAwesome6.Svg;

namespace Cafe_Kiosk.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        // Properties
        private readonly IMenuDataService _menuDataService;
        private readonly IDialogService _dialogService;

        private List<CafeMenuItem> _allMenuItems;

        private readonly List<string> _categories = new() { "All", "Coffee", "Latte", "Tea" };
        public string CurrentIcon => CurrentCategory switch
        {
            "All" => "Solid_GripLines",
            "Coffee" => "Solid_MugSaucer",
            "Latte" => "Solid_MugHot",
            "Tea" => "Solid_Leaf",
            _ => string.Empty
        };

        private int _currentCategoryIndex = 0;

        public string CurrentCategory => _categories[_currentCategoryIndex];

        public ObservableCollection<CafeMenuItem> FilteredMenuItems { get; } = new();

        // Commands
        public ICommand NextCategoryCommand { get; set; }
        public ICommand PreviousCategoryCommand { get; set; }
        public ICommand MenuItemClickCommand { get; set; }

        // Constructor
        public MenuViewModel(IMenuDataService menuDataService, IDialogService dialogService)
        {
            _menuDataService = menuDataService;
            _dialogService = dialogService;

            NextCategoryCommand = new RelayCommand<object>(NextCategory);
            PreviousCategoryCommand = new RelayCommand<object>(PreviousCategory);
            MenuItemClickCommand = new RelayCommand<CafeMenuItem>(MenuItemClick);

            LoadMenu();
        }

        // Methods
        private void LoadMenu()
        {
            _allMenuItems = _menuDataService.LoadCafeMenu();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            FilteredMenuItems.Clear();

            var filtered = CurrentCategory == "All"
                ? _allMenuItems
                : _allMenuItems.Where(item => item.Category == CurrentCategory);

            foreach (var item in filtered)
                FilteredMenuItems.Add(item);

            OnPropertyChanged(nameof(CurrentCategory));
            OnPropertyChanged(nameof(CurrentIcon));
        }

        private void NextCategory(object _)
        {
            _currentCategoryIndex = (_currentCategoryIndex + 1) % _categories.Count;
            ApplyFilter();
        }

        private void PreviousCategory(object _)
        {
            _currentCategoryIndex = (_currentCategoryIndex - 1 + _categories.Count) % _categories.Count;
            ApplyFilter();
        }

        private void MenuItemClick(CafeMenuItem selectedItem)
        {
            _dialogService.ShowMenuOptionDialog(selectedItem);
        }
    }
}