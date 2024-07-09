using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CandyControls
{
    public class CandySwitch : ToggleButton
    {
        static CandySwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandySwitch), new FrameworkPropertyMetadata(typeof(CandySwitch)));
        }

        public CornerRadius BorderRadius
        {
            get { return (CornerRadius)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }
        public static readonly DependencyProperty BorderRadiusProperty =
            DependencyProperty.Register("BorderRadius", typeof(CornerRadius), typeof(CandySwitch), new PropertyMetadata(new CornerRadius(15)));

        public Brush HoverBrushes
        {
            get { return (Brush)GetValue(HoverBrushesProperty); }
            set { SetValue(HoverBrushesProperty, value); }
        }
        public static readonly DependencyProperty HoverBrushesProperty =
            DependencyProperty.Register("HoverBrushes", typeof(Brush), typeof(CandySwitch), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(80, 249, 232, 248))));
    }
}
