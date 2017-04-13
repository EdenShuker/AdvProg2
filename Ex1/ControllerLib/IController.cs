using System.Net.Sockets;

namespace ServerProject.ControllerLib
{
    public interface IController
    {
        AnswerInfo ExecuteCommand(string commandLine, TcpClient client);
    }
}
