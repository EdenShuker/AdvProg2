using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Ex1.ModelLib;
using Newtonsoft.Json;

namespace Ex1.Command
{
    public class ListCommand : Command
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