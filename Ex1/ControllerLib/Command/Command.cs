using System.Net.Sockets;
using ServerProject.ModelLib;

namespace ServerProject.ControllerLib.Command
{
    /// <summary>
    /// Abstract command.
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        /// Model of server.
        /// </summary>
        protected IModel Model;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Model of server.</param>
        protected Command(IModel model)
        {
            this.Model = model;
        }

        public abstract ForwardMessageEventArgs Execute(string[] args, TcpClient client = null);

        public abstract Checksum Check(string[] args);
    }
}