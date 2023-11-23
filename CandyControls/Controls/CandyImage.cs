﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using CandyControls.ControlsModel;

namespace CandyControls
{
    public class CandyImage : Control
    {
        static CandyImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyImage), new FrameworkPropertyMetadata(typeof(CandyImage)));
            AutoEvent = new AutoResetEvent(true);
            (new Thread(new ThreadStart(DownMethod))
            {
                IsBackground = true
            }).Start();
        }

        private static AutoResetEvent AutoEvent;
        private static Image PART_IMG;
        private static Path PART_LOAD;
        private static Button PART_BTN;
        private static Path PART_INFO;
        private static Grid PART_RECT;
        private static Trigger TRIGGER;
        public override void OnApplyTemplate()
        {
            PART_IMG = (Image)this.Template.FindName("PART_IMG", this);
            PART_LOAD = (Path)this.Template.FindName("PART_LOAD", this);
            PART_BTN = (Button)this.Template.FindName("PART_BTN", this);
            PART_RECT = (Grid)this.Template.FindName("PART_RECT", this);
            PART_BTN.Click += ClickEvent;
            PART_LOAD.Height = LoadingThickness.Height;
            PART_LOAD.Width = LoadingThickness.Width;

            TRIGGER = (Trigger)this.Template.Triggers.Where(t => t is Trigger).First();
            CloseAnime();
            TRIGGER.ExitActions.Add(new BeginStoryboard
            {
                Storyboard = CloseStory
            });
            LoadAnime();
        }

        #region Anime
        private static Storyboard LoadAnimeStory;
        private static Storyboard CollapsedStory;
        private static Storyboard ExpendStory;
        private static Storyboard CloseStory;
        private void LoadAnime()
        {
            LoadAnimeStory = new Storyboard();
            DoubleAnimationUsingKeyFrames KF = new DoubleAnimationUsingKeyFrames
            {
                RepeatBehavior = RepeatBehavior.Forever
            };
            Storyboard.SetTarget(KF, PART_LOAD);
            Storyboard.SetTargetProperty(KF, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)"));
            KF.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            KF.KeyFrames.Add(new EasingDoubleKeyFrame(180, TimeSpan.FromSeconds(1)));
            KF.KeyFrames.Add(new EasingDoubleKeyFrame(360, TimeSpan.FromSeconds(2)));
            LoadAnimeStory.Children.Add(KF);
            LoadAnimeStory.Begin();
        }
        private void ExpendAnime() 
        {
            ExpendStory = new Storyboard();
            DoubleAnimationUsingKeyFrames Revolve = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(Revolve, PART_INFO);
            Storyboard.SetTargetProperty(Revolve, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)"));
            Revolve.KeyFrames.Add(new EasingDoubleKeyFrame(-90, TimeSpan.FromSeconds(0)));
            Revolve.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(1)));
            ExpendStory.Children.Add(Revolve);
            ExpendStory.Begin();
        }
        private void CollapsedAnime()
        {
            CollapsedStory = new Storyboard();
            DoubleAnimationUsingKeyFrames Revolve = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(Revolve, PART_INFO);
            Storyboard.SetTargetProperty(Revolve, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)"));
            Revolve.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            Revolve.KeyFrames.Add(new EasingDoubleKeyFrame(-90, TimeSpan.FromSeconds(1)));
            CollapsedStory.Children.Add(Revolve);
            CollapsedStory.Begin();
        }
        private static void CloseAnime()
        {
            CloseStory = new Storyboard();
            DoubleAnimationUsingKeyFrames Close = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(Close, PART_RECT);
            Storyboard.SetTargetProperty(Close, new PropertyPath("Height"));
            Close.KeyFrames.Add(new EasingDoubleKeyFrame(100, TimeSpan.FromSeconds(0)));
            Close.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromSeconds(1)));
            CloseStory.Children.Add(Close);
        }
        #endregion

        #region Dp
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(CandyImage), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CandyImage), new PropertyMetadata(default));
        public static readonly DependencyProperty BorderEffectProperty =
            DependencyProperty.Register("BorderEffect", typeof(Effect), typeof(CandyImage), new PropertyMetadata(default));
        public static readonly DependencyProperty IsAsyncLoadProperty =
            DependencyProperty.Register("IsAsyncLoad", typeof(bool), typeof(CandyImage), new PropertyMetadata(true));
        public static readonly DependencyProperty ImageThicknessProperty =
            DependencyProperty.Register("ImageThickness", typeof(ImageThickness), typeof(CandyImage), new PropertyMetadata(new ImageThickness(160, 240)));
        public static readonly DependencyProperty LoadingThicknessProperty =
            DependencyProperty.Register("LoadingThickness", typeof(ImageThickness), typeof(CandyImage), new PropertyMetadata(new ImageThickness(25, 25)));
        public static readonly DependencyProperty PopupThicknessProperty =
            DependencyProperty.Register("PopupThickness", typeof(ImageThickness), typeof(CandyImage), new PropertyMetadata(new ImageThickness(0, 0)));
        public static readonly DependencyProperty EnableLoadingProperty =
            DependencyProperty.Register("EnableLoading", typeof(bool), typeof(CandyImage), new PropertyMetadata(false));
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(CandyImage), new PropertyMetadata(default));
        public static readonly DependencyProperty PopTemplateProperty =
            DependencyProperty.Register("PopTemplate", typeof(DataTemplate), typeof(CandyImage), new PropertyMetadata(default));
        public static readonly DependencyProperty SrcProperty =
            DependencyProperty.Register("Src", typeof(string), typeof(CandyImage), new PropertyMetadata(string.Empty, OnSrcChanged));
        internal static readonly DependencyProperty CompleteProperty =
            DependencyProperty.Register("Complete", typeof(bool), typeof(CandyImage), new PropertyMetadata(false));
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(CandyImage), new PropertyMetadata(default));
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CandyImage), new PropertyMetadata(default));
        public static readonly DependencyProperty PopupBtnProperty =
            DependencyProperty.Register("PopupBtn", typeof(Visibility), typeof(CandyImage), new PropertyMetadata(Visibility.Collapsed));
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
        [Description("是否显示第二弹出层")]
        public Visibility PopupBtn
        {
            get { return (Visibility)GetValue(PopupBtnProperty); }
            set { SetValue(PopupBtnProperty, value); }
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
            CandyImage eda = (obj as CandyImage);
            if (eda.IsAsyncLoad)
            {
                lock (DownloadQueue.ItemsQueue)
                {
                    DownloadQueue.ItemsQueue.Enqueue(Tuple.Create(eda, events.NewValue.ToString()));
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
            PART_INFO = (Path)((Button)sender).Template.FindName("PART_INFO", PART_BTN);
            ExpendAnime();
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
            popup.Closed += delegate
            {
                CollapsedAnime();
            };
        }

        private static async void DownMethod()
        {
            while (true)
            {
                Tuple<CandyImage, string> Items = null;
                lock (DownloadQueue.ItemsQueue)
                {
                    if (DownloadQueue.ItemsQueue.Count > 0)
                    {
                        Items = DownloadQueue.ItemsQueue.Dequeue();
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
                        LoadAnimeStory.Stop();
                    });
                }
                if (DownloadQueue.ItemsQueue.Count > 0) continue;
                //阻塞线程
                AutoEvent.WaitOne();
            }
        }
        #endregion
    }
    internal static class DownloadQueue
    {
        internal static Queue<Tuple<CandyImage, string>> ItemsQueue;

        static DownloadQueue()
        {
            ItemsQueue = new Queue<Tuple<CandyImage, string>>();
        }
    }
}
