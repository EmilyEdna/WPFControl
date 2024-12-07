using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CandyControls
{
    public static class ValidataAttach
    {

        public static void SetErrorInfo(DependencyObject element, string value)
    => element.SetValue(ErrorInfoProperty, value);

        public static string GetErrorInfo(DependencyObject element)
            => (string)element.GetValue(ErrorInfoProperty);

        public static readonly DependencyProperty ErrorInfoProperty =
            DependencyProperty.RegisterAttached("ErrorInfo", typeof(string), typeof(ValidataAttach),
                new PropertyMetadata(default(string), (dp, e) =>
                {
                    if (dp is TextBox box)
                    {
                        ControlTemplate controlTemplate = new ControlTemplate(typeof(Control));
                        FrameworkElementFactory panelFactory = new FrameworkElementFactory(typeof(StackPanel));

                        FrameworkElementFactory adornedFactroy = new FrameworkElementFactory(typeof(AdornedElementPlaceholder));

                        FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
                        textBlockFactory.SetValue(TextBlock.TextProperty, GetErrorInfo(dp));
                        textBlockFactory.SetValue(TextBlock.ForegroundProperty, Brushes.Red);

                        panelFactory.AppendChild(adornedFactroy);
                        panelFactory.AppendChild(textBlockFactory);

                        controlTemplate.VisualTree = panelFactory;

                        Validation.SetErrorTemplate(box, controlTemplate);
                    }
                }));


    }
}
