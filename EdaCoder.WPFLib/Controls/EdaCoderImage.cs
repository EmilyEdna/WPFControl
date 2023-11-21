using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Xml.Linq;
using EdaCoder.WPFLib.ControlsModel;

namespace EdaCoder.WPFLib
{
    public class EdaCoderImage : Control
    {
        static EdaCoderImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EdaCoderImage), new FrameworkPropertyMetadata(typeof(EdaCoderImage)));
            ItemsQueue = new Queue<Tuple<EdaCoderImage, string>>();
            AutoEvent = new AutoResetEvent(true);
            (new Thread(new ThreadStart(DownMethod))
            {
                IsBackground = true
            }).Start();
        }
        private static Queue<Tuple<EdaCoderImage, string>> ItemsQueue;
        private static AutoResetEvent AutoEvent;
        private static Image PART_IMG;
        private static Path PART_LOAD;
        public override void OnApplyTemplate()
        {
            PART_IMG = (Image)this.Template.FindName("PART_IMG", this);
            PART_LOAD = (Path)this.Template.FindName("PART_LOAD", this);
            PART_LOAD.Height = LoadingThickness.Height;
            PART_LOAD.Width = LoadingThickness.Width;


            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames KF = new DoubleAnimationUsingKeyFrames
            {
                RepeatBehavior = RepeatBehavior.Forever
            };
            Storyboard.SetTarget(KF, PART_LOAD);
            Storyboard.SetTargetProperty(KF, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)"));
            KF.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            KF.KeyFrames.Add(new EasingDoubleKeyFrame(180, TimeSpan.FromSeconds(1)));
            KF.KeyFrames.Add(new EasingDoubleKeyFrame(360, TimeSpan.FromSeconds(2)));
            storyboard.Children.Add(KF);
            storyboard.Begin();
        }

        [Description("弹出层颜色")]
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(EdaCoderImage), new PropertyMetadata(Brushes.Transparent));

        [Description("半角")]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(EdaCoderImage), new PropertyMetadata(default));

        [Description("材质阴影")]
        public Effect BorderEffect
        {
            get { return (Effect)GetValue(BorderEffectProperty); }
            set { SetValue(BorderEffectProperty, value); }
        }
        public static readonly DependencyProperty BorderEffectProperty =
            DependencyProperty.Register("BorderEffect", typeof(Effect), typeof(EdaCoderImage), new PropertyMetadata(default));

        [Description("异步加载")]
        public bool IsAsyncLoad
        {
            get { return (bool)GetValue(IsAsyncLoadProperty); }
            set { SetValue(IsAsyncLoadProperty, value); }
        }
        public static readonly DependencyProperty IsAsyncLoadProperty =
            DependencyProperty.Register("IsAsyncLoad", typeof(bool), typeof(EdaCoderImage), new PropertyMetadata(true));

        [Description("图片链接")]
        public string Src
        {
            get { return (string)GetValue(SrcProperty); }
            set { SetValue(SrcProperty, value); }
        }
        public static readonly DependencyProperty SrcProperty =
            DependencyProperty.Register("Src", typeof(string), typeof(EdaCoderImage), new PropertyMetadata(string.Empty, OnSrcChanged));

        [Description("重绘图片的长宽")]
        public ImageThickness ImageThickness
        {
            get { return (ImageThickness)GetValue(ImageThicknessProperty); }
            set { SetValue(ImageThicknessProperty, value); }
        }
        public static readonly DependencyProperty ImageThicknessProperty =
            DependencyProperty.Register("ImageThickness", typeof(ImageThickness), typeof(EdaCoderImage), new PropertyMetadata(new ImageThickness(160, 240)));

        [Description("重绘图片的长宽")]
        public ImageThickness LoadingThickness
        {
            get { return (ImageThickness)GetValue(LoadingThicknessProperty); }
            set { SetValue(LoadingThicknessProperty, value); }
        }
        public static readonly DependencyProperty LoadingThicknessProperty =
            DependencyProperty.Register("LoadingThickness", typeof(ImageThickness), typeof(EdaCoderImage), new PropertyMetadata(new ImageThickness(25, 25)));

        [Description("是否启用图片加载等待")]
        public bool EnableLoading
        {
            get { return (bool)GetValue(EnableLoadingProperty); }
            set { SetValue(EnableLoadingProperty, value); }
        }
        public static readonly DependencyProperty EnableLoadingProperty =
            DependencyProperty.Register("EnableLoading", typeof(bool), typeof(EdaCoderImage), new PropertyMetadata(false));

        [Description("是否完成加载")]
        internal bool Complete
        {
            get { return (bool)GetValue(CompleteProperty); }
            set { SetValue(CompleteProperty, value); }
        }
        internal static readonly DependencyProperty CompleteProperty =
            DependencyProperty.Register("Complete", typeof(bool), typeof(EdaCoderImage), new PropertyMetadata(false));

        private static async void OnSrcChanged(DependencyObject obj, DependencyPropertyChangedEventArgs events)
        {
            EdaCoderImage eda = (obj as EdaCoderImage);
            if (eda.IsAsyncLoad)
            {
      
                lock (ItemsQueue)
                {
                    ItemsQueue.Enqueue(Tuple.Create(eda, events.NewValue.ToString()));
                    AutoEvent.Set();
                }
            }
            else
            {
                var Bytes = await new HttpClient().GetByteArrayAsync(events.NewValue.ToString());
                await eda.Dispatcher.BeginInvoke(() =>
                {
                    PART_IMG.Source = BitmapHelper.Bytes2Image(Bytes, eda.ImageThickness.Width, eda.ImageThickness.Height);
                });
            }
        }

        private static async void DownMethod()
        {
            while (true)
            {
                Tuple<EdaCoderImage, string> Items = null;
                lock (ItemsQueue)
                {
                    if (ItemsQueue.Count > 0)
                    {
                        Items = ItemsQueue.Dequeue();
                    }
                }
                if (Items != null)
                {
                    Items.Item1.Dispatcher.Invoke(() => {
                        Items.Item1.Complete = false;
                    });
                    var Bytes = await new HttpClient().GetByteArrayAsync(Items.Item2);
                    await Items.Item1.Dispatcher.BeginInvoke(() =>
                    {
                        PART_IMG.Source = BitmapHelper.Bytes2Image(Bytes, Items.Item1.ImageThickness.Width, Items.Item1.ImageThickness.Height);
                        Items.Item1.Complete = true;
                    });
                }
                if (ItemsQueue.Count > 0) continue;
                //阻塞线程
                AutoEvent.WaitOne();
            }
        }
    }

}
