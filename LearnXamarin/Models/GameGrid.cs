using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LearnXamarin.Models
{
    public class GameGrid : IEnumerable<GridCell>
    {
        private List<GridCell> _cells = new List<GridCell>();

        public Size Size { get; }

        public GameGrid() { }

        public GridCell TryGetCell(int x, int y)
        {
            return _cells.FirstOrDefault(p => p.GridPosition.X == x && p.GridPosition.Y == y);
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

        public GameGrid(IEnumerable<MovingCell> cells, Size gridSize)
        {
            _cells.AddRange(cells
                .Where(p => p.NewValue > 0)
               .Select(p => new GridCell(p.GridDestination.X, p.GridDestination.Y, p.NewValue)));
            Size = gridSize;
        }

        public GameGrid(IEnumerable<GridCell> cells, Size gridSize)
        {
            _cells.AddRange(cells);
            Size = gridSize;
        }
    }
}
