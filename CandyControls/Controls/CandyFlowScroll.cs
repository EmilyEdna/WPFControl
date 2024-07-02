using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CandyControls
{
    public class CandyFlowScroll : FlowDocumentScrollViewer
    {
        static CandyFlowScroll()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyFlowScroll), new FrameworkPropertyMetadata(typeof(CandyFlowScroll)));
        }
        public Thickness InnerMargin
        {
            get { return (Thickness)GetValue(InnerMarginProperty); }
            set { SetValue(InnerMarginProperty, value); }
        }
        public static readonly DependencyProperty InnerMarginProperty =
            DependencyProperty.Register("InnerMargin", typeof(Thickness), typeof(CandyFlowScroll), new PropertyMetadata(new Thickness(0)));

        public Visibility Back
        {
            get { return (Visibility)GetValue(BackProperty); }
            set { SetValue(BackProperty, value); }
        }
        public static readonly DependencyProperty BackProperty =
            DependencyProperty.Register("Back", typeof(Visibility), typeof(CandyFlowScroll), new PropertyMetadata(Visibility.Visible));

        public Visibility Fonts
        {
            get { return (Visibility)GetValue(FontsProperty); }
            set { SetValue(FontsProperty, value); }
        }
        public static readonly DependencyProperty FontsProperty =
            DependencyProperty.Register("Fonts", typeof(Visibility), typeof(CandyFlowScroll), new PropertyMetadata(Visibility.Visible));

        public Visibility PerNext
        {
            get { return (Visibility)GetValue(PerNextProperty); }
            set { SetValue(PerNextProperty, value); }
        }
        public static readonly DependencyProperty PerNextProperty =
            DependencyProperty.Register("PerNext", typeof(Visibility), typeof(CandyFlowScroll), new PropertyMetadata(Visibility.Collapsed));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CandyFlowScroll), new PropertyMetadata(default));







    }
}
