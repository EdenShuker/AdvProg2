using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Client
{
    /// <summary>
    /// Represents a player that can send and receive messages.
    /// </summary>
    class Player : IPlayer
    {
        /// <summary>
        /// The object which used to transport the messages.
        /// </summary>
        private TcpClient client;

        /// <summary>
        /// Tells if the player is connected to some game.
        /// </summary>
        private bool isConnected;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ip">IP address that the client will use.</param>
        /// <param name="port">Port that the client will use.</param>
        public Player(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            this.isConnected = true;
        }

        /// <summary>
        /// Start send and receive messages.
        /// </summary>
        public void Start()
        {
            Listen();
            Talk();
            // Handle the stream
            this.client.GetStream().Dispose();
            this.client.Close();
        }


        /// <summary>
        /// Receive messages.
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
                        // End of connection.
                        this.isConnected = false;
                        Console.Write("End of connection. Press any key to close program...");
                        break;
                    }
                    Console.WriteLine("Result = {0}", answer);
                }
            }).Start();
        }

        /// <summary>
        /// Send messages.
        /// </summary>
        private void Talk()
        {
            NetworkStream stream = this.client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            while (this.isConnected)
            {
                // Get the command to send.
                Console.WriteLine("Enter your command: ");
                string command = Console.ReadLine();
                if (!this.isConnected)
                {
                    // In case that the connection was closed and the client tried to enter command.
                    break;
                }
                // Send the command.
                writer.Write(command);
                stream.Flush();
                Console.WriteLine("Data has been sent to server");
            }
        }
    }
}