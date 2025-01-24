using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CandyControls
{
    public static class ScrollViewAttach
    {
       
        public static void SetOrientation(DependencyObject element, Orientation value)
            => element.SetValue(OrientationProperty, value);

        public static Orientation GetOrientation(DependencyObject element)
            => (Orientation)element.GetValue(OrientationProperty);

        public static readonly DependencyProperty OrientationProperty 
            = DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(ScrollViewAttach), 
                new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.Inherits, OnOrientationChanged));

        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            if (d is ScrollViewer scrollViewer)
            {
                if ((Orientation)e.NewValue == Orientation.Horizontal)
                    scrollViewer.PreviewMouseWheel += ScrollViewerPreviewMouseWheel;
                else
                    scrollViewer.PreviewMouseWheel -= ScrollViewerPreviewMouseWheel;
            }

           
        }
        private static void ScrollViewerPreviewMouseWheel(object sender, MouseWheelEventArgs args)
        {
            var scrollViewerNative = (ScrollViewer)sender;
            scrollViewerNative.ScrollToHorizontalOffset(Math.Min(Math.Max(0, scrollViewerNative.HorizontalOffset - args.Delta), scrollViewerNative.ScrollableWidth));

            args.Handled = true;
        }
    }
}
