namespace MazeMVVM.ModelLib.Player.SinglePlayer
{
    /// <summary>
    /// Interface of single player model.
    /// </summary>
    public interface ISinglePlayerModel : IPlayerModel
    {
        /// <summary>
        /// Solve the maze of the current game.
        /// </summary>
        /// <returns> string that represents the solution of the maze </returns>
        string SolveMaze();

        /// <summary>
        /// Restart the current game.
        /// </summary>
        void RestartGame();
    }
}