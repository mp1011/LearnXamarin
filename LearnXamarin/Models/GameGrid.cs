using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LearnXamarin.Models
{
    public class GameGrid : IEnumerable<GridCell>
    {
        private ICollection<GridCell> _cells;

        public Size Size { get; }

        public GridCell[] FilledCells => _cells.Where(p => p.Value > 0).ToArray();


        public GridCell TryGetCell(int x, int y)
        {
            return _cells
                .OrderByDescending(p=>p.Value)
                .FirstOrDefault(p => p.OriginalGridPosition.X == x && p.OriginalGridPosition.Y == y);
        }

        public bool ContainsPoint(Point pt)
        {
            return pt.X >= 0
                    && pt.Y >= 0
                    && pt.X < Size.Width
                    && pt.Y < Size.Height;
        }

        public IEnumerator<GridCell> GetEnumerator()
        {
            return ((IEnumerable<GridCell>)_cells).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<GridCell>)_cells).GetEnumerator();
        }

        public void AddCell(GridCell cell)
        {
            _cells.Add(cell);
        }

        public GameGrid(ICollection<GridCell> cells, Size gridSize)
        {
            _cells = cells;
            Size = gridSize;
        }
    }
}
