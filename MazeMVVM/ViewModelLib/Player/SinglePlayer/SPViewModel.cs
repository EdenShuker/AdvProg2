using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeMVVM.ModelLib.Player.SinglePlayer;
using MazeMVVM.ViewLib;
using MazeMVVM.ViewLib.Controls;

namespace MazeMVVM.ViewModelLib.Player.SinglePlayer
{
    public class SPViewModel : PlayerViewModel, ISPViewModel
    {
        private ISinglePlayerModel model;

        public SPViewModel(ISinglePlayerModel singlePlayerModel) : base(singlePlayerModel)
        {
            this.model = singlePlayerModel;
        }

        public string VM_MazeStr
        {
            get
            {
                string mazeStr = model.Maze.ToString();
                return this.ProduceStrFromMaze(mazeStr);
            }
        }

        public int VM_Rows => model.Maze.Rows;

        public int VM_Cols => model.Maze.Cols;

        public string VM_MazeName => model.Maze.Name;

        public string VM_InitialPos => model.Maze.InitialPos.ToString();

        public string VM_GoalPos => model.Maze.GoalPos.ToString();

        public string VM_Pos => model.Pos.ToString();

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

        public async void SolveMaze()
        {
            this.model.RestartGame();
            await Task.Delay(200);
            string solution = this.model.SolveMaze();
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