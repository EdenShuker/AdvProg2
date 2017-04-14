using System;
using System.Collections.Generic;
using System.Configuration;
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
            string ip = "127.0.0.1";
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            Player player = new Player(ip, port);
            player.Start();
        
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
