using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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

            // Check for long term connection (means starting multiplayer game)
            if (this.longTermCommands.Contains(command.Split(' ')[0]) && !answer.Contains("Error"))
            {
                this.isWaitingForAnswer = true;
                Listen();
                Talk();
            }

            stream.Dispose();
            this.client.Close();
        }


        /// <summary>
        /// 
        /// </summary>
        private void Listen()
        {
            NetworkStream stream = this.client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            string endMsg = new JObject().ToString();
            new Task(() =>
            {
                while (this.isConnected)
                {
                    // Check if has something to read from stream
                    if (this.isWaitingForAnswer && stream.DataAvailable)
                    {
                        string answer = reader.ReadString();
                        if (answer.Equals(endMsg))
                        {
                            this.isConnected = false;
                            this.canSendMessage = false;
                            Console.Write("Competitor closed the game. Press any key to close program...");
                            break;
                        }
                        Console.WriteLine("Result = {0}", answer);
                    }
                }
            }).Start();
        }

        private void Talk()
        {
            NetworkStream stream = this.client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            while (this.isConnected)
            {
                if (this.canSendMessage)
                {
                    Console.WriteLine("Enter your command: ");
                    string command = Console.ReadLine();
                    if (!this.isConnected)
                    {
                        writer.Write("close");
                        break;
                    }
                    writer.Write(command);
                    stream.Flush();
                    Console.WriteLine("Data has been sent to server");

                    if (command.Split(' ')[0].Equals("close"))
                    {
                        this.isConnected = false;
                        this.isWaitingForAnswer = false;
                    }
                }
            }
        }
    }
}