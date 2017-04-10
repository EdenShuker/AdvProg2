using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Ex1
{
    public class PlayerMovedEventArgs : EventArgs
    {
        public Direction Direction { get; private set; }
        public PlayerMovedEventArgs(Direction direction)
        {
            Direction = direction;
        }
    }
}
