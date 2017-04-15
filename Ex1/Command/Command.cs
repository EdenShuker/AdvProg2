using System.Net.Sockets;
using ServerProject.ControllerLib;
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

        public abstract ForwardMessageEventArgs Execute(string[] args, TcpClient client = null);

        public abstract Checksum Check(string[] args);
    }
}