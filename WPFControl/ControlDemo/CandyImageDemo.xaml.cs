using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WPFControl.ControlDemo
{
    /// <summary>
    /// CandyImageDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CandyImageDemo : UserControl
    {
        public CandyImageDemo()
        {
            InitializeComponent();
            this.DataContext = new CandyImageDemoVM();
        }
    }
    public partial class CandyImageDemoVM : ObservableObject
    {
        public CandyImageDemoVM()
        {
            Img = new ObservableCollection<TestImageModel>
            {
                  new TestImageModel { Key="1",Value="https://fc.sinaimg.cn/mw1024/006yt1Omgy1hfwrvzvdztj30nh0xcqbh.jpg"},
                  new TestImageModel { Key="2",Value="https://fc.sinaimg.cn/mw1024/006yt1Omgy1hfwrvzvdztj30nh0xcqbh.jpg"},
                  new TestImageModel { Key="3",Value="https://fc.sinaimg.cn/mw1024/006yt1Omgy1hfwrvzvdztj30nh0xcqbh.jpg"},
            };
        }
        [ObservableProperty]
        private ObservableCollection<TestImageModel> _Img;
    }

    public class TestImageModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
