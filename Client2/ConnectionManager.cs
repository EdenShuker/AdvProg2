using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client2
{
    public class ConnectionManager
    {
        public bool IsConnected { get; set; }
        public bool IsWaitingForAnswer { get; set; }
        public bool CanWrite { get; set; }

        delegate void ChangeConditionState();

        private Dictionary<string, ChangeConditionState> commandToChangeConditionDelegate;

        public ConnectionManager()
        {
            IsConnected = true;
            IsWaitingForAnswer = false;
            CanWrite = true;
            this.commandToChangeConditionDelegate = new Dictionary<string, ChangeConditionState>();
            ChangeConditionState waitForAnswerAndCanNotWrite = new ChangeConditionState(WaitForAnswer);
        }

        private void Disconnect()
        {
            IsConnected = false;
        }

        private void WaitForAnswer()
        {
            IsWaitingForAnswer = true;
        }

        private void DoNotWaitForAnswer()
        {
            IsWaitingForAnswer = false;
        }

        private void CanSendMessages()
        {
            CanWrite = true;
        }

        private void CanNotSendMessages()
        {
            CanWrite = false;
        }
    }
}