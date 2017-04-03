using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : Searcher<T>
    {
        public override Solution<T> search(ISearchable<T> searchable)
        {
            addToOpenList(searchable.getInitialState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> n = popOpenList();
                closed.Add(n);
                if (n.Equals(searchable.getGoalState()))
                    return new Solution<T>(n);
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !isInOpenList(s))
                    {
                        s.cameFrom = n;
                        addToOpenList(s);
                    }
                    else
                    {
                        if (!isInOpenList(s))
                        {
                            addToOpenList(s);
                        }
                        else
                        {
                            //  adjust its priority in OPEN
                        }
                    }
                }
            }
            return null;
        }
    }
}

