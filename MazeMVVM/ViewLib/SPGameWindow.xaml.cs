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

        public SPGameWindow(SinglePlayerModel model)
        {
            InitializeComponent();
            vm = new SPViewModel(model);
            vm.Subscribe(mazeBoard);
            this.DataContext = vm;
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}