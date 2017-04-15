using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Mission1
{
    class Program
    {
        static void Main(string[] args)
        {
            CompareSolvers(5,5);
            CompareSolvers(40,40);
            CompareSolvers(100,100);
        }


        /// <summary>
        /// Create a maze with the specified dimensions and solve it with the 
        /// DFS and BFS algorithm.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        static void CompareSolvers(int rows, int cols)
        {
            // generate the maze.
            IMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            Console.WriteLine(maze.ToString());
            ISearchable<Position> searchableMaze = new SearchableMaze(maze);
            // solve it with both algorithms.
            ISearcher<Position> BFSSearcher = new BFS<Position>();
            Search(searchableMaze, BFSSearcher, "BFS");
            ISearcher<Position> DFSSearcher = new DFS<Position>();
            Search(searchableMaze, DFSSearcher, "DFS");
        }

        /// <summary>
        /// Solve the given searchable object with the searcher object.
        /// </summary>
        /// <param name="searchable"></param>
        /// <param name="searcher"></param>
        /// <param name="searcherType"></param>
        private static void Search(ISearchable<Position> searchable, ISearcher<Position> searcher, String searcherType)
        {
            Solution<Position> solution = searcher.Search(searchable);
            Console.WriteLine("{0} solved the maze with {1} evaluated nodes", searcherType,
                searcher.GetNumberOfNodesEvaluated());
        }
    }
}