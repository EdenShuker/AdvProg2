using System;
using ServerProject.ControllerLib;
using ServerProject.ViewLib;
using System.Configuration;

namespace ServerProject
{
    class Program
    {

        /// <summary>
        /// Generate the model, view and controller objects.
        /// Operate the server.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            ClientHandler clientHadler = new ClientHandler(controller);
            controller.View = clientHadler;
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            Server server = new Server(port, clientHadler);
            server.Start();
            Console.ReadLine();
            server.Stop();
        }
    }
}