using System.Drawing;

namespace LearnXamarin.Models
{
    public class MovingCell
    {
        private GridCell _originalCell;

        public Point OriginalPosition => _originalCell.GridPosition;
        public Point GridDestination { get; set; }

        public int OriginalValue => _originalCell.Value;
        public int NewValue { get; set; }

        public bool ValueChanged => NewValue != OriginalValue;

        public MovingCell(GridCell originalCell)
        {
            _originalCell = originalCell;
            GridDestination = originalCell.GridPosition;
            NewValue = originalCell.Value;
        }

        public override string ToString()
        {
            return $"[{OriginalValue}=>{NewValue}] ({OriginalPosition.X},{OriginalPosition.Y}) => ({GridDestination.X},{GridDestination.Y})";
        }
    }
}
