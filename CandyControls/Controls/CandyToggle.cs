using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XExten.Advance.LinqFramework;

namespace CandyControls
{
    public class CandyToggle : ListBox
    {
        public IEnumerable<string> ModelSource
        {
            get { return (IEnumerable<string>)GetValue(ModelSourceProperty); }
            set { SetValue(ModelSourceProperty, value); }
        }
        public static readonly DependencyProperty ModelSourceProperty =
            DependencyProperty.Register("ModelSource", typeof(IEnumerable<string>), typeof(CandyToggle), new FrameworkPropertyMetadata(OnModelChanged));

        public Brush SelectedBrush
        {
            get { return (Brush)GetValue(SelectedBrushProperty); }
            set { SetValue(SelectedBrushProperty, value); }
        }
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(CandyToggle), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0x70,0x80,0xCB,0xCC))));

        public Brush MouseOverBrush
        {
            get { return (Brush)GetValue(MouseOverBrushProperty); }
            set { SetValue(MouseOverBrushProperty, value); }
        }
        public static readonly DependencyProperty MouseOverBrushProperty =
            DependencyProperty.Register("MouseOverBrush", typeof(Brush), typeof(CandyToggle), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0x70, 0xE0, 0xE0, 0xDB))));

        public Brush BackBrush
        {
            get { return (Brush)GetValue(BackBrushProperty); }
            set { SetValue(BackBrushProperty, value); }
        }
        public static readonly DependencyProperty BackBrushProperty =
            DependencyProperty.Register("BackBrush", typeof(Brush), typeof(CandyToggle), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0x40, 0xB3, 0xB3,0xB3))));

        public Brush ForeBrush
        {
            get { return (Brush)GetValue(ForeBrushProperty); }
            set { SetValue(ForeBrushProperty, value); }
        }
        public static readonly DependencyProperty ForeBrushProperty =
            DependencyProperty.Register("ForeBrush", typeof(Brush), typeof(CandyToggle), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xFD, 0xF5, 0xE6))));


        private static void OnModelChanged(DependencyObject dp, DependencyPropertyChangedEventArgs eve)
        {
            var toggle = dp as CandyToggle;
            toggle.ModelSource.ForEnumerEach(item =>
            {
                toggle.AddChild(new CandyToggleItem { Content = item });
            });
        }
    }
}
