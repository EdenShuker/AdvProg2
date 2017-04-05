using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Generic state that holds some data.
    /// </summary>
    /// <typeparam name="T"> Kind of state. </typeparam>
    public class State<T>
    {
        /// <summary>
        /// Data of the state.
        /// </summary>
        private T state;

        /// <summary>
        /// Cost of the state.
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Wwhere this state came from.
        /// </summary>
        public State<T> CameFrom { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="state"> The data that the current satate holds. </param>
        public State(T state)
        {
            this.state = state;
        }

        /// <summary>
        /// Check if the current state equals to the given one.
        /// Comparison done with the data of the states.
        /// </summary>
        /// <param name="s"> Other state. </param>
        /// <returns> True if the data of both states equal, False otherwise. </returns>
        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }

        /// <summary>
        /// Get the hash-code of the state.
        /// </summary>
        /// <returns> Hash code of its data. </returns>
        public override int GetHashCode()
        {
            return state.GetHashCode();
        }
    }
}