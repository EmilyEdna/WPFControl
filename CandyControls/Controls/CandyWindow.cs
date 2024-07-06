using System.Collections;
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
            this.IconVisibility = this.Icon == null ? Visibility.Collapsed : Visibility.Visible;
            this.Width = this.MinWidth = 1200;
            this.Height = this.MinHeight = 700;
        }

        #region 依赖属性
        internal Visibility IconVisibility
        {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }

        internal static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(CandyWindow), new(Visibility.Visible));

        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            set { SetValue(BlurRadiusProperty, value); }
        }

        public static readonly DependencyProperty BlurRadiusProperty =
            DependencyProperty.Register("BlurRadius", typeof(double), typeof(CandyWindow), new PropertyMetadata(15d));

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
                this.Width = this.MinWidth;
                this.Height = this.MinHeight;
            }
            else if (data == 3)
            {
                if (ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip)
                    SystemCommands.MaximizeWindow(this);
                this.Width = SystemParameters.PrimaryScreenWidth;
                this.Height = SystemParameters.PrimaryScreenHeight;
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
