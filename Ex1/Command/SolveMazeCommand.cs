using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using ServerProject.ModelLib;

namespace ServerProject.Command
{
    public class SolveMazeCommand : Command
    {
        public SolveMazeCommand(IModel model) : base(model)
        {
        }

        static string ParseDirections(Stack<State<Position>> path)
        {
            StringBuilder builder = new StringBuilder();
            Position from = path.Pop().Data;
            while (path.Count != 0)
            {
                Position to = path.Pop().Data;
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
                from = to;
            }
            return builder.ToString();
        }

        public override string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            Solution<Position> solution = Model.SolveMaze(name, algorithm);
            string solutionJSON = solution.ToJSON(ParseDirections);
            JObject solutionObj = JObject.Parse(solutionJSON);
            JObject mergedObj = new JObject();
            mergedObj["Name"] = name;
            mergedObj.Merge(solutionObj);
            return mergedObj.ToString();
        }

        public override Checksum Check(string[] args)
        {
            Checksum checksum = new Checksum();
            if (args.Length != 2)
            {
                checksum.Valid = false;
                checksum.ErrorMsg = "Invalid number of arguments";
            }
            else
            {
                try
                {
                    int algo = int.Parse(args[1]);
                    checksum.Valid = true;
                }
                catch (Exception)
                {
                    checksum.Valid = false;
                    checksum.ErrorMsg = "Search-Aalgorithm need to be an integer";
                }
            }
            return checksum;
        }
    }
}