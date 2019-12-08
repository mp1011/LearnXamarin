using LearnXamarin.Extensions;
using LearnXamarin.Models;
using LearnXamarin.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridView : ContentView
    {
        public GameViewModel GameViewModel => BindingContext as GameViewModel;

        private List<CellView> _cells = new List<CellView>();

        public string DebugText()
        {
            var firstCell = TheGrid.Children.FirstOrDefault() as CellView;
            string firstCellText = firstCell?.DebugText() ?? "There are no cells.";
            
            return $"Grid contains {TheGrid.Children.Count} children, and there are {GameViewModel.Cells.Count} Cells. {firstCellText}";
        }

        public GridView()
        {
            InitializeComponent();
            TheGrid.ChildAdded += TheGrid_ChildAdded;
        }

        private void TheGrid_ChildAdded(object sender, ElementEventArgs e)
        {
            _cells.Add(e.Element as CellView);
        }

        protected override void OnBindingContextChanged()
        {
            GameViewModel.PropertyChanged += GameViewModel_PropertyChanged;
        }

        private void GameViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(GameViewModel.GameState))
            {
                if (GameViewModel.GameState == GameState.Animating)
                    MoveAllTiles();
            }
        }

        private async void MoveAllTiles()
        {
            var tasks = _cells
                .ToArray()
                .Where(c => c.NeedsToMove)
                .Select(c => c.MoveToDestination())
                .ToArray();

            await Task.WhenAll(tasks);

            GameViewModel.StartNextRound.Execute(null);
        }
    }
}