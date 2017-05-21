namespace MazeMVVM.ViewModelLib.Settings
{
    /// <summary>
    /// Interface of settings view model.
    /// </summary>
    interface ISettingsViewModel
    {
        /// <summary>
        /// Server Ip address.
        /// </summary>
        string ServerIP { get; set; }

        /// <summary>
        /// Number of port of the server.
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