using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Searchable interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// Get the initial state of the problem.
        /// </summary>
        /// <returns> Initial state. </returns>
        State<T> GetInitialState();

        /// <summary>
        /// Get the goal state of the problem.
        /// </summary>
        /// <returns> Goal state. </returns>
        State<T> GetGoalState();

        /// <summary>
        /// Get all of the posible states that relevant to the given state.
        /// </summary>
        /// <param name="s"> The current state in the problem. </param>
        /// <returns> List of all of the possible states from the given state.</returns>
        List<State<T>> GetAllPossibleStates(State<T> s);

        /// <summary>
        /// TODO: complete documentation.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        double GetTransferCost(State<T> from, State<T> to);
    }
}