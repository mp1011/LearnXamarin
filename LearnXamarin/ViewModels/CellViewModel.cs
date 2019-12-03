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
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public Point GridPosition { get;}

        public event PropertyChangedEventHandler PropertyChanged;

        public CellViewModel(GridCell cell)
        {
            Value = cell.Value;
            GridPosition = cell.GridPosition;
        }

    }
}
    
