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
        /// <summary>
        /// bool if button was pressed.
        /// </summary>
        private bool isButtonPressed;

        /// <summary>
        /// constructor.
        /// </summary>
        public SPMenuWindow()
        {
            InitializeComponent();
            this.isButtonPressed = false;
            StartMenu.bStart.Click += bStart_Click;
        }

        /// <summary>
        /// start a game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bStart_Click(object sender, RoutedEventArgs e)
        {
            this.isButtonPressed = true;
            SinglePlayerModel model =
                new SinglePlayerModel(new Client(), StartMenu.MazeName, StartMenu.Rows, StartMenu.Cols);
            var newForm = new SPGameWindow(model);
            newForm.Show();
            this.Close();
        }

        /// <summary>
        /// closing.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
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