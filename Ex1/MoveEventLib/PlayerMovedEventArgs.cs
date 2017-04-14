using System;
using MazeLib;

namespace ServerProject.MoveEventLib
{
    public class PlayerMovedEventArgs : EventArgs
    {
        public Direction Direction { get; private set; }
        public PlayerMovedEventArgs(string direction)
        {
            if (direction.Equals("right"))
            {
                Direction = Direction.Right;
            }
            else if (direction.Equals("left"))
            {
                Direction = Direction.Left;
            }
            else if (direction.Equals("up"))
            {
                Direction = Direction.Up;
            }
            else if (direction.Equals("down"))
            {
                Direction = Direction.Down;
            }
        }
    }
}
