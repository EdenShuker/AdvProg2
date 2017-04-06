using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Ex1
{
    class ClientHandler : IClientHandler
    {
        public void HandleClient(TcpClient client)
        {
            Task task = new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    Console.WriteLine("Waiting for a number");
                    int num = reader.ReadInt32();
                    Console.WriteLine("Number accepted: " + num);
                    num *= 2;
                    writer.Write(num);
                } 
            });
            task.Start();
            task.Wait();
        }
    }
}
