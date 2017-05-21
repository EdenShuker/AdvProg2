using System;
using MazeLib;

namespace MazeMVVM.ModelLib.Player.MultiPlayer
{
    public interface IMultiPlayerModel : IPlayerModel
    {
        event EventHandler GameEnded;

        Position PosOtherPlayer { get; set; }

        void Start();

        void CloseGame();

    }
}
