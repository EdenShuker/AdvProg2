using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeMVVM.ModelLib
{
    public interface IPlayerModel: INotifyPropertyChanged
    {
        void Connect(string ip, int port);
        void Disconnect();
        void Start();
        void Move(Direction direction);
        void SolveMaze();
        void RestartGame();

        // Properties
        Position Pos { get; set; }
        Maze Maze { get; set; }
    }
}
