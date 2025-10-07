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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cafe_Kiosk.Views
{
    public partial class CartView : UserControl
    {
        public CartView()
        {
            InitializeComponent();
        }

        #region Smoot Scrolling Animation (Custom MouseWheel Handling)
        //private ScrollViewer _listViewScrollViewer;
        //private double _targetOffset = 0;
        //private bool _isAnimating = false;

        //private void MyListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    if (_listViewScrollViewer == null)
        //        _listViewScrollViewer = FindScrollViewer(MyListView);

        //    if (_listViewScrollViewer == null)
        //        return;

        //    e.Handled = true;

        //    _targetOffset -= e.Delta; // Delta는 120 단위
        //    _targetOffset = Math.Max(0, Math.Min(_listViewScrollViewer.ScrollableHeight, _targetOffset));

        //    if (!_isAnimating)
        //        SmoothScroll();
        //}

        //private async void SmoothScroll()
        //{
        //    _isAnimating = true;
        //    const double duration = 200;
        //    const int frames = 20;

        //    double start = _listViewScrollViewer.VerticalOffset;
        //    double delta = _targetOffset - start;

        //    for (int i = 0; i <= frames; i++)
        //    {
        //        double progress = (double)i / frames;
        //        double eased = EaseOutCubic(progress);
        //        _listViewScrollViewer.ScrollToVerticalOffset(start + delta * eased);
        //        await Task.Delay((int)(duration / frames));
        //    }

        //    _isAnimating = false;
        //}

        //private double EaseOutCubic(double t)
        //{
        //    return 1 - Math.Pow(1 - t, 3);
        //}

        //private ScrollViewer FindScrollViewer(DependencyObject parent)
        //{
        //    if (parent is ScrollViewer sv)
        //        return sv;

        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parent, i);
        //        var result = FindScrollViewer(child);
        //        if (result != null)
        //            return result;
        //    }
        //    return null;
        //}
        #endregion
    }
}