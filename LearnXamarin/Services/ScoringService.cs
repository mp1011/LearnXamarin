using LearnXamarin.Models;
using System.Linq;

namespace LearnXamarin.Services
{
    public class ScoringService
    {
        private Game _currentGame;

        public Game StartGame()
        {
            _currentGame = new Game();
            return _currentGame;
        }

        public int UpdatePlayerScore(GameGrid grid)
        {
            if (_currentGame == null)
                return 0;

            _currentGame.Score += grid.Sum(cell => cell.Value);
            return _currentGame.Score;
        }
    }
}
