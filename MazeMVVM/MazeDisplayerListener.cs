using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeMVVM.ViewLib;
using MazeMVVM.ViewLib.Controls;
using MazeMVVM.ViewModelLib;

namespace MazeMVVM
{
    public class MazeDisplayerListener
    {
        private MazeDisplayer mazeDisplayer;
        private SPViewModel viewModel;

        public MazeDisplayerListener(MazeDisplayer mazeDisplayer1, SPViewModel viewModel1)
        {
            this.mazeDisplayer = mazeDisplayer1;
            this.viewModel = viewModel1;
            mazeDisplayer.PlayerMoved += MazeDisplayer_PlayerMoved;
        }

        private void MazeDisplayer_PlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            this.viewModel.Move(e.Direction);
            this.mazeDisplayer.CurrentPosition = this.viewModel.VM_Pos;
            this.mazeDisplayer.UpdatePlayerLocation();
        }
    }
}