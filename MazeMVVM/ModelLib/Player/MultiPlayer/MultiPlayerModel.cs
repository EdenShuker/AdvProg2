using System;
using System.Threading.Tasks;
using MazeLib;
using MazeMVVM.ModelLib.Communication;
using Newtonsoft.Json.Linq;

namespace MazeMVVM.ModelLib.Player.MultiPlayer
{
    public class MultiPlayerModel : PlayerModel, IMultiPlayerModel
    {
        public event EventHandler GameEnded;

        private Position posOtherPlayer;

        public Position PosOtherPlayer
        {
            get { return posOtherPlayer; }
            set
            {
                posOtherPlayer = value;
                NotifyPropertyChanged("PosOtherPlayer");
            }
        }

        // for starting a game
        public MultiPlayerModel(IClient client, string nameOfGame, int rows, int cols) : base(client)
        {
            this.Client.Write($"start {nameOfGame} {rows} {cols}");
            // at the moment when it get the game it means the game started.
            this.InitMembers();
        }

        // for joining a game
        public MultiPlayerModel(IClient client, string nameOfGame) : base(client)
        {
            this.Client.Write($"join {nameOfGame}");
            this.InitMembers();
        }

        private void InitMembers()
        {
            this.Maze = Maze.FromJSON(this.Client.Read());
            this.Pos = this.PosOtherPlayer = this.Maze.InitialPos;
        }

        /// <summary>
        /// In multiPlayer game we must update other player we made a move.
        /// </summary>
        /// <param name="direction"></param>
        public override void Move(Direction direction)
        {
            base.Move(direction);
            this.Client.Write("play " + direction.ToString().ToLower());
        }

        public void CloseGame()
        {
            this.Client.Write($"close {this.Maze.Name}");
        }

        public void Start()
        {
            new Task(async () =>
            {
                string endMsg = new JObject().ToString();
                while (Client.IsConnected())
                {
                    string answer = Client.Read();
                    if (answer.Equals(endMsg))
                    {
                        // End of connection
                        Client.Disconnect();
                        GameEnded?.Invoke(this, null);
                        break;
                    }
                    JObject msg = JObject.Parse(answer);
                    if (msg["Direction"] != null)
                    {
                        // change other player position.
                        Direction direction = ParseDirection(msg["Direction"].ToString());
                        MoveOtherPlayer(direction);
                    }
                    // sleep
                    await Task.Delay(500);
                }
            }).Start();
        }


        private void MoveOtherPlayer(Direction direction)
        {
            PosOtherPlayer = CalcPosition(direction, PosOtherPlayer.Row, PosOtherPlayer.Col);
        }


        private Direction ParseDirection(string str)
        {
            Direction direction;
            if (str == "right")
            {
                direction = Direction.Right;
            }
            else if (str == "left")
            {
                direction = Direction.Left;
            }
            else if (str == "up")
            {
                direction = Direction.Up;
            }
            else if (str == "down")
            {
                direction = Direction.Down;
            }
            else
            {
                direction = Direction.Unknown;
            }
            return direction;
        }
    }
}