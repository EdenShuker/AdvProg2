using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client2
{
    public class Player
    {
        private TcpClient client;
        private bool isConnected;
        private bool isWaitingForAnswer;
        private bool canWriteToServer;

        public Player(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");

            this.isConnected = true;
            this.canWriteToServer = true;
            this.isWaitingForAnswer = false;
        }

        public void Start()
        {
            Listen();
            Talk();
            this.client.GetStream().Dispose();
            this.client.Close();
        }

        public void Listen()
        {
            new Task(() =>
            {
                NetworkStream stream = this.client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                while (this.isConnected)
                {
                    if (isWaitingForAnswer)
                    {
                        string answer = reader.ReadString();
                        Console.WriteLine("Result = {0}", answer);

                        answer = reader.ReadString();
                        if (answer.Equals("close client"))
                        {
                            this.isConnected = false;
                            Console.WriteLine("Press any key to quit");
                        }
                    }
                }
            }).Start();
        }

        public void Talk()
        {
            NetworkStream stream = this.client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            while (this.isConnected)
            {
                if (canWriteToServer)
                {
                    Console.Write("Please enter your command: ");
                    string command = Console.ReadLine();
                    writer.Write(command);
                    Console.WriteLine("Data sent to server");
                    stream.Flush();
                }
            }
        }
    }
}
