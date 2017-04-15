using System.Net.Sockets;
using ServerProject.ControllerLib;

namespace ServerProject.ViewLib
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);

        void ForwardMessage(object sender, ForwardMessageEventArgs eventArgs);
    }
}