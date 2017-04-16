using System.Net.Sockets;
using ServerProject.ControllerLib;

namespace ServerProject.ViewLib
{
    /// <summary>
    /// Object that handles a client.
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// Handle the given client.
        /// </summary>
        /// <param name="client">Client to take care of.</param>
        void HandleClient(TcpClient client);

        /// <summary>
        /// Send a message to its destination.
        /// </summary>
        /// <param name="sender">The caller of the function.</param>
        /// <param name="eventArgs">Holds the message and its destination.</param>
        void ForwardMessage(object sender, ForwardMessageEventArgs eventArgs);
    }
}