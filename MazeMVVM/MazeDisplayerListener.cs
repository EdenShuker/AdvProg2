using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeMVVM.ViewLib;
using MazeMVVM.ViewLib.Controls;

namespace MazeMVVM
{
    public class MazeDisplayerListener
    {
        private MazeDisplayer mazeDisplayer;

        public MazeDisplayerListener(MazeDisplayer mazeDisplayer1)
        {
            this.mazeDisplayer = mazeDisplayer1;
            mazeDisplayer.PlayerMoved += MazeDisplayer_PlayerMoved;
        }

        private void MazeDisplayer_PlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            this.mazeDisplayer.UpdatePlayerLocation(e.Direction);
        }
    }
}