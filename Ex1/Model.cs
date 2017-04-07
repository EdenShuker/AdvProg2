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
            GameInfo game = null;
            if (unAvailablesGames.ContainsKey(nameOfGame))
            {
                game = unAvailablesGames[nameOfGame];
                unAvailablesGames.Remove(nameOfGame);
                game.PlayerInfo2.Player.Close();
            }
            else
            {
                game = availablesGames[nameOfGame];
                availablesGames.Remove(nameOfGame);
            }
            game.PlayerInfo1.Player.Close();
        }

        public Maze GenerateMaze(string nameOfMaze, int rows, int cols)
        {
            Maze maze = new Maze(rows, cols);
            mazes.Add(nameOfMaze, new MazeInfo(maze));
            return maze;
        }

        public string[] GetAvailableGames()
        {
            return availablesGames.Keys.ToArray();
        }


        public Maze JoinTo(string nameOfGame, TcpClient player)
        {
            GameInfo game = availablesGames[nameOfGame];
            Maze maze = mazes[game.NameOfMaze].Maze;
            game.PlayerInfo2 = new PlayerInfo(player, maze.InitialPos);
            availablesGames.Remove(nameOfGame);
            unAvailablesGames.Add(nameOfGame, game);
            return maze;
        }


        public string Play(string move, TcpClient player)
        {
            throw new NotImplementedException();
        }


        public Solution<Position> SolveMaze(string nameOfMaze, int algorithm)
        {
            // means we don't have the solution
            if (mazes[nameOfMaze].Solution == null)
            {
                ISearchable<Position> searchableMaze = new SearchableMaze(mazes[nameOfMaze].Maze);
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
                mazes[nameOfMaze].Solution = searcher.Search(searchableMaze);
            }
            return mazes[nameOfMaze].Solution;
        }

        public bool IsGameBegun(string nameOfGame)
        {
            return unAvailablesGames.ContainsKey(nameOfGame);
        }

        public Maze StartGame(string nameOfGame, int rows, int cols, TcpClient client)
        {
            string mazeStr = null;
            Maze maze = null;
            foreach (string nameOfMaze in mazes.Keys)
            {
                maze = mazes[nameOfMaze].Maze;
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
            public string NameOfMaze { get; set; }
            public PlayerInfo PlayerInfo1 { get; set; }
            public PlayerInfo PlayerInfo2 { get; set; }
            public Position Player2Location { get; set; }

            public GameInfo(string mazeName, TcpClient player, Position location)
            {
                NameOfMaze = mazeName;
                this.PlayerInfo1 = new PlayerInfo(player, location);
            }

            public PlayerInfo GetPlayer(TcpClient player)
            {
                if (PlayerInfo1.Player == player)
                {
                    return PlayerInfo1;
                }
                else if (PlayerInfo2.Player == player)
                {
                    return PlayerInfo2;
                }
                return null;
            }
        }

        private class PlayerInfo
        {
            public TcpClient Player { get; set; }
            public Position Location { get; set; }

            public PlayerInfo(TcpClient player, Position location)
            {
                this.Player = player;
                this.Location = location;
            }
        }

        private class MazeInfo
        {
            public Maze Maze { get; set; }
            public Solution<Position> Solution { get; set; }

            public MazeInfo(Maze maze)
            {
                this.Maze = maze;
                Solution = null;
            }
        }
    }
}