using System.ComponentModel;
using System.Drawing;

namespace LearnXamarin.Models
{
    public class GridCell : INotifyPropertyChanged
    {
        private int _value;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public Point GridPosition  { get; private set; }

        public GridCell(int x, int y, int value)
        {
            GridPosition = new Point(x, y);
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"[{Value}] ({GridPosition.X},{GridPosition.Y})";
        }
    }
}
