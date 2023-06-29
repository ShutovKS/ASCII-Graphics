#region

using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using ASCII_Graphics;

#endregion


const bool isDebug = false;
Bitmap? _bitmap = null;
object _bitmapLock = new();


Console.ForegroundColor = ConsoleColor.Green;
Console.BackgroundColor = ConsoleColor.Black;

var _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
if (_videoDevices == null) throw new ArgumentNullException(nameof(_videoDevices));

var _videoSource = new VideoCaptureDevice(_videoDevices[0].MonikerString);
_videoSource.Start();

await InitializeVideoSource();

await ProcessFramesAsync();

ImageProcessing();

Console.ReadLine();


async Task InitializeVideoSource()
{
    await Task.Run(() => _videoSource.Start());
}

async Task ProcessFramesAsync()
{
    await Task.Run(() => { _videoSource.NewFrame += VideoSourceNewFrame; });
}

void VideoSourceNewFrame(object sender, NewFrameEventArgs eventArgs)
{
#if DEBUG
    DebugLog("VideoSource_NewFrame", ConsoleColor.Yellow);
#endif
    lock (_bitmapLock)
    {
#pragma warning disable CA1416
        _bitmap = (Bitmap)eventArgs.Frame.Clone();
#pragma warning restore CA1416
    }
}

async void ImageProcessing()
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
                lock (_bitmapLock)
                {
                    if (_bitmap == null) continue;
#pragma warning disable CA1416
                    bitmap = (Bitmap)_bitmap.Clone();
#pragma warning restore CA1416
                }

                bitmap = Extensions.ResizeBitmap(bitmap);

                asciiString = BitmapToAsciiConverter.BitmapToAscii(bitmap);

                OutputInConsole(asciiString);
            }
        });
}

void OutputInConsole(string asciiString)
{
#if DEBUG
    DebugLog("OutputInConsole", ConsoleColor.Green);
#endif
    Console.Clear();
    Console.Write(asciiString);
}

#if DEBUG
void DebugLog(string message, ConsoleColor color = default)
{
    if (!isDebug) return;
    Console.ForegroundColor = color;
    Console.WriteLine($@"{message}	{DateTime.Now}");
}
#endif