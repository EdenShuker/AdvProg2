using System.Windows;
using System.Windows.Controls;

namespace MazeMVVM.ViewLib.Controls
{
    /// <summary>
    /// Interaction logic for GameStartMenu.xaml
    /// </summary>
    public partial class GameStartMenu : UserControl
    {
        public GameStartMenu()
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
            DependencyProperty.Register("MazeName", typeof(string), typeof(GameStartMenu), new PropertyMetadata("name"));


        public int Rows
        {
            get { return (int) GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(GameStartMenu), new PropertyMetadata(0));


        public int Cols
        {
            get { return (int) GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(GameStartMenu), new PropertyMetadata(0));

        private void GameStartMenu_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Rows = Properties.Settings.Default.MazeRows;
            this.Cols = Properties.Settings.Default.MazeCols;
        }
    }
}