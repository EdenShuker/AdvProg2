using System.Net.Sockets;

namespace ServerProject.ViewLib
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);
        void WriteMessageTo(TcpClient client, string message);
    }
}
