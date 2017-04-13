using System.Net.Sockets;
using MazeLib;
using SearchAlgorithmsLib;

namespace ServerProject.ModelLib
{
    public interface IModel
    {
        Maze GenerateMaze(string nameOfGame, int rows, int cols);

        Solution<Position> SolveMaze(string nameOfGame, int algorithm);

        Maze StartGame(string nameOfGame, int rows, int cols, TcpClient player);

        string[] GetAvailableGames();

        Maze JoinTo(string nameOfGame, TcpClient player);

        string Play(string direction, TcpClient player);

        void Close(string nameOfGame);

        bool IsGameBegun(string nameOfGame);

        bool IsClientInGame(TcpClient client);

        TcpClient GetCompetitorOf(TcpClient player);
    }
}