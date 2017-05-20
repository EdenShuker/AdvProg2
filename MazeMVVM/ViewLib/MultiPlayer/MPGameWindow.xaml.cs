using MazeMVVM.ModelLib.Player;
using MazeMVVM.ViewModelLib.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MazeMVVM.ViewLib.MultiPlayer
{
    /// <summary>
    /// Interaction logic for MPGameWindow.xaml
    /// </summary>
    public partial class MPGameWindow : Window
    {
        private IMPViewModel vm;
        private bool isBackToMenuButtonPressed;

        public MPGameWindow(IMultiPlayerModel model)
        {
            InitializeComponent();
            this.vm = new MPViewModel(model);
            vm.Subscribe(mazeBoard);
            mazeBoard.PlayerMoved += ShowMsg;
            this.DataContext = vm;
            vm.Start();
            this.isBackToMenuButtonPressed = false;
        }

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

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.vm.CloseGame();
            this.isBackToMenuButtonPressed = true;
            Window win = Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.isBackToMenuButtonPressed)
            {
                this.vm.CloseGame();
                Window window = Application.Current.MainWindow;
                window.Close();
            }

        }
    }
}
