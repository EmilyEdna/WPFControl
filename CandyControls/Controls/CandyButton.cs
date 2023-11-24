using System.Windows;
using System.Windows.Controls;
using CandyControls.ControlsModel;

namespace CandyControls
{
    public class CandyButton : Button
    {
        static CandyButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyButton), new FrameworkPropertyMetadata(typeof(CandyButton)));
        }

        public ECatagory ButtonType
        {
            get { return (ECatagory)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }
        /// <summary>
        /// [Primary] [Info] [Success] [Warn] [Error]
        /// </summary>
        public static readonly DependencyProperty ButtonTypeProperty =
            DependencyProperty.Register("ButtonType", typeof(ECatagory), typeof(CandyButton), new PropertyMetadata(ECatagory.Primary));
    }
}
