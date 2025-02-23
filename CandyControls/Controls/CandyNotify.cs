using CandyControls.ControlsModel.Enums;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CandyControls
{
    public sealed class CandyNotify : Window
    {
        private static CandyNotify NotifyWindow;
        private DispatcherTimer CloseTimer;
        private int TickCount = 0;
        private readonly int WaitTime = 3;
        private Panel GrowlPanel { get; set; }


        public CandyNotify()
        {

            this.Topmost = true;
            this.WindowState = WindowState.Normal;
            this.ResizeMode = ResizeMode.NoResize;
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            this.VerticalAlignment = VerticalAlignment.Bottom;
            this.Background = Brushes.Transparent;
            this.Width = this.MaxWidth = 340d;
            this.Height = this.MaxHeight = 400d;
            this.GrowlPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 10, 10, 10),
            };

            Content = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                Content = GrowlPanel
            };
            StartTimer();
        }

        private void StartTimer()
        {
            CloseTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            CloseTimer.Tick += delegate
            {
                if (GrowlPanel.IsMouseOver)
                {
                    TickCount = 0;
                    return;
                }
                if (GrowlPanel.Children.Count <= 0)
                {
                    TickCount = 0;
                    return;
                }
                TickCount++;
                if (TickCount >= WaitTime)
                {
                    Button btn = GrowlPanel.Children.OfType<UIElement>()
                    .SelectMany(t => t.FindChildren<Button>())
                    .OrderBy(t => long.Parse(t.Tag.ToString())).FirstOrDefault();
                    btn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, btn));
                    TickCount = 0;
                    return;
                }
            };
            CloseTimer.Start();
        }

        private void WorkArea()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width;
            Top = desktopWorkingArea.Height - Height;
        }

        private static void Init()
        {
            if (NotifyWindow == null)
            {
                NotifyWindow = new CandyNotify();
                NotifyWindow.Show();
                NotifyWindow.WorkArea();
            }
        }

        private static DoubleAnimation Anime(UIElement UI)
        {

            DoubleAnimation anime = new DoubleAnimation(340d, new Duration(TimeSpan.FromMilliseconds(200)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            };
            anime.Completed += delegate
            {
                NotifyWindow.GrowlPanel.Children.Remove(UI);
                if (NotifyWindow.GrowlPanel.Children.Count <= 0)
                {
                    NotifyWindow.Close();
                    NotifyWindow = null;
                }
            };
            return anime;
        }

        private static StackPanel Panel(ECatagory Cate, string Msg)
        {
            StackPanel stack = new StackPanel
            {
                Width = 340d,
                Height = 80d,
                Margin = new Thickness(0, 5, 0, 5),
                RenderTransformOrigin = new Point(.5, .5),
            };
            var transform = new TranslateTransform { X = 0d };
            stack.RenderTransform = transform;

            switch (Cate)
            {
                case ECatagory.Primary:
                case ECatagory.Info:
                    stack.Background = Brushes.DeepSkyBlue;
                    break;
                case ECatagory.Success:
                    stack.Background = Brushes.LightGreen;
                    break;
                case ECatagory.Warning:
                    stack.Background = Brushes.Tomato;
                    break;
                case ECatagory.Error:
                case ECatagory.Fatal:
                    stack.Background = Brushes.DarkRed;
                    break;
                default:
                    break;
            }

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            TextBlock text = new TextBlock
            {
                FontSize = 16,
                Margin = new Thickness(10, 0, 10, 0),
                TextWrapping = TextWrapping.Wrap,
                Text = Msg,
                Foreground = Brushes.White,
                TextAlignment = TextAlignment.Left,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };

            Button btn = new Button
            {
                Height = 25,
                Width = 25,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 10, 0),
                Tag = DateTime.Now.Ticks,
            };
            btn.Content = new Path
            {
                Data = Geometry.Parse("M195.904 154.368l316.8 316.8 316.8-316.8 45.248 45.248-316.8 316.8 316.8 316.8-45.28 45.216-316.8-316.736-316.768 316.736-45.248-45.248 316.8-316.8-316.8-316.768z"),
                Stretch = Stretch.Uniform,
                Height = 13,
                Width = 13,
                Fill = Brushes.Black
            };
            btn.SetResourceReference(Button.StyleProperty, "CaptionButtonStyleForClose");
            btn.Click += (sender, args) =>
            {
                var panel = (sender as Button).FindParent<StackPanel>();
                panel.RenderTransform.BeginAnimation(TranslateTransform.XProperty, Anime(panel));
            };


            Grid.SetRow(btn, 0);
            Grid.SetRow(text, 1);
            grid.Children.Add(text);
            grid.Children.Add(btn);

            stack.Children.Add(grid);
            return stack;
        }

        /// <summary>
        /// 弹出成功
        /// </summary>
        /// <param name="msg"></param>
        public static void Success(string msg)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                Init();
                NotifyWindow.GrowlPanel.Children.Add(Panel(ECatagory.Success, msg));
            });

        }
        /// <summary>
        /// 弹出提示
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                Init();
                NotifyWindow.GrowlPanel.Children.Add(Panel(ECatagory.Info, msg));
            });
        }
        /// <summary>
        /// 弹出警告
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                Init();
                NotifyWindow.GrowlPanel.Children.Add(Panel(ECatagory.Warning, msg));
            });
        }
        /// <summary>
        /// 弹出错误
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                Init();
                NotifyWindow.GrowlPanel.Children.Add(Panel(ECatagory.Error, msg));
            });
        }
    }
}
