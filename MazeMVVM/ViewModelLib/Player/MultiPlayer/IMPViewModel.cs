using System;

namespace MazeMVVM.ViewModelLib.Player.MultiPlayer
{
    public interface IMPViewModel : IPlayerVM
    {
        event EventHandler VMGameEnded;

        // Properties

        string VM_PosOtherPlayer { get; }

        // Methods

        void Start();

        void CloseGame();
    }
}
