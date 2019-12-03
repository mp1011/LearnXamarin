using LearnXamarin.Extensions;
using LearnXamarin.Models;
using LearnXamarin.Services;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearnXamarin.ViewModels
{
    public class GridViewModel : INotifyPropertyChanged
    {
        private readonly GridService _gridService;
        private GameGrid _grid;

        public CellViewModel[] Cells { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

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

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cells)));
        }
    }
}
