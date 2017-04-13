using System.Net.Sockets;

namespace ServerProject.Command
{
    public interface ICommand
    {
        string Execute(string[] args, TcpClient client = null);
    }
}
