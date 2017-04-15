using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MazeLib;
using System.Net.Sockets;

namespace ServerProject.ModelLib
{
    /// <summary>
    /// Holds info aboute multiplayer game.
    /// </summary>
    class MultiPlayerGame : SinglePlayerGame
    {
        // The two participents in the game.
        public PlayerInfo Host { get; set; }
        public PlayerInfo Guest { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maze"> the maze of the game </param>
        /// <param name="player"> the player starting the game </param>
        /// <param name="location"> the starting location of the player </param>
        public MultiPlayerGame(Maze maze, TcpClient player, Position location) : base(maze)
        {
            this.Host = new PlayerInfo(player, location);
        }


        /// <summary>
        /// return the player info of the given player.
        /// </summary>
        /// <param name="player"></param>
        /// <returns> PlayerInfo of the specified player </returns>
        public PlayerInfo GetPlayer(TcpClient player)
        {
            if (Host.Player == player)
            {
                return Host;
            }
            if (Guest.Player == player)
            {
                return Guest;
            }
            return null;
        }

        /// <summary>
        /// Given a player returns its competitor im the game/
        /// </summary>
        /// <param name="player"></param>
        /// <returns> player's competitor </returns>
        public PlayerInfo GetCompetitorOf(TcpClient player)
        {
            if (Host.Player == player)
            {
                return Guest;
            }
            if (Guest.Player == player)
            {
                return Host;
            }
            return null;
        }
    }
}
