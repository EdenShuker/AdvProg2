using System;
using MazeLib;
using SearchAlgorithmsLib;

namespace ServerProject
{
    /// <summary>
    /// Create an ISearcher object according to the specified algorithm.
    /// </summary>
    public class SearcherFactory
    {
        /// <summary>
        /// Create a DFS searcher for 0 and BFS searcher for 1.
        /// </summary>
        /// <param name="algorithm">Number represents the search-algorithm.</param>
        /// <returns>ISearcher object.</returns>
        public static ISearcher<Position> Create(int algorithm)
        {
            if (algorithm == 0)
            {
                return new BFS<Position>();
            }
            else if (algorithm == 1)
            {
                return new DFS<Position>();
            }
            else
            {
                throw new ArgumentException("Unknown algorithm");
            }
        }
    }
}