#region

using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video.DirectShow;

#endregion

namespace ASCII_Graphics
{
    public static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            var videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            Console.SetWindowSize(1920 / 2, 1080 / 2);
            videoSource.NewFrame += (sender, eventArgs) =>
            {
                var bitmap = (Bitmap)eventArgs.Frame.Clone();
                bitmap = bitmap.ResizeBitmap();
                // bitmap = bitmap.ToGrayScale();
                var str = bitmap.ToAscii();
                
                Console.Clear();
                Console.WriteLine(str);
            };

            videoSource.Start();
            Console.ReadLine();
        }
    }
}