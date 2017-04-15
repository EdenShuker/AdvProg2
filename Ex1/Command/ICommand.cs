using System.Net.Sockets;
using ServerProject.ControllerLib;

namespace ServerProject.Command
{
    public interface ICommand
    {
        ForwardMessageEventArgs Execute(string[] args, TcpClient client = null);

        Checksum Check(string[] args);
    }

    public struct Checksum
    {
        public bool Valid;
        public string ErrorMsg;
    }
}
