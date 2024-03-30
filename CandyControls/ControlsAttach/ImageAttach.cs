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
            if (!string.IsNullOrEmpty(@event.NewValue.ToString()))
            {
                CandyImage candy = (CandyImage)((Image)sender).TemplatedParent;
                Image image = (Image)sender;
                DownloadQueue.Init(@event.NewValue.ToString(), image, candy);
            }
        }

        internal static void SetBase64Soucre(DependencyObject obj, string value) 
        {
            obj.SetValue(Base64SoucreProperty, value);
        }

        internal static readonly DependencyProperty Base64SoucreProperty =
            DependencyProperty.RegisterAttached("Base64Soucre", typeof(object), typeof(ImageAttach), new PropertyMetadata(OnStreamComplete));

        private static void OnStreamComplete(DependencyObject sender, DependencyPropertyChangedEventArgs @event)
        {
            if (@event.NewValue != null)
            {
                var base64 = Convert.FromBase64String(@event.NewValue.ToString());
                Image image = (Image)sender;
                CandyImage candy = (CandyImage)((Image)sender).TemplatedParent;
                image.Source= BitmapHelper.Bytes2Image(base64, candy.ImageThickness.Width, candy.ImageThickness.Height);
            }
        }
    }
}
