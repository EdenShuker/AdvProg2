using System;
using System.Net.Sockets;

namespace ServerProject.ControllerLib
{
    /// <summary>
    /// Object holding data about a message that the server needs to forward.
    /// </summary>
    public class ForwardMessageEventArgs : EventArgs
    {
        /// <summary>
        /// The client that needs to receive the message.
        /// </summary>
        public TcpClient Addressee { get; private set; }

        /// <summary>
        /// The message to send.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client">The addressee client.</param>
        /// <param name="message">The message to send to him.</param>
        public ForwardMessageEventArgs(TcpClient client, string message)
        {
            Addressee = client;
            Message = message;
        }
    }
}