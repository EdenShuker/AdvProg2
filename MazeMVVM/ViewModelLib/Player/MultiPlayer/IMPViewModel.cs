using System;

namespace MazeMVVM.ViewModelLib.Player.MultiPlayer
{
    /// <summary>
    /// Interface of multi player model.
    /// </summary>
    public interface IMPViewModel : IPlayerVM
    {
        /// <summary>
        /// Event of game ending.
        /// </summary>
        event EventHandler VMGameEnded;

        /// <summary>
        /// Postion of the competitor represented by string.
        /// </summary>
        string VM_PosOtherPlayer { get; }

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