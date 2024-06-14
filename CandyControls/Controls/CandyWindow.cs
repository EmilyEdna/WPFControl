using System;
using System.Diagnostics.Tracing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using XExten.Advance.LinqFramework;

namespace CandyControls
{
    public class CandyWindow : Window
    {

        static CandyWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyWindow), new FrameworkPropertyMetadata(typeof(CandyWindow)));
        }

        public Brush BlurBrush
        {
            get { return (Brush)GetValue(BlurBrushProperty); }
            set { SetValue(BlurBrushProperty, value); }
        }

        public static readonly DependencyProperty BlurBrushProperty =
            DependencyProperty.Register("BlurBrush", typeof(Brush), typeof(CandyWindow), new PropertyMetadata(default));


        public override void OnApplyTemplate()
        {
            Color blurColor = (Color)ColorConverter.ConvertFromString(this.BlurBrush.ToString());
            var Opacity = blurColor.R << 0 | blurColor.G << 8 | blurColor.B << 16 | blurColor.A << 24;
            WindowHelper.SetBlurWindow(this, Opacity);
            ((StackPanel)this.Template.FindName("HeadLayout", this)).PreviewMouseLeftButtonDown += MoveEvent;
            ((Button)this.Template.FindName("Minimize", this)).Click += HandleEvent;
            ((Button)this.Template.FindName("Restore", this)).Click += HandleEvent;
            ((Button)this.Template.FindName("Maximize", this)).Click += HandleEvent;
            ((Button)this.Template.FindName("Close", this)).Click += HandleEvent;
            base.OnApplyTemplate();
        }

        private void HandleEvent(object sender, RoutedEventArgs e)
        {
            var data = (sender as Button).CommandParameter.AsString().AsInt();
            if (data == 1)
            {
                if (ResizeMode != ResizeMode.NoResize)
                    SystemCommands.MinimizeWindow(this);
            }
            else if (data == 2)
            {
                if (ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip)
                    SystemCommands.RestoreWindow(this);
            }
            else if (data == 3)
            {
                if (ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip)
                    SystemCommands.MaximizeWindow(this);
            }
            else
                this.Close();
        }

        private void MoveEvent(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

    }
}
