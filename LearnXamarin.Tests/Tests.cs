using FluentAssertions;
using LearnXamarin.IOC;
using LearnXamarin.Services;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace LearnXamarin.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
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

        [TestCase(true)]
        [TestCase(false)]
        public void WhenUserSwipesCellsMove(bool leftToRight)
        {
            var cells = app.WaitForElement(c => c.Text("2"));
            cells.Should().NotBeEmpty();

            var xPositionsBefore = cells
                .ToDictionary(k => k.Id, v => v.Rect.CenterX);

            if (leftToRight)
                app.SwipeLeftToRight();
            else
                app.SwipeRightToLeft();

            cells = app.WaitForElement(c => c.Text("2"));
            cells.Length.Should().Be(4);

            var xPositionsNow = cells
              .ToDictionary(k => k.Id, v => v.Rect.CenterX);

            bool anyLess = false;
            foreach(var oldPos in xPositionsBefore)
            {
                var newPos = xPositionsNow[oldPos.Key];
                if (newPos < oldPos.Value)
                    anyLess = true;
                if(leftToRight)
                    newPos.Should().BeGreaterThan(oldPos.Value);
                else
                    newPos.Should().BeLessOrEqualTo(oldPos.Value);
            }

            anyLess.Should().Be(!leftToRight);

        }
    }
}
