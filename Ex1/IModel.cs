using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    public interface IModel
    {
        Maze GenerateMaze(string nameOfMaze, int rows, int cols);

        Solution<Position> SolveMaze(string nameOfMaze, int algorithm);

        Maze StartGame(string nameOfGame, int rows, int cols, TcpClient player);

        string[] GetAvailableGames();

        Maze JoinTo(string nameOfGame, TcpClient player);

        string Play(string move, TcpClient player);

        void Close(string nameOfGame);
    }
}