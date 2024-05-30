using SkiaSharp;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace CandyControls
{
    public class BitmapHelper
    {
        /// <summary>
        /// bytes转图片
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static SKBitmap Bytes2Image(byte[] bytes, int width = 160, int height = 240) => SKBitmap.Decode(bytes)
            .Resize(new SKImageInfo(width, height), SKFilterQuality.High);
    }
}
