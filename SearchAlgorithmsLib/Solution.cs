using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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

        private int evaluatedNodes;

        public Solution(State<T> state, int numEvaluatedNodes)
        {
            this.backTrace = new Stack<State<T>>();
            FindBackTrace(state);
            evaluatedNodes = numEvaluatedNodes;
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
            if (backTrace.Count == 0)
            {
                return null;
            }
            return this.backTrace.Pop();
        }

        public delegate string PathToString(Stack<State<T>> path);

        /// <summary>
        /// Create Json object out ogf the solution.
        /// </summary>
        /// <param name="func"> function which convert solution path to string </param>
        /// <returns> JSON object describes th solution. </returns>
        public string ToJSON(PathToString func)
        {
            JObject solutionObj = new JObject();
            solutionObj["Solution"] = func(new Stack<State<T>>(this.backTrace));
            solutionObj["NodesEvaluated"] = evaluatedNodes.ToString();
            return solutionObj.ToString();
        }

    }
}