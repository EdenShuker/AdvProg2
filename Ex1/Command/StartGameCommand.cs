using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Ex1.Command
{
    public class StartGameCommand : Command
    {
        public StartGameCommand(IModel model) : base(model)
        {
        }

        public override string Execute(string[] args, TcpClient client = null)
        {
            string nameOfGame = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = this.Model.StartGame(nameOfGame, rows, cols, client);
            while (!this.Model.IsGameBegun(nameOfGame)) ;
            return maze.ToJSON();
        }
    }
}