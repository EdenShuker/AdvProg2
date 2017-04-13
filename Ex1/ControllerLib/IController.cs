using System.Net.Sockets;

namespace ServerProject.ControllerLib
{
    public interface IController
    {
        string ExecuteCommand(string commandLine, TcpClient client);
    }
}
