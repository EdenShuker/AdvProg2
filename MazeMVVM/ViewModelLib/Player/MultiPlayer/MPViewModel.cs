using System;
using System.Text;
using MazeMVVM.ModelLib.Player.MultiPlayer;
using MazeMVVM.ViewLib;
using MazeMVVM.ViewLib.Controls;

namespace MazeMVVM.ViewModelLib.Player.MultiPlayer
{
    /// <summary>
    /// Multi player view model.
    /// </summary>
    public class MPViewModel : PlayerViewModel, IMPViewModel
    {
        /// <summary>
        /// Multi player model.
        /// </summary>
        private IMultiPlayerModel model;

        /// <summary>
        /// Event of game ending.
        /// </summary>
        public event EventHandler VMGameEnded;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="MultiPlayermodel"></param>
        public MPViewModel(IMultiPlayerModel MultiPlayermodel) : base(MultiPlayermodel)
        {
            this.model = MultiPlayermodel;
            this.model.GameEnded += RaiseEvent_GameEnded;
        }

        /// <summary>
        /// String representing the maze.
        /// </summary>
        public string VM_MazeStr
        {
            get
            {
                string mazeStr = model.Maze.ToString();
                return this.ProduceStrFromMaze(mazeStr);
            }
        }

        /// <summary>
        /// Number of rows.
        /// </summary>
        public int VM_Rows => model.Maze.Rows;

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int VM_Cols => model.Maze.Cols;

        /// <summary>
        /// Name of the maze.
        /// </summary>
        public string VM_MazeName => model.Maze.Name;

        /// <summary>
        /// Initial position of the maze.
        /// </summary>
        public string VM_InitialPos => model.Maze.InitialPos.ToString();

        /// <summary>
        /// Goal position of the maze.
        /// </summary>
        public string VM_GoalPos => model.Maze.GoalPos.ToString();

        /// <summary>
        /// Current position of the player in the maze.
        /// </summary>
        public string VM_Pos => model.Pos.ToString();

        /// <summary>
        /// Current position of the competitor in the maze.
        /// </summary>
        public string VM_PosOtherPlayer => model.PosOtherPlayer.ToString();

        /// <summary>
        /// Subscribe th egiven maze displayer.
        /// </summary>
        /// <param name="mazeDisplayer"></param>
        public void Subscribe(MazeDisplayer mazeDisplayer)
        {
            mazeDisplayer.PlayerMoved += PlayerMovedOnBoard;
        }

        /// <summary>
        /// Player moved on the board.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void PlayerMovedOnBoard(object sender, PlayerMovedEventArgs e)
        {
            this.model.Move(e.Direction);
        }

        /// <summary>
        /// Close the game.
        /// </summary>
        public void CloseGame()
        {
            this.model.CloseGame();
        }

        /// <summary>
        /// Start the game.
        /// </summary>
        public void Start()
        {
            this.model.Start();
        }

        /// <summary>
        /// Raise the event of game ending.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RaiseEvent_GameEnded(object sender, EventArgs e)
        {
            VMGameEnded?.Invoke(this, null);
        }
    }
}