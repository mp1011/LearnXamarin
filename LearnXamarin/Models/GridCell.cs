using System.ComponentModel;
using System.Drawing;

namespace LearnXamarin.Models
{
    public class GridCell 
    {
        public int Value { get; set; }

        public Point GridPosition  { get; private set; }

        public GridCell(int x, int y, int value)
        {
            GridPosition = new Point(x, y);
            Value = value;
        }

        public override string ToString()
        {
            return $"[{Value}] ({GridPosition.X},{GridPosition.Y})";
        }
    }
}
