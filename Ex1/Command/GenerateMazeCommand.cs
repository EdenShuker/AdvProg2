using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Ex1.ModelLib;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Ex1.Command
{
    public class GenerateMazeCommand : Command
    {
        public GenerateMazeCommand(IModel model) : base(model)
        {
        }

        public override string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = this.Model.GenerateMaze(name, rows, cols);
            return maze.ToJSON();
        }
    }
}