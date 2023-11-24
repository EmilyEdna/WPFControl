using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CandyControls.InternalUtils;

namespace CandyControls
{
    internal class ImageAttach
    {
        internal static string GetSourceAsync(DependencyObject obj)
        {
            return (string)obj.GetValue(SoucreAysncProperty);
        }
        internal static void SetSourceAsync(DependencyObject obj, string value)
        {
            obj.SetValue(SoucreAysncProperty, value);
        }
        internal static readonly DependencyProperty SoucreAysncProperty =
            DependencyProperty.RegisterAttached("SourceAsync", typeof(string), typeof(ImageAttach), new PropertyMetadata(OnComplate));

        private static void OnComplate(DependencyObject sender, DependencyPropertyChangedEventArgs @event)
        {
            CandyImage candy = (CandyImage)((Image)sender).TemplatedParent;
            Image image = (Image)sender;
            DownloadQueue.Init(@event.NewValue.ToString(), image, candy);
        }
    }
}
