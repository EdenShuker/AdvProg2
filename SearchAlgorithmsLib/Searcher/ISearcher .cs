using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Searcher interface.
    /// </summary>
    /// <typeparam name="T">Type of object to search on.</typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// Find a solution to the given problem using an algorithm.
        /// </summary>
        /// <param name="searchable"> The problem to solve. </param>
        /// <returns> Solution to the given problem. </returns>
        Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// Get how many nodes were evaluated by the algorithm. 
        /// </summary>
        /// <returns> Number of evaluated nodes. </returns>
        int GetNumberOfNodesEvaluated();
    }
}