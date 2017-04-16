using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServerProject.ViewLib
{
    /// <summary>
    /// Server that can accept multiple clients.
    /// </summary>
    class Server
    {
        /// <summary>
        /// The port that the server listens to.
        /// </summary>
        private int port;

        /// <summary>
        /// The object that transport the messages.
        /// </summary>
        private TcpListener listener;

        /// <summary>
        /// View.
        /// </summary>
        private IClientHandler clientHandler;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="port">The port that the server will listen to.</param>
        /// <param name="ch">The server's view.</param>
        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.clientHandler = ch;
        }

        public void Start()
        {
            // Start listen to clients.
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        // Handle single client.
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        clientHandler.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        // End of operation.
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }

        /// <summary>
        /// Stop the listener from listening.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
    }
}