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
            return $"Grid contains {TheGrid.Children.Count} children, and there are {GameViewModel.Cells.Count} Cells";
        }

        public GridView()
        {
            InitializeComponent();
        }
    }
}