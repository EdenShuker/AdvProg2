using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using ServerProject.ControllerLib;

namespace ServerProject.ViewLib
{
    public class ClientHandler : IClientHandler
    {
        private Controller controller;

        public ClientHandler(Controller controller)
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
                do
                {
                    string commandLine = reader.ReadString();
                    if (commandLine.Equals("close"))
                        break;
                    Console.WriteLine("Got command: {0}", commandLine);
                    string result = controller.ExecuteCommand(commandLine, client);
                    if(!result.Equals("do nothing"))
                        writer.Write(result);
                    stream.Flush();
                } while (controller.IsClientInGame(client));
                stream.Dispose();
                // Client is not in game (or no longer in game)
                client.Close();
            }).Start();
        }


        public void SendMesaageToCompetitor(TcpClient client, string message)
        {
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(message);
            stream.Flush();
        }
    }

}