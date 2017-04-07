using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.Command
{
    public abstract class Command : ICommand
    {
        protected IModel Model;

        protected Command(IModel model)
        {
            this.Model = model;
        }

        public abstract string Execute(string[] args, TcpClient client = null);
    }
}