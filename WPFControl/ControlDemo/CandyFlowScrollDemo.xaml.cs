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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WPFControl.ControlDemo
{
    /// <summary>
    /// CandyFlowScrollDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CandyFlowScrollDemo : UserControl
    {
        public CandyFlowScrollDemo()
        {
            InitializeComponent();
            this.DataContext = new CandyFlowScrollDemoVM();
        }
    }
    public partial class CandyFlowScrollDemoVM : ObservableObject
    {


        [RelayCommand]
        public void Test(string input)
        {
            var m = input;

        }
    }
}
