﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeMVVM.ViewLib.Controls;

namespace MazeMVVM.ViewModelLib
{
    public interface ISPViewModel
    {
        // Properties

        string VM_MazeStr { get; }

        int VM_Rows { get; }

        int VM_Cols { get; }

        string VM_MazeName { get; }

        string VM_InitialPos { get; }

        string VM_GoalPos { get; }

        string VM_Pos { get; }

        // Methods

        void Subscribe(MazeDisplayer mazeDisplayer);

        void RestartGame();

        void SolveMaze();
    }
}