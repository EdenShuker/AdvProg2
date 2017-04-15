using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using ServerProject.ControllerLib;
using Newtonsoft.Json.Linq;

namespace ServerProject.ViewLib
{
    public class ClientHandler : IClientHandler
    {
        private IController controller;

        public ClientHandler(IController controller)
        {
            this.controller = controller;
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
                    do
                    {
                        string commandLine = reader.ReadString();
                        Console.WriteLine("Got command: {0}", commandLine);
                        controller.ExecuteCommand(commandLine, client);
                    } while (controller.ProceedConnectionWith(client));

                    // Notify the client about end-of-connection
                    string closeMessage = new JObject().ToString();
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(closeMessage);
                    stream.Flush();
                }
                catch (EndOfStreamException)
                {
                    // Client has closed the connection, so reading from stream failed
                }
                finally
                {
                    // Close the connection with the client
                    stream.Dispose();
                    client.Close();
                }
            }).Start();
        }

        public void ForwardMessage(object sender, ForwardMessageEventArgs eventArgs)
        {
            NetworkStream stream = eventArgs.Addressee.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(eventArgs.Message);
            stream.Flush();
        }
    }
}