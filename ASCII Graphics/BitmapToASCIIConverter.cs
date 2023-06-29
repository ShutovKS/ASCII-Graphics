#region

using System.Drawing;

#endregion

namespace ASCII_Graphics;

public static class BitmapToAsciiConverter
{
    private readonly static char[] AsciiCharsForGrayColor =
        // { ' ', '.', ',', ':', ';', '!', '+', '*', '?', '1', '0', '=', 'S', '&', '%', '$', '№', '#', '@' };
        { ' ', '.', ',', ':', ';', '!', '+', '*', '?', '%', 'S', '№', '#', '@' };

    public static string BitmapToAscii(Bitmap bitmap)
    {
        var asciiString = "";
        for (var y = 0; y < bitmap.Height; y++)
        {
            for (var x = 0; x < bitmap.Width; x++)
            {
                var pixel = bitmap.GetPixel(x, y);
                var grayScale = (pixel.R + pixel.G + pixel.B) / 3;
                var index = grayScale * (AsciiCharsForGrayColor.Length - 1) / 255;
                asciiString += AsciiCharsForGrayColor[index];
            }

            asciiString += "\n";
        }

        return asciiString;
    }
}