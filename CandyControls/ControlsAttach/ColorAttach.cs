using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CandyControls
{
    public static class ColorAttach
    {
        public static Brush GetBackColor(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackColorProperty);
        }
        public static void SetBackColor(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackColorProperty, value);
        }
        public static readonly DependencyProperty BackColorProperty =
            DependencyProperty.RegisterAttached("BackColor", typeof(Brush), typeof(ColorAttach), new PropertyMetadata(Brushes.Transparent, (d, e) =>
            {
                var Element = (d as Control);
                Element.Background = GetBackColor(d);
            }));

        public static Brush GetForeColor(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ForeColorProperty);
        }
        public static void SetForeColor(DependencyObject obj, Brush value)
        {
            obj.SetValue(ForeColorProperty, value);
        }
        public static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.RegisterAttached("ForeColor", typeof(Brush), typeof(ColorAttach), new PropertyMetadata(Brushes.Transparent, (d, e) =>
            {
                var Element = (d as Control);
                Element.Foreground = GetForeColor(d);
            }));
    }
}
