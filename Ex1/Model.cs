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
        private Dictionary<string, GameInfo> availablesGames;
        private Dictionary<string, GameInfo> unAvailablesGames;
        private Dictionary<string, MazeInfo> mazes;

        public Model()
        {
            availablesGames = new Dictionary<string, GameInfo>();
            unAvailablesGames = new Dictionary<string, GameInfo>();
            mazes = new Dictionary<string, MazeInfo>();
        }

        public void Close(string nameOfGame)
        {
            GameInfo game = games[nameOfGame];
            // close players socket and renove tha game.
            game.player1.Close();
            game.player2.Close();
            games.Remove(nameOfGame);
        }

        public Maze GenerateMaze(string nameOfMaze, int rows, int cols)
        {
            Maze maze = new Maze(rows, cols);
            mazes.Add(nameOfMaze, new MazeInfo(maze));
            return maze;
        }

        public string[] GetAvailableGames()
        {
            string[] listOfAvailableGames = availablesGames.Keys.ToArray();
            return listOfAvailableGames;
        }



        public Maze JoinTo(string nameOfGame, TcpClient player)
        {
            GameInfo game = availablesGames[nameOfGame];
            if (game.player1 != null)
            {
                game.player2 = player;
                //TODO: send both players a message
            }
            else
            {
                game.player1 = player;
            }
            return mazes[game.nameOfMaze].maze;
        }



        public string Play(string move, TcpClient player)
        {
            throw new NotImplementedException();
        }



        public Solution<Position> SolveMaze(string nameOfMaze, int algorithm)
        {
            // means we don't have the solution
            if (mazes[nameOfMaze].solution == null)
            {
                ISearchable<Position> searchableMaze = new SearchableMaze(mazes[nameOfMaze].maze);
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
                mazes[nameOfMaze].solution = searcher.Search(searchableMaze);
            }
            return mazes[nameOfMaze].solution;
        }



        public Maze StartGame(string nameOfGame, int rows, int cols, TcpClient client)
        {
            string mazeStr = null;
            Maze maze = null;
            foreach (string nameOfMaze in mazes.Keys) {
                maze = mazes[nameOfMaze].maze;
                if (maze.Rows == rows && maze.Cols == cols)
                {
                    mazeStr = nameOfMaze;
                    break;
                }
            }
            availablesGames.Add(nameOfGame, new GameInfo(mazeStr, client, maze.InitialPos));
            return maze;
        }


        private class GameInfo
        {
            public string nameOfMaze { get; set; }
            public TcpClient player1 { get; set; }
            public Position player1Location { get; set; }
            public TcpClient player2 { get; set; }
            public Position player2Location { get; set; }

            public GameInfo(string mazeName, TcpClient player, Position location)
            {
                nameOfMaze = mazeName;
                player1 = player;
                player1Location = location;
            }
            
        }

        private class MazeInfo
        {
            public Maze maze { get; set; }
            public Solution<Position> solution { get; set; }

            public MazeInfo(Maze maze)
            {
                this.maze = maze;
                solution = null;
            }
        }
    }

    
}
