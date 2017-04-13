using System.Net.Sockets;
using ServerProject.ModelLib;

namespace ServerProject.Command
{
    public class CloseGameCommand : Command
    {
        public CloseGameCommand(IModel model) : base(model)
        {
        }

        public override string Execute(string[] args, TcpClient client = null)
        {
            string nameOfGame = args[0];
            this.Model.Close(nameOfGame);
            return "";
        }
    }
}