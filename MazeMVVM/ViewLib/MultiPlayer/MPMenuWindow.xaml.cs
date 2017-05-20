using System.Windows;
using MazeMVVM.ModelLib.Player;
using MazeMVVM.ModelLib.Communication;
using MazeMVVM.ViewModelLib.Player;
using System.Threading.Tasks;

namespace MazeMVVM.ViewLib.MultiPlayer
{
    /// <summary>
    /// Interaction logic for MPMenuWindow.xaml
    /// </summary>
    public partial class MPMenuWindow : Window
    {
        private bool isButtonPressed;
        private MPMenuViewModel vm;

        public MPMenuWindow()
        {
            InitializeComponent();
            this.isButtonPressed = false;
            MPMenuModel model = new MPMenuModel(new Client());
            this.vm = new MPMenuViewModel(model);
            this.DataContext = vm;
            StartMenu.bStart.Click += bStart_Click;
        }

        private void bStart_Click(object sender, RoutedEventArgs e)
        {
            this.isButtonPressed = true;
            MultiPlayerModel
                model =
                new MultiPlayerModel(new Client(), StartMenu.MazeName, StartMenu.Rows, StartMenu.Cols);
            ShowGameWindow(model);
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            this.isButtonPressed = true;
            MultiPlayerModel
                model =
                new MultiPlayerModel(new Client(), comboBox.SelectedItem.ToString());
            ShowGameWindow(model);
        }

        private void ShowGameWindow(MultiPlayerModel model)
        {
            var newForm = new MultiPlayer.MPGameWindow(model);
            newForm.Show();
            this.Close();
        }

        private void comboBox_DropDownOpened(object sender, System.EventArgs e)
        {
            this.vm.RefreshList();
        }

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
