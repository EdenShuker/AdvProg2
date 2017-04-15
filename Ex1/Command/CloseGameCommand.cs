using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using ServerProject.ModelLib;

namespace ServerProject.Command
{
    /// <summary>
    /// Execute close game command.
    /// </summary>
    public class CloseGameCommand : Command
    {

        public CloseGameCommand(IModel model) : base(model)
        {
        }

        public override string Execute(string[] args, TcpClient client = null)
        {
            string nameOfGame = args[0];
            this.Model.Close(nameOfGame);
            return new JObject().ToString();
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