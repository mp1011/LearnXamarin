using System.Drawing;

namespace LearnXamarin.Extensions
{
    public static class PointExtensions
    {
        public static Point Translate(this Point pt, Point offset)
        {
            return new Point(pt.X + offset.X, pt.Y + offset.Y);
        }

        public static bool IsInBounds(this Point pt, Size size)
        {
            return pt.X >= 0
                && pt.Y >= 0
                && pt.X < size.Width
                && pt.Y < size.Height;
        }
    }
}
