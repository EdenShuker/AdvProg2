using System.Net.Sockets;

namespace Ex1.View
{
    interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
