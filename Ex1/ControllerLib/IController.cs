using System.Net.Sockets;

namespace Ex1.ControllerLib
{
    public interface IController
    {
        string ExecuteCommand(string commandLine, TcpClient client);
    }
}
