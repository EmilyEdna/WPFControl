using SkiaImageView;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XExten.Advance.CacheFramework;
using XExten.Advance.LinqFramework;
using XExten.Advance.NetFramework;
using XExten.Advance.StaticFramework;

namespace CandyControls
{
    public class CandyViewer : UserControl
    {
        static CandyViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CandyViewer), new FrameworkPropertyMetadata(typeof(CandyViewer)));
        }

        private SKImageView PART_IMG;
        private static Action<double> ProcessAction;
        private static bool ExcuteApply = false;
        public override void OnApplyTemplate()
        {
            PART_IMG = (SKImageView)this.Template.FindName("PART_IMG", this);
            ProcessAction = (process) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.ProcessValue = CalcProgress(process, 50, 12);
                    if (process >= 100)
                        Show = true;
                });
            };
            ExcuteApply = true;
        }

        public string ViewSoucre
        {
            get { return (string)GetValue(ViewSoucreProperty); }
            set { SetValue(ViewSoucreProperty, value); }
        }

        public static readonly DependencyProperty ViewSoucreProperty =
            DependencyProperty.Register("ViewSoucre", typeof(string), typeof(CandyViewer), new FrameworkPropertyMetadata(Onchanged));

        internal DoubleCollection ProcessValue
        {
            get { return (DoubleCollection)GetValue(ProcessValueProperty); }
            set { SetValue(ProcessValueProperty, value); }
        }

        internal static readonly DependencyProperty ProcessValueProperty =
            DependencyProperty.Register("ProcessValue", typeof(DoubleCollection), typeof(CandyViewer), new FrameworkPropertyMetadata(new DoubleCollection { 0, 0 }));

        internal bool Show
        {
            get { return (bool)GetValue(ShowProperty); }
            set { SetValue(ShowProperty, value); }
        }

        internal static readonly DependencyProperty ShowProperty =
            DependencyProperty.Register("Show", typeof(bool), typeof(CandyViewer), new FrameworkPropertyMetadata(false));

        private static async void Onchanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uc = (CandyViewer)d;
            while (!ExcuteApply)
            {
                await Task.Delay(100);
            }
            uc.Show = false;
            if (e.NewValue == null) return;
            if (e.NewValue.ToString().Contains("http://") || e.NewValue.ToString().Contains("https://"))
            {
                var key = e.NewValue.ToString().ToMd5();
                uc.ProcessValue = [0, 0];
                var result = Caches.RunTimeCacheGet<byte[]>(key);
                if (result != null)
                {
                    var data = BitmapHelper.Bytes2Image(result, (int)uc.Width, (int)uc.Height);
                    uc.PART_IMG.Source = data;
                }
                else
                {
                    HttpClient Client = new(ProgressHandler());
                    Client.DefaultRequestHeaders.Add(ConstDefault.UserAgent, ConstDefault.UserAgentValue);
                    var bytes = await Client.GetByteArrayAsync(e.NewValue.ToString());
                    var data = BitmapHelper.Bytes2Image(bytes, (int)uc.Width, (int)uc.Height);
                    Caches.RunTimeCacheSet(key, bytes, 5);
                    uc.PART_IMG.Source = data;
                }
            }
            else
            {
                var bytes = SyncStatic.ConvertBytesImage(Convert.FromBase64String(e.NewValue.ToString()));
                var data = BitmapHelper.Bytes2Image(bytes, (int)uc.Width, (int)uc.Height);
                uc.PART_IMG.Source = data;
                uc.Show = true;
            }
        }
        private static ProgressMessageHandler ProgressHandler()
        {
            var ProgressHandler = new ProgressMessageHandler(new HttpClientHandler());
            ProgressHandler.HttpReceiveProgress += HttpReceiveProgress;
            return ProgressHandler;
        }

        private static void HttpReceiveProgress(object sender, HttpProgressEventArgs e)
        {
            ProcessAction?.Invoke(e.ProgressPercentage);
        }

        private static DoubleCollection CalcProgress(double progress, double radius, double thickness)
        {
            var r = radius - thickness / 2;
            var perimeter = 2 * Math.PI * r / thickness;
            var step = progress / 100 * perimeter;
            return [step, 1000];
        }
    }
}
