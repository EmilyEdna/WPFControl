using System.Collections.Generic;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPFControl.ControlDemo;

namespace WPFControl
{
    public partial class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
            Funcs = new Dictionary<string, int>
            {
                {"边框",1 }, {"按钮",2 },{"图片",3},{"切换",4},{"标签",5}
            };
        }

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
            }
        }
    }
}
