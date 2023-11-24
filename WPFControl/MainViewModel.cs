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
            Img = new ObservableCollection<string>
            {
             "https://fc.sinaimg.cn/mw1024/006yt1Omgy1hfwrvzvdztj30nh0xcqbh.jpg",
             "https://fc.sinaimg.cn/mw1024/006yt1Omgy1hfwrvzvdztj30nh0xcqbh.jpg",
             "https://fc.sinaimg.cn/mw1024/006yt1Omgy1hfwrvzvdztj30nh0xcqbh.jpg"
            };
        }
        [ObservableProperty]
        private ObservableCollection<string> _Img;
    }
}
