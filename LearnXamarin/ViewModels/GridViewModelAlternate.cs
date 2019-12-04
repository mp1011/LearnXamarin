using System.Collections.ObjectModel;
using LearnXamarin.Extensions;
using LearnXamarin.Models;
using LearnXamarin.Services;
using LearnXamarin.XamarinBase;

namespace LearnXamarin.ViewModels
{
    public class GridViewModelAlternate : ObservableObject
    {
        private readonly GridService _gridService;
        private GameGrid _grid;

        private ObservableCollection<GridCell> _cells;
        public ObservableCollection<GridCell> Cells
        {
            get { return _cells; }
            set { Set(nameof(Cells), ref _cells, value); }
        }

        public RelayCommand<string> SwipeCommand => new RelayCommand<string>(SwipeExecuted);

        public GridViewModelAlternate(GridService gridService)
        {
            _gridService = gridService;
            _grid = _gridService.CreateNew(new System.Drawing.Size(5, 5), 2);

            Cells = new ObservableCollection<GridCell>();
            Cells.Clear();
            foreach (var cell in _grid)
                Cells.Add(cell);
        }

        private void SwipeExecuted(string directionString)
        {
            var direction = directionString.ParseEnum<Direction>();
            OnSwiped(direction);
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
