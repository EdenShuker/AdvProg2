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
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the user clicked on the "single player button".
        /// </summary>
        /// <param name="sender">caller.</param>
        /// <param name="e">argument.</param>
        private void bSP_Click(object sender, RoutedEventArgs e)
        {
            // Create Single-Player window and hide the main window.
            Window newForm = new SPMenuWindow();
            newForm.Show();
            this.Hide();
        }

        /// <summary>
        /// When the user clicked on the "multi player button".
        /// </summary>
        /// <param name="sender">caller.</param>
        /// <param name="e">argument.</param>
        private void bMP_Click(object sender, RoutedEventArgs e)
        {
            // Create Multi-Player window and hide the main window.
            Window newForm = new MPMenuWindow();
            newForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Go to the settings of the application.
        /// </summary>
        /// <param name="sender">caller.</param>
        /// <param name="e">argument.</param>
        private void bSettings_Click(object sender, RoutedEventArgs e)
        {
            // Open the settings-window
            Window newForm = new SettingsWindow();
            newForm.Show();
            this.Hide();
        }
    }
}