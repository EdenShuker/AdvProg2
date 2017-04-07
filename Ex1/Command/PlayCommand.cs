using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Ex1.Command
{
    public class PlayCommand : Command
    {
        public PlayCommand(IModel model) : base(model)
        {
        }

        public override string Execute(string[] args, TcpClient client = null)
        {
            string move = args[0];
            string nameOfGame = this.Model.Play(move, client);
            JObject moveObj = new JObject();
            moveObj["Name"] = nameOfGame;
            moveObj["Direction"] = move;
            return moveObj.ToString();
        }
    }
}