using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Searcher using BFS algorithm.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BFS<T> : PrioritySearcher<T>
    {
        /// <summary>
        /// Aapply the BFS algorithm on the given problem.
        /// </summary>
        /// <param name="searchable"> The problem to solve. </param>
        /// <returns> Solution to the problem. </returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            AddToOpenList(searchable.GetInitialState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> n = PopOpenList();
                closed.Add(n);
                if (n.Equals(searchable.GetGoalState()))
                {
                    // Gaol has been reached.
                    return new Solution<T>(n, EvaluatedNodes);
                }
                // Handle succerssors.
                List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    double newPathCost = n.Cost + searchable.GetTransferCost(n, s);
                    if (!closed.Contains(s) && !IsInOpenList(s))
                    {
                        s.CameFrom = n;
                        s.Cost = newPathCost;
                        AddToOpenList(s);
                    }
                    else if (newPathCost < s.Cost)
                    {
                        s.Cost = newPathCost;
                        if (!IsInOpenList(s))
                        {
                            AddToOpenList(s);
                        }
                        else
                        {
                            AdjustStatePriority(s, (float) s.Cost);
                        }
                    }
                }
            }
            return null;
        }
    }
}