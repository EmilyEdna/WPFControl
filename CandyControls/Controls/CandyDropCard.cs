using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace CandyControls
{
    public class CandyDropCard : Control
    {
        static CandyDropCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyDropCard), new FrameworkPropertyMetadata(typeof(CandyDropCard)));
        }

        private Button _PART_TOP;
        private Button _PART_DOWN;
        private Border _PART_CONTENT;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Dispatcher.BeginInvoke(() =>
            {
                _PART_CONTENT = (Border)this.Template.FindName("PART_CONTENT", this);

                _PART_TOP = ((Button)this.Template.FindName("PART_TOP", this));
                _PART_TOP.Click -= ChangeEvent;
                _PART_TOP.Click += ChangeEvent;

                _PART_DOWN = ((Button)this.Template.FindName("PART_DOWN", this));
                _PART_DOWN.Click -= ChangeEvent;
                _PART_DOWN.Click += ChangeEvent;
            }, DispatcherPriority.Loaded);
        }

        private void ChangeEvent(object sender, RoutedEventArgs e)
        {
            _PART_CONTENT.RenderTransform = new ScaleTransform();
            var Scale = (_PART_CONTENT.RenderTransform as ScaleTransform);
            if ((sender as Button).Name.Equals("PART_TOP"))
            {
                _PART_CONTENT.Visibility = Visibility.Collapsed;
                _PART_TOP.Visibility = Visibility.Collapsed;
                _PART_DOWN.Visibility = Visibility.Visible;

                Scale.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromMilliseconds(200),
                });

                ChangedCommand?.Execute(_PART_TOP);
            }
            else
            {
                _PART_CONTENT.Visibility = Visibility.Visible;
                _PART_TOP.Visibility = Visibility.Visible;
                _PART_DOWN.Visibility = Visibility.Collapsed;

                Scale.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromMilliseconds(200),
                });

                ChangedCommand?.Execute(_PART_DOWN);
            }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CandyDropCard), new PropertyMetadata(default));

        public ICommand ChangedCommand
        {
            get { return (ICommand)GetValue(ChangedCommandProperty); }
            set { SetValue(ChangedCommandProperty, value); }
        }
        public static readonly DependencyProperty ChangedCommandProperty =
            DependencyProperty.Register("ChangedCommand", typeof(ICommand), typeof(CandyDropCard), new PropertyMetadata(default));

        public FrameworkElement InnerControl
        {
            get { return (Control)GetValue(InnerControlProperty); }
            set { SetValue(InnerControlProperty, value); }
        }
        public static readonly DependencyProperty InnerControlProperty =
            DependencyProperty.Register("InnerControl", typeof(FrameworkElement), typeof(CandyDropCard), new PropertyMetadata(default));
    }
}
