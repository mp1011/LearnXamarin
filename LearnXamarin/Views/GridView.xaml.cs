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

        private List<Cell> _cells = new List<Cell>();

        public string DebugText()
        {
            return $"Grid contains {TheGrid.Children.Count} children, and there are {GameViewModel.Cells.Count} Cells";
        }

        public GridView()
        {
            InitializeComponent();
            BindingContextChanged += GameGrid_BindingContextChanged;
            TheGrid.ChildAdded += TheGrid_ChildAdded;
        }

        private void TheGrid_ChildAdded(object sender, ElementEventArgs e)
        {
            System.Console.WriteLine($"Added cell, grid size = {TheGrid.ColumnDefinitions.Count} x {TheGrid.RowDefinitions.Count} ");
            _cells.Add(e.Element as Cell);
        }

        protected override void OnBindingContextChanged()
        {
            if (GameViewModel != null)
            {
                TheGrid.SetNumRowsAndColumns(rows: GameViewModel.GridSize.Height, columns: GameViewModel.GridSize.Width);
                GameViewModel.PropertyChanged += GameViewModel_PropertyChanged;

                GameViewModel.Cells.CollectionChanged += Cells_CollectionChanged;
            }
        }

        private int messageCounter = 0;
        private void Cells_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            System.Console.WriteLine($"{messageCounter++}: Cells_CollectionChanged, action={e.Action}, NewItems={e.NewItems?.Count} grid size = {TheGrid.ColumnDefinitions.Count} x {TheGrid.RowDefinitions.Count} ");

        }

        private void GameGrid_BindingContextChanged(object sender, System.EventArgs e)
        {
           
        }

        private void GameViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(GameViewModel != null && e.PropertyName == nameof(GameViewModel.IsTransitioning))
            {
                if(GameViewModel.IsTransitioning)
                    TransitionCells();
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

        private void TransitionCells()
        {
            //var motionTasks = _cells
            //    .Where(cell => cell.NeedsToMove)
            //    .ToArray() //avoids collection modified problem
            //    .Select(cell => cell.MoveToDestination())
            //    .ToArray();

            //await Task.WhenAll(motionTasks.ToArray());

            //GameViewModel.StartNextRound.Execute(null);
        }

      

    }
}