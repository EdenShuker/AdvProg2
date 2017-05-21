using System;
using System.Text;
using MazeMVVM.ModelLib.Player.MultiPlayer;
using MazeMVVM.ViewLib;
using MazeMVVM.ViewLib.Controls;

namespace MazeMVVM.ViewModelLib.Player.MultiPlayer
{
    public class MPViewModel: PlayerViewModel, IMPViewModel
    {
        private IMultiPlayerModel model;

        public event EventHandler VMGameEnded;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="MultiPlayermodel"></param>
        public MPViewModel(IMultiPlayerModel MultiPlayermodel): base(MultiPlayermodel)
        {
            this.model = MultiPlayermodel;
            this.model.GameEnded += RaiseEvent_GameEnded; 
        }


        // Properties

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

        public string VM_MazeName => model.Maze.Name;

        public string VM_InitialPos => model.Maze.InitialPos.ToString();

        public string VM_GoalPos => model.Maze.GoalPos.ToString();

        public string VM_Pos => model.Pos.ToString();

        public string VM_PosOtherPlayer => model.PosOtherPlayer.ToString();


        // Methods

        public void Subscribe(MazeDisplayer mazeDisplayer)
        {
            mazeDisplayer.PlayerMoved += PlayerMovedOnBoard;
        }

        private void PlayerMovedOnBoard(object sender, PlayerMovedEventArgs e)
        {
            this.model.Move(e.Direction);
        }

        public void CloseGame()
        {
            this.model.CloseGame();
        }

        public void Start()
        {
            this.model.Start();
        }


        private void RaiseEvent_GameEnded(object sender, EventArgs e)
        {
            VMGameEnded?.Invoke(this, null);
        }

    }
}
