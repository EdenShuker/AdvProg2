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
    public class Model : IModel
    {
        private Dictionary<string, GameInfo> games;

    
        public void Close(string nameOfGame)
        {
            GameInfo game = games[nameOfGame];
            // close players socket and renove tha game.
            game.player1.Close();
            game.player2.Close();
            games.Remove(nameOfGame);
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = new Maze(rows, cols);
            games.Add(name, new GameInfo(maze));
            return maze;
        }

        public string[] GetAvailableGames()
        {
            string[] availableGames = games.Keys.ToArray();
            return availableGames;
        }



        public Maze JoinTo(string nameOfGame, TcpClient player)
        {
            GameInfo game = games[nameOfGame];
            if (game.player1 != null)
            {
                game.player2 = player;
                //TODO: send both players a message
            }
            else
            {
                game.player1 = player;
            }
            return game.maze;
        }



        public string Play(string move, TcpClient player)
        {
            throw new NotImplementedException();
        }



        public Solution<Position> SolveMaze(string name, int algorithm)
        {
            // means we don't have the solution
            if (games[name].solution == null)
            {
                ISearchable<Position> searchableMaze = new SearchableMaze(games[name].maze);
                ISearcher<Position> searcher;
                // BFS algorithm
                if (algorithm == 0)
                {
                    searcher = new BFS<Position>();
                }
                // DFS algorithm
                else
                {
                    searcher = new DFS<Position>();
                }
                games[name].solution = searcher.Search(searchableMaze);
            }
            return games[name].solution;
        }



        public Maze StartGame(string nameOfGame, int rows, int cols, TcpClient client)
        {
            Maze maze = new Maze(rows, cols);
            games.Add(nameOfGame, new GameInfo(maze, client));
            return maze;
        }
    }
}
