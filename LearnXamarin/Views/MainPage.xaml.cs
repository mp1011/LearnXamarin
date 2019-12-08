using LearnXamarin.Models;
using LearnXamarin.Services;
using LearnXamarin.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace LearnXamarin
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public GameViewModel GameViewModel => BindingContext as GameViewModel;

        public MainPage()
        {
            InitializeComponent();
            GameViewModel.PropertyChanged += GameViewModel_PropertyChanged;
        }

        private void GameViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GameViewModel.GameState))
            {
                if (GameViewModel.GameState == GameState.GameOver)
                    GameOverPanel.IsVisible = true;
                else
                    GameOverPanel.IsVisible = false;
            }
        }

        private void DebugButton_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Debug",TheGameGrid.DebugText(), "Floop the Pig");
        }
    }
}
