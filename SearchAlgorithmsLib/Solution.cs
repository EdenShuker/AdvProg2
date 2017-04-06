using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Represents the solution of general problem.
    /// </summary>
    /// <typeparam name="T"> Kind of problem. </typeparam>
    public class Solution<T>
    {
        /// <summary>
        /// Holds the full solution.
        /// </summary>
        private Stack<State<T>> backTrace;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="state"> The last step of the solution. </param>
        public Solution(State<T> state)
        {
            this.backTrace = new Stack<State<T>>();
            FindBackTrace(state);
        }

        /// <summary>
        /// Climb up from the given state to the start of the solution.
        /// </summary>
        /// <param name="state"> One step of the solution. </param>
        private void FindBackTrace(State<T> state)
        {
            if (state != null)
            {
                this.backTrace.Push(state);
                FindBackTrace(state.CameFrom);
            }
        }

        /// <summary>
        /// Get the next state in path.
        /// </summary>
        /// <returns> The next state of the solution. </returns>
        public State<T> GetNextState()
        {
            if (backTrace.Count == 0){
                return null;
            }
            return this.backTrace.Pop();
        }
    }
}