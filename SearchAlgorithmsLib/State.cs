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

        public T Data { get; set; }

        public double Cost { get; set; }

        public State<T> CameFrom { get; set; }

        private State(T data)
        {
            this.Data = data;
            this.CameFrom = null;
        }

        public bool Equals(State<T> s)
        {
            return Data.Equals(s.Data);
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        public static State<T> GetState(T id)
        {
            return StatePool<T>.Instance.GetState(id);
        }

        /// <summary>
        /// State Pool holds all the states that have been created.
        /// Singleton class.
        /// </summary>
        private sealed class StatePool<T>
        {
            private static volatile StatePool<T> instance;

            private static object syncRoot = new object();

            private Dictionary<T, State<T>> states;

            private StatePool()
            {
                this.states = new Dictionary<T, State<T>>();
            }

            public static StatePool<T> Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (syncRoot)
                        {
                            if (instance == null)
                            {
                                instance = new StatePool<T>();
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
                states.Add(id, newState);
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