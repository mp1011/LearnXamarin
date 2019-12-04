using LearnXamarin.XamarinBase;

namespace LearnXamarin.ViewModels
{
    class MainViewModel : ObservableObject
    {
        private int _score = 1234;
        public int Score
        {
            get => _score;
            set { Set(nameof(Score), ref _score, value); }
        }

    }
}
