using System;
using MazeGeneratorLib;
using MazeLib;
using MazeObjectAdapterLib;
using SearchAlgorithmsLib;

namespace Mission1
{
    /// <summary>
    /// Demonstrates the different between BFS and DFS.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Run few cases of searching algorithms.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CompareSolvers(5, 5);
            CompareSolvers(40, 40);
            CompareSolvers(100, 100);
        }

        /// <summary>
        /// Create a maze with the specified dimensions and solve it with the 
        /// DFS and BFS algorithm.
        /// </summary>
        /// <param name="rows">number of rows of the maze.</param>
        /// <param name="cols">number of cols of the maze.</param>
        static void CompareSolvers(int rows, int cols)
        {
            // Generate the maze.
            IMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            Console.WriteLine(maze.ToString());
            ISearchable<Position> searchableMaze = new SearchableMaze(maze);
            // Solve it with both algorithms.
            ISearcher<Position> BFSSearcher = new BFS<Position>();
            Search(searchableMaze, BFSSearcher, "BFS");
            ISearcher<Position> DFSSearcher = new DFS<Position>();
            Search(searchableMaze, DFSSearcher, "DFS");
        }

        /// <summary>
        /// Solve the given searchable object with the searcher object.
        /// </summary>
        /// <param name="searchable">Maze.</param>
        /// <param name="searcher"> Search-algorithm.</param>
        /// <param name="searcherType">String that represents the type of search-algorithm.</param>
        private static void Search(ISearchable<Position> searchable, ISearcher<Position> searcher, String searcherType)
        {
            Solution<Position> solution = searcher.Search(searchable);
            Console.WriteLine("{0} solved the maze with {1} evaluated nodes", searcherType,
                searcher.GetNumberOfNodesEvaluated());
        }
    }
}