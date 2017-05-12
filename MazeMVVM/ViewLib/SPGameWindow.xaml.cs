using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MazeMVVM.ModelLib;
using MazeMVVM.ViewModelLib;

namespace MazeMVVM.ViewLib
{
    /// <summary>
    /// Interaction logic for SPGameWindow.xaml
    /// </summary>
    public partial class SPGameWindow : Window
    {
        private SPViewModel vm;
        private bool isBackToMenuButtonPressed;

        public SPGameWindow(SinglePlayerModel model)
        {
            InitializeComponent();
            vm = new SPViewModel(model);
            vm.Subscribe(mazeBoard);
            this.DataContext = vm;
            this.isBackToMenuButtonPressed = false;
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            vm.RestartGame();
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            vm.SolveMaze();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.isBackToMenuButtonPressed = true;
            Window win = Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

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