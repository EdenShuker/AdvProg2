using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace Adaptation
{
    /// <summary>
    /// Maze object that different algorithm can be applied on it.
    /// </summary>
    public class SearchableMaze : ISearchable<Position>
    {
        private Maze maze;

        /// <summary>
        /// Constructor.
        /// Extract all the valid states out of the maze.
        /// </summary>
        /// <param name="maze"> The maze to search on. </param>
        public SearchableMaze(Maze maze)
        {
            this.maze = maze;
            // Extract valid states from the maze.
            for (int i = 0; i < this.maze.Rows; i++)
            {
                for (int j = 0; j < this.maze.Cols; j++)
                {
                    if (this.maze[i, j] == CellType.Free)
                    {
                        State<Position>.GetState(new Position(i, j));
                    }
                }
            }
        }

        public State<Position> GetInitialState()
        {
            return State<Position>.GetState(this.maze.InitialPos);
        }


        public State<Position> GetGoalState()
        {
            return State<Position>.GetState(this.maze.GoalPos);
        }

        /// <summary>
        /// Create a list with all the possible states that can be accessed by the given state.
        /// Only valid (free cell) states will be added to the list.
        /// </summary>
        /// <param name="s"> general state. </param>
        /// <returns> List of states. </returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> possibleStatesList = new List<State<Position>>();
            int currentRow = s.Data.Row;
            int currentCol = s.Data.Col;
            // Check for going Up.
            if (currentRow > 0 && this.maze[currentRow - 1, currentCol] == CellType.Free)
            {
                possibleStatesList.Add(State<Position>.GetState(new Position(currentRow - 1, currentCol)));
            }
            // Check for going Down.
            if (currentRow < maze.Rows - 1 && this.maze[currentRow + 1, currentCol] == CellType.Free)
            {
                possibleStatesList.Add(State<Position>.GetState(new Position(currentRow + 1, currentCol)));
            }
            // Check for going Right.
            if (currentCol < maze.Cols - 1 && this.maze[currentRow, currentCol + 1] == CellType.Free)
            {
                possibleStatesList.Add(State<Position>.GetState(new Position(currentRow, currentCol + 1)));
            }
            // Check for going Left.
            if (currentCol > 0 && this.maze[currentRow, currentCol - 1] == CellType.Free)
            {
                possibleStatesList.Add(State<Position>.GetState(new Position(currentRow, currentCol - 1)));
            }
            return possibleStatesList;
        }


        public float GetTransferCost(State<Position> from, State<Position> to)
        {
            return 1;
        }
    }
}