using System;
using ServerProject.ControllerLib;
using ServerProject.ViewLib;
using System.Configuration;

namespace ServerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            ClientHandler clientHadler = new ClientHandler(controller);
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            Server server = new Server(8000, clientHadler);
            server.Start();
            Console.ReadLine();
            server.Stop();
        }
    }
}