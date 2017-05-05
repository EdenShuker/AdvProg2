using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeMVVM.ModelLib;

namespace MazeMVVM.ViewModelLib
{
    class SPViewModel : INotifyPropertyChanged
    {
        private SinglePlayerModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        public SPViewModel(SinglePlayerModel singlePlayerModel)
        {
            this.model = singlePlayerModel;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
        }

        public string VM_MazeStr => model.Maze.ToString();

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
            set
            {
                int i = value.IndexOf(",", StringComparison.Ordinal);
                int x = int.Parse(value.Substring(0, i));
                int y = int.Parse(value.Substring(i + 1));
                model.Maze.InitialPos = new Position(x, y);
                NotifyPropertyChanged("VM_InitialPos");
            }
        }

        public string VM_GoalPos
        {
            get { return model.Maze.GoalPos.ToString(); }
            set
            {
                int i = value.IndexOf(",", StringComparison.Ordinal);
                int x = int.Parse(value.Substring(0, i));
                int y = int.Parse(value.Substring(i + 1));
                model.Maze.GoalPos = new Position(x, y);
                NotifyPropertyChanged("VM_GoalPos");
            }
        }

        public Maze VM_Maze => model.Maze;
        private Position position;

        public Position VM_Pos
        {
            get { return position; }
            set
            {
                model.Pos = value;
                position = value;
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void Move(Direction direction)
        {
            this.model.Move(direction);
        }
    }
}