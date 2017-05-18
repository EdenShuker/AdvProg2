using System.ComponentModel;
using MazeLib;

namespace MazeMVVM.ModelLib.Player
{
    public interface IPlayerModel : INotifyPropertyChanged
    {
        Position Pos { get; set; }

        Maze Maze { get; set; }

        void Connect(string ip, int port);

        void Disconnect();

        void Move(Direction direction);
    }
}