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
            return solution.ToJSON(name, pathDirections);
        }

        public static string pathDirections(State<Position> start)
        {
            StringBuilder builder = new StringBuilder();
            Position from = start.Data;
            while (start.CameFrom != null)
            {
                Position to = start.CameFrom.Data;
                string num;
                if (from.Row > to.Row)
                {
                    num = "2";
                }
                else if (from.Row < to.Row)
                {
                    num = "3";
                }
                else if (from.Col > to.Col)
                {
                    num = "0";
                }
                else
                {
                    num = "1";
                }
                builder.Append(num);
                start = start.CameFrom;
                from = to;
            }
            return builder.ToString();
        }
    }
}
