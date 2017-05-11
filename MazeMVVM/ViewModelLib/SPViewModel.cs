using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeMVVM.ModelLib;
using MazeMVVM.ViewLib;
using MazeMVVM.ViewLib.Controls;

namespace MazeMVVM.ViewModelLib
{
    public class SPViewModel : ViewModel
    {
        private SinglePlayerModel model;

        public SPViewModel(SinglePlayerModel singlePlayerModel)
        {
            this.model = singlePlayerModel;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
        }

        public string VM_MazeStr
        {
            get
            {
                string mazeStr = model.Maze.ToString();
                StringBuilder builder = new StringBuilder();
                foreach (char c in mazeStr)
                {
                    if (c != '\r' && c != '\n')
                    {
                        builder.Append(c);
                    }
                }
                return builder.ToString();
            }
        }

        public int VM_Rows => model.Maze.Rows;

        public int VM_Cols => model.Maze.Cols;

        public string VM_MazeName
        {
            get { return model.Maze.Name; }
            set
            {
                model.Maze.Name = value;
                NotifyPropertyChanged("VM_MazeName");
            }
        }

        public string VM_InitialPos
        {
            get { return model.Maze.InitialPos.ToString(); }
        }

        public string VM_GoalPos
        {
            get { return model.Maze.GoalPos.ToString(); }
        }

        public string VM_Pos
        {
            get { return model.Pos.ToString(); }
        }

        private Position StringToPosition(string position)
        {
            int index = position.IndexOf(",", StringComparison.Ordinal);
            int row = int.Parse(position.Substring(1, index - 1));
            int col = int.Parse(position.Substring(index + 1, position.Length - index - 2));
            return new Position(row, col);
        }

        public void Subscribe(MazeDisplayer mazeDisplayer)
        {
            mazeDisplayer.PlayerMoved += PlayerMovedOnBoard;
        }

        private void PlayerMovedOnBoard(object sender, PlayerMovedEventArgs e)
        {
            this.model.Move(e.Direction);
        }

        public void RestartGame()
        {
            this.model.RestartGame();
        }
       
    }
}