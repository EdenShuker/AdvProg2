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
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                // Send data to server
                writer.WriteLine("generate maze1 5 5");
                Console.WriteLine("data sent to server");
                // Get result from server
                string result = reader.ReadLine();
                Console.WriteLine("Result = {0}", result);
            }
            Console.ReadLine();
            client.Close();
        }
    }
}
