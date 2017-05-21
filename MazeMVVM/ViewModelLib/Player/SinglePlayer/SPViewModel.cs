using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeMVVM.ModelLib.Player.SinglePlayer;
using MazeMVVM.ViewLib;
using MazeMVVM.ViewLib.Controls;

namespace MazeMVVM.ViewModelLib.Player.SinglePlayer
{
    /// <summary>
    /// Single player view model.
    /// </summary>
    public class SPViewModel : PlayerViewModel, ISPViewModel
    {
        /// <summary>
        /// Single player model.
        /// </summary>
        private ISinglePlayerModel model;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="singlePlayerModel"> Single player model </param>
        public SPViewModel(ISinglePlayerModel singlePlayerModel) : base(singlePlayerModel)
        {
            this.model = singlePlayerModel;
        }

        /// <summary>
        /// string representing the maze.
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
        /// String representing the initial position of the maze.
        /// </summary>
        public string VM_InitialPos => model.Maze.InitialPos.ToString();

        /// <summary>
        /// String representing the goal position of the maze.
        /// </summary>
        public string VM_GoalPos => model.Maze.GoalPos.ToString();

        /// <summary>
        /// String representing the current position of the player in the maze.
        /// </summary>
        public string VM_Pos => model.Pos.ToString();

        /// <summary>
        /// Subscribe the given maze displayer.
        /// </summary>
        /// <param name="mazeDisplayer"> maze displayer </param>
        public void Subscribe(MazeDisplayer mazeDisplayer)
        {
            mazeDisplayer.PlayerMoved += PlayerMovedOnBoard;
        }

        /// <summary>
        /// Player moved on the maze-board.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void PlayerMovedOnBoard(object sender, PlayerMovedEventArgs e)
        {
            this.model.Move(e.Direction);
        }

        /// <summary>
        /// Restart the game.
        /// </summary>
        public void RestartGame()
        {
            this.model.RestartGame();
        }

        /// <summary>
        /// Solve the maze.
        /// </summary>
        public async void SolveMaze()
        {
            // Get solution.
            this.model.RestartGame();
            await Task.Delay(200);
            string solution = this.model.SolveMaze();
            // Move the player according to the solution
            foreach (char c in solution)
            {
                Direction direction;
                switch (c)
                {
                    case '0':
                        direction = Direction.Left;
                        break;
                    case '1':
                        direction = Direction.Right;
                        break;
                    case '2':
                        direction = Direction.Up;
                        break;
                    case '3':
                        direction = Direction.Down;
                        break;
                    default:
                        direction = Direction.Unknown;
                        break;
                }
                this.model.Move(direction);
                await Task.Delay(300);
            }
        }
    }
}