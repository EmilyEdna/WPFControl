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
    /// CandySearchBoxDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CandySearchBoxDemo : UserControl
    {
        public CandySearchBoxDemo()
        {
            InitializeComponent();
            this.DataContext = new CandySearchBoxDemoDemoVM();
        }
    }
    public partial class CandySearchBoxDemoDemoVM : ObservableObject
    {
        public CandySearchBoxDemoDemoVM()
        {
            Data = new ObservableCollection<SearchModel>();
        }
        [ObservableProperty]
        private ObservableCollection<SearchModel> _Data;

        [RelayCommand]
        public void Search(string input)
        {
            if (input == "1")
            {
                Data.Add(new SearchModel { Id = 1, Name = "1" });
                Data.Add(new SearchModel { Id = 2, Name = "2" });
                Data.Add(new SearchModel { Id = 3, Name = "3" });
                Data.Add(new SearchModel { Id = 4, Name = "4" });
                Data.Add(new SearchModel { Id = 5, Name = "5" });
                Data.Add(new SearchModel { Id = 6, Name = "6" });
                Data.Add(new SearchModel { Id = 7, Name = "7" });
                Data.Add(new SearchModel { Id = 8, Name = "8" });
                Data.Add(new SearchModel { Id = 9, Name = "9" });
                Data.Add(new SearchModel { Id = 10, Name = "10" });
            }
            else
                Data = new ObservableCollection<SearchModel>();
        }
    }

    public class SearchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
