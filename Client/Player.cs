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

        public Player(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");

            this.isConnected = true;
        }

        public void Start()
        {
            Listen();
            Talk();

            this.client.GetStream().Dispose();
            this.client.Close();
        }


        /// <summary>
        /// 
        /// </summary>
        private void Listen()
        {
            new Task(() =>
            {
                NetworkStream stream = this.client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                string endMsg = new JObject().ToString();

                while (this.isConnected)
                {
                    string answer = reader.ReadString();
                    if (answer.Equals(endMsg))
                    {
                        this.isConnected = false;
                        Console.Write("End of connection. Press any key to close program...");
                        break;
                    }
                    Console.WriteLine("Result = {0}", answer);
                }
            }).Start();
        }

        private void Talk()
        {
            NetworkStream stream = this.client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            while (this.isConnected)
            {
                Console.WriteLine("Enter your command: ");
                string command = Console.ReadLine();
                if (!this.isConnected)
                {
                    // In case that the connection was closed and the client tried to enter command
                    break;
                }
                writer.Write(command);
                stream.Flush();
                Console.WriteLine("Data has been sent to server");
            }
        }
    }
}