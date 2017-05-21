using MazeMVVM.ModelLib.Settings;

namespace MazeMVVM.ViewModelLib.Settings
{
    /// <summary>
    /// Settings view model.
    /// </summary>
    public class SettingsViewModel : ViewModel, ISettingsViewModel
    {
        /// <summary>
        /// Settings Model.
        /// </summary>
        private ISettingsModel model;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model"> settings model </param>
        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// IP address of server.
        /// </summary>
        public string ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }

        /// <summary>
        /// Number of server's port.
        /// </summary>
        public int ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        /// <summary>
        /// Number of rows in the maze.
        /// </summary>
        public int MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        /// <summary>
        /// Number of columns in the maze.
        /// </summary>
        public int MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        /// <summary>
        /// Number that represents the searching algorithm.
        /// </summary>
        public int SearchAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set
            {
                this.model.SearchAlgorithm = value;
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }

        /// <summary>
        /// Save the current settings.
        /// </summary>
        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}