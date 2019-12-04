using LearnXamarin.Models;
using System.ComponentModel;
using System.Drawing;

namespace LearnXamarin.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private int _value = 0;
        public int Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        private Point gridPosition;
        public Point GridPosition
        {
            get { return gridPosition; }
            set
            {
                if (gridPosition == value) return;
                gridPosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GridPosition)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CellViewModel(GridCell cell)
        {
            Value = cell.Value;
            GridPosition = cell.GridPosition;
        }

    }
}
    
