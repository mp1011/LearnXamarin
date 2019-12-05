using LearnXamarin.Extensions;
using LearnXamarin.Models;
using LearnXamarin.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearnXamarin.ViewModels
{
    public class GridViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly GridService _gridService;
        private GameGrid _grid;

        private ObservableCollection<GridCell> _cells;
        public ObservableCollection<GridCell> Cells
        {
            get
            {
                return _cells;
            }
            set
            {
                _cells = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cells)));
            }
        }

        public ICommand SwipeCommand => new Command(directionString =>
        {
            var dir = (directionString.ToString()).ParseEnum<Direction>();
            OnSwiped(dir);
        });

        public GridViewModel(GridService gridService)
        {
            _gridService = gridService;
            _grid = _gridService.CreateNew(new System.Drawing.Size(5, 5), 2);

            Cells = new ObservableCollection<GridCell>();
            Cells.Clear();
            foreach (var cell in _grid)
                Cells.Add(cell);
        }

        private void OnSwiped(Direction direction)
        {
            _grid = _gridService.MoveGrid(_grid, direction);
            _gridService.AddRandomCell(_grid);
            _gridService.AddRandomCell(_grid);

            //hack
            Cells.Clear();
            foreach (var cell in _grid)
                Cells.Add(cell);
        }
    }
}
