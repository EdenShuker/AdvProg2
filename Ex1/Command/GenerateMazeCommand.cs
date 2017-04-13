using System.Net.Sockets;
using MazeLib;
using ServerProject.ModelLib;

namespace ServerProject.Command
{
    public class GenerateMazeCommand : ServerProject.Command.Command
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