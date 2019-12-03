using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            TheGrid.LayoutChanged += TheGrid_LayoutChanged;
        }

        private void TheGrid_LayoutChanged(object sender, EventArgs e)
        {
            var foo = TheGrid.Children.Count();
            var bar = TheGrid.Children.FirstOrDefault();
            if (bar != null)
            {
                var womp = bar.Bounds.Width;
                Console.WriteLine(womp);
            }
        }
    }
}