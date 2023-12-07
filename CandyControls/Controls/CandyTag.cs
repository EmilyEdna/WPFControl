using System.Windows;
using System.Windows.Controls;

namespace CandyControls
{
    public class CandyTag : ContentControl
    {
        static CandyTag()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyTag), new FrameworkPropertyMetadata(typeof(CandyTag)));
        }


        public override void OnApplyTemplate()
        {
            (this.Template.FindName("PART_BTN", this) as Button).Click += (s, e) =>
            {
                this.Visibility = Visibility.Collapsed;
            };
        }
    }
}
