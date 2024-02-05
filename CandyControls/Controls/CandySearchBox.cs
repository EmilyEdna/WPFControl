using CandyControls.ControlsModel.Enums;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace CandyControls
{
    public class CandySearchBox : TextBox
    {
        public event Action<CandySearchBox, TextChangedEventArgs> TextActionChanged;

        static CandySearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandySearchBox), new FrameworkPropertyMetadata(typeof(CandySearchBox)));
        }

        Button PART_CLEAR_BTN;
        ScrollViewer PART_CONTENTHOST;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_CLEAR_BTN = (Button)this.Template.FindName("PART_CLEAR_BTN", this);
            PART_CONTENTHOST = (ScrollViewer)this.Template.FindName("PART_ContentHost", this);
            PART_CLEAR_BTN.Click += ClearEvent;
            PART_CONTENTHOST.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.TextChanged += TextEvent;
        }

        #region Property
        internal Point Point
        {
            get { return (Point)GetValue(PointProperty); }
            set { SetValue(PointProperty, value); }
        }
        internal static readonly DependencyProperty PointProperty =
            DependencyProperty.Register("Point", typeof(Point), typeof(CandySearchBox), new PropertyMetadata(new Point(0, 0)));

        public EPlacment Placment
        {
            get { return (EPlacment)GetValue(PlacmentProperty); }
            set { SetValue(PlacmentProperty, value); }
        }
        public static readonly DependencyProperty PlacmentProperty =
            DependencyProperty.Register("Placment", typeof(EPlacment), typeof(CandySearchBox), new FrameworkPropertyMetadata(EPlacment.Right, OnPlacementChanged));
        public double BoxWidth
        {
            get { return (double)GetValue(BoxWidthProperty); }
            set { SetValue(BoxWidthProperty, value); }
        }
        public static readonly DependencyProperty BoxWidthProperty =
            DependencyProperty.Register("BoxWidth", typeof(double), typeof(CandySearchBox), new PropertyMetadata(150d));

        public bool ShowClear
        {
            get { return (bool)GetValue(ShowClearProperty); }
            set { SetValue(ShowClearProperty, value); }
        }
        public static readonly DependencyProperty ShowClearProperty =
            DependencyProperty.Register("ShowClear", typeof(bool), typeof(CandySearchBox), new PropertyMetadata(true));

        public Brush MoveBrush
        {
            get { return (Brush)GetValue(MoveBrushProperty); }
            set { SetValue(MoveBrushProperty, value); }
        }
        public static readonly DependencyProperty MoveBrushProperty =
            DependencyProperty.Register("MoveBrush", typeof(Brush), typeof(CandySearchBox), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d3d3d3"))));
        #endregion

        #region Method
        private void ClearEvent(object sender, RoutedEventArgs e)
        {
            this.Text = string.Empty;
        }

        private void TextEvent(object sender, TextChangedEventArgs e)
        {
            var box = (sender as CandySearchBox);
            box.ScrollToHorizontalOffset(box.VisualOffset.X);
            TextActionChanged?.Invoke(box, e);
        }
        private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = (d as CandySearchBox);
            if ((EPlacment)e.NewValue == EPlacment.Left)
                box.Point = new Point(1, 1);
            else
                box.Point = new Point(0, 0);
        }
        #endregion
    }
}
