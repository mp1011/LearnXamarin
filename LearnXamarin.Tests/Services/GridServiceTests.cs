﻿using FluentAssertions;
using LearnXamarin.Models;
using LearnXamarin.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LearnXamarin.Tests.Services
{
    [TestFixture]
    class GridServiceTests
    {
        [Test]
        public void CanFillGridRandomly()
        {
            var service = new GridService(new RandomService());
            var grid = service.CreateNew(new List<GridCell>(), new Size(3, 3),0);

            foreach(var index in Enumerable.Range(0,grid.Size.Width*grid.Size.Height))
                service.AddRandomCell(grid);
            
            foreach(var row in Enumerable.Range(0,grid.Size.Height))
            {
                foreach(var column in Enumerable.Range(0,grid.Size.Width))
                {
                    grid.TryGetCell(column, row)
                        .Should()
                        .NotBeNull();
                }
            }
        }

        [TestCase("002,020,200", Direction.Right, "002,002,002")]
        [TestCase("002,020,200", Direction.Left, "200,200,200")]
        [TestCase("002,020,200", Direction.Up, "222,000,000")]
        [TestCase("002,020,200", Direction.Down, "000,000,222")]
        [TestCase("202,020,220", Direction.Right, "004,002,004")]
        [TestCase("400,400,400", Direction.Down, "000,800,400")]

        public void CanMoveGrid(string gridText, Direction dir, string expected)
        {
            var service = new GridService(new RandomService());
            var grid = ParseGrid(gridText);
            
            service.MoveAndCombineCells(grid, dir);
            service.CommitPositions(grid);

            var newGridText = GridToText(grid);
            newGridText.Should().Be(expected);
        }

        private GameGrid ParseGrid(string gridText)
        {
            var rows = gridText.Split(',');
            var cellValues = rows
                .Select(r => r.Select(c => int.Parse(c.ToString())).ToArray())
                .ToList();

            List<GridCell> cells = new List<GridCell>();

            int x = 0, y = 0;
            foreach(var row in cellValues)
            {
                x = 0;
                foreach(var cell in row)
                {
                    cells.Add(new GridCell(x, y, cell));
                    x++;
                }
                y++;
            }

            return new GameGrid(cells, new Size(x,y));
        }

        private string GridToText(GameGrid grid)
        {
            var sb = new StringBuilder();

            foreach(var row in Enumerable.Range(0,grid.Size.Height))
            {
                foreach(var column in Enumerable.Range(0,grid.Size.Width))
                {
                    var cell = grid.TryGetCell(column,row);
                    if (cell == null)
                        sb.Append("0");
                    else
                        sb.Append(cell.Value);
                }
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
