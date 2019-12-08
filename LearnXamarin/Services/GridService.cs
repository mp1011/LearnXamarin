using LearnXamarin.Extensions;
using LearnXamarin.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LearnXamarin.Services
{
    public class GridService
    {
        private readonly RandomService _randomService;

        public GridService(RandomService randomService)
        {
            _randomService = randomService;
        }

        public GameGrid CreateNew(ICollection<GridCell> cellCollection, Size size, int filledCells)
        {
            var grid = new GameGrid(cellCollection, size);
            foreach (var ix in Enumerable.Range(0, filledCells))
                AddRandomCell(grid);

            return grid;
        }

        public void AddRandomCell(GameGrid grid)
        {
            var emptyCells = GetEmptyCells(grid).ToArray();
            if(emptyCells.Any())
            {
                var cell = _randomService.RandomElement(emptyCells);
                var value = _randomService.RandomNumber(1, 3) * 2;

                var zeroCell = grid.TryGetCell(cell.X, cell.Y);
                if (zeroCell != null)
                    zeroCell.Value = value;
                else 
                    grid.AddCell(new GridCell(cell.X, cell.Y, value));
            }
        }

        private IEnumerable<Point> GetEmptyCells(GameGrid grid)
        {
            foreach (var row in Enumerable.Range(0, grid.Size.Height))
            {
                foreach (var column in Enumerable.Range(0, grid.Size.Width))
                {
                    var cell = grid.TryGetCell(column, row);
                    if (cell == null || cell.Value == 0)
                        yield return new Point(column, row);
                }
            }
        }

        /// <summary>
        /// Moves a line in the direction of the highest index, 
        /// combining equal cells into the sum of their values
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public void MoveAndCombineCells(GameGrid grid, MoveDirection direction)
        {
            foreach (var cell in grid)
            {
                cell.SuppressEvents = true;
                cell.TargetGridPosition = cell.OriginalGridPosition;
            }

            MoveCells(grid, direction);
            CombineCells(grid, direction);
            MoveCells(grid, direction);

            foreach (var cell in grid)
            {
                cell.SuppressEvents = false;
                cell.TargetGridPosition = cell.TargetGridPosition;
                cell.Value = cell.Value;
            }
        }

        public void EndTurn(GameGrid grid)
        {
            foreach(var cell in grid)
            {
                cell.OriginalGridPosition = cell.TargetGridPosition;
                cell.ValueChanged = false;
            }
        }

        private void MoveCells(GameGrid grid, MoveDirection direction)
        {
            var motionOffset = direction.ToOffset();

            bool movedAny = true;
            while (movedAny)
            {
                movedAny = false;
                foreach (var cell in grid.Where(c=>c.Value>0))
                {
                    var newPosition = cell.TargetGridPosition.Translate(motionOffset);
                    if (IsSpaceAvailable(grid, newPosition))
                    {
                        cell.TargetGridPosition = newPosition;
                        movedAny = true;
                    }
                }
            }
        }

        private void CombineCells(GameGrid grid, MoveDirection direction)
        {
            foreach (var cell in grid)
            {
                var neighbor = GetNeighbor(cell, grid, direction);

                if (neighbor != null && 
                    !cell.ValueChanged &&
                    !neighbor.ValueChanged &&
                    neighbor.Value == cell.Value)
                {
                    neighbor.Value = neighbor.Value + cell.Value;
                    cell.Value = 0;
                    cell.ValueChanged = true;
                    neighbor.ValueChanged = true;
                }
            }
        }

        public GridCell GetNeighbor(GridCell cell, GameGrid grid, MoveDirection dir)
        {
            var offset = dir.ToOffset();
            var neighborPosition = cell.TargetGridPosition.Translate(offset);
            var cellAtPosition = grid.FilledCells.FirstOrDefault(p =>
                p.TargetGridPosition.X == neighborPosition.X
                && p.TargetGridPosition.Y == neighborPosition.Y);

            return cellAtPosition;

        }

        public bool IsSpaceAvailable(GameGrid grid, Point pt)
        {
            if (!pt.IsInBounds(grid.Size))
                return false;

            var cellAtPosition = grid.FilledCells.FirstOrDefault(p =>
                p.TargetGridPosition.X == pt.X
                && p.TargetGridPosition.Y == pt.Y);

            return cellAtPosition == null || cellAtPosition.Value == 0;
        }
    }
}
