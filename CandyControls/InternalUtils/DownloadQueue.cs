using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using XExten.Advance.CacheFramework.RunTimeCache;
using XExten.Advance.LinqFramework;

namespace CandyControls.InternalUtils
{
    internal static class DownloadQueue
    {
        private static Queue<Tuple<string, Image, CandyImage>> Tiggers;
        private static AutoResetEvent AutoEvent;
        static DownloadQueue()
        {
            AutoEvent = new AutoResetEvent(true);
            Tiggers = new Queue<Tuple<string, Image, CandyImage>>();
            (new Thread(new ThreadStart(DownMethod))
            {
                IsBackground = true
            }).Start();
        }

        private static async void DownMethod()
        {
            while (true)
            {
                Tuple<string, Image, CandyImage> Tigger = null;
                lock (Tiggers)
                {
                    if (Tiggers.Count > 0)
                    {
                        Tigger = Tiggers.Dequeue();
                    }
                }
                if (Tigger != null)
                {
                    await Application.Current.Dispatcher.BeginInvoke(async () =>
                    {
                        Tigger.Item3.Complete = false;
                        var Bytes = Cache(await new HttpClient().GetByteArrayAsync(Tigger.Item1), Tigger.Item1, Tigger.Item3.EnableCache, Tigger.Item3.CacheSpan);
                        Tigger.Item2.Source = BitmapHelper.Bytes2Image(Bytes, Tigger.Item3.ImageThickness.Width, Tigger.Item3.ImageThickness.Height);
                        Tigger.Item3.Complete = true;
                        Tigger.Item3.LoadAnimeStory.Stop();
                    });
                }
                if (Tiggers.Count > 0) continue;
                //阻塞线程
                AutoEvent.WaitOne();
            }
        }

        internal async static void Init(string route, Image image, CandyImage candy)
        {
            if (candy.IsAsyncLoad)
            {
                lock (Tiggers)
                {
                    Tiggers.Enqueue(Tuple.Create(route, image, candy));
                    AutoEvent.Set();
                }
            }
            else
            {
                var Bytes = Cache(await new HttpClient().GetByteArrayAsync(route), route, candy.EnableCache, candy.CacheSpan);
                await candy.Dispatcher.BeginInvoke(() =>
                {
                    image.Source = BitmapHelper.Bytes2Image(Bytes, candy.ImageThickness.Width, candy.ImageThickness.Height);
                });
                candy.Complete = true;
            }
        }

        internal static byte[] Cache(byte[] bytes, string route, bool enableCache, int cacheSpan)
        {
            if (!enableCache) return bytes;
            var result = MemoryCaches.GetCache<byte[]>(route.ToMd5());
            if (result != null)
                return result;
            else
            {
                MemoryCaches.AddCache(route.ToMd5(), bytes, cacheSpan);
                return bytes;
            }
        }
    }
}
