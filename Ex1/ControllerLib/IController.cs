using System;
using System.Net.Sockets;

namespace ServerProject.ControllerLib
{
    public interface IController
    {
        event EventHandler<ForwardMessageEventArgs> ForwardMessageEvent;

        void ExecuteCommand(string commandLine, TcpClient client);

        bool ProceedConnectionWith(TcpClient client);
    }
}
