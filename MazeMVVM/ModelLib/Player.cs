using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeMVVM.ModelLib
{
    abstract class Player:IPlayer
    {
        private Position position;
        // update the position of the player has changed
        // ViewModel need to subscribe to this event
        public event PropertyChangedEventHandler PropertyChanged;

        protected Player(Position initialPos)
        {
            position = initialPos;
        }

        public abstract void Start();

        public abstract void RestartGame();

        // TODO: Implement method
        public void Move(Direction direction)
        {
            throw new NotImplementedException();
        }

        public string SolveMaze()
        {
            throw new NotImplementedException();
        }
    }
}
