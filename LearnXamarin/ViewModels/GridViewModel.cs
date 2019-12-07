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

        private ObservableCollection<GridCell> _cells = new ObservableCollection<GridCell>();
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

        private bool _isTransitioning;
        public bool IsTransitioning
        {
            get => _isTransitioning;
            set
            {
                _isTransitioning = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTransitioning)));
            }
        }

        public ICommand SwipeCommand => new Command(directionString =>
        {
            var dir = (directionString.ToString()).ParseEnum<Direction>();
            OnSwiped(dir);
        });

        public GridViewModel(GridService gridService)
        {
            Cells = new ObservableCollection<GridCell>();

            _gridService = gridService;
            _grid = _gridService.CreateNew(Cells, new System.Drawing.Size(5, 5), 2);
        }

        private void OnSwiped(Direction direction)
        {
            _gridService.MoveAndCombineCells(_grid, direction);
            IsTransitioning = true;
        }

        public void StartNextRound()
        {
            _gridService.CommitPositions(_grid);
            _gridService.AddRandomCell(_grid);
            _gridService.AddRandomCell(_grid);

            IsTransitioning = false;
        }
    }
}
