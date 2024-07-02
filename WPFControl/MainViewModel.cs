using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WPFControl.ControlDemo;

namespace WPFControl
{
    public partial class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
            Funcs = new Dictionary<string, int>
            {
                {"边框",1 }, {"按钮",2 },{"图片",3},{"切换",4},{"容器标签",5},{"标签",6 },
                {"滚动条",7 },{ "搜索框",8},{ "缓冲图",9},{"水滴按钮",10 }
            };

        }

        [ObservableProperty]
        private string _Key;

        [ObservableProperty]
        private Control _Ctrl;

        [ObservableProperty]
        private Dictionary<string, int> _Funcs;

        [RelayCommand]
        public void MenuTarget(int key)
        {
            switch (key)
            {
                case 1:
                    Ctrl = new CandyBorderDemo();
                    break;
                case 2:
                    Ctrl = new CandyButtonDemo();
                    break;
                case 3:
                    Ctrl = new CandyImageDemo();
                    break;
                case 4:
                    Ctrl = new CandyToggleDemo();
                    break;
                case 5:
                    Ctrl = new CandyContainerDemo();
                    break;
                case 6:
                    Ctrl = new CandyTagDemo();
                    break;
                case 7:
                    Ctrl = new CandyScrollViewDemo();
                    break;
                case 8:
                    Ctrl = new CandySearchBoxDemo();
                    break;
                case 9:
                    Ctrl = new CandyViewerDemo();
                    break;
                case 10:
                    Ctrl = new CandyWaterButtonDemo();
                    break;
            }
        }

        [RelayCommand]
        public void SearchActive(string obj)
        {
            var mm = obj;
        }
    }
}
