using System.ComponentModel;
using System.Drawing;

namespace LearnXamarin.Models
{
    public class GridCell : INotifyPropertyChanged
    {
 
        public bool SuppressEvents { get; set; }       //this is ugly

        public bool NeedsToMove => !OriginalGridPosition.Equals(TargetGridPosition);

        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                if(!SuppressEvents) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public bool ValueChanged { get; set; }

        private Point _originalGridPosition;
        public Point OriginalGridPosition
        {
            get => _originalGridPosition;
            set
            {
                _originalGridPosition = value;
                if (!SuppressEvents) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginalGridPosition)));
            }
        }


        public Point TempTargetPosition { get; set; }

        private Point _targetGridPosition;
        public Point TargetGridPosition
        {
            get => _targetGridPosition;
            set
            {
                _targetGridPosition = value;
                if (!SuppressEvents) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargetGridPosition)));
            }
        }
        public GridCell(int x, int y, int value)
        {
            OriginalGridPosition = new Point(x, y);
            TargetGridPosition = new Point(x, y);
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"[{Value}] ({TargetGridPosition.X},{TargetGridPosition.Y})";
        }
    }
}