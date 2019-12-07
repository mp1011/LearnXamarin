using LearnXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LearnXamarin.Extensions
{
    public static class SwipeDirectionExtensions
    {
        public static MoveDirection ToGridDirection(this SwipeDirection swipeDirection)
        {
            switch(swipeDirection)
            {
                case SwipeDirection.Left: return MoveDirection.Left;
                case SwipeDirection.Right: return MoveDirection.Right;
                case SwipeDirection.Up: return MoveDirection.Up;
                case SwipeDirection.Down: return MoveDirection.Down;
                default: throw new ArgumentException($"Unknown direction: {swipeDirection}");
            }
        }
    }
}
