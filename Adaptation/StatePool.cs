using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace Adaptation
{
    public class StatePool
    {
        private Dictionary<int, State<Position>> states;

        public StatePool(Maze maze)
        {
            this.states = new Dictionary<int, State<Position>>();
            for (int i = 0; i < maze.Rows; i++)
            {
                for (int j = 0; j < maze.Cols; j++)
                {
                    if (maze[i, j] == 0)
                    {
                        // Create a state only for an empty place in the maze.
                        Position statePosition = new Position(i, j);
                        this.states[(i + 1) * (j + 1)] = new State<Position>(statePosition);
                    }
                }
            }
        }

        public bool IsValidState(int i, int j)
        {
            return this.states.ContainsKey((i + 1) * (j + 1));
        }

        public State<Position> this[int i, int j]
        {
            get
            {
                if (IsValidState(i, j))
                {
                    return this.states[(i + 1) * (j + 1)];
                }
                return null;
            }
        }

        public void ResetStates()
        {
            foreach (State<Position> state in states.Values)
            {
                state.CameFrom = null;
                // TODO: check if the cost of the state need to be initialized.
            }
        }
    }
}