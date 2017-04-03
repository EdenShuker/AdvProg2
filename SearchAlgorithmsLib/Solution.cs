using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        private Stack<State<T>> backTrace;

        public Solution(State<T> state)
        {
            backTrace = new Stack<State<T>>();
            findBackTrace(state);
        }

        // Recursive method that fills the stack with the founded path.
        private void findBackTrace(State<T> state)
        {
            if (state != null)
            {
                backTrace.Push(state);
                findBackTrace(state.cameFrom);
            }
        }

        // Get the next state in path.
        public State<T> getNextState()
        {
            return backTrace.Pop();
        }
    }
}
