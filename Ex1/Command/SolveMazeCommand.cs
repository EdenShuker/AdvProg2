using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;

namespace Ex1.Command
{
    public class SolveMazeCommand : Command
    {
        public SolveMazeCommand(IModel model) : base(model)
        {
        }

        static string ParseDirections(Stack<State<Position>> path)
        {
            StringBuilder builder = new StringBuilder(); 
            Position from = path.
            while (goal.CameFrom != null)
            {
                Position to = goal.CameFrom.Data;
                string num;
                if (from.Row > to.Row)
                {
                    num = "2";
                }
                else if (from.Row > to.Row)
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
                goal = goal.CameFrom;
                from = to;
            }
            string path = builder.ToString();
            Console.WriteLine(path);
            return path;
        }

        public override string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            Solution<Position> solution = Model.SolveMaze(name, algorithm);
            JObject solutionObj = JObject.Parse(solution.ToJSON(ParseDirections));
            JObject mergedObj = new JObject();
            mergedObj["Name"] = name;
            mergedObj.Merge(solutionObj);
            return mergedObj.ToString();
        }
    }
}