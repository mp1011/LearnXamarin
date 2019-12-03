using LearnXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LearnXamarin.Extensions
{
    public static class SwipeDirectionExtensions
    {
        public static Direction ToGridDirection(this SwipeDirection swipeDirection)
        {
            switch(swipeDirection)
            {
                case SwipeDirection.Left: return Direction.Left;
                case SwipeDirection.Right: return Direction.Right;
                case SwipeDirection.Up: return Direction.Up;
                case SwipeDirection.Down: return Direction.Down;
                default: throw new ArgumentException($"Unknown direction: {swipeDirection}");
            }
        }
    }
}
