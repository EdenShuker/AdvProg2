using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using Ex1.MoveEventLib;

namespace Ex1.ModelLib
{
    public class Model : IModel
    {
        private Dictionary<string, MultiPlayerGame> availablesMPGames;
        private Dictionary<string, MultiPlayerGame> unAvailablesMPGames;
        private Dictionary<string, SinglePlayerGame> SPGames;
        private Dictionary<TcpClient, MultiPlayerGame> playerToGame;

        public Model()
        {
            availablesMPGames = new Dictionary<string, MultiPlayerGame>();
            unAvailablesMPGames = new Dictionary<string, MultiPlayerGame>();
            SPGames = new Dictionary<string, SinglePlayerGame>();
            playerToGame = new Dictionary<TcpClient, MultiPlayerGame>();
        }

        public Maze GenerateMaze(string nameOfGame, int rows, int cols)
        {
            IMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = nameOfGame;
            SPGames.Add(nameOfGame, new SinglePlayerGame(maze));
            return maze;
        }

        public Solution<Position> SolveMaze(string nameOfGame, int algorithm)
        {
            MazeInfo mazeInfo = null;
            if (SPGames.ContainsKey(nameOfGame))
            {
                mazeInfo = SPGames[nameOfGame].MazeInfo;
            }
            else if (availablesMPGames.ContainsKey(nameOfGame))
            {
                mazeInfo = availablesMPGames[nameOfGame].MazeInfo;
            }
            else if (unAvailablesMPGames.ContainsKey(nameOfGame))
            {
                mazeInfo = unAvailablesMPGames[nameOfGame].MazeInfo;
            }
            if (mazeInfo.Solution == null)
            {
                ISearchable<Position> searchableMaze = new SearchableMaze(mazeInfo.Maze);
                ISearcher<Position> searcher = SearcherFactory.Create(algorithm);
                mazeInfo.Solution = searcher.Search(searchableMaze);
            }
            return mazeInfo.Solution;
        }

        public Maze StartGame(string nameOfGame, int rows, int cols, TcpClient client)
        {
            IMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(rows, cols);
            maze.Name = nameOfGame;
            MultiPlayerGame mpGame = new MultiPlayerGame(maze, client, maze.InitialPos);
            this.availablesMPGames.Add(nameOfGame, mpGame);
            this.playerToGame.Add(client, mpGame);
            return maze;
        }

        public string[] GetAvailableGames()
        {
            return availablesMPGames.Keys.ToArray();
        }

        public Maze JoinTo(string nameOfGame, TcpClient player)
        {
            MultiPlayerGame game = availablesMPGames[nameOfGame];
            Maze maze = game.Maze;
            game.Guest = new PlayerInfo(player, maze.InitialPos);
            availablesMPGames.Remove(nameOfGame);
            unAvailablesMPGames.Add(nameOfGame, game);
            playerToGame.Add(player, game);
            return maze;
        }

        // return - name of game that 'player' takes.
        public string Play(string direction, TcpClient player)
        {
            MultiPlayerGame game = playerToGame[player];
            PlayerInfo playerInfo = game.GetPlayer(player);
            // Update the player location
            bool validMove = playerInfo.Move(game.Maze, direction);
  
            if (!validMove)
            {
                return "Invalid Direction";
            }
            game.Play(direction);
            return game.Maze.Name;
        }

        public void Close(string nameOfGame)
        {
            MultiPlayerGame game = null;
            if (unAvailablesMPGames.ContainsKey(nameOfGame))
            {
                game = unAvailablesMPGames[nameOfGame];
                unAvailablesMPGames.Remove(nameOfGame);
               playerToGame.Remove(game.Guest.Player);
            }
            else
            {
                // available games
                game = availablesMPGames[nameOfGame];
                availablesMPGames.Remove(nameOfGame);
            }
            playerToGame.Remove(game.Host.Player);
        }

        public bool IsGameBegun(string nameOfGame)
        {
            return unAvailablesMPGames.ContainsKey(nameOfGame);
        }


        public bool IsClientInGame(TcpClient client)
        {
            return playerToGame.ContainsKey(client);
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

        private class SinglePlayerGame
        {
            public MazeInfo MazeInfo { get; set; }
            public Maze Maze => MazeInfo.Maze;
            public Solution<Position> Solution => MazeInfo.Solution;

            public SinglePlayerGame(Maze maze)
            {
                this.MazeInfo = new MazeInfo(maze);
            }
        }

        private class MultiPlayerGame : SinglePlayerGame
        {
            public PlayerInfo Host { get; set; }
            public PlayerInfo Guest { get; set; }
            private event EventHandler<PlayerMovedEventArgs> PlayerMoved;

            public MultiPlayerGame(Maze maze, TcpClient player, Position location) : base(maze)
            {
                this.Host = new PlayerInfo(player, location);
                PlayerMoved += MazeBoard_PlayerMoved;
            }

            private void MazeBoard_PlayerMoved(object sender, PlayerMovedEventArgs e)
            {
                Console.WriteLine($"Player moved in direction: {e.Direction}");
            }

            public void Play(string direction)
            {
                PlayerMoved?.Invoke(this, new PlayerMovedEventArgs(direction));
            }

            public PlayerInfo GetPlayer(TcpClient player)
            {
                if (Host.Player == player)
                {
                    return Host;
                }
                if (Guest.Player == player)
                {
                    return Guest;
                }
                return null;
            }
        }


        // Holds Info about player
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
            public bool Move(Maze maze, string direction)
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
    }
}