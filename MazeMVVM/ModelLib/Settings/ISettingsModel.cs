namespace MazeMVVM.ModelLib.Settings
{
    public interface ISettingsModel
    {
        string ServerIP { get; set; }

        int ServerPort { get; set; }

        int MazeRows { get; set; }

        int MazeCols { get; set; }

        int SearchAlgorithm { get; set; }

        void SaveSettings();
    }
}