using MazeLib;
using MazeMVVM.ModelLib.Communication;
using Newtonsoft.Json.Linq;

namespace MazeMVVM.ModelLib.Player
{
    public class SinglePlayerModel : PlayerModel, ISinglePlayerModel
    {
        public SinglePlayerModel(IClient client, string nameOfGame, int rows, int cols) : base(client)
        {
            this.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.Write($"generate {nameOfGame} {rows} {cols}");
            this.Maze = Maze.FromJSON(this.Client.Read());
            this.Pos = this.Maze.InitialPos;
            if (this.Client.Read() == new JObject().ToString())
            {
                this.Disconnect();
            }
        }

        public void RestartGame()
        {
            Pos = this.Maze.InitialPos;
        }

        public string SolveMaze()
        {
            this.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.Write($"solve {this.Maze.Name} {Properties.Settings.Default.SearchAlgorithm}");
            JObject solution = JObject.Parse(this.Client.Read());
            if (this.Client.Read() == new JObject().ToString())
            {
                this.Disconnect();
            }
            return solution["Solution"].ToString();
        }
    }
}