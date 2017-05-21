namespace MazeMVVM.ViewModelLib.Player.SinglePlayer
{
    /// <summary>
    /// Interface of single payer view model.
    /// </summary>
    public interface ISPViewModel : IPlayerVM
    {
        /// <summary>
        /// Restart the game.
        /// </summary>
        void RestartGame();

        /// <summary>
        /// Solve the maze.
        /// </summary>
        void SolveMaze();
    }
}