using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Searcher using DFS algorithm.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DFS<T> : Searcher<T>
    {
        /// <summary>
        /// Aapply the DFS algorithm on the given problem.
        /// </summary>
        /// <param name="searchable"> The problem to solve. </param>
        /// <returns> Solution to the problem. </returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            throw new NotImplementedException();
        }
    }
}