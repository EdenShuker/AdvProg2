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
                Console.WriteLine("performing client handler task");
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = reader.ReadLine();
                    Console.WriteLine("Got command: {0}", commandLine);
                    writer.Write("hello");
                    //string result = ExecuteCommand(commandLine, client);
                    //writer.Write(result);
                }
                client.Close();
            });
            task.Start();
        }
    }

}
