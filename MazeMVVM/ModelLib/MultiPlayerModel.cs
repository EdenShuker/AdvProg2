using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMVVM.ModelLib
{
    public class MultiPlayerModel : PlayerModel
    {
        public Position PosOtherPlayer
        {
            get
            {
                return PosOtherPlayer;
            }
            set
            {
                PosOtherPlayer = value;
                NotifyPropertyChanged("PosOtherPlayer");
            }
        }

        // for starting a game
        public MultiPlayerModel(IClient client, string nameOfGame, int rows, int cols) : base(client) {
            this.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.write($"start {nameOfGame} {rows} {cols}");
            // at the moment when it get the game it means the game started.
            this.Maze = Maze.FromJSON(this.Client.read());
            this.Pos = this.PosOtherPlayer = this.Maze.InitialPos;
            Start();
        }

        // for joining a game
        public MultiPlayerModel(IClient client, string nameOfGame) : base(client) {
            this.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.write($"join {nameOfGame}");
            this.Maze = Maze.FromJSON(this.Client.read());
            this.Pos = this.PosOtherPlayer = this.Maze.InitialPos;
            Start();
        }





        /// <summary>
        /// In multiPlayer game we must update other player we made a move.
        /// </summary>
        /// <param name="direction"></param>
        override public void Move(Direction direction)
        {
            base.Move(direction);
            this.Client.write("move " + direction);
        }

    


        private void Start()
        {
            new Task(() =>
            {
                string endMsg = new JObject().ToString();
                while (Client.IsConnected())
                {
                    string answer = Client.read();
                    if (answer.Equals(endMsg))
                    {
                        // End of connection.
                        Client.disconnect();
                        break;
                    }
                    JObject msg = JObject.Parse(answer);
                    if (msg["Direction"] != null)
                    {
                        // change other player position.
                    }
                    // sleep
                }
            }).Start();
        }



    }
}
