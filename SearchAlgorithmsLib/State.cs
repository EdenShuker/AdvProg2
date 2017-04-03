using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        private T state;
        public double cost { get; set; }
        public State<T> cameFrom { get; set; }

        public State(T state)
        {
            this.state = state;
        }

        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }
    }
}