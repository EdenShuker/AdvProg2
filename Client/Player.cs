using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Player
    {
        private TcpClient client;
        private bool isConnected;
        private bool isWaitingForAnswer;
        private bool canSendMessage;
        private HashSet<string> longTermCommands;

        public Player(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");

            this.isConnected = true;
            this.isWaitingForAnswer = false;
            this.canSendMessage = true;

            this.longTermCommands = new HashSet<string>();
            this.longTermCommands.Add("start");
            this.longTermCommands.Add("join");
        }

        public void Start()
        {
            // Handle first command
            NetworkStream stream = this.client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            Console.Write("Enter your command: ");
            string command = Console.ReadLine();
            writer.Write(command);
            stream.Flush();
            Console.WriteLine("Data has been sent to server");

            string answer = reader.ReadString();
            Console.WriteLine("Result = {0}", answer);

            // Check for long term connection
            if (this.longTermCommands.Contains(command.Split(' ')[0]))
            {
                this.isWaitingForAnswer = true;
                Listen();
                Talk();
            }

            stream.Dispose();
            this.client.Close();
        }

        private void Listen()
        {
            NetworkStream stream = this.client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            new Task(() =>
            {
                while (this.isConnected)
                {
                    // Check if has something to read from stream
                    if (stream.DataAvailable)
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

        private void Talk()
        {
            NetworkStream stream = this.client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            string command = null;
            int c;
            while (this.isConnected)
            {
                if ((c = Console.Read()) != -1 && this.isConnected)
                {
                    command = Console.ReadLine();
                    StringBuilder builder = new StringBuilder();
                    builder.Append((char) c);
                    builder.Append(command);
                    command = builder.ToString();

                    writer.Write(command);
                    Console.WriteLine("data sent to server");
                    stream.Flush();
                }
            }
        }
    }
}