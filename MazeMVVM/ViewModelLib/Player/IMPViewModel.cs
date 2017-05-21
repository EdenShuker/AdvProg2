using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeMVVM.ViewLib.Controls;

namespace MazeMVVM.ViewModelLib.Player
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
