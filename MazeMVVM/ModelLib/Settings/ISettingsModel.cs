namespace MazeMVVM.ModelLib.Settings
{
    /// <summary>
    /// Interface of settings model.
    /// </summary>
    public interface ISettingsModel
    {
        /// <summary>
        /// string that represents the IP of the server.
        /// </summary>
        string ServerIP { get; set; }

        /// <summary>
        /// Port number of the server.
        /// </summary>
        int ServerPort { get; set; }

        /// <summary>
        /// Number of rows in the maze.
        /// </summary>
        int MazeRows { get; set; }

        /// <summary>
        /// Number of columns in the maze.
        /// </summary>
        int MazeCols { get; set; }

        /// <summary>
        /// Number represents the searching algorithm.
        /// </summary>
        int SearchAlgorithm { get; set; }

        /// <summary>
        /// Save the current settings.
        /// </summary>
        void SaveSettings();
    }
}