using LearnXamarin.Models;
using LearnXamarin.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using LearnXamarin.Extensions;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

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

        private GameState _gameState = GameState.WaitingForPlayer;

        public GameState GameState
        {
            get => _gameState;
            set
            {
                if(_gameState != value)
                {
                    _gameState = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameState)));
                }
            }
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
            Score = 0;
            _grid = _gridService.CreateNew(Cells, _game.GridSize, _game.NewTilesPerRound);
            GameState = GameState.WaitingForPlayer;
        });

        public ICommand QuitGame => new Command(() =>
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        });

        public ICommand SwipeCommand => new Command
        (
            execute: directionString =>
            {
                var direction = (directionString.ToString()).ParseEnum<MoveDirection>();
                _gridService.MoveAndCombineCells(_grid, direction);
                GameState = GameState.Animating;
            },
            canExecute: o => GameState == GameState.WaitingForPlayer
        );

        public ICommand StartNextRound => new Command(
            execute: o =>
            {
                _gridService.EndTurn(_grid);
                _gridService.AddRandomCell(_grid);
                _gridService.AddRandomCell(_grid);

                _game.Turns++;

                Score = _scoringService.UpdatePlayerScore(_grid);
                if (_gridService.CanBeMoved(_grid))
                    GameState = GameState.WaitingForPlayer;
                else
                    GameState = GameState.GameOver;
            },
            canExecute: o=>GameState == GameState.Animating
        );
    }
}
