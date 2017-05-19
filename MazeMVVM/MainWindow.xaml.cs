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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MazeMVVM.ViewLib;
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
