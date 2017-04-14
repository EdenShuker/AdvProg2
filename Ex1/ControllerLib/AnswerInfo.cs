using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject.ControllerLib
{
    public class AnswerInfo
    {
        public bool IsAnswerForSender { get; private set; }
        public TcpClient DestClient { get; private set; }
        public string Answer { get; set; }

        public AnswerInfo(bool isAnswerForSender, TcpClient destClient, string answer)
        {
            IsAnswerForSender = isAnswerForSender;
            DestClient = destClient;
            Answer = answer;
        }

        public AnswerInfo(bool isAnswerForSender, TcpClient destClient)
        {
            IsAnswerForSender = isAnswerForSender;
            DestClient = destClient;
        }
    }
}
