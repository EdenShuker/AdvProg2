using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using MazeObjectAdapterLib;
using SearchAlgorithmsLib;

namespace Client
{
    class SPGame
    {
        private Maze maze;
        private Solution<Position> solution;
        public Position Position { get; private set; }

        public SPGame(string mazeStr)
        {
            this.maze = Maze.FromJSON(mazeStr);
            this.Position = maze.InitialPos;
        }

        /// <summary>
        /// Return the solution of the maze. Solve it if the solution doesnwt exist.
        /// </summary>
        /// <param name="algorithm"></param>
        /// <returns> Solution of maze </returns>
        public Solution<Position> Solve(int algorithm)
        {
            if (this.solution != null)
            {
                ISearchable<Position> searchableMaze = new SearchableMaze(this.maze);
                ISearcher<Position> searcher = SearcherFactory<Position>.Create(algorithm);
                this.solution = searcher.Search(searchableMaze);
            }
            return this.solution;
        }


        /// <summary>
        /// Make the move as long as it valid move.
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Direction direction)
        {
            int currentCol = this.Position.Col;
            int currentRow = this.Position.Row;
            // Right
            if (direction.Equals("right") && currentCol < this.maze.Cols - 1 &&
                this.maze[currentRow, currentCol + 1] == CellType.Free)
            {
                this.Position = new Position(currentRow, currentCol + 1);
            }
            // Left
            else if (direction.Equals("left") && currentCol > 0 &&
                     this.maze[currentRow, currentCol - 1] == CellType.Free)
            {
                this.Position = new Position(currentRow, currentCol - 1);
            }
            // Up
            else if (direction.Equals("up") && currentRow > 0 &&
                     this.maze[currentRow - 1, currentCol] == CellType.Free)
            {
                this.Position = new Position(currentRow - 1, currentCol);
            }
            // Down
            else if (direction.Equals("down") && currentRow < this.maze.Rows - 1 &&
                     this.maze[currentRow + 1, currentCol] == CellType.Free)
            {
                this.Position = new Position(currentRow + 1, currentCol);
            }
        }
    }
}
