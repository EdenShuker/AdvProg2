using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        private SimplePriorityQueue<State<T>> openList;
        private int evaluatedNodes;
        // a property of openList 
        public int OpenListSize
        {
            get { return openList.Count; }
        }

        public Searcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }

        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        protected void AddToOpenList(State<T> state)
        {
            openList.Enqueue(state, (float)state.Cost);
        }

        protected bool IsInOpenList(State<T> state) {
            return openList.Contains(state);
        }

        protected void AdjustStatePriority(State<T> state, float priority)
        {
            openList.UpdatePriority(state, priority);
        }

        // ISearcher’s methods:
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        public abstract Solution<T> Search(ISearchable<T> searchable);
    }
}