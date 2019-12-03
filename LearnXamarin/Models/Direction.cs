using System;
using System.Drawing;

namespace LearnXamarin.Models
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionExtensions
    {
        public static Point ToOffset(this Direction dir)
        {
            switch(dir)
            {
                case Direction.Right: return new Point(1, 0);
                case Direction.Left: return new Point(-1, 0);
                case Direction.Up: return new Point(0, -1);
                case Direction.Down: return new Point(0, 1);
                default: throw new ArgumentException($"Invalid direction: {dir}");
            }
        }
    }
}
