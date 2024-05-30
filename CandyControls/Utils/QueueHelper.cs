using CandyControls.ControlsModel.Dto;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using XExten.Advance.CacheFramework;
using XExten.Advance.LinqFramework;

namespace CandyControls
{
    internal static class QueueHelper
    {
        private static AutoResetEvent AutoEvent;
        private static ConcurrentQueue<DownQueueDto> WaitQueue;
        private static HttpClient HttpClient;

        static QueueHelper()
        {
            HttpClient = new HttpClient();
            AutoEvent = new AutoResetEvent(true);
            WaitQueue = new ConcurrentQueue<DownQueueDto>();
            new Thread(new ThreadStart(WaitDown)) { IsBackground = true }.Start();
        }

        internal static async void Push(DownQueueDto dto)
        {
            if (dto.CandyImage.PushDown)
            {
                WaitQueue.Enqueue(dto);
                AutoEvent.Set();
            }
            else
            {
                var result = await Cache(dto.URL, dto.CandyImage.EnableCache, dto.CandyImage.CacheSpan);
                await Application.Current.Dispatcher.BeginInvoke(() =>
                 {
                     dto.SKImageView.Source = BitmapHelper.Bytes2Image(result,dto.CandyImage.ImageThickness.Width, dto.CandyImage.ImageThickness.Height);
                 });
                dto.CandyImage.Complete = true;
            }
        }

        private static async Task<byte[]> Cache(string route, bool enableCache, int cacheSpan)
        {
            if (!enableCache)
            {
                return await HttpClient.GetByteArrayAsync(route);
            }
            var result = Caches.RunTimeCacheGet<byte[]>(route.ToMd5());
            if (result != null) return result;
            else
            {
                var bytes = await HttpClient.GetByteArrayAsync(route);
                Caches.RunTimeCacheSet(route.ToMd5(), bytes, cacheSpan);
                return bytes;
            }
        }

        private static async void WaitDown()
        {
            while (true)
            {
                if (WaitQueue.TryDequeue(out DownQueueDto dto))
                {
                    await Application.Current.Dispatcher.BeginInvoke(async () =>
                     {
                         dto.CandyImage.Complete = false;
                         var Bytes = await Cache(dto.URL, dto.CandyImage.EnableCache, dto.CandyImage.CacheSpan);
                         dto.SKImageView.Source = BitmapHelper.Bytes2Image(Bytes, dto.CandyImage.ImageThickness.Width, dto.CandyImage.ImageThickness.Height);
                         dto.CandyImage.Complete = true;
                         dto.CandyImage.LoadAnimeStory.Stop();
                     });
                }
                if (!WaitQueue.IsEmpty) continue;
                AutoEvent.WaitOne();
            }
        }
    }
}
