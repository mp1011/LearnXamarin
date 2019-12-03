using LearnXamarin.Models;
using System.ComponentModel;

namespace LearnXamarin.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public GridViewModel Grid { get; }

        private int _score = 1234;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
            }
        }

        public MainViewModel(GridViewModel grid)
        {
            Grid = grid;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
