using System.Net.Sockets;

namespace Ex1.Controller
{
    public interface IController
    {
        string ExecuteCommand(string commandLine, TcpClient client);
    }
}
