using System.Net.Sockets;
using MazeLib;
using ServerProject.ModelLib;

namespace ServerProject.Command
{
    public class JoinToGameCommand : ServerProject.Command.Command
    {
        public JoinToGameCommand(IModel model) : base(model)
        {
        }

        public override string Execute(string[] args, TcpClient client = null)
        {
            string nameOfGame = args[0];
            Maze maze = this.Model.JoinTo(nameOfGame, client);
            return maze.ToJSON();
        }
    }
}