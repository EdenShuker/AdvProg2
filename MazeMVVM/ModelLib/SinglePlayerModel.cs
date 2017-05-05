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
            this.Connect("127.0.0.1", 8000);
            this.Client.write($"generate {nameOfGame} {rows} {cols}");
            this.Maze = Maze.FromJSON(this.Client.read());
            this.Pos = this.Maze.InitialPos;
            this.Disconnect();
        }

        public void RestartGame()
        {
            this.Pos = this.Maze.InitialPos;
        }

        public string SolveMaze()
        {
            this.Connect("127.0.0.1", 8000);
            this.Client.write($"solve {this.Maze.Name}");
            JObject solution = JObject.Parse(this.Client.read());
            this.Disconnect();
            return solution["Solution"].ToString();
        }
    }
}



