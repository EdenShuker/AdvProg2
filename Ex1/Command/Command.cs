using System.Net.Sockets;
using ServerProject.ModelLib;

namespace ServerProject.Command
{
    public abstract class Command : ICommand
    {
        protected IModel Model;

        protected Command(IModel model)
        {
            this.Model = model;
        }

        public abstract string Execute(string[] args, TcpClient client = null);
    }
}