using CandyControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// CandyNotifyDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CandyNotifyDemo : UserControl
    {
        public CandyNotifyDemo()
        {
            InitializeComponent();
        }

        private void InfoEvent(object sender, RoutedEventArgs e)
        {
            CandyNotify.Success("Success");
            Thread.Sleep(20);
            CandyNotify.Info("Info");
            Thread.Sleep(20);
            CandyNotify.Warn("Warn");
            Thread.Sleep(20);
            CandyNotify.Error("Error");
        }
    }
}
