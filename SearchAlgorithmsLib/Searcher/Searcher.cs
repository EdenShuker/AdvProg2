using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// An algorithm that perform a search method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// Number of nodes evaluated by the algorithm (how many paths had been checked).
        /// </summary>
        protected int EvaluatedNodes;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected Searcher()
        {
            this.EvaluatedNodes = 0;
        }

        /// <summary>
        /// Get the number of nodes evaluated.
        /// </summary>
        /// <returns> number of nodes evaluated. </returns>
        public int GetNumberOfNodesEvaluated()
        {
            return this.EvaluatedNodes;
        }

        /// <summary>
        /// Perform the searching algorithm on the searchable object.
        /// </summary>
        /// <param name="searchable"> An object to search on. </param>
        /// <returns> Solution to the given problem. </returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);
    }
}