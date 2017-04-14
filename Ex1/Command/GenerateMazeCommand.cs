using System;
using System.Net.Sockets;
using MazeLib;
using ServerProject.ModelLib;

namespace ServerProject.Command
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

        public override Checksum Check(string[] args)
        {
            Checksum checksum = new Checksum();
            if (args.Length != 3)
            {
                checksum.Valid = false;
                checksum.ErrorMsg = "Invalid number of arguments";
            }
            try
            {
                int.Parse(args[1]);
                int.Parse(args[2]);
                checksum.Valid = true;
            }
            catch (Exception)
            {
                checksum.Valid = false;
                checksum.ErrorMsg = "Rows and Cols need to be an integer";
            }
            return checksum;
        }
    }
}