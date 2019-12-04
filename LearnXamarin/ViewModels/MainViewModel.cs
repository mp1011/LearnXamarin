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
                _score = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
