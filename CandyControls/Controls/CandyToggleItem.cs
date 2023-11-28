using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace CandyControls
{
    public class CandyToggleItem : ListBoxItem
    {
        public bool UnderLine
        {
            get { return (bool)GetValue(UnderLineProperty); }
            set { SetValue(UnderLineProperty, value); }
        }

        public static readonly DependencyProperty UnderLineProperty =
            DependencyProperty.Register("UnderLine", typeof(bool), typeof(CandyToggleItem), new PropertyMetadata(true));
    }
}
