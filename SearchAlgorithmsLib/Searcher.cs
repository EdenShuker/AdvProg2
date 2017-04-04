using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// An algorithm that perform a search method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// Priority queue to manage the states.
        /// </summary>
        private SimplePriorityQueue<State<T>> openList;

        /// <summary>
        /// TODO: fill the documentation.
        /// </summary>
        private int evaluatedNodes;

        /// <summary>
        /// Size of the list.
        /// </summary>
        public int OpenListSize
        {
            get { return openList.Count; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Searcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }

        /// <summary>
        /// Pop the next state from the list.
        /// </summary>
        /// <returns> The next state in the list. </returns>
        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        /// <summary>
        /// Aadd the given state to the list.
        /// </summary>
        /// <param name="state"> State to handle in the algorithm. </param>
        protected void AddToOpenList(State<T> state)
        {
            openList.Enqueue(state, (float) state.Cost);
        }

        /// <summary>
        /// Check if the given state is in the list.
        /// </summary>
        /// <param name="state"> A state to check. </param>
        /// <returns> True if the given state is in the list, false otherwise. </returns>
        protected bool IsInOpenList(State<T> state)
        {
            return openList.Contains(state);
        }

        // ISearcher’s methods:
        /// <summary>
        /// Get the number of nodes evaluated.
        /// </summary>
        /// <returns> number of nodes evaluated. </returns>
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        /// <summary>
        /// Perform the searching algorithm on the searchable object.
        /// </summary>
        /// <param name="searchable"> An object to search on. </param>
        /// <returns> Solution to the given problem. </returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);
    }
}