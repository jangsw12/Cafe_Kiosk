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
using Cafe_Kiosk.Services.Dialog;
using Cafe_Kiosk.Commands;
using FontAwesome6.Svg;
using Cafe_Kiosk.Services.API;

namespace Cafe_Kiosk.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        // Properties
        private readonly IDialogService _dialogService;
        private readonly IMenuApiService _menuApiService;

        public ObservableCollection<CafeMenuItem> MenuItems { get; } = new();

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
        public MenuViewModel(IDialogService dialogService, IMenuApiService menuApiService)
        {
            _dialogService = dialogService;
            _menuApiService = menuApiService;

            NextCategoryCommand = new RelayCommand<object>(NextCategory);
            PreviousCategoryCommand = new RelayCommand<object>(PreviousCategory);
            MenuItemClickCommand = new RelayCommand<CafeMenuItem>(MenuItemClick);

            LoadMenuAsync();
        }

        private async void LoadMenuAsync()
        {
            try
            {
                var items = await _menuApiService.GetCafeMenuAsync();

                if (items == null || items.Count == 0)
                {
                    MessageBox.Show("데이터가 없습니다. DB 또는 API 연결을 확인하세요.");
                    return;
                }

                MenuItems.Clear();
                foreach (var item in items)
                {
                    if (!string.IsNullOrEmpty(item.Image) && !item.Image.StartsWith("http"))
                    {
                        item.Image = $"pack://application:,,,/Images/{item.Image}";
                    }

                    MenuItems.Add(item);
                }

                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"API 호출 실패: {ex.Message}");
            }
        }

        private void ApplyFilter()
        {
            FilteredMenuItems.Clear();

            var filtered = CurrentCategory == "All"
                ? MenuItems
                : MenuItems.Where(item => item.Category == CurrentCategory);

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
            if (selectedItem == null)
                return;

            _dialogService.ShowMenuOptionDialog(selectedItem);
        }
    }
}