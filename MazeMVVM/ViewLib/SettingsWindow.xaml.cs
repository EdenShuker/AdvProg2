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
using MazeMVVM.ViewModelLib;
using MazeMVVM.ModelLib;

namespace MazeMVVM.ViewLib
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        // VM
        private SettingsViewModel vm;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();
            ISettingsModel model = new ApplicationSettingsModel();
            vm = new SettingsViewModel(model);
            // define vm as data context
            this.DataContext = vm;
        }

        /// <summary>
        /// Called when OK button is pressed. Save settings and Return to main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            Window win = Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

        /// <summary>
        /// Called when Cancel button is pressed. Return to main window without 
        /// saving changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Window win = Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}