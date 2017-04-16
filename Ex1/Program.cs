using System;
using ServerProject.ControllerLib;
using ServerProject.ViewLib;
using System.Configuration;

namespace ServerProject
{
    /// <summary>
    /// Runs a server.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generate the model, view and controller objects.
        /// Operate the server.
        /// </summary>
        /// <param name="args">Aarguments of command-line.</param>
        static void Main(string[] args)
        {
            // Create the MVC.
            Controller controller = new Controller();
            IClientHandler clientHadler = new ClientHandler(controller);
            // Start the server.
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            Server server = new Server(port, clientHadler);
            server.Start();
            // End of operation.
            Console.ReadLine();
            server.Stop();
        }
    }
}