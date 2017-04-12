using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class ClientPlayer
    {
        private TcpClient client;
        private bool isConnected;

        public ClientPlayer(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.client = new TcpClient();
            this.client.Connect(ep);
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
            new Task(() =>
            {
                string closeConnectionMessage = "";
                NetworkStream stream = this.client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                while (this.isConnected)
                {
                    // check if there is something to read from the stream
                    try
                    {
                        // read answer
                        string answer = reader.ReadString();
                        if (answer.Equals(closeConnectionMessage))
                        {
                            Console.WriteLine("Competitor closed the game.");
                            this.isConnected = false;
                            break;
                        }
                        Console.WriteLine("Result = {0}", answer);

                        // check if need to preceed with the connection
                        answer = reader.ReadString();
                        this.isConnected = answer.Equals("close client");

                        stream.Flush();
                    }
                    catch (EndOfStreamException e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }).Start();
        }

        private void Talk()
        {
            NetworkStream stream = this.client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            string command = null;
            while (this.isConnected)
            {
                // Check if there is something to read from the console
                if ((command = Console.ReadLine()) != null)
                {
                    writer.Write(command);
                    if (command.Split(' ')[0].Equals("close"))
                    {
                        this.isConnected = false;
                        break;
                    }
                }
                stream.Flush();
            }
        }
    }
}