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

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5556);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            // Send data to server
            string command = null;
            string result = null;
            do
            {
                Console.WriteLine("enter commmand");
                command = Console.ReadLine();
                writer.Write(command);
                Console.WriteLine("data sent to server");
                // Get result from server
                result = reader.ReadString();
                Console.WriteLine("Result = {0}", result);
                stream.Flush();
                command = command.Split(' ')[0];
            } while (!command.Equals("close"));
            stream.Dispose();
            client.Close();
        }
    }
}
