﻿using System.IO;
using System.Net;
using System.Net.Sockets;

namespace MazeMVVM.ModelLib.Communication
{
    /// <summary>
    /// Implementation of client.
    /// </summary>
    public class Client : IClient
    {
        /// <summary>
        /// Tcp client object to communicate.
        /// </summary>
        private TcpClient client;

        /// <summary>
        /// Represent if the current client is connected.
        /// </summary>
        private bool isConnected;

        /// <summary>
        /// Connect to server.
        /// </summary>
        /// <param name="ip"> ip address</param>
        /// <param name="port"> port number </param>
        public void Connect(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.client = new TcpClient();
            client.Connect(ep);
            this.isConnected = true;
        }

        /// <summary>
        /// Disconnect from Server.
        /// </summary>
        public void Disconnect()
        {
            this.isConnected = false;
            this.client.GetStream().Dispose();
            this.client.Close();
        }

        /// <summary>
        /// Read a string from stream.
        /// </summary>
        /// <returns> the string recieved </returns>
        public string Read()
        {
            NetworkStream stream = this.client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            return reader.ReadString();
        }

        /// <summary>
        /// Write a commant to stream.
        /// </summary>
        /// <param name="command"> the string to be send </param>
        public void Write(string command)
        {
            NetworkStream stream = this.client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(command);
        }

        /// <summary>
        /// Check if the current client is connected.
        /// </summary>
        /// <returns>true if it is connected, false otherwise</returns>
        bool IClient.IsConnected()
        {
            return this.isConnected;
        }
    }
}