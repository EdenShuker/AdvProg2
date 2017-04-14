using System.Net.Sockets;
using MazeLib;
using ServerProject.ModelLib;

namespace ServerProject.Command
{
    public class StartGameCommand : ServerProject.Command.Command
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