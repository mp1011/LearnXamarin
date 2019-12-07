using System.ComponentModel;
using System.Drawing;

namespace LearnXamarin.Models
{
    public class Game
    {
        public int Score { get; set; }
       
        public int NewTilesPerRound { get; } = 2;

        public Size GridSize { get; } = new Size(5, 5);
    }
}
