#region

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace ASCII_Graphics
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png"
            };

            while (true)
            {
                Console.ReadLine();
                Console.Clear();

                if (openFileDialog.ShowDialog() != DialogResult.OK) continue;

                var bitmap = new Bitmap(openFileDialog.FileName);
                bitmap = bitmap.ResizeBitmap();
                bitmap = bitmap.ToGrayScale();
                var str = bitmap.ToAscii();
                Console.WriteLine(str);
            }

            Console.ReadLine();
        }
    }
}