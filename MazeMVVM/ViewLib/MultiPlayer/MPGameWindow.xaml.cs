using System;
using System.Windows;
using MazeMVVM.ModelLib.Player.MultiPlayer;
using MazeMVVM.ViewModelLib.Player.MultiPlayer;

namespace MazeMVVM.ViewLib.MultiPlayer
{
    /// <summary>
    /// Interaction logic for MPGameWindow.xaml
    /// </summary>
    public partial class MPGameWindow : Window
    {
        /// <summary>
        /// View model.
        /// </summary>
        private IMPViewModel vm;

        /// <summary>
        /// bool of the button was pressed.
        /// </summary>
        private bool isBackToMenuButtonPressed;

        /// <summary>
        /// bool if exit window.
        /// </summary>
        private bool isExitWindow;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model"> model </param>
        public MPGameWindow(IMultiPlayerModel model)
        {
            InitializeComponent();
            this.vm = new MPViewModel(model);
            vm.Subscribe(mazeBoard);
            mazeBoard.PlayerMoved += ShowMsg;
            this.DataContext = vm;
            vm.Start();
            this.isBackToMenuButtonPressed = false;
            this.isExitWindow = false;
            this.vm.VMGameEnded += CloseCurrentWindow;
        }

        /// <summary>
        /// Show a message on the screen.
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
        /// Back to menu button was clicked.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.isBackToMenuButtonPressed = true;
            this.vm.CloseGame();
        }

        /// <summary>
        /// Closing the window.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.isBackToMenuButtonPressed)
            {
                this.isExitWindow = true;
                this.vm.CloseGame();
                // Close main window
                Window window = Application.Current.MainWindow;
                window.Close();
            }
        }

        /// <summary>
        /// Close the window.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void CloseCurrentWindow(object sender, EventArgs e)
        {
            if (!this.isExitWindow)
            {
                Dispatcher.BeginInvoke((Action) (() =>
                {
                    // Show main window
                    Window win = Application.Current.MainWindow;
                    win.Show();
                    this.Close();
                }));
                this.isBackToMenuButtonPressed = true;
            }
        }
    }
}