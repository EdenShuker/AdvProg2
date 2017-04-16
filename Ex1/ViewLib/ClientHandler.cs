using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using ServerProject.ControllerLib;
using Newtonsoft.Json.Linq;

namespace ServerProject.ViewLib
{
    /// <summary>
    /// Handle a client.
    /// </summary>
    public class ClientHandler : IClientHandler
    {
        /// <summary>
        /// Controller of server.
        /// </summary>
        private IController controller;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="controller">Constroller of server.</param>
        public ClientHandler(IController controller)
        {
            this.controller = controller;
            // Subcribe the event.
            this.controller.ForwardMessageEvent += ForwardMessage;
        }

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                try
                {
                    // As long as the server needs to take care of the client.
                    do
                    {
                        // Get a command line from the client and execute it.
                        string commandLine = reader.ReadString();
                        Console.WriteLine("Got command: {0}", commandLine);
                        controller.ExecuteCommand(commandLine, client);
                    } while (controller.ProceedConnectionWith(client));

                    // Notify the client about end-of-connection (by empty-JSON message).
                    string closeMessage = new JObject().ToString();
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(closeMessage);
                    stream.Flush();
                }
                catch (EndOfStreamException)
                {
                    // Client has closed the connection, so reading from stream failed.
                }
                finally
                {
                    // Close the connection with the client.
                    stream.Dispose();
                    client.Close();
                }
            }).Start();
        }

        public void ForwardMessage(object sender, ForwardMessageEventArgs eventArgs)
        {
            // Open a stream of the desired client.
            NetworkStream stream = eventArgs.Addressee.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            // Send him the message.
            writer.Write(eventArgs.Message);
            stream.Flush();
        }
    }
}