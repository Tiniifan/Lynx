using System.Drawing;

namespace Lynx.Tools
{
    /// <summary>
    /// Provides methods for drawing images.
    /// </summary>
    public class Draw
    {
        /// <summary>
        /// Draws an image onto a bitmap at the specified coordinates.
        /// </summary>
        /// <param name="bmp">The bitmap to draw on.</param>
        /// <param name="x">The x-coordinate to draw the image at.</param>
        /// <param name="y">The y-coordinate to draw the image at.</param>
        /// <param name="imagePath">The image to draw.</param>
        /// <returns>The bitmap with the drawn image.</returns>
        public static Bitmap DrawImage(Bitmap bmp, int x, int y, Image imagePath)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(imagePath, new Point(x, y));
            }
            return bmp;
        }
    }
}
