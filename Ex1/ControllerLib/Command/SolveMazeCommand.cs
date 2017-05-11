using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using ServerProject.ModelLib;

namespace ServerProject.ControllerLib.Command
{
    public class SolveMazeCommand : Command
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Model of server.</param>
        public SolveMazeCommand(IModel model) : base(model)
        {
        }

        /// <summary>
        /// Function that convert states to string-representation.
        /// </summary>
        /// <param name="path">Stack of states.</param>
        /// <returns>String representation of the path.</returns>
        static string ParseDirections(Stack<State<Position>> path)
        {
            StringBuilder builder = new StringBuilder();
            Position from = path.Pop().Data;
            while (path.Count != 0)
            {
                Position to = path.Pop().Data;
                string num;
                // Up.
                if (from.Row > to.Row)
                {
                    num = "2";
                }
                // Down.
                else if (from.Row < to.Row)
                {
                    num = "3";
                }
                // Left.
                else if (from.Col > to.Col)
                {
                    num = "0";
                }
                // Right.
                else
                {
                    num = "1";
                }
                builder.Append(num);
                // Advance to the next state.
                from = to;
            }
            return builder.ToString();
        }

        public override ForwardMessageEventArgs Execute(string[] args, TcpClient client = null)
        {
            // Get the solution of the maze.
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            Solution<Position> solution = Model.SolveMaze(name, algorithm);
            // Get string represetation of the path.
            string solutionJSON = solution.ToJSON(ParseDirections);
            // Build the needed answer.
            JObject solutionObj = JObject.Parse(solutionJSON);
            JObject mergedObj = new JObject();
            mergedObj["Name"] = name;
            mergedObj.Merge(solutionObj);
            return new ForwardMessageEventArgs(client, mergedObj.ToString());
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