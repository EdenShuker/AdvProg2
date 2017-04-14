using System.Net.Sockets;
using Newtonsoft.Json;
using ServerProject.ModelLib;

namespace ServerProject.Command
{
    public class ListCommand : ServerProject.Command.Command
    {
        public ListCommand(IModel model) : base(model)
        {
        }

        public override string Execute(string[] args, TcpClient client = null)
        {
            string[] joinableGames = this.Model.GetAvailableGames();
            // JsonConvert.SerializeObject(arr) will produce something of the form "[arr[0], arr[1], ...]"
            return JsonConvert.SerializeObject(joinableGames);
        }
    }
}