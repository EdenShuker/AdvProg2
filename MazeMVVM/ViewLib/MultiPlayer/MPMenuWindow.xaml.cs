using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MazeMVVM.ModelLib.Communication;
using MazeMVVM.ModelLib.Player.MultiPlayer;
using MazeMVVM.ViewModelLib.Player.MultiPlayer;

namespace MazeMVVM.ViewLib.MultiPlayer
{
    /// <summary>
    /// Interaction logic for MPMenuWindow.xaml
    /// </summary>
    public partial class MPMenuWindow : Window
    {
        /// <summary>
        /// bool if the button was pressed.
        /// </summary>
        private bool isButtonPressed;

        /// <summary>
        /// view model.
        /// </summary>
        private MPMenuViewModel vm;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MPMenuWindow()
        {
            InitializeComponent();
            this.isButtonPressed = false;
            MPMenuModel model = new MPMenuModel(new Client());
            this.vm = new MPMenuViewModel(model);
            this.DataContext = vm;
            StartMenu.bStart.Click += bStart_Click;
        }

        /// <summary>
        /// start a new game.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private async void bStart_Click(object sender, RoutedEventArgs e)
        {
            this.isButtonPressed = true;
            WaitMsg.Text = "Waiting for competitor...";
            await Task.Delay(10);

            MultiPlayerModel model =
                new MultiPlayerModel(new Client(), StartMenu.MazeName, StartMenu.Rows, StartMenu.Cols);
            ShowGameWindow(model);
        }

        /// <summary>
        /// join to exist game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            this.isButtonPressed = true;
            MultiPlayerModel model =
                new MultiPlayerModel(new Client(), ComboBox.SelectedItem.ToString());
            ShowGameWindow(model);
        }

        /// <summary>
        /// Show the window of the game.
        /// </summary>
        /// <param name="model"></param>
        private void ShowGameWindow(MultiPlayerModel model)
        {
            var newForm = new MPGameWindow(model);
            newForm.Show();
            this.Close();
        }

        /// <summary>
        /// On dropdown of the combo box.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void comboBox_DropDownOpened(object sender, System.EventArgs e)
        {
            this.vm.RefreshList();
        }

        /// <summary>
        /// closing.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void MPMenuWindow_OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.isButtonPressed)
            {
                Window window = Application.Current.MainWindow;
                window.Close();
            }
        }
    }
}