using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeMVVM.ModelLib
{
    class SinglePlayer : Player
    {

        public SinglePlayer(Position position) : base(position)
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
