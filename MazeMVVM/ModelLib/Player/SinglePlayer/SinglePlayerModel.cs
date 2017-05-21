using MazeLib;
using MazeMVVM.ModelLib.Communication;
using Newtonsoft.Json.Linq;

namespace MazeMVVM.ModelLib.Player.SinglePlayer
{
    /// <summary>
    /// Implementation of single player model.
    /// </summary>
    public class SinglePlayerModel : PlayerModel, ISinglePlayerModel
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client"> client to communicate </param>
        /// <param name="nameOfGame"> name of the current game </param>
        /// <param name="rows"> number of rows for the maze </param>
        /// <param name="cols"> number of columns for the maze </param>
        public SinglePlayerModel(IClient client, string nameOfGame, int rows, int cols) : base(client)
        {
            // Send messae to the server
            this.Client.Write($"generate {nameOfGame} {rows} {cols}");
            // Get response
            this.Maze = Maze.FromJSON(this.Client.Read());
            this.Pos = this.Maze.InitialPos;
            if (this.Client.Read() == new JObject().ToString())
            {
                this.Disconnect();
            }
        }

        /// <summary>
        /// Restart the current game.
        /// </summary>
        public void RestartGame()
        {
            // Change the current position of the player to the initial-position
            Pos = this.Maze.InitialPos;
        }

        /// <summary>
        /// Solve the maze.
        /// </summary>
        /// <returns> string representing the solution of the current maze </returns>
        public string SolveMaze()
        {
            // Connect to server and request solution
            this.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.Write($"solve {this.Maze.Name} {Properties.Settings.Default.SearchAlgorithm}");
            // Get response
            JObject solution = JObject.Parse(this.Client.Read());
            if (this.Client.Read() == new JObject().ToString())
            {
                this.Disconnect();
            }
            return solution["Solution"].ToString();
        }
    }
}