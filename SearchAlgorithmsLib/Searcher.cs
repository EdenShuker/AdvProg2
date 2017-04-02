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
        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        // ISearcher’s methods:
        public int getNumberOfNodesEvaluated() { return evaluatedNodes; }
        public abstract Solution search(ISearchable<T> searchable);
    }
}
