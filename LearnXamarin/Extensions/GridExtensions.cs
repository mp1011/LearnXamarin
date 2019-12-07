using System.Linq;
using Xamarin.Forms;

namespace LearnXamarin.Extensions
{
    public static class GridExtensions
    {
        public static void SetNumRowsAndColumns(this Grid grid, int rows, int columns)
        {
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();

            foreach (var row in Enumerable.Range(0, rows))
                grid.RowDefinitions.Add(new RowDefinition());

            foreach (var col in Enumerable.Range(0, columns))
                grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.ForceLayout();
        }
    }
}
