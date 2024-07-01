using System.Windows;
using System.Windows.Controls;
using CandyControls.ControlsModel.Enums;

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


        /// <summary>
        /// 是否启用斜边框
        /// </summary>
        public bool EnableBeveleBorder
        {
            get { return (bool)GetValue(EnableBeveleBorderProperty); }
            set { SetValue(EnableBeveleBorderProperty, value); }
        }

        public static readonly DependencyProperty EnableBeveleBorderProperty =
            DependencyProperty.Register("EnableBeveleBorder", typeof(bool), typeof(CandyButton), new PropertyMetadata(false));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(CandyButton), new PropertyMetadata(new CornerRadius()));
    }
}
