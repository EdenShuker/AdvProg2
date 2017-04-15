using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject.ControllerLib
{
    public class ForwardMessageEventArgs : EventArgs
    {
        public TcpClient Addressee { get; private set; }
        public string Message { get; private set; }

        public ForwardMessageEventArgs(TcpClient client, string message)
        {
            Addressee = client;
            Message = message;
        }
    }
}