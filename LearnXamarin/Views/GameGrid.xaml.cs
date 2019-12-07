
using LearnXamarin.Models;
using LearnXamarin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameGrid : ContentView
    {
      
        public GameGrid()
        {
            InitializeComponent();
            BindingContextChanged += GameGrid_BindingContextChanged;
        }

        private void GameGrid_BindingContextChanged(object sender, System.EventArgs e)
        {
            if(BindingContext is GridViewModel gg)
            {
                gg.PropertyChanged += Gg_PropertyChanged;
            }
        }

        private void Gg_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(BindingContext is GridViewModel gameGrid && e.PropertyName == nameof(gameGrid.IsTransitioning))
            {
                if(gameGrid.IsTransitioning)
                    TransitionCells(gameGrid);
                else
                {
                    foreach (var visualCell in TheGrid.Children)
                    {
                        visualCell.TranslationX = 0;
                        visualCell.TranslationY = 0;
                    }
                }
            }
        }

        private async void TransitionCells(GridViewModel grid)
        {
            List<Task> motionTasks = new List<Task>();
            foreach(var visualCell in TheGrid.Children)
            {
                var gameCell = visualCell.BindingContext as GridCell;
                if(gameCell.Value > 0 && !gameCell.TargetGridPosition.Equals(gameCell.OriginalGridPosition))
                    motionTasks.Add(TransitionCellToDestination(gameCell,visualCell));
            }

            await Task.WhenAll(motionTasks.ToArray());

            grid.StartNextRound();
        }

        private async Task TransitionCellToDestination(GridCell cell, VisualElement element)
        {
            var transition = CalculateCellTransitionOffset(cell, element);
            await element.TranslateTo(transition.X, transition.Y, 100);
        }

        private Point CalculateCellTransitionOffset(GridCell cell, VisualElement element)
        {
            var gridHeight = element.Height;
            var gridWidth = element.Width;

            var deltaY = (cell.TargetGridPosition.Y - cell.OriginalGridPosition.Y) * gridHeight;
            var deltaX = (cell.TargetGridPosition.X - cell.OriginalGridPosition.X) * gridWidth;
            return new Point(deltaX,deltaY);
        }

    }
}