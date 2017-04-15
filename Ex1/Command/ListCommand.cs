using System.Net.Sockets;
using Newtonsoft.Json;
using ServerProject.ControllerLib;
using ServerProject.ModelLib;

namespace ServerProject.Command
{
    public class ListCommand : Command
    {
        public ListCommand(IModel model) : base(model)
        {
        }

        public override ForwardMessageEventArgs Execute(string[] args, TcpClient client = null)
        {
            string[] joinableGames = this.Model.GetAvailableGames();
            // JsonConvert.SerializeObject(arr) will produce something of the form "[arr[0], arr[1], ...]"
            return new ForwardMessageEventArgs(client, JsonConvert.SerializeObject(joinableGames));
        }

        public override Checksum Check(string[] args)
        {
            Checksum checksum = new Checksum();
            if (args.Length != 0)
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