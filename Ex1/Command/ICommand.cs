using System.Net.Sockets;

namespace ServerProject.Command
{
    public interface ICommand
    {
        string Execute(string[] args, TcpClient client = null);

        Checksum Check(string[] args);
    }

    public struct Checksum
    {
        public bool Valid;
        public string ErrorMsg;
    }
}
