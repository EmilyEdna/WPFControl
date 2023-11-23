using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WPFControl
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _Name;

        [RelayCommand]
        private void Test(string name)
        {
            Name = name;
        }
    }
}
