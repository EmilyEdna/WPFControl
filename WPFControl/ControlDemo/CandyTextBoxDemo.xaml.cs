using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using XExten.Advance.LinqFramework;

namespace WPFControl.ControlDemo
{
    /// <summary>
    /// CandyTextBoxDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CandyTextBoxDemo : UserControl
    {
        public CandyTextBoxDemo()
        {
            InitializeComponent();
        }
    }
    public partial class CandyTextBoxDemoViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _Content;
    }

    public class NumberVilidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.AsString().AsInt() == 0)
                return new ValidationResult(false, "只能是整数");
            return ValidationResult.ValidResult;
        }
    }

}
