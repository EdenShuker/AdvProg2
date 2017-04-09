using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
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

        public Maze GenerateMaze(string nameOfMaze, int rows, int cols)
        {
            IMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = nameOfMaze;
            mazes.Add(nameOfMaze, new MazeInfo(maze));
            return maze;
        }

        public Solution<Position> SolveMaze(string nameOfMaze, int algorithm)
        {
            // means we don't have the solution
            if (mazes[nameOfMaze].Solution == null)
            {
                ISearchable<Position> searchableMaze = new SearchableMaze(mazes[nameOfMaze].Maze);
                ISearcher<Position> searcher = SearcherFactory.Create(algorithm);
                mazes[nameOfMaze].Solution = searcher.Search(searchableMaze);
            }
            return mazes[nameOfMaze].Solution;
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

        // return - name of game that 'player' takes.
        public string Play(string direction, TcpClient player)
        {
            PlayerInfo playerInfo = null;
            GameInfo gameInfo = null;
            string nameOfGame = null;
            // find name of game and game-info of 'player'
            foreach (string nameOfcurrGame in this.unAvailablesGames.Keys)
            {
                gameInfo = this.unAvailablesGames[nameOfcurrGame];
                playerInfo = gameInfo.GetPlayer(player);
                if (player != null)
                {
                    nameOfGame = nameOfcurrGame;
                    break;
                }
            }
            // Update the player location
            bool validMove = playerInfo.move(mazes[gameInfo.NameOfMaze].Maze, direction);
            if (!validMove)
            {
                return "Invalid Direction";
            }
            return nameOfGame;
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

        public bool IsGameBegun(string nameOfGame)
        {
            return unAvailablesGames.ContainsKey(nameOfGame);
        }


        public bool IsClientInGame(TcpClient client)
        {
            foreach(GameInfo game in unAvailablesGames.Values)
            {
                if (game.GetPlayer(client) != null)
                {
                    return true;
                }
            }
            return false;
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

            // return true for valid move, false otherwise.
            public bool move(Maze maze, string direction)
            {
                int currentRow = this.Location.Row;
                int currentCol = this.Location.Col;
                if (direction.Equals("right") && currentCol < maze.Cols - 1 &&
                    maze[currentRow, currentCol + 1] == CellType.Free)
                {
                    this.Location = new Position(currentRow, currentCol + 1);
                }
                else if (direction.Equals("left") && currentCol > 0 &&
                         maze[currentRow, currentCol - 1] == CellType.Free)
                {
                    this.Location = new Position(currentRow, currentCol - 1);
                }
                else if (direction.Equals("up") && currentRow > 0 &&
                         maze[currentRow - 1, currentCol] == CellType.Free)
                {
                    this.Location = new Position(currentRow - 1, currentCol);
                }
                else if (direction.Equals("down") && currentRow < maze.Rows - 1 &&
                         maze[currentRow + 1, currentCol] == CellType.Free)
                {
                    this.Location = new Position(currentRow + 1, currentCol);
                }
                else
                {
                    // Invalid direction.
                    return false;
                }
                return true;
            }
        }

        private class MazeInfo
        {
            public Maze Maze { get; set; }
            public Solution<Position> Solution { get; set; }

            public MazeInfo(Maze maze)
            {
                this.Maze = maze;
                this.Solution = null;
            }
        }
    }
}