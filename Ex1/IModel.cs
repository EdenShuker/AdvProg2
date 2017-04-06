using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);

        Solution<Position> SolveMaze(string name, int algorithm);
    }
}