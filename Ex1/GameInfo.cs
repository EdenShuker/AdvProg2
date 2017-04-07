using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace Ex1
{
    public class GameInfo
    {
        public string nameOfMaze { get; }
        public Solution<Position> solution { get; set; }
        public TcpClient player1 { get; set; }
        public TcpClient player2 { get; set; }

        public GameInfo(string mazeName, TcpClient player)
        {
            nameOfMaze = mazeName;
            solution = null;
            player1 = player;
            player2 = null;
        }
    }
}
