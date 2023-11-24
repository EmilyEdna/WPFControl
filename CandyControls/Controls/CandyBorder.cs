using System.Windows;
using System.Windows.Controls;
using CandyControls.ControlsModel;

namespace CandyControls
{
    public class CandyBorder : Border
    {
        static CandyBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyBorder), new FrameworkPropertyMetadata(typeof(CandyBorder)));
        }

        public ECatagory BorderType
        {
            get { return (ECatagory)GetValue(BorderTypeProperty); }
            set { SetValue(BorderTypeProperty, value); }
        }
        /// <summary>
        /// [Primary] [Info] [Success] [Warn] [Error]
        /// </summary>
        public static readonly DependencyProperty BorderTypeProperty =
            DependencyProperty.Register("BorderType", typeof(ECatagory), typeof(CandyBorder), new PropertyMetadata(ECatagory.Primary));
    }
}
