using System;
using System.Net.Sockets;
using MazeLib;
using ServerProject.ControllerLib;
using ServerProject.ModelLib;

namespace ServerProject.Command
{
    public class StartGameCommand : Command
    {
        public StartGameCommand(IModel model) : base(model)
        {
        }

        public override ForwardMessageEventArgs Execute(string[] args, TcpClient client = null)
        {
            string nameOfGame = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = this.Model.StartGame(nameOfGame, rows, cols, client);
            while (!this.Model.IsGameBegun(nameOfGame)) ;
            return new ForwardMessageEventArgs(client, maze.ToJSON());
        }

        public override Checksum Check(string[] args)
        {
            Checksum checksum = new Checksum();
            if (args.Length != 3)
            {
                checksum.Valid = false;
                checksum.ErrorMsg = "Invalid number of arguments";
            }
            else
            {
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
            }
            return checksum;
        }
    }
}