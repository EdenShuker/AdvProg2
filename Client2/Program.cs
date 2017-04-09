using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = null;
            do
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
                TcpClient client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Send data to server
                    command = Console.ReadLine();
                    writer.Write(command);
                    Console.Write("data sent to server");
                    // Get result from server
                    string result = reader.ReadString();
                    Console.WriteLine("Result = {0}", result);
                }

                Console.ReadLine();
                client.Close();
            } while (!command.Equals("close"));
        }
    }
}
