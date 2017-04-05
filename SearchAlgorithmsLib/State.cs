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
        public T Data { get; set; }

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
        /// <param name="data"> The data that the current state holds. </param>
        private State(T data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Check if the current state equals to the given one.
        /// Comparison done with the data of the states.
        /// </summary>
        /// <param name="s"> Other state. </param>
        /// <returns> True if the data of both states equal, False otherwise. </returns>
        public bool Equals(State<T> s)
        {
            return Data.Equals(s.Data);
        }

        /// <summary>
        /// Get the hash-code of the state.
        /// </summary>
        /// <returns> Hash code of its data. </returns>
        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        /// <summary>
        /// State Pool holds all the states that have been created.
        /// Singleton class.
        /// </summary>
        public sealed class StatePool
        {
            /// <summary>
            /// Instance of the singleton.
            /// </summary>
            private static volatile StatePool instance;

            /// <summary>
            /// Variable to lock critical section.
            /// </summary>
            private static object syncRoot = new object();

            /// <summary>
            /// Maps an id-data of state to its state.
            /// </summary>
            private Dictionary<T, State<T>> states;

            /// <summary>
            /// Constructor.
            /// </summary>
            private StatePool()
            {
                this.states = new Dictionary<T, State<T>>();
            }

            /// <summary>
            /// Get the instance of the singleton.
            /// Thread safety.
            /// </summary>
            public static StatePool Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (syncRoot)
                        {
                            if (instance == null)
                            {
                                instance = new StatePool();
                            }
                        }
                    }
                    return instance;
                }
            }

            /// <summary>
            /// Get the state with the given id.
            /// </summary>
            /// <param name="id"> Id of general state. </param>
            /// <returns> State that holds the id as its data. </returns>
            public State<T> GetState(T id)
            {
                // The state already in the pool (created before)
                if (this.states.ContainsKey(id))
                {
                    return states[id];
                }
                // This is a new state, add it to the dictionary.
                State<T> newState = new State<T>(id);
                states[id] = newState;
                return newState;
            }

            /// <summary>
            /// Reset the states of the last data caused by the last operations on it.
            /// </summary>
            public void ResetStates()
            {
                foreach (State<T> stateVal in states.Values)
                {
                    stateVal.CameFrom = null;
                    // TODO: check if the cost of the state need to be initialized.
                }
            }
        }
    }
}