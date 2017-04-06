using System.Net.Sockets;

namespace Ex1.Command
{
    interface ICommand
    {
        string Execute(string[] args, TcpClient client = null);
    }
}
