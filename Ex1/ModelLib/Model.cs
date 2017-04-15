using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using MazeGeneratorLib;
using MazeLib;
using Mission1;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;


namespace ServerProject.ModelLib
{
    /// <summary>
    /// This class responsible of all tha logic of the game.
    /// </summary>
    public class Model : IModel
    {
        private Dictionary<string, MultiPlayerGame> availablesMPGames;
        private Dictionary<string, MultiPlayerGame> unAvailablesMPGames;
        private Dictionary<string, SinglePlayerGame> SPGames;
        private Dictionary<TcpClient, MultiPlayerGame> playerToGame;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Model()
        {
            availablesMPGames = new Dictionary<string, MultiPlayerGame>();
            unAvailablesMPGames = new Dictionary<string, MultiPlayerGame>();
            SPGames = new Dictionary<string, SinglePlayerGame>();
            playerToGame = new Dictionary<TcpClient, MultiPlayerGame>();
        }


        /// <summary>
        /// Generate a maze and add it to the singlePlayer dictionary.
        /// </summary>
        /// <param name="nameOfGame"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public Maze GenerateMaze(string nameOfGame, int rows, int cols)
        {
            IMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = nameOfGame;
            // Check if the game is already exist.
            if (SPGames.ContainsKey(nameOfGame))
            {
                throw new Exception($"The game '{nameOfGame}' already exists");
            }
            else
            {
                SPGames.Add(nameOfGame, new SinglePlayerGame(maze));
            }
            return maze;
        }


        /// <summary>
        /// Return the maze info of the specified game.
        /// </summary>
        /// <param name="nameOfGame"></param>
        /// <returns> MazeInfo </returns>
        private MazeInfo GetMazeInfoOf(string nameOfGame)
        {
            MazeInfo mazeInfo = null;
            // Search in all dictionaries.
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
            return mazeInfo;
        }


        /// <summary>
        /// Return the Solution of the maze of the spacified game.
        /// </summary>
        /// <param name="nameOfGame"></param>
        /// <param name="algorithm"> integer that symbolizes the wanted algorithm</param>
        /// <returns> Solution </returns>
        public Solution<Position> SolveMaze(string nameOfGame, int algorithm)
        {
            MazeInfo mazeInfo = GetMazeInfoOf(nameOfGame);
            if (mazeInfo == null)
            {
                throw new Exception($"There is no game with the name '{nameOfGame}'");
            }

            if (mazeInfo.Solution == null)
            {
                ISearchable<Position> searchableMaze = new SearchableMaze(mazeInfo.Maze);
                ISearcher<Position> searcher = SearcherFactory.Create(algorithm);
                mazeInfo.Solution = searcher.Search(searchableMaze);
            }
            return mazeInfo.Solution;
        }


        /// <summary>
        /// Create a multiplayer game with the given name, with a maze with the 
        /// specified dimensions.
        /// </summary>
        /// <param name="nameOfGame"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="client"></param>
        /// <returns> The created maze </returns>
        public Maze StartGame(string nameOfGame, int rows, int cols, TcpClient client)
        {
            if (GetMazeInfoOf(nameOfGame) != null)
            {
                throw new Exception($"The game '{nameOfGame}' already exists");
            }
            IMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(rows, cols);
            maze.Name = nameOfGame;
            MultiPlayerGame mpGame = new MultiPlayerGame(maze, client, maze.InitialPos);
            // add the game to the suitable dictionaries.
            this.availablesMPGames.Add(nameOfGame, mpGame);
            this.playerToGame.Add(client, mpGame);
            return maze;
        }


        /// <summary>
        /// Return all the games that can be joined to.
        /// </summary>
        /// <returns> array with the names of the availables games</returns>
        public string[] GetAvailableGames()
        {
            return availablesMPGames.Keys.ToArray();
        }


        /// <summary>
        /// Add the given player to the specified game.
        /// </summary>
        /// <param name="nameOfGame"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public Maze JoinTo(string nameOfGame, TcpClient player)
        {
            if (!this.availablesMPGames.ContainsKey(nameOfGame))
            {
                throw new Exception($"There is no game with the name '{nameOfGame}'");
            }
            MultiPlayerGame game = availablesMPGames[nameOfGame];
            Maze maze = game.Maze;
            game.Guest = new PlayerInfo(player, maze.InitialPos);
            // remove the game from the available games and add it to the unavailable games.
            availablesMPGames.Remove(nameOfGame);
            unAvailablesMPGames.Add(nameOfGame, game);
            playerToGame.Add(player, game);
            return maze;
        }


        /// <summary>
        /// Change the position of the player according to the direction given.
        /// </summary>
        /// <param name="direction"> the direction to  take </param>
        /// <param name="player"></param>
        /// <returns> the name of the maze the player participate in </returns>
        public string Play(string direction, TcpClient player)
        {
            if (!playerToGame.ContainsKey(player))
            {
                throw new Exception("Player is not in a game, need to be in a game to play");
            }
            MultiPlayerGame game = playerToGame[player];
            PlayerInfo playerInfo = game.GetPlayer(player);
            // Update the player location
            bool validMove = playerInfo.Move(game.Maze, direction);
            if (!validMove)
            {
                throw new Exception($"Invalid Direction '{direction}'");
            }
            return game.Maze.Name;
        }


        /// <summary>
        /// Close the game with the specified name. Delete it from the dictionaries.
        /// </summary>
        /// <param name="nameOfGame"></param>
        public void Close(string nameOfGame)
        {
            MultiPlayerGame game = null;
            // make sure the game exist.
            if (unAvailablesMPGames.ContainsKey(nameOfGame))
            {
                game = unAvailablesMPGames[nameOfGame];
                unAvailablesMPGames.Remove(nameOfGame);
                playerToGame.Remove(game.Guest.Player);
            }
            else if (availablesMPGames.ContainsKey(nameOfGame))
            {
                game = availablesMPGames[nameOfGame];
                availablesMPGames.Remove(nameOfGame);
            }
            else
            {
                throw new Exception($"There is no game with the name '{nameOfGame}'");
            }
            playerToGame.Remove(game.Host.Player);
        }


        /// <summary>
        /// Check if the multiplayer game with the specified name has begun.
        /// </summary>
        /// <param name="nameOfGame"></param>
        /// <returns></returns>
        public bool IsGameBegun(string nameOfGame)
        {
            return unAvailablesMPGames.ContainsKey(nameOfGame);
        }


        /// <summary>
        /// Check if the client participate in a game.
        /// </summary>
        /// <param name="client"></param>
        /// <returns> true if the client is in a game, false otherwise </returns>
        public bool IsClientInGame(TcpClient client)
        {
            return playerToGame.ContainsKey(client);
        }


        /// <summary>
        /// Get the competitor of the given player in an multiplayer game. 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public TcpClient GetCompetitorOf(TcpClient player)
        {
            // the if below is ugly :(
            if (!playerToGame.ContainsKey(player))
            {
                return null;
            }
            MultiPlayerGame game = this.playerToGame[player];
            return game.GetCompetitorOf(player).Player;
        }


        /// <summary>
        /// Class holds info about a maze.
        /// </summary>
        public class MazeInfo
        {
            public Maze Maze { get; set; }
            public Solution<Position> Solution { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="maze"></param>
            public MazeInfo(Maze maze)
            {
                this.Maze = maze;
                this.Solution = null;
            }
        }
    }
}