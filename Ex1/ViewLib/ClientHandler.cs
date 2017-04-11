using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ex1.ControllerLib;

namespace Ex1.ViewLib
{
    class ClientHandler : IClientHandler
    {
        private Controller controller;

        public ClientHandler(ControllerLib.Controller controller)
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
                string commandLine = null;
                string result = null;
                bool InGame = true;
                do
                {
                    Console.WriteLine("performing task");
                    commandLine = reader.ReadString();
                    Console.WriteLine("Got command: {0}", commandLine);
                    result = controller.ExecuteCommand(commandLine, client);
                    writer.Write(result);
                    stream.Flush();
                    if (InGame = controller.IsClientInGame(client))
                    {
                        writer.Write("keep going");
                    }
                } while (InGame);
                writer.Write("close client");
                stream.Dispose();
                // Client is not in game (or no longer in game)
                client.Close();
            }).Start();
        }
    }
}