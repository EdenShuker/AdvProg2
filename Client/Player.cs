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