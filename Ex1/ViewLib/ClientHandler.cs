using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using ServerProject.ControllerLib;

namespace ServerProject.ViewLib
{
    class ClientHandler : IClientHandler
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
                bool inGame = true;
                do
                {
                    Console.WriteLine("performing task");
                    string commandLine = reader.ReadString();
                    if (commandLine.Equals("close"))
                    {
                        break;
                    }
                    Console.WriteLine("Got command: {0}", commandLine);
                    AnswerInfo result = controller.ExecuteCommand(commandLine, client);
                    if (result.IsAnswerForSender)
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.Write(result.Answer);
                    }
                    else
                    {
                        BinaryWriter writer = new BinaryWriter(result.DestClient.GetStream());
                        writer.Write(result.Answer);
                        result.DestClient.GetStream().Flush();
                    }
                    inGame = controller.IsClientInGame(client);
                    stream.Flush();
                } while (inGame);
                stream.Dispose();
                // Client is not in game (or no longer in game)
                client.Close();
            }).Start();
        }
    }
}