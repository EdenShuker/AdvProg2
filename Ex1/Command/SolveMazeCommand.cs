using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1.Command
{
    class SolveMazeCommand : ICommand
    {
        private IModel model;

        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            Solution<Position> solution = model.SolveMaze(name, algorithm);
        }
    }
}
