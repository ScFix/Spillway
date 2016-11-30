using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Spillway.Utilities
{
    public class ImageLoadedEventArgs : EventArgs
    {
        public string LoadedUrl { get; private set; }

        public ImageLoadedEventArgs(string url)
        {
            LoadedUrl = url;
        }
    }

    public static class ImageCache
    {
        private static Dictionary<string, BitmapImage> _CachedImages;

        public static BitmapImage GetImage(string url)
        {
            if (_CachedImages == null)
            {
                _CachedImages = new Dictionary<string, BitmapImage>();
                BitmapImage bi = new BitmapImage();
                bi.UriSource = new Uri("../Resources/loading_anim.gif", UriKind.Relative);
                _CachedImages.Add("loading", bi);
            }

            if (_CachedImages.ContainsKey(url))
            {
                BitmapImage imageSource;
                if (_CachedImages.TryGetValue(url, out imageSource))
                {
                    return imageSource;
                }
            }
            else
            {
                BackgroundWorker bg = new BackgroundWorker();
                bg.DoWork += loadImageAsync;
                bg.RunWorkerCompleted += NotifyImageLoaded;
                bg.RunWorkerAsync(url);
            }

            BitmapImage loadingImage;
            if (_CachedImages.TryGetValue("loading", out loadingImage))
            {
                return loadingImage;
            }

            return null;
        }

        private static void NotifyImageLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                ImageLoaded?.Invoke(null, new ImageLoadedEventArgs(e.Result.ToString()));
            });
        }

        private static void loadImageAsync(object sender, DoWorkEventArgs e)
        {
            LoadImage(e.Argument.ToString());
            e.Result = e.Argument;
        }

        private static void LoadImage(string url)
        {
            WebRequest request = WebRequest.Create(new Uri(url, UriKind.Absolute));
            request.Timeout = -1;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            BinaryReader reader = new BinaryReader(responseStream);
            MemoryStream memoryStream = new MemoryStream();
            int bytesToRead = 65536;
            byte[] bytebuffer = new byte[bytesToRead];
            int bytesRead = reader.Read(bytebuffer, 0, bytesToRead);

            while (bytesRead > 0)
            {
                memoryStream.Write(bytebuffer, 0, bytesRead);
                bytesRead = reader.Read(bytebuffer, 0, bytesToRead);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);

            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = memoryStream;
            imageSource.EndInit();
            imageSource.Freeze();

            Application.Current.Dispatcher.Invoke(() =>
            {
                _CachedImages.Add(url, imageSource);
            });
        }

        public delegate void ImageLoadedEvenHandler(object sender, ImageLoadedEventArgs e);

        public static event ImageLoadedEvenHandler ImageLoaded;
    }
}