using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            TcpListener listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for client connections...");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected");
            string str = null;
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            using (stream)
            using (reader)
            using (writer)
            {
                Console.WriteLine("Waiting for a command");
                str = reader.ReadString();
                Console.WriteLine("{0}", str);
                writer.Write("hello");
            }
            using (stream)
            using (reader)
            {
                Console.WriteLine("Waiting for a command");
                str = reader.ReadString();
                Console.WriteLine("{0}", str);
            }

            client.Close();
            listener.Stop();
        }
    }
}
