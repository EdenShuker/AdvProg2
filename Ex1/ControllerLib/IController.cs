using System;
using System.Net.Sockets;

namespace ServerProject.ControllerLib
{
    /// <summary>
    /// Controller of server.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Event of message-forwarding.
        /// </summary>
        event EventHandler<ForwardMessageEventArgs> ForwardMessageEvent;

        /// <summary>
        /// Execute some command.
        /// </summary>
        /// <param name="commandLine">The command to execute.</param>
        /// <param name="client">The client which sent the command.</param>
        void ExecuteCommand(string commandLine, TcpClient client);

        /// <summary>
        /// Check if the server need to proceed the connection with the given client.
        /// </summary>
        /// <param name="client">The client to check.</param>
        /// <returns>True if server needs to proceed the connection, false otherwise.</returns>
        bool ProceedConnectionWith(TcpClient client);
    }
}