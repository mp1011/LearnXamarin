using FluentAssertions;
using LearnXamarin.Models;
using NUnit.Framework;

namespace LearnXamarin.Tests.Models
{
    [TestFixture]
    class GridPointTests
    {
        [TestCase(5,3, Direction.Right, 6,3)]
        [TestCase(8, 3, Direction.Right, 0, 4)]
        [TestCase(5, 3, Direction.Down, 5, 4)]
        [TestCase(5, 2, Direction.Up, 5, 1)]
        [TestCase(0, 0, Direction.Left, 8, 1)]
        public void CanOffsetPointOn9x9Grid(int x, int y, Direction dir, int newX, int newY)
        {
            var gridPoint = new GridCell(x, y,0, new Size(9, 9));
            gridPoint.Offset(dir);

            gridPoint.GridPosition.X.Should().Be(newX);
            gridPoint.GridPosition.Y.Should().Be(newY);
        }
    }
}
