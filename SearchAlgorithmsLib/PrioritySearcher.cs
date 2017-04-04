using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Searcher that use priority-queue in the algorithm.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        /// <summary>
        /// Priority queue to manage the states.
        /// </summary>
        private SimplePriorityQueue<State<T>> openList;

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
        protected PrioritySearcher() : base()
        {
            openList = new SimplePriorityQueue<State<T>>();
        }

        /// <summary>
        /// Pop the next state from the list.
        /// </summary>
        /// <returns> The next state in the list. </returns>
        protected State<T> PopOpenList()
        {
            EvaluatedNodes++;
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

        /// <summary>
        /// Adjust the old priority of the given state to the new given priority.
        /// </summary>
        /// <param name="state"> State to change its priority. </param>
        /// <param name="priority"> New value of the priority. </param>
        protected void AdjustStatePriority(State<T> state, float priority)
        {
            openList.UpdatePriority(state, priority);
        }
    }
}