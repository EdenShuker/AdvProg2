using MazeLib;
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

        public MultiPlayerModel(IClient client) : base(client) { }

        public void Start()
        {
            throw new NotImplementedException();
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

        public void RestartGame()
        {
            throw new NotImplementedException();
        }

        public string SolveMaze()
        {
            throw new NotImplementedException();
        }
    }
}
