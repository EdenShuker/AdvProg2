using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using MazeLib;

namespace ServerProject.ModelLib
{

    /// <summary>
    /// Holds info about the player.
    /// </summary>
     class PlayerInfo
    {
        public TcpClient Player { get; set; }
        public Position Location { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="location"></param>
        public PlayerInfo(TcpClient player, Position location)
        {
            this.Player = player;
            this.Location = location;
        }

        /// <summary>
        /// Advance the player in the given direction.
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="direction"></param>
        /// <returns> true for valid move, false otherwise.</returns>
        public bool Move(Maze maze, string direction)
        {
            // Takes out player current position.
            int currentRow = this.Location.Row;
            int currentCol = this.Location.Col;
            
            // Right
            if (direction.Equals("right") && currentCol < maze.Cols - 1 &&
                maze[currentRow, currentCol + 1] == CellType.Free)
            {
                this.Location = new Position(currentRow, currentCol + 1);
            }
            // Left
            else if (direction.Equals("left") && currentCol > 0 &&
                     maze[currentRow, currentCol - 1] == CellType.Free)
            {
                this.Location = new Position(currentRow, currentCol - 1);
            }
            // Up
            else if (direction.Equals("up") && currentRow > 0 &&
                     maze[currentRow - 1, currentCol] == CellType.Free)
            {
                this.Location = new Position(currentRow - 1, currentCol);
            }
            // Down
            else if (direction.Equals("down") && currentRow < maze.Rows - 1 &&
                     maze[currentRow + 1, currentCol] == CellType.Free)
            {
                this.Location = new Position(currentRow + 1, currentCol);
            }
            else
            {
                // Invalid direction.
                return false;
            }
            return true;
        }
    }
}

