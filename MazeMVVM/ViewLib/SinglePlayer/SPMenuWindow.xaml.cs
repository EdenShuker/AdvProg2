using System.ComponentModel;
using System.Windows;
using MazeMVVM.ModelLib.Communication;
using MazeMVVM.ModelLib.Player;
using MazeMVVM.ModelLib.Player.SinglePlayer;

namespace MazeMVVM.ViewLib.SinglePlayer
{
    /// <summary>
    /// Interaction logic for SPMenuWindow.xaml
    /// </summary>
    public partial class SPMenuWindow : Window
    {
        private bool isButtonPressed;

        public SPMenuWindow()
        {
            InitializeComponent();
            this.isButtonPressed = false;
            StartMenu.bStart.Click += bStart_Click;
        }

        private void bStart_Click(object sender, RoutedEventArgs e)
        {
            this.isButtonPressed = true;
            SinglePlayerModel model =
                new SinglePlayerModel(new Client(), StartMenu.MazeName, StartMenu.Rows, StartMenu.Cols);
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