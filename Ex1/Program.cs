using System;
using ServerProject.ControllerLib;
using ServerProject.ViewLib;

namespace ServerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            ClientHandler clientHadler = new ClientHandler(controller);
            Server server = new Server(8000, clientHadler);
            server.Start();
            Console.ReadLine();
            server.Stop();
        }
    }
}