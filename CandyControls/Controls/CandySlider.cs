using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CandyControls
{
    public class CandySlider : Slider
    {
        static CandySlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandySlider), new FrameworkPropertyMetadata(typeof(CandySlider)));
        }
        public override void OnApplyTemplate()
        {
            this.ValueChanged += (sender, e) => Command?.Execute(e.NewValue);
            base.OnApplyTemplate();
        }

        public double BarHeight
        {
            get { return (double)GetValue(BarHeightProperty); }
            set { SetValue(BarHeightProperty, value); }
        }
        public static readonly DependencyProperty BarHeightProperty =
            DependencyProperty.Register("BarHeight", typeof(double), typeof(CandySlider), new PropertyMetadata(2d));

        public double ThumbWidthAndHeight
        {
            get { return (double)GetValue(ThumbWidthAndHeightProperty); }
            set { SetValue(ThumbWidthAndHeightProperty, value); }
        }
        public static readonly DependencyProperty ThumbWidthAndHeightProperty =
            DependencyProperty.Register("ThumbWidthAndHeight", typeof(double), typeof(CandySlider), new PropertyMetadata(15d));

        public Brush ThumbBackgroud
        {
            get { return (Brush)GetValue(ThumbBackgroudProperty); }
            set { SetValue(ThumbBackgroudProperty, value); }
        }
        public static readonly DependencyProperty ThumbBackgroudProperty =
            DependencyProperty.Register("ThumbBackgroud", typeof(Brush), typeof(CandySlider), new PropertyMetadata(Brushes.Pink));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CandySlider), new PropertyMetadata(default));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CandySlider), new PropertyMetadata(default));
    }
}
