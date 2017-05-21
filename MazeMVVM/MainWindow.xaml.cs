using System.Windows;
using SettingsWindow = MazeMVVM.ViewLib.Settings.SettingsWindow;
using SPMenuWindow = MazeMVVM.ViewLib.SinglePlayer.SPMenuWindow;
using MPMenuWindow = MazeMVVM.ViewLib.MultiPlayer.MPMenuWindow;

namespace MazeMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bSP_Click(object sender, RoutedEventArgs e)
        {
            Window newForm = new SPMenuWindow();
            newForm.Show();
            this.Hide();
        }

        private void bMP_Click(object sender, RoutedEventArgs e)
        {
            Window newForm = new MPMenuWindow();
            newForm.Show();
            this.Hide();
        }

        private void bSettings_Click(object sender, RoutedEventArgs e)
        {
            Window newForm = new SettingsWindow();
            newForm.Show();
            this.Hide();
        }
    }
}
