using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Create an ISearcher object according to the specified algorithm.
    /// </summary>
    public class SearcherFactory<T>
    {
        /// <summary>
        /// Create a DFS searcher for 0 and BFS searcher for 1.
        /// </summary>
        /// <param name="algorithm">Number represents the search-algorithm.</param>
        /// <returns>ISearcher object.</returns>
        public static ISearcher<T> Create(int algorithm)
        {
            if (algorithm == 0)
            {
                return new BFS<T>();
            }
            else if (algorithm == 1)
            {
                return new DFS<T>();
            }
            else
            {
                throw new ArgumentException("Unknown algorithm");
            }
        }
    }
}
