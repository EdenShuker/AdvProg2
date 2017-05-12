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

namespace MazeMVVM.ViewLib
{
    /// <summary>
    /// Interaction logic for SPMenuWindow.xaml
    /// </summary>
    public partial class SPMenuWindow : Window
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        private bool isButtonPressed;

        public SPMenuWindow()
        {
            InitializeComponent();
            this.Rows = Properties.Settings.Default.MazeRows;
            this.Cols = Properties.Settings.Default.MazeCols;
            this.isButtonPressed = false;
            this.DataContext = this;
        }

        private void bStart_Click(object sender, RoutedEventArgs e)
        {
            this.isButtonPressed = true;
            SinglePlayerModel model = new SinglePlayerModel(new Client(), textBName.Text,
                int.Parse(textBRows.Text), int.Parse(textBCols.Text));
            var newForm = new SPGameWindow(model);
            newForm.Show();
            this.Close();
        }

        private void SPMenuWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!this.isButtonPressed)
            {
                Window window = Application.Current.MainWindow;
                window.Close();
            }
        }
    }
}