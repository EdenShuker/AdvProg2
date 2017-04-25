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
    /// <typeparam name="T"> The kind of state. </typeparam>
    public class State<T> : IComparable
    {
        /// <summary>
        /// Data that the state holds.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Cost of the state.
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// The state where it came from.
        /// </summary>
        public State<T> CameFrom { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Data to hold in the current state.</param>
        private State(T data)
        {
            this.Data = data;
            this.CameFrom = null;
        }

        /// <summary>
        /// Override Equals method.
        /// </summary>
        /// <param name="s"></param>
        /// <returns> true if equals, false otherwise. </returns>
        public bool Equals(State<T> s)
        {
            return Data.Equals(s.Data);
        }

        /// <summary>
        /// Override GetHashCode method.
        /// </summary>
        /// <returns> hash code f the object. </returns>
        public override int GetHashCode()
        {
            return Data.ToString().GetHashCode();
        }

        /// <summary>
        /// Return state given its id.
        /// </summary>
        /// <param name="id">Id of state will be the data that the states holds.</param>
        /// <returns> State out of StatePool </returns>
        public static State<T> GetState(T id)
        {
            return StatePool<T>.Instance.GetState(id);
        }

        /// <summary>
        /// Compare this object with the given.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>Compare result according to the cost of both states.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            State<T> other = obj as State<T>;
            if (other != null)
                return this.Cost.CompareTo(other.Cost);
            else
                throw new NullReferenceException();
        }

        /// <summary>
        /// State Pool holds all the states that have been created.
        /// Singleton class.
        /// </summary>
        private sealed class StatePool<T>
        {
            /// <summary>
            /// Instance of the state-pool.
            /// </summary>
            private static volatile StatePool<T> instance;

            /// <summary>
            /// Object to use for the lock.
            /// </summary>
            private static object syncRoot = new object();

            /// <summary>
            /// Dictionary that maps data of state to the state that holds it.
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
            /// Get instance of StatePool class.
            /// </summary>
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
        }
    }
}