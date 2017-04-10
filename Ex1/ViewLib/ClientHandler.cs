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
                do
                {
                    Console.WriteLine("performing task");
                    commandLine = reader.ReadString();
                    Console.WriteLine("Got command: {0}", commandLine);
                    result = controller.ExecuteCommand(commandLine, client);
                    writer.Write(result);
                    stream.Flush();
                    //TODO: problem when we enter "list" before "join" because we close 
                    // the client before joining. maybe we should change the condition 
                    // "IsClientInGame".
                } while (controller.IsClientInGame(client));
                stream.Dispose();
                // Client is not in game (or no longer in game)
                client.Close();
            }).Start();
        }
    }
}