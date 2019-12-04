using System.ComponentModel;
using System.Drawing;
using LearnXamarin.XamarinBase;

namespace LearnXamarin.Models
{
    public class GridCell : ObservableObject
    {
        private int _value;
        public int Value
        {
            get { return _value; }
            set { Set(nameof(Value), ref _value, value); }
        }

        public Point GridPosition { get; private set; }

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
