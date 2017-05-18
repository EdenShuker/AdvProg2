using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMVVM.ModelLib.Player
{
    public interface ISinglePlayerModel : IPlayerModel
    {
        string SolveMaze();

        void RestartGame();
    }
}