using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    public class SearcherFactory
    {
        public static ISearcher<Position> Create(int algorithm)
        {
            if (algorithm == 0)
            {
                return new BFS<Position>();
            }
            else if (algorithm == 1)
            {
                return new DFS<Position>();
            }
            else
            {
                throw new ArgumentException("Unknown algorithm");
            }
        }
    }
}