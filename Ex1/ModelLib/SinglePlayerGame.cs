using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;


namespace ServerProject.ModelLib
{
    /// <summary>
    /// Holds info about single player game. 
    /// </summary>
    class SinglePlayerGame
    {
        public Model.MazeInfo MazeInfo { get; set; }
        public Maze Maze => MazeInfo.Maze;
        public Solution<Position> Solution => MazeInfo.Solution;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maze"></param>
        public SinglePlayerGame(Maze maze)
        {
            this.MazeInfo = new Model.MazeInfo(maze);
        }
    }
}
