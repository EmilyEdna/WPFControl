using CandyControls.ControlsModel.Enums;
using CandyControls.ControlsModel.Thicks;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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
        Border PART_TXT;
        TextBlock PART_SEARCH;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_SEARCH = (TextBlock)this.Template.FindName("PART_SEARCH", this);
            PART_TXT = (Border)this.Template.FindName("PART_TXT", this);
            PART_CLEAR_BTN = (Button)this.Template.FindName("PART_CLEAR_BTN", this);
            PART_CONTENTHOST = (ScrollViewer)this.Template.FindName("PART_ContentHost", this);
            PART_CLEAR_BTN.Click += ClearEvent;
            PART_CONTENTHOST.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            PART_SEARCH.PreviewMouseLeftButtonDown += SearchIconEvent;
            this.TextChanged += TextEvent;
            this.KeyDown += KeyEvent;
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

        public bool UseAutoComplete
        {
            get { return (bool)GetValue(UseAutoCompleteProperty); }
            set { SetValue(UseAutoCompleteProperty, value); }
        }
        public static readonly DependencyProperty UseAutoCompleteProperty =
            DependencyProperty.Register("UseAutoComplete", typeof(bool), typeof(CandySearchBox), new PropertyMetadata(false));

        public ItemsPanelTemplate ItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(ItemsPanelProperty); }
            set { SetValue(ItemsPanelProperty, value); }
        }
        public static readonly DependencyProperty ItemsPanelProperty =
            DependencyProperty.Register("ItemsPanel", typeof(ItemsPanelTemplate), typeof(CandySearchBox), new PropertyMetadata(new ItemsPanelTemplate(new FrameworkElementFactory(typeof(StackPanel)))));

        public DataTemplate AutoComplete
        {
            get { return (DataTemplate)GetValue(AutoCompleteProperty); }
            set { SetValue(AutoCompleteProperty, value); }
        }
        public static readonly DependencyProperty AutoCompleteProperty =
            DependencyProperty.Register("AutoComplete", typeof(DataTemplate), typeof(CandySearchBox), new PropertyMetadata(default));

        public ICommand AutoKeyCommand
        {
            get { return (ICommand)GetValue(AutoKeyCommandProperty); }
            set { SetValue(AutoKeyCommandProperty, value); }
        }
        public static readonly DependencyProperty AutoKeyCommandProperty =
            DependencyProperty.Register("AutoKeyCommand", typeof(ICommand), typeof(CandySearchBox), new PropertyMetadata(default));

        public ICommand EnterCommand
        {
            get { return (ICommand)GetValue(EnterCommandProperty); }
            set { SetValue(EnterCommandProperty, value); }
        }
        public static readonly DependencyProperty EnterCommandProperty =
            DependencyProperty.Register("EnterCommand", typeof(ICommand), typeof(CandySearchBox), new PropertyMetadata(default));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CandySearchBox), new PropertyMetadata(default));

        public Brush MaskFill
        {
            get { return (Brush)GetValue(MaskFillProperty); }
            set { SetValue(MaskFillProperty, value); }
        }
        public static readonly DependencyProperty MaskFillProperty =
            DependencyProperty.Register("MaskFill", typeof(Brush), typeof(CandySearchBox), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20DADADA"))));
        public Style ListStyle
        {
            get { return (Style)GetValue(ListStyleProperty); }
            set { SetValue(ListStyleProperty, value); }
        }
        public static readonly DependencyProperty ListStyleProperty =
            DependencyProperty.Register("ListStyle", typeof(Style), typeof(CandySearchBox), new PropertyMetadata(default));
        #endregion

        #region Method
        private void ClearEvent(object sender, RoutedEventArgs e)
        {
            var box = (sender as Button).FindParent<CandySearchBox>();
            this.Text = string.Empty;
            if (box.UseAutoComplete)
            {
                box.AutoKeyCommand?.Execute(this.Text);
            }
        }

        private void KeyEvent(object sender, KeyEventArgs e)
        {
            var box = (sender as CandySearchBox);
            if (e.Key == Key.Enter && EnterCommand != null) EnterCommand.Execute(box.Text); 
        }

        private void TextEvent(object sender, TextChangedEventArgs e)
        {
            var box = (sender as CandySearchBox);
            box.ScrollToHorizontalOffset(box.VisualOffset.X);
            TextActionChanged?.Invoke(box, e);
            if (box.UseAutoComplete)
            {
                box.AutoKeyCommand?.Execute(this.Text);

                Grid panal = new Grid
                {
                    Height = 150,
                };
                panal.Children.Add(new Rectangle
                {
                    Height = panal.Height,
                    Width = box.BoxWidth,
                    Fill = MaskFill,
                });
                panal.Children.Add(new ListBox
                {
                    Style=box.ListStyle,
                    Height = panal.Height,
                    ItemsPanel = ItemsPanel,
                    ItemsSource = box.ItemsSource,
                    ItemTemplate = AutoComplete
                });
                Popup popup = new Popup
                {
                    Placement = PlacementMode.Bottom,
                    PlacementTarget = PART_TXT,
                    AllowsTransparency = true,
                    StaysOpen = false,
                    Height = panal.Height,
                    Width = panal.Width,
                    Child = panal
                };
                popup.IsOpen = true;
                popup.Closed += delegate
                {
                    CloseAnime();
                };
                popup.LostFocus += delegate
                {
                    popup.IsOpen = false;
                };
            }
        }

        private void SearchIconEvent(object sender, MouseButtonEventArgs e)
        {
            OpenAnime();
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

        #region Anime
        Storyboard Close;
        Storyboard Open;
        private void CloseAnime()
        {
            Close = new Storyboard();
            DoubleAnimationUsingKeyFrames Revolve = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(Revolve, PART_TXT);
            Storyboard.SetTargetProperty(Revolve, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Revolve.KeyFrames.Add(new EasingDoubleKeyFrame(1, TimeSpan.FromSeconds(0)));
            Revolve.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(1)));

            DoubleAnimationUsingKeyFrames Revolve2 = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(Revolve2, PART_TXT);
            Storyboard.SetTargetProperty(Revolve2, new PropertyPath("(UIElement.Opacity)"));
            Revolve2.KeyFrames.Add(new EasingDoubleKeyFrame(1, TimeSpan.FromSeconds(0)));
            Revolve2.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(1)));

            Close.Children.Add(Revolve);
            Close.Children.Add(Revolve2);
            Close.Begin();
        }

        private void OpenAnime()
        {
            Open = new Storyboard();
            DoubleAnimationUsingKeyFrames Revolve = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(Revolve, PART_TXT);
            Storyboard.SetTargetProperty(Revolve, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Revolve.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            Revolve.KeyFrames.Add(new EasingDoubleKeyFrame(1, TimeSpan.FromSeconds(1)));

            DoubleAnimationUsingKeyFrames Revolve2 = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(Revolve2, PART_TXT);
            Storyboard.SetTargetProperty(Revolve2, new PropertyPath("(UIElement.Opacity)"));
            Revolve2.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            Revolve2.KeyFrames.Add(new EasingDoubleKeyFrame(1, TimeSpan.FromSeconds(1)));

            Open.Children.Add(Revolve);
            Open.Children.Add(Revolve2);
            Open.Begin();
        }
        #endregion
    }
}
