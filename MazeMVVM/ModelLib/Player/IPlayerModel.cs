using System.ComponentModel;
using MazeLib;

namespace MazeMVVM.ModelLib.Player
{
    /// <summary>
    /// Interface of player model.
    /// </summary>
    public interface IPlayerModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Position of the main player in the maze.
        /// </summary>
        Position Pos { get; set; }

        /// <summary>
        /// The maze that the player is playing on.
        /// </summary>
        Maze Maze { get; set; }

        /// <summary>
        /// Connect to the given IP and port.
        /// </summary>
        /// <param name="ip">Ip address</param>
        /// <param name="port">Port number</param>
        void Connect(string ip, int port);

        /// <summary>
        /// Disconnect from the IP address and port.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// The player moved with the given direction.
        /// </summary>
        /// <param name="direction">Direction of movement</param>
        void Move(Direction direction);
    }
}