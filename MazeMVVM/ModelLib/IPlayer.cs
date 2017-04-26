using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeMVVM.ModelLib
{
    interface IPlayer: INotifyPropertyChanged
    {
        void Start();
        void Move(Direction direction);
        string SolveMaze();
        void RestartGame();
    }
}
