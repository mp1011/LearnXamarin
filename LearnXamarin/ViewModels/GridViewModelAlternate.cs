using LearnXamarin.Extensions;
using LearnXamarin.Models;
using LearnXamarin.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearnXamarin.ViewModels
{
    public class GridViewModelAlternate : INotifyPropertyChanged
    {
        private readonly GridService _gridService;
        private GameGrid _grid;

        private ObservableCollection<GridCell> cells;
        public ObservableCollection<GridCell> Cells
        {
            get { return cells; }
            set
            {
                if (cells == value)
                    return;
                cells = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cells)));
            }
        } 

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SwipeCommand => new Command(directionString =>
        {
            var dir = (directionString.ToString()).ParseEnum<Direction>();
            OnSwiped(dir);
        });

        public GridViewModelAlternate(GridService gridService)
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

            // unnecessary - the ObservableCollection raises the INotifyCollectionChanged.CollectionChanged event when you call Add/Remove/Clear
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cells)));
        }
    }
}
