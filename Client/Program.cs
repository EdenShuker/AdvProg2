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
    class Program
    {
        static void Main(string[] args)
        {

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            // Send data to server
            string command = null;
            string result = null;
            string message = null;
            bool isClose = false;
            do
            {
                Console.WriteLine("enter commmand");
                if (!isClose)
                {
                    command = Console.ReadLine();
                    writer.Write(command);
                    Console.WriteLine("data sent to server");
                    // Get result from server
                    result = reader.ReadString();
                    Console.WriteLine("Result = {0}", result);
                }
                message = reader.ReadString();
                Console.WriteLine(message);
                if (isClose = message.Equals("close client"))
                {
                    break;
                }
                stream.Flush();
            } while (true);
            stream.Dispose();
            client.Close();
        }
    }
}
