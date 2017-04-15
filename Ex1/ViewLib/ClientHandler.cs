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
        }

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);
                try
                {
                    do
                    {
                        string commandLine = reader.ReadString();
                        Console.WriteLine("Got command: {0}", commandLine);
                        string result = controller.ExecuteCommand(commandLine, client);
                        if (!result.Equals("do nothing"))
                            writer.Write(result);
                        stream.Flush();
                    } while (controller.ProceedConnectionWith(client));

                    // Notify the client about end-of-connection
                    string closeMessage = new JObject().ToString();
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

        /// <summary>
        ///  
        /// </summary>
        /// <param name="client"></param>
        /// <param name="message"></param>
        public void WriteMessageTo(TcpClient client, string message)
        {
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(message);
            stream.Flush();
        }
    }
}