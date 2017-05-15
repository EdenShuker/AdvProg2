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

namespace MazeMVVM.ViewLib.Controls
{
    /// <summary>
    /// Interaction logic for MazeDescription.xaml
    /// </summary>
    public partial class MazeDescription : UserControl
    {
        public MazeDescription()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string MazeName
        {
            get { return (string) GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeDescription),
                new PropertyMetadata("name"));

        public int Rows
        {
            get { return (int) GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeDescription), new PropertyMetadata(0));

        public int Cols
        {
            get { return (int) GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeDescription), new PropertyMetadata(0));

        private void MazeDescription_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Rows = Properties.Settings.Default.MazeRows;
            this.Cols = Properties.Settings.Default.MazeCols;
        }
    }
}