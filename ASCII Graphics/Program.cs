#region

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

#endregion

namespace ASCII_Graphics
{
    public static class Program
    {
        private const bool IS_DEBUG = false;
        private const int TIMER_INTERVAL = 1000 / 30;
        private static FilterInfoCollection _videoDevices;
        private static VideoCaptureDevice _videoSource;
        private static string _asciiString;
        private static Bitmap _bitmap;
        private readonly static object BitmapLock = new object();

        private static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;

            _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (_videoDevices == null) throw new ArgumentNullException(nameof(_videoDevices));

            _videoSource = new VideoCaptureDevice(_videoDevices[0].MonikerString);
            _videoSource.Start();

            await InitializeVideoSource();

            await ProcessFramesAsync();

            ImageProcessing();

            Console.ReadLine();
        }

        private static async Task InitializeVideoSource()
        {
            await Task.Run(() => _videoSource.Start());
        }

        private static async Task ProcessFramesAsync()
        {
            await Task.Run(() => { _videoSource.NewFrame += VideoSource_NewFrame; });
        }

        private static void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
#if DEBUG
            DebugLog("VideoSource_NewFrame", ConsoleColor.Yellow);
#endif
            lock (BitmapLock)
            {
                _bitmap = (Bitmap)eventArgs.Frame.Clone();
            }
        }

        private static async void ImageProcessing()
        {
            string asciiString;
            Bitmap bitmap;
            await Task.Run(
                () =>
                {
                    while (true)
                    {
#if DEBUG
                        DebugLog("ImageProcessing", ConsoleColor.Blue);
#endif
                        lock (BitmapLock)
                        {
                            if (_bitmap == null) continue;
                            bitmap = (Bitmap)_bitmap.Clone();
                        }

                        bitmap = Extensions.ResizeBitmap(bitmap);

                        asciiString = BitmapToAsciiConverter.BitmapToAscii(bitmap);

                        _asciiString = asciiString;
                        OutputInConsole(asciiString);
                    }
                });
        }

        private static void OutputInConsole(string asciiString)
        {
#if DEBUG
            DebugLog("OutputInConsole", ConsoleColor.Green);
#endif
            Console.Clear();
            Console.Write(asciiString);
        }

        private static void DebugLog(string message, ConsoleColor color = default)
        {
            if (!IS_DEBUG) return;
            Console.ForegroundColor = color;
            Console.WriteLine($@"{message}	{DateTime.Now}");
        }

        private static void InitializeForm()
        {
            var form = new Form
            {
                Width = 800,
                Height = 600
            };

            var pictureBox = new PictureBox
            {
                Width = form.Width,
                Height = form.Height,
                Left = 0,
                Top = 0
            };

            form.Controls.Add(pictureBox);

            form.Show();

            var timer = new Timer();
            timer.Interval = 1000 / 30;
            timer.Tick += (sender, args) => OutputInForm(pictureBox, _asciiString);
            timer.Start();
        }

        private static void OutputInForm(PictureBox pictureBox, string asciiString)
        {
        }
    }
}