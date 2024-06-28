using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XExten.Advance.LinqFramework;

namespace CandyControls
{
    public class CandyWindow : Window
    {
        public CandyWindow()
        {
            this.SetResourceReference(StyleProperty, "BlurWindow");
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #region 依赖属性
        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            set { SetValue(BlurRadiusProperty, value); }
        }

        public static readonly DependencyProperty BlurRadiusProperty =
            DependencyProperty.Register("BlurRadius", typeof(double), typeof(CandyWindow), new PropertyMetadata(15d));

        internal VerticalAlignment InfoAlignment
        {
            get { return (VerticalAlignment)GetValue(InfoAlignmentProperty); }
            set { SetValue(InfoAlignmentProperty, value); }
        }

        internal static readonly DependencyProperty InfoAlignmentProperty =
            DependencyProperty.Register("InfoAlignment", typeof(VerticalAlignment), typeof(CandyWindow), new PropertyMetadata(VerticalAlignment.Center));

        public Control SearchBox
        {
            get { return (Control)GetValue(SearchBoxProperty); }
            set { SetValue(SearchBoxProperty, value); }
        }
        public static readonly DependencyProperty SearchBoxProperty =
            DependencyProperty.Register("SearchBox", typeof(Control), typeof(CandyWindow), new PropertyMetadata(default));

        public Control MenuBox
        {
            get { return (Control)GetValue(MenuBoxProperty); }
            set { SetValue(MenuBoxProperty, value); }
        }
        public static readonly DependencyProperty MenuBoxProperty =
            DependencyProperty.Register("MenuBox", typeof(Control), typeof(CandyWindow), new PropertyMetadata(default));
        #endregion

        public override void OnApplyTemplate()
        {
            this.Width =this.MinWidth= 1900;
            this.Height =this.MinHeight= 1000;
            ((Border)this.Template.FindName("HeadLayout", this)).PreviewMouseLeftButtonDown += MoveEvent;
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
                this.InfoAlignment = VerticalAlignment.Center;
                this.Width = this.MinWidth;
                this.Height = this.MinHeight;
            }
            else if (data == 3)
            {
                if (ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip)
                    SystemCommands.MaximizeWindow(this);
                this.Width = SystemParameters.PrimaryScreenWidth;
                this.Height = SystemParameters.PrimaryScreenHeight-48;
                this.InfoAlignment = VerticalAlignment.Bottom;
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
