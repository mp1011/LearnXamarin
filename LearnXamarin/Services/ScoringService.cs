using LearnXamarin.Models;
using System;
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

            var maxCell = grid.Max(cell => cell.Value);
            var sum = grid.Sum(cell => cell.Value);


            _currentGame.Score = 1000 * (int)Math.Sqrt(maxCell) + (int)(sum / 5) * 5;

            return _currentGame.Score;
        }
    }
}
