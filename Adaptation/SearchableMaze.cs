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
            // TODO: Maybe switch the call for the method
            // TODO: from - State<>.StatePool.Instance.GetState(Pos)
            // TODO: to   - State<>.GetState(Pos)
            // TODO: so all of the state-pool implementation will be hidden (and the line wont be long). 
            for (int i = 0; i < maze.Rows; i++)
            {
                for (int j = 0; j < maze.Cols; j++)
                {
                    if (maze[i, j] == CellType.Free)
                    {
                        State<Position>.StatePool.Instance.GetState(new Position(i, j));
                    }
                }
            }
        }

        public State<Position> GetInitialState()
        {
            return State<Position>.StatePool.Instance.GetState(maze.InitialPos);
        }

        public State<Position> GetGoalState()
        {
            return State<Position>.StatePool.Instance.GetState(maze.GoalPos);
        }

        /// <summary>
        /// Create a list with all the possible states that can be accessed by the given state.
        /// Only valid (free cell) states will be added to the list.
        /// </summary>
        /// <param name="s"> general state. </param>
        /// <returns> List of states. </returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> possibleStates = new List<State<Position>>();
            int currentRow = s.Data.Row;
            int currentCol = s.Data.Col;
            // Check for going Up.
            if (maze[currentRow - 1, currentCol] == CellType.Free)
            {
                possibleStates.Add(State<Position>.StatePool.Instance.GetState(new Position(currentRow - 1, currentCol)));
            }
            // Check for going Down.
            if (maze[currentRow + 1, currentCol] == CellType.Free)
            {
                possibleStates.Add(State<Position>.StatePool.Instance.GetState(new Position(currentRow + 1, currentCol)));
            }
            // Check for going Right.
            if (maze[currentRow, currentCol + 1] == CellType.Free)
            {
                possibleStates.Add(State<Position>.StatePool.Instance.GetState(new Position(currentRow, currentCol + 1)));
            }
            // Check for going Left.
            if (maze[currentRow, currentCol - 1] == CellType.Free)
            {
                possibleStates.Add(State<Position>.StatePool.Instance.GetState(new Position(currentRow, currentCol - 1)));
            }
            return possibleStates;
        }

        public float GetTransferCost(State<Position> from, State<Position> to)
        {
            return 0;
        }
    }
}