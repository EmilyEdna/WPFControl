using CandyControls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// CandyWaterButtonDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CandyWaterButtonDemo : UserControl
    {
        public CandyWaterButtonDemo()
        {
            InitializeComponent();
            this.DataContext = new CandyWaterButtonDemoVM();
        }

        private void CandyWaterButton_Click(object sender, RoutedEventArgs e)
        {
            var name = (sender as CandyWaterButton).SelectName;
        }
    }
    public partial class CandyWaterButtonDemoVM : ObservableObject
    {
        public CandyWaterButtonDemoVM()
        {
            Data = [1, 2, 3, 4, 5, 6, 7, 8];
        }

        [ObservableProperty]
        private ObservableCollection<int> _Data;

        [RelayCommand]
        public void Click(object param) 
        {
            var input = param;
        }
    }
}
