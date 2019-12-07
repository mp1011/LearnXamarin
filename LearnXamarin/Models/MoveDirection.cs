using System;
using System.Drawing;

namespace LearnXamarin.Models
{
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionExtensions
    {
        public static Point ToOffset(this MoveDirection dir)
        {
            switch(dir)
            {
                case MoveDirection.Right: return new Point(1, 0);
                case MoveDirection.Left: return new Point(-1, 0);
                case MoveDirection.Up: return new Point(0, -1);
                case MoveDirection.Down: return new Point(0, 1);
                default: throw new ArgumentException($"Invalid direction: {dir}");
            }
        }
    }
}
