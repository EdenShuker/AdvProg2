using System;
using MazeLib;

namespace MazeMVVM.ModelLib.Player.MultiPlayer
{
    /// <summary>
    /// Interface of multi player model.
    /// </summary>
    public interface IMultiPlayerModel : IPlayerModel
    {
        /// <summary>
        /// Event of game ending.
        /// </summary>
        event EventHandler GameEnded;

        /// <summary>
        /// Position of the competitor.
        /// </summary>
        Position PosOtherPlayer { get; set; }

        /// <summary>
        /// Start the game.
        /// </summary>
        void Start();

        /// <summary>
        /// Close the game.
        /// </summary>
        void CloseGame();
    }
}