using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Starship.Win32.Extensions {
    public static class ImageExtensions {

        public static Bitmap Crop(this Image source, Rectangle selection) {
            var image = new Bitmap(selection.Width, selection.Height);

            using (var graphics = Graphics.FromImage(image)) {
                graphics.DrawImage(source, 0, 0, selection, GraphicsUnit.Pixel);
            }

            return image;
        }

        public static byte[] ToBytes(this Image image, ImageFormat format = null) {
            using (var stream = new MemoryStream()) {
                if (format == null) {
                    format = image.RawFormat;
                }

                image.Save(stream, format);
                return stream.ToArray();
            }
        }

        public static void OpenInPaint(this Image image) {
            var path = Path.GetTempFileName() + ".png";
            image.Save(path, ImageFormat.Png);

            Process.Start("mspaint.exe", path);
        }
    }
}