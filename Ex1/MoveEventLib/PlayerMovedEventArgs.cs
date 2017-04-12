using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Ex1.MoveEventLib
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
