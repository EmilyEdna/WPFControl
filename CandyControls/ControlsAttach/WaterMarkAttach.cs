using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XExten.Advance.LinqFramework;

namespace CandyControls
{
    public static class WaterMarkAttach
    {
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(WaterMarkAttach),
                new FrameworkPropertyMetadata(default(string), (sender, args) => {
                    var box = (TextBox)sender;
                    if (!args.NewValue.ToString().IsNullOrEmpty())
                    {
                        box.Foreground = Brushes.Gray;
                        box.Background = Brushes.Transparent;
                        box.Text = args.NewValue.ToString();

                        box.GotFocus += (s, e) =>
                        {
                            if (box.Text == args.NewValue.ToString())
                            {
                                box.Text = string.Empty;
                                box.Background = null;
                            }

                        };

                        box.LostFocus += (s, e) => 
                        {
                            box.Foreground = Brushes.Gray;
                            box.Background = Brushes.Transparent;
                            box.Text = args.NewValue.ToString();
                        };
                    }
                }));

        public static void SetPlaceholder(DependencyObject element, string value)
            => element.SetValue(PlaceholderProperty, value);

        public static string GetPlaceholder(DependencyObject element)
            => (string)element.GetValue(PlaceholderProperty);

    }
}
