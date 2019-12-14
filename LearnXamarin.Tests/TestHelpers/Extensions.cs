using LearnXamarin.Models;
using LearnXamarin.Tests.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.UITest;

namespace LearnXamarin.Tests.TestHelpers
{
    public static class Extensions
    {

        public static IApp Swipe(this IApp app, MoveDirection direction)
        {
            var grid = app
                .WaitForElement("TheGrid")
                .Single()
                .Rect;

            var moveDistance = grid.Width / 2;
            var dragStart = new Point(grid.CenterX, grid.CenterY);
            var offset = direction.ToOffset();
            var dragEnd = dragStart.Offset(offset.X * moveDistance, offset.Y * moveDistance);

            app.DragCoordinates((float)dragStart.X, (float)dragStart.Y, (float)dragEnd.X, (float)dragEnd.Y);
            
            return app;
        }

        public static Dictionary<string, VisualCell> GetCells(this IApp app)
        {
            var visualCells = app
                .WaitForElement("GridCell")
                .ToDictionary
                (
                    k => k.Id,
                    v => new VisualCell
                    (
                        value: int.Parse(v.Text),
                        coordinates: v.Rect
                    )
                );


            return visualCells;
        }

        public static Comparison[] CompareWith(this Dictionary<string, VisualCell> cellsBefore,
            Dictionary<string, VisualCell> cellsAfter)
        {
            var keys = cellsBefore.Keys
                .Union(cellsAfter.Keys)
                .Distinct()
                .ToArray();

            var comparisons = keys
                .Select(key =>
                {
                    VisualCell before = null, after = null;
                    cellsBefore.TryGetValue(key, out before);
                    cellsAfter.TryGetValue(key, out after);
                    return new Comparison(before, after);
                })
                .ToArray();

            return comparisons;
        }
    }
}
