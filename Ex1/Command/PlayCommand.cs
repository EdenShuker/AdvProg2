using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using ServerProject.ModelLib;

namespace ServerProject.Command
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

        public override Checksum Check(string[] args)
        {
            Checksum checksum = new Checksum();
            if (args.Length != 1)
            {
                checksum.Valid = false;
                checksum.ErrorMsg = "Invalid number of arguments";
            }
            else
            {
                checksum.Valid = true;
            }
            return checksum;
        }
    }
}