using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;

namespace MazeMVVM.ModelLib
{
    public class SinglePlayerModel : PlayerModel
    {
        public SinglePlayerModel(IClient client, string nameOfGame, int rows, int cols) : base(client)
        {
            this.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.write($"generate {nameOfGame} {rows} {cols}");
            this.Maze = Maze.FromJSON(this.Client.read());
            this.Pos = this.Maze.InitialPos;
            this.Disconnect();
        }

        public void RestartGame()
        {
            Pos = this.Maze.InitialPos;
        }

        public string SolveMaze()
        {
            this.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.write($"solve {this.Maze.Name} {Properties.Settings.Default.SearchAlgorithm}");
            JObject solution = JObject.Parse(this.Client.read());
            this.Disconnect();
            return solution["Solution"].ToString();
        }
    }
}



