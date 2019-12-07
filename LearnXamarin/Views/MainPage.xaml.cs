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
        public MainPage()
        {
            InitializeComponent();
        }

        private void DebugButton_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Debug",TheGameGrid.DebugText(), "Floop the Pig");
        }
    }
}
