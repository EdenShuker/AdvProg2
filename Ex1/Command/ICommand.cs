using System.Net.Sockets;

namespace Ex1.Command
{
    public interface ICommand
    {
        string Execute(string[] args, TcpClient client = null);
    }
}
