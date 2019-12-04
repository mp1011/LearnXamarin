using LearnXamarin.Models;
using LearnXamarin.XamarinBase;
using System.Drawing;

namespace LearnXamarin.ViewModels
{
    public class CellViewModel : ObservableObject
    {
        private int _value;
        public int Value
        {
            get => _value;
            set { Set(nameof(Value), ref _value, value); }
        }

        public Point GridPosition { get; }


        public CellViewModel(GridCell cell)
        {
            Value = cell.Value;
            GridPosition = cell.GridPosition;
        }

    }
}

