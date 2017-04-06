using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class Server
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;

        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }

        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");

            Task task = new Task(() =>
            {
                Console.WriteLine("Task is being performed");
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Got new connection");
                    ch.HandleClient(client);
                    //using (NetworkStream stream = client.GetStream())
                    //using (BinaryReader reader = new BinaryReader(stream))
                    //using (BinaryWriter writer = new BinaryWriter(stream))
                    //{
                    //    Console.WriteLine("Waiting for a number");
                    //    int num = reader.ReadInt32();
                    //    Console.WriteLine("Number accepted: " + num);
                    //    num *= 2;
                    //    writer.Write(num);
                    //}
                }
                catch (SocketException)
                {
                }
            });
            task.Start();
            task.Wait();
        }

        public void Stop()
        {
            listener.Stop();
        }
    }
}
