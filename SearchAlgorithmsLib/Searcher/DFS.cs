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
        /// Apply the DFS algorithm on the given problem.
        /// </summary>
        /// <param name="searchable"> The problem to solve. </param>
        /// <returns> Solution to the problem. </returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            Stack<State<T>> openStates = new Stack<State<T>>();
            openStates.Push(searchable.GetInitialState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (openStates.Count > 0)
            {
                State<T> n = openStates.Pop();
                EvaluatedNodes++;
                if (n.Equals(searchable.GetGoalState()))
                {
                    // Gaol has been reached.
                    return new Solution<T>(n);
                }
                if (!closed.Contains(n))
                {
                    closed.Add(n);
                    // Handle succerssors.
                    List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
                    foreach (State<T> s in succerssors)
                    {
                        s.CameFrom = n;
                        openStates.Push(s);
                    }
                }
            }
            return null;
        }
    }
}