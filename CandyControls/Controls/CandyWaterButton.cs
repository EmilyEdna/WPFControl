using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CandyControls
{
    public class CandyWaterButton : UserControl
    {
        private Grid Layout;
        private ToggleButton Toggle;
        private Grid LayoutItem;

        public CandyWaterButton()
        {
            this.Content = ElementCreate();
        }

        private FrameworkElement ElementCreate()
        {
            Layout = new Grid()
            {
                Name = "Layout"
            };

            Toggle = new ToggleButton
            {
                Name = "Toggle"
            };
            Toggle.Click += Toggle_Click;
            Toggle.Checked += Toggle_Checked;
            Toggle.Unchecked += Toggle_Unchecked;
            Toggle.SetResourceReference(ToggleButton.StyleProperty, "MainWaterBtn");

            LayoutItem = new Grid
            {
                Name = "LayoutItem"
            };

            Layout.Children.Add(Toggle);
            Layout.Children.Add(LayoutItem);

            return Layout;
        }

        #region 事件
        private void Toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Toggle_Checked(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
