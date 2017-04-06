﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Adaptation
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void CompareSolvers(int rows, int cols)
        {
            IMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            Console.WriteLine(maze.ToString());
            ISearchable<Position> searchableMaze = new SearchableMaze(maze);
            ISearcher<Position> BFSSearcher = new BFS<Position>();
            Search(searchableMaze, BFSSearcher, "BFS");
            ISearcher<Position> DFSSearcher = new DFS<Position>();
            Search(searchableMaze, DFSSearcher, "DFS");
        }

        private static void Search(ISearchable<Position> searchable, ISearcher<Position> searcher, String searcherType)
        {
            searcher.Search(searchable);
            Console.WriteLine("{0} solved the maze with {1} evaluated nodes",searcherType, searcher.GetNumberOfNodesEvaluated());
        }
    }
}