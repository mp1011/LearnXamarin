using System.Linq;
using System.Windows.Input;
using LearnXamarin.Extensions;
using LearnXamarin.Models;
using LearnXamarin.Services;
using LearnXamarin.XamarinBase;
using Xamarin.Forms;

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

        public ICommand SwipeCommand => new Command(directionString =>
        {
            var dir = (directionString.ToString()).ParseEnum<Direction>();
            OnSwiped(dir);
        });

        public GridViewModel(GridService gridService)
        {
            _gridService = gridService;
            _grid = _gridService.CreateNew(new System.Drawing.Size(5, 5), 2);
            Cells = _grid
                .Select(cell => new CellViewModel(cell))
                .ToArray();
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
