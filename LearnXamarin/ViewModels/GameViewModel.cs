using LearnXamarin.Models;
using LearnXamarin.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using LearnXamarin.Extensions;
using Xamarin.Forms;

namespace LearnXamarin.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly ScoringService _scoringService;
        private readonly GridService _gridService;

        private Game _game;
        private GameGrid _grid;

        public event PropertyChangedEventHandler PropertyChanged;

        public GameViewModel(ScoringService scoringService, GridService gridService)
        {
            _scoringService = scoringService;
            _gridService = gridService;
            Cells = new ObservableCollection<GridCell>();

            StartGame.Execute(null);
        }

        public System.Drawing.Size GridSize => _game.GridSize;

        public int Score
        {
            get => _game.Score;
            set
            {
                _game.Score = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Score)));
            }
        }

        private ObservableCollection<GridCell> _cells;
        public ObservableCollection<GridCell> Cells 
        {
            get => _cells;
            set
            {
                _cells = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cells)));
            }
        }

        public ICommand StartGame => new Command(() =>
        {
            Cells.Clear();
            _game = _scoringService.StartGame();
            _grid = _gridService.CreateNew(Cells, _game.GridSize, _game.NewTilesPerRound);
        });

        public ICommand SwipeCommand => new Command
        (
            execute: directionString =>
            {
                var direction = (directionString.ToString()).ParseEnum<MoveDirection>();
                _gridService.MoveAndCombineCells(_grid, direction);
                //IsTransitioning = true;
            },
            canExecute: o => !IsTransitioning
        );

        public ICommand StartNextRound => new Command(() =>
        {
            _gridService.EndTurn(_grid);
           // _gridService.AddRandomCell(_grid);
          //  _gridService.AddRandomCell(_grid);

            Score = _scoringService.UpdatePlayerScore(_grid);
         //   IsTransitioning = false;
        });


        //todo: might be better represented as a Game State "waiting for turn, animating, game over, etc"
        private bool _isTransitioning;
        public bool IsTransitioning
        {
            get => _isTransitioning;
            set
            {
                if (IsTransitioning != value)
                {
                    _isTransitioning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTransitioning)));
                }
            }
        }

      
    }
}
