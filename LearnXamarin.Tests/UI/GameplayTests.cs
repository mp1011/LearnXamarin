using FluentAssertions;
using LearnXamarin.Models;
using LearnXamarin.Tests.TestHelpers;
using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;

namespace LearnXamarin.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class GameplayTests
    {
        IApp app;
        Platform platform;

        public GameplayTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void CellsAppearWhenAppLoads()
        {
            var cells = app.WaitForElement(c => c.Text("2"));
            cells.Should().NotBeEmpty();
            cells.Length.Should().Be(2);
        }

        [TestCase(MoveDirection.Up)]
        [TestCase(MoveDirection.Left)]
        [TestCase(MoveDirection.Down)]
        [TestCase(MoveDirection.Right)]
        public void WhenUserSwipesCellsMove(MoveDirection direction)
        {
            var cellsBefore = app.GetCells();
            app.Swipe(direction);

            var cellChanges = cellsBefore
                .CompareWith(app.GetCells());

            switch(direction)
            {
                case MoveDirection.Up:
                    cellChanges
                        .Any(c => c.After.Center.Y < c.Before.Center.Y)
                        .Should().BeTrue();
                    break;
                case MoveDirection.Down:
                    cellChanges
                        .Any(c => c.After.Center.Y > c.Before.Center.Y)
                        .Should().BeTrue();
                    break;
                case MoveDirection.Left:
                    cellChanges
                        .Any(c => c.After.Center.X < c.Before.Center.Y)
                        .Should().BeTrue();
                    break;
                case MoveDirection.Right:
                    cellChanges
                        .Any(c => c.After.Center.X > c.Before.Center.Y)
                        .Should().BeTrue();
                    break;
            }
        }
    }
}
