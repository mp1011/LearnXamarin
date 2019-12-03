using LearnXamarin.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LearnXamarin.Models
{
    public class MovingCells : IEnumerable<MovingCell>
    {
        private MovingCell[] _cells;
        private Size _gridSize;

        public MovingCells(IEnumerable<MovingCell> cells, Size gridSize)
        {
            _cells = cells.ToArray();
            _gridSize = gridSize;
        }

        public MovingCell GetNeighbor(MovingCell cell, Direction dir)
        {
            var offset = dir.ToOffset();
            var neighborPosition = cell.GridDestination.Translate(offset);
            var cellAtPosition = _cells.FirstOrDefault(p =>
                p.GridDestination.X == neighborPosition.X
                && p.GridDestination.Y == neighborPosition.Y);

            return cellAtPosition;

        }

        public bool IsSpaceAvailable(Point pt)
        {
            if (!pt.IsInBounds(_gridSize))
                return false;

            var cellAtPosition = _cells.FirstOrDefault(p =>
                p.GridDestination.X == pt.X
                && p.GridDestination.Y == pt.Y);

            return cellAtPosition == null || cellAtPosition.NewValue == 0;
        }

        public IEnumerator<MovingCell> GetEnumerator()
        {
            return ((IEnumerable<MovingCell>)_cells).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<MovingCell>)_cells).GetEnumerator();
        }
    }
}
