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

        /// <summary>
        /// Moves all cells in the grid in the given direction as
        /// far as they will go, combining adjacent equal cells into
        /// the sum of their values
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="direction"></param>
        public GameGrid MoveGrid(GameGrid grid, Direction direction)
        {
            var newCells = MoveAndCombineCells(grid, direction);
            return new GameGrid(newCells, grid.Size);
        }

        public GameGrid CreateNew(Size size, int filledCells)
        {
            var grid = new GameGrid(new GridCell[] { }, size);
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
        public MovingCell[] MoveAndCombineCells(GameGrid grid, Direction direction)
        {
            var movingCells = new MovingCells(
                grid.Where(c=>c.Value > 0)
                    .Select(c => new MovingCell(c)),
                grid.Size);
                
            MoveCells(movingCells, direction);
            CombineCells(movingCells, direction);
            MoveCells(movingCells, direction);

            return movingCells
                .Where(p => p.ValueChanged || p.NewValue > 0)
                .ToArray();
        }

        private void MoveCells(MovingCells cells, Direction direction)
        {
            var motionOffset = direction.ToOffset();

            bool movedAny = true;
            while (movedAny)
            {
                movedAny = false;
                foreach (var cell in cells.Where(c=>c.NewValue >0))
                {
                    var newPosition = cell.GridDestination.Translate(motionOffset);
                    if (cells.IsSpaceAvailable(newPosition))
                    {
                        cell.GridDestination = newPosition;
                        movedAny = true;
                    }
                }
            }
        }

        private void CombineCells(MovingCells cells, Direction direction)
        {
            foreach (var cell in cells)
            {
                var neighbor = cells.GetNeighbor(cell, direction);

                if (neighbor != null 
                    && !cell.ValueChanged 
                    && !neighbor.ValueChanged
                    && neighbor.OriginalValue == cell.OriginalValue)
                {
                    cell.NewValue = 0;
                    neighbor.NewValue = neighbor.OriginalValue + cell.OriginalValue;
                }
            }
        }
    }
}
