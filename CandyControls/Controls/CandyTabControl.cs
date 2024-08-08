using CandyControls.ControlsModel.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CandyControls
{
    public class CandyTabControl : TabControl
    {
        static CandyTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyTabControl), new FrameworkPropertyMetadata(typeof(CandyTabControl)));
        }

        public Brush BorderBackgroud
        {
            get { return (Brush)GetValue(BorderBackgroudProperty); }
            set { SetValue(BorderBackgroudProperty, value); }
        }
        public static readonly DependencyProperty BorderBackgroudProperty =
            DependencyProperty.Register("BorderBackgroud", typeof(Brush), typeof(CandyTabControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20B3B3B3"))));

        public Brush HeadBackgroud
        {
            get { return (Brush)GetValue(HeadBackgroudProperty); }
            set { SetValue(HeadBackgroudProperty, value); }
        }
        public static readonly DependencyProperty HeadBackgroudProperty =
            DependencyProperty.Register("HeadBackgroud", typeof(Brush), typeof(CandyTabControl), new PropertyMetadata(Brushes.White));

        public Brush MoveBackgroud
        {
            get { return (Brush)GetValue(MoveBackgroudProperty); }
            set { SetValue(MoveBackgroudProperty, value); }
        }
        public static readonly DependencyProperty MoveBackgroudProperty =
            DependencyProperty.Register("MoveBackgroud", typeof(Brush), typeof(CandyTabControl), new PropertyMetadata(new LinearGradientBrush(
            [
                new GradientStop((Color)ColorConverter.ConvertFromString("#337AE2FD"), 0),
                new GradientStop((Color)ColorConverter.ConvertFromString("#33F1C6C6"), 1)
            ])));

        public Brush SelectBackgroud
        {
            get { return (Brush)GetValue(SelectBackgroudProperty); }
            set { SetValue(SelectBackgroudProperty, value); }
        }
        public static readonly DependencyProperty SelectBackgroudProperty =
            DependencyProperty.Register("SelectBackgroud", typeof(Brush), typeof(CandyTabControl), new PropertyMetadata(new LinearGradientBrush(
            [
                new GradientStop((Color)ColorConverter.ConvertFromString("#33F1C6C6"), 0),
                new GradientStop((Color)ColorConverter.ConvertFromString("#33A5F9C1"), 1)
            ])));
        public ECatagory BorderType
        {
            get { return (ECatagory)GetValue(BorderTypeProperty); }
            set { SetValue(BorderTypeProperty, value); }
        }
        public static readonly DependencyProperty BorderTypeProperty =
            DependencyProperty.Register("BorderType", typeof(ECatagory), typeof(CandyTabControl), new PropertyMetadata(ECatagory.Primary));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(CandyTabControl), new PropertyMetadata(new CornerRadius(5)));

        public EPlacement Placement
        {
            get { return (EPlacement)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(EPlacement), typeof(CandyTabControl), new PropertyMetadata(EPlacement.Top));

        public bool UseViewBox
        {
            get { return (bool)GetValue(UseViewBoxProperty); }
            set { SetValue(UseViewBoxProperty, value); }
        }
        public static readonly DependencyProperty UseViewBoxProperty =
            DependencyProperty.Register("UseViewBox", typeof(bool), typeof(CandyTabControl), new PropertyMetadata(false));
    }
}           
