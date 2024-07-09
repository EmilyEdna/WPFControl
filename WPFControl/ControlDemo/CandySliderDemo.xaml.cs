using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFControl.ControlDemo
{
    /// <summary>
    /// CandySliderDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CandySliderDemo : UserControl
    {
        public CandySliderDemo()
        {
            InitializeComponent();
            this.DataContext = new CandySliderDemoVM();
        }
    }
    public partial class CandySliderDemoVM : ObservableObject
    {
        [RelayCommand]
        public void Change(double obj)
        {
            var x = obj;
        }
    }
}
