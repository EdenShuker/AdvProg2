using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        State<T> GetInitialState();

        State<T> getGoalState();


        List<State<T>> getAllPossibleStates(State<T> s);
    }
}
