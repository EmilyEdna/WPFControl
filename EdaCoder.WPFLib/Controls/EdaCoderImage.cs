using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
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
        private static Queue<Tuple<EdaCoderImage, string>> ItemsQueue;
        private static AutoResetEvent AutoEvent;
        private static Image PART_IMG;
        private static Path PART_LOAD;
        private static Button PART_BTN;
        public override void OnApplyTemplate()
        {
            PART_IMG = (Image)this.Template.FindName("PART_IMG", this);
            PART_LOAD = (Path)this.Template.FindName("PART_LOAD", this);
            PART_BTN = (Button)this.Template.FindName("PART_BTN", this);
            PART_BTN.Click += ClickEvent;
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

        #region Dp
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(EdaCoderImage), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(EdaCoderImage), new PropertyMetadata(default));
        public static readonly DependencyProperty BorderEffectProperty =
            DependencyProperty.Register("BorderEffect", typeof(Effect), typeof(EdaCoderImage), new PropertyMetadata(default));
        public static readonly DependencyProperty IsAsyncLoadProperty =
            DependencyProperty.Register("IsAsyncLoad", typeof(bool), typeof(EdaCoderImage), new PropertyMetadata(true));
        public static readonly DependencyProperty ImageThicknessProperty =
            DependencyProperty.Register("ImageThickness", typeof(ImageThickness), typeof(EdaCoderImage), new PropertyMetadata(new ImageThickness(160, 240)));
        public static readonly DependencyProperty LoadingThicknessProperty =
            DependencyProperty.Register("LoadingThickness", typeof(ImageThickness), typeof(EdaCoderImage), new PropertyMetadata(new ImageThickness(25, 25)));
        public static readonly DependencyProperty PopupThicknessProperty =
            DependencyProperty.Register("PopupThickness", typeof(ImageThickness), typeof(EdaCoderImage), new PropertyMetadata(new ImageThickness(0, 0)));
        public static readonly DependencyProperty EnableLoadingProperty =
            DependencyProperty.Register("EnableLoading", typeof(bool), typeof(EdaCoderImage), new PropertyMetadata(false));
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(EdaCoderImage), new PropertyMetadata(default));
        public static readonly DependencyProperty PopTemplateProperty =
            DependencyProperty.Register("PopTemplate", typeof(DataTemplate), typeof(EdaCoderImage), new PropertyMetadata(default));
        public static readonly DependencyProperty SrcProperty =
            DependencyProperty.Register("Src", typeof(string), typeof(EdaCoderImage), new PropertyMetadata(string.Empty, OnSrcChanged));
        internal static readonly DependencyProperty CompleteProperty =
            DependencyProperty.Register("Complete", typeof(bool), typeof(EdaCoderImage), new PropertyMetadata(false));
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(EdaCoderImage), new PropertyMetadata(default));
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(int), typeof(EdaCoderImage), new PropertyMetadata(default));
        #endregion

        #region Property
        [Description("弹出层颜色")]
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        [Description("半角")]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        [Description("材质阴影")]
        public Effect BorderEffect
        {
            get { return (Effect)GetValue(BorderEffectProperty); }
            set { SetValue(BorderEffectProperty, value); }
        }
        [Description("异步加载")]
        public bool IsAsyncLoad
        {
            get { return (bool)GetValue(IsAsyncLoadProperty); }
            set { SetValue(IsAsyncLoadProperty, value); }
        }
        [Description("图片链接")]
        public string Src
        {
            get { return (string)GetValue(SrcProperty); }
            set { SetValue(SrcProperty, value); }
        }
        [Description("重绘图片的长宽")]
        public ImageThickness ImageThickness
        {
            get { return (ImageThickness)GetValue(ImageThicknessProperty); }
            set { SetValue(ImageThicknessProperty, value); }
        }
        [Description("重绘图片的长宽")]
        public ImageThickness LoadingThickness
        {
            get { return (ImageThickness)GetValue(LoadingThicknessProperty); }
            set { SetValue(LoadingThicknessProperty, value); }
        }
        [Description("第二弹出层长宽")]
        public ImageThickness PopupThickness
        {
            get { return (ImageThickness)GetValue(PopupThicknessProperty); }
            set { SetValue(PopupThicknessProperty, value); }
        }
        [Description("是否启用图片加载等待")]
        public bool EnableLoading
        {
            get { return (bool)GetValue(EnableLoadingProperty); }
            set { SetValue(EnableLoadingProperty, value); }
        }
        [Description("信息模板")]
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        [Description("信息模板")]
        public DataTemplate PopTemplate
        {
            get { return (DataTemplate)GetValue(PopTemplateProperty); }
            set { SetValue(PopTemplateProperty, value); }
        }
        [Description("是否完成加载")]
        internal bool Complete
        {
            get { return (bool)GetValue(CompleteProperty); }
            set { SetValue(CompleteProperty, value); }
        }
        [Description("命令")]
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        [Description("命令参数")]
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        #region Method
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

        private void ClickEvent(object sender, RoutedEventArgs e)
        {
            Command?.Execute(CommandParameter);
            Grid panal = new Grid();
            panal.Children.Add(new Rectangle
            {
                Height = PopupThickness.Height == 0 ? this.Height : PopupThickness.Height,
                Width = PopupThickness.Width == 0 ? Application.Current.MainWindow.ActualWidth : PopupThickness.Width,
                Fill = Fill,
            });
            panal.Children.Add(new ContentPresenter
            {
                ContentTemplate = PopTemplate
            });
            Popup popup = new Popup
            {
                Placement = PlacementMode.Bottom,
                PlacementTarget = this,
                AllowsTransparency = true,
                StaysOpen = false,
                Height = PopupThickness.Height == 0 ? this.Height : PopupThickness.Height,
                Width = PopupThickness.Width == 0 ? Application.Current.MainWindow.ActualWidth : PopupThickness.Width,
                Child = panal
            };
            popup.IsOpen = true;
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
                    Items.Item1.Dispatcher.Invoke(() =>
                    {
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
        #endregion
    }

}
