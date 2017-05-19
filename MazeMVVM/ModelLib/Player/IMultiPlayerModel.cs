using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMVVM.ModelLib.Player
{
    public interface IMultiPlayerModel : IPlayerModel
    {
        Position PosOtherPlayer { get; set; }

        void CloseGame();

    }
}
