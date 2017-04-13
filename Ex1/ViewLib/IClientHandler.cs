using System.Net.Sockets;

namespace ServerProject.ViewLib
{
    interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
