using System;
using MazeLib;

namespace MazeMVVM.ViewLib
{
    /// <summary>
    /// Argument of player moved event.
    /// </summary>
    public class PlayerMovedEventArgs : EventArgs
    {
        /// <summary>
        /// The direction which the player moved.
        /// </summary>
        public Direction Direction { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="direction"></param>
        public PlayerMovedEventArgs(Direction direction)
        {
            Direction = direction;
        }
    }
}