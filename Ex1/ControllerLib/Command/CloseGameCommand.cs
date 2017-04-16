using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using ServerProject.ModelLib;

namespace ServerProject.ControllerLib.Command
{
    /// <summary>
    /// Execute close-game command.
    /// </summary>
    public class CloseGameCommand : Command
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Model of server.</param>
        public CloseGameCommand(IModel model) : base(model)
        {
        }

        public override ForwardMessageEventArgs Execute(string[] args, TcpClient client = null)
        {
            string nameOfGame = args[0];
            // The addressee of the messgae is the competitor of the given client.
            ForwardMessageEventArgs eventArgs =
                new ForwardMessageEventArgs(this.Model.GetCompetitorOf(client), new JObject().ToString());
            this.Model.Close(nameOfGame);
            return eventArgs;
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