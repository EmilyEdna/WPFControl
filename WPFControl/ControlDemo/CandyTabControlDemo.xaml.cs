using CandyControls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WPFControl.ControlDemo
{
    /// <summary>
    /// CandyTabControlDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CandyTabControlDemo : UserControl
    {
        public CandyTabControlDemo()
        {
            InitializeComponent();
            this.DataContext = new CandyTabControlDemoVM();
        }
    }
    public class CandyTabControlDemoDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public partial class CandyTabControlDemoVM : ObservableObject
    {
        public CandyTabControlDemoVM()
        {
            Title = [];
            Title.Add(new CandyTabControlDemoDto
            {
                Key = "我是第1个",
                Value = "1"
            });
            Title.Add(new CandyTabControlDemoDto
            {
                Key = "我是第2个",
                Value = "2"
            });
            Title.Add(new CandyTabControlDemoDto
            {
                Key = "我是第3个",
                Value = "3"
            });
        }

        [ObservableProperty]
        private ObservableCollection<CandyTabControlDemoDto> _Title;

        [RelayCommand]
        private void Open(object dto)
        { 
        
        }
    }
}
