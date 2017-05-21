using System.ComponentModel;
using System.Windows;
using MazeMVVM.ModelLib.Player;
using MazeMVVM.ModelLib.Player.SinglePlayer;
using MazeMVVM.ViewModelLib.Player;
using MazeMVVM.ViewModelLib.Player.SinglePlayer;

namespace MazeMVVM.ViewLib.SinglePlayer
{
    /// <summary>
    /// Interaction logic for SPGameWindow.xaml
    /// </summary>
    public partial class SPGameWindow : Window
    {
        /// <summary>
        /// view model.
        /// </summary>
        private ISPViewModel vm;

        /// <summary>
        /// bool if button was pressed.
        /// </summary>
        private bool isBackToMenuButtonPressed;

        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="model"> model </param>
        public SPGameWindow(ISinglePlayerModel model)
        {
            InitializeComponent();
            vm = new SPViewModel(model);
            vm.Subscribe(mazeBoard);
            mazeBoard.PlayerMoved += ShowMsg;
            this.DataContext = vm;
            this.isBackToMenuButtonPressed = false;
        }

        /// <summary>
        /// show message on the screen.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void ShowMsg(object sender, PlayerMovedEventArgs e)
        {
            // Check if player reached the goal
            if (this.vm.VM_Pos == this.vm.VM_GoalPos)
            {
                MessageWindow msgWindow = new MessageWindow();
                msgWindow.Msg = "Winner!";
                msgWindow.Show();
            }
        }

        /// <summary>
        /// reastrt the game.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            vm.RestartGame();
        }

        /// <summary>
        /// solve the maze.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            vm.SolveMaze();
        }

        /// <summary>
        /// go back to menu.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.isBackToMenuButtonPressed = true;
            Window win = Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

        /// <summary>
        /// closing
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void SPGameWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!this.isBackToMenuButtonPressed)
            {
                Window window = Application.Current.MainWindow;
                window.Close();
            }
        }
    }
}