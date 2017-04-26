using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeMVVM.ModelLib
{
    class MultiPlayer : Player
    {
        private TcpClient client;
        private bool isConnected;


        public MultiPlayer(Position position) : base(position)
        {

        }


        public override void Start()
        {
            throw new NotImplementedException();
        }


        public override void RestartGame()
        {
            throw new NotImplementedException();
        }

    }
}
