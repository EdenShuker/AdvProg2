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
        public Maze maze { get; }
        public Solution<Position> solution { get; set; }
        public TcpClient player1 { get; set; }
        public TcpClient player2 { get; set; }

        public GameInfo(Maze maze)
        {
            this.maze = maze;
            solution = null;
            player1 = player2 = null;
        }

        public GameInfo(Maze maze, TcpClient player)
        {
            this.maze = maze;
            solution = null;
            player1 = player;
            player2 = null;
        }

        



    }
}
