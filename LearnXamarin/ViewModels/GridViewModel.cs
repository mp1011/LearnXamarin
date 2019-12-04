using System.Linq;
using LearnXamarin.Extensions;
using LearnXamarin.Models;
using LearnXamarin.Services;
using LearnXamarin.XamarinBase;

namespace LearnXamarin.ViewModels
{
    public class GridViewModel : ObservableObject
    {
        private readonly GridService _gridService;
        private GameGrid _grid;

        private CellViewModel[] _cells;
        public CellViewModel[] Cells
        {
            get { return _cells; }
            set { Set(nameof(Cells), ref _cells, value); }
        }

        public RelayCommand<string> SwipeCommand => new RelayCommand<string>(SwipeExecuted);

        public GridViewModel(GridService gridService)
        {
            _gridService = gridService;
            _grid = _gridService.CreateNew(new System.Drawing.Size(5, 5), 2);
            Cells = _grid
                .Select(cell => new CellViewModel(cell))
                .ToArray();
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
            Cells = _grid
                .Select(cell => new CellViewModel(cell))
                .ToArray();
        }
    }
}
