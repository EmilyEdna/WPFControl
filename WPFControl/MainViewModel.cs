using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WPFControl
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Img = new ObservableCollection<TestModel>
            {
               new TestModel{ Src="https://fc.sinaimg.cn/mw1024/006yt1Omgy1hfwrvzvdztj30nh0xcqbh.jpg",Name="测试" },
               new TestModel{ Src="https://fc.sinaimg.cn/mw1024/006yt1Omgy1hfwrvzvdztj30nh0xcqbh.jpg",Name="测试" },
               new TestModel{ Src="https://fc.sinaimg.cn/mw1024/006yt1Omgy1hfwrvzvdztj30nh0xcqbh.jpg",Name="测试" },
            };
        }
        [ObservableProperty]
        private ObservableCollection<TestModel> _Img;
    }
    public class TestModel { 
    
        public string Src { get; set; }
        public string Name { get; set; }
    }
}
