using MazeMVVM.ViewLib.Controls;

namespace MazeMVVM.ViewModelLib.Player
{
    /// <summary>
    /// Interface of player view model.
    /// </summary>
    public interface IPlayerVM
    {
        /// <summary>
        /// String represents the maze.
        /// </summary>
        string VM_MazeStr { get; }

        /// <summary>
        /// number of rows in the maze.
        /// </summary>
        int VM_Rows { get; }

        /// <summary>
        /// Number of columns in the maze.
        /// </summary>
        int VM_Cols { get; }

        /// <summary>
        /// Name of the maze.
        /// </summary>
        string VM_MazeName { get; }

        /// <summary>
        /// String represents the initial position in the maze.
        /// </summary>
        string VM_InitialPos { get; }

        /// <summary>
        /// String represents the goal position in the maze.
        /// </summary>
        string VM_GoalPos { get; }

        /// <summary>
        /// String represents the current position of the player in the maze.
        /// </summary>
        string VM_Pos { get; }

        /// <summary>
        /// Subscribes the given maze displayer.
        /// </summary>
        /// <param name="mazeDisplayer"> maze displayer</param>
        void Subscribe(MazeDisplayer mazeDisplayer);
    }
}