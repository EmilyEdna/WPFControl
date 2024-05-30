using CandyControls.ControlsModel.Dto;
using SkiaImageView;
using System;
using System.Windows;

namespace CandyControls
{
    public class SkiaViewAttach
    {
        public static string GetSource(DependencyObject obj)=> (string)obj.GetValue(SoucreProperty);

        public static void SetSource(DependencyObject obj, string value)=> obj.SetValue(SoucreProperty, value);

        public static readonly DependencyProperty SoucreProperty =
            DependencyProperty.RegisterAttached("Source", typeof(string), typeof(SkiaViewAttach), new PropertyMetadata((sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.NewValue.ToString()))
                {
                    CandyImage candy = (CandyImage)((SKImageView)sender).TemplatedParent;
                    SKImageView image = (SKImageView)sender;
                    QueueHelper.Push(new DownQueueDto
                    {
                        SKImageView = image,
                        CandyImage = candy,
                        URL = e.NewValue.ToString()
                    });
                }
            }));

        public static object GetBase64Soucre(DependencyObject obj) => obj.GetValue(Base64SoucreProperty);

        public static void SetBase64Soucre(DependencyObject obj, string value) => obj.SetValue(Base64SoucreProperty, value);

        public static readonly DependencyProperty Base64SoucreProperty =
            DependencyProperty.RegisterAttached("Base64Soucre", typeof(object), typeof(SkiaViewAttach), new PropertyMetadata((sender, e) =>
            {

                if (e.NewValue != null)
                {
                    var base64 = Convert.FromBase64String(e.NewValue.ToString());
                    SKImageView image = (SKImageView)sender;
                    CandyImage candy = (CandyImage)((SKImageView)sender).TemplatedParent;
                    image.Source = BitmapHelper.Bytes2Image(base64, candy.ImageThickness.Width, candy.ImageThickness.Height);
                }
            }));
    }
}
