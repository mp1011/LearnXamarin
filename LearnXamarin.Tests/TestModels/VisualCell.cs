using Xamarin.Forms;
using Xamarin.UITest.Queries;

namespace LearnXamarin.Tests.TestModels
{
    public class VisualCell
    {
        public int Value { get; }

        public AppRect Coordinates { get; }

        public Point Center => new Point(Coordinates.CenterX, Coordinates.CenterY);

        public VisualCell(int value, AppRect coordinates)
        {
            Value = value;
            Coordinates = coordinates;
        }
    }
}
