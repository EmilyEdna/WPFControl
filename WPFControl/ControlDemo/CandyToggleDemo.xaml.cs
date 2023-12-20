using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WPFControl.ControlDemo
{
    /// <summary>
    /// CandyToggleDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CandyToggleDemo : UserControl
    {
        public CandyToggleDemo()
        {
            InitializeComponent();
        }
    }
    public partial class CandyToggleDemoVM : ObservableObject
    {
        public CandyToggleDemoVM()
        {
            Data = new ObservableCollection<string> { "测试1", "测试2" };
        }

        [ObservableProperty]
        private ObservableCollection<string> _Data;

        [RelayCommand]
        private void Test(object p)
        {
            var m = p;
        }
    }
}
