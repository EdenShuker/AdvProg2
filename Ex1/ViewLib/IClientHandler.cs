using System.Net.Sockets;

namespace Ex1.ViewLib
{
    interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
