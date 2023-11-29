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
using CommunityToolkit.Mvvm.ComponentModel;

namespace WPFControl.ControlDemo
{
    /// <summary>
    /// CandyTagDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CandyContainerDemo : UserControl
    {
        public CandyContainerDemo()
        {
            InitializeComponent();
        }
    }

    public partial class CandyContainerDemoVM : ObservableObject
    {
        public CandyContainerDemoVM()
        {
            Data = new ObservableCollection<string> { "1", "2", "3" };
        }
        [ObservableProperty]
        private ObservableCollection<string> _Data;
    }
}
