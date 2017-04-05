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
        private State<Position> intialState;
        private State<Position> goalState;

        public SearchableMaze(Maze maze)
        {
            // TODO: add state-pool class and add it here as a member.
            intialState = new State<Position>(maze.InitialPos);
            goalState = new State<Position>(maze.GoalPos);
        }

        public State<Position> GetInitialState()
        {
            return intialState;
        }

        public State<Position> GetGoalState()
        {
            return goalState;
        }

        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            throw new NotImplementedException();
        }

        public double GetTransferCost(State<Position> @from, State<Position> to)
        {
            throw new NotImplementedException();
        }
    }
}
