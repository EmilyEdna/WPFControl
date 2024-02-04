using CandyControls.ControlsModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CandyControls
{
    public class CandySearchBox : TextBox
    {
        static CandySearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandySearchBox), new FrameworkPropertyMetadata(typeof(CandySearchBox)));
        }
        public EPlacment Placment
        {
            get { return (EPlacment)GetValue(PlacmentProperty); }
            set { SetValue(PlacmentProperty, value); }
        }
        public static readonly DependencyProperty PlacmentProperty =
            DependencyProperty.Register("Placment", typeof(EPlacment), typeof(EPlacment), new PropertyMetadata(EPlacment.Right));
    }
}
