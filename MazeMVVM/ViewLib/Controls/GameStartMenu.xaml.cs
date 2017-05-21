using System.Windows;
using System.Windows.Controls;

namespace MazeMVVM.ViewLib.Controls
{
    /// <summary>
    /// Interaction logic for GameStartMenu.xaml
    /// </summary>
    public partial class GameStartMenu : UserControl
    {
        /// <summary>
        /// Constrctor.
        /// </summary>
        public GameStartMenu()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Name of the maze.
        /// </summary>
        public string MazeName
        {
            get { return (string) GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(GameStartMenu), new PropertyMetadata("name"));


        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows
        {
            get { return (int) GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(GameStartMenu), new PropertyMetadata(0));


        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Cols
        {
            get { return (int) GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(GameStartMenu), new PropertyMetadata(0));

        /// <summary>
        /// When the control is loaded.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void GameStartMenu_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Get the rows and cols.
            this.Rows = Properties.Settings.Default.MazeRows;
            this.Cols = Properties.Settings.Default.MazeCols;
        }
    }
}