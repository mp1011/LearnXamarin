using LearnXamarin.Models;
using System.ComponentModel;

namespace LearnXamarin.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private int _score = 1234;
        public int Score
        {
            get => _score;
            set
            {
                if (_score == value) return;
                _score = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Score)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
