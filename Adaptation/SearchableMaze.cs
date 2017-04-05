using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace Adaptation
{
    public class SearchableMaze : ISearchable<Position>
    {
        private Maze maze;

        public SearchableMaze(Maze maze)
        {
            this.maze = maze;
        }

        public State<Position> GetInitialState()
        {
            throw new NotImplementedException();
        }

        public State<Position> GetGoalState()
        {
            throw new NotImplementedException();
        }

        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            throw new NotImplementedException();
        }

        public float GetTransferCost(State<Position> from, State<Position> to)
        {
            return 0;
        }
    }
}
