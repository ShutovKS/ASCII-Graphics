#region

using System.Drawing;

#endregion

namespace ASCII_Graphics;

public static class Extensions
{
    private const double ASPECT_RATIO = 0.4;
    private const int MAX_WIDTH = 200;

    public static Bitmap BitmapToGrayScale(Bitmap bitmap)
    {
        for (var y = 0; y < bitmap.Height; y++)
        {
            for (var x = 0; x < bitmap.Width; x++)
            {
                var pixel = bitmap.GetPixel(x, y);
                var grayScale = (pixel.R + pixel.G + pixel.B) / 3;
                bitmap.SetPixel(x, y, Color.FromArgb(grayScale, grayScale, grayScale));
            }
        }

        return bitmap;
    }

    public static Bitmap ResizeBitmap(Bitmap bitmap)
    {
        if (bitmap == null) return null;

        var newHeight = (int)(bitmap.Height * MAX_WIDTH / bitmap.Width * ASPECT_RATIO);
        if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
        {
            bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, newHeight));
        }

        return bitmap;
    }
}