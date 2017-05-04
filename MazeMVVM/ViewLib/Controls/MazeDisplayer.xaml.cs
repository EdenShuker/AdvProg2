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
using MazeLib;
using MazeGeneratorLib;
using Newtonsoft.Json;

namespace MazeMVVM.ViewLib.Controls
{
    /// <summary>
    /// Interaction logic for MazeDisplayer.xaml
    /// </summary>
    public partial class MazeDisplayer : UserControl
    {

        //public string MazeStr { get; set; }

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register
            ("Rows", typeof(int), typeof(MazeDisplayer), new PropertyMetadata(0));

        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }
        public static readonly DependencyProperty ColsProperty = DependencyProperty.Register
            ("Cols", typeof(int), typeof(MazeDisplayer), new PropertyMetadata(0));

        //public string MazeName
        //{
        //    get { return (string)GetValue(MazeNameProperty); }
        //    set { SetValue(MazeNameProperty, value); }
        //}
        //public static readonly DependencyProperty MazeNameProperty = DependencyProperty.Register
        //   ("MazeName", typeof(string), typeof(MazeDisplayer), new PropertyMetadata(0));


        //public string InitialPos { get; set; }
        //public string GoalPos { get; set; }
        //public string PlayerImageFile { get; set; }
        //public string ExitImageFile { get; set; }
        //private Image playerImg;
        //private string currPos;
        //public string CurrPos
        //{
        //    get
        //    {
        //        return currPos; ;
        //    }
        //    set
        //    {
        //        currPos = value;
        //        string[] dim = currPos.Split(',');
        //        int row = Int32.Parse(dim[0]);
        //        int col = Int32.Parse(dim[1]);
        //        Grid.SetRow(playerImg, row);
        //        Grid.SetColumn(playerImg, col);
        //    }
        //}


        public MazeDisplayer()
        {
            InitializeComponent();
            AdjustGrid();
            TextBlock tb = new TextBlock();
            tb.Text = Rows.ToString();
            grid.Children.Add(tb);
            //DFSMazeGenerator generator = new DFSMazeGenerator();
            //Maze demoMaze = generator.Generate(10, 10);
            //this.MazeStr = demoMaze.ToJSON();
            //if (this.MazeStr != null)
            //{
            //    Maze maze = Maze.FromJSON(this.MazeStr);
            //    AdjustGrid(maze.Rows, maze.Cols);
            //    // Add walls as labels
            //    for (int i = 0; i < maze.Cols; i++)
            //    {
            //        for (int j = 0; j < maze.Rows; j++)
            //        {
            //            if (maze[i, j] == CellType.Wall)
            //            {
            //                Label lb = new Label();
            //                lb.Background = new SolidColorBrush(Colors.Black);
            //                Grid.SetRow(lb, j);
            //                Grid.SetColumn(lb, i);
            //                // Add the labels as grid's children
            //                grid.Children.Add(lb);
            //            }
            //        }
            //    }
            //    Image player = new Image();
            //    BitmapImage logo = new BitmapImage();
            //    logo.BeginInit();
            //    logo.UriSource = new Uri("C:/Users/Eden/Source/Repos/AdvProg2/MazeMVVM/minion.gif");
            //    logo.EndInit();
            //    player.Source = logo;
            //    Grid.SetRow(player, maze.InitialPos.Row);
            //    Grid.SetColumn(player, maze.InitialPos.Col);
            //    grid.Children.Add(player);

        }

        private void AdjustGrid()
        {
            int i;
            // Create Columns
            for (i = 0; i < Cols; i++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                grid.ColumnDefinitions.Add(gridCol);
            }

            // Create Rows
            for (i = 0; i < Rows; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                grid.RowDefinitions.Add(gridRow);
            }
            grid.ShowGridLines = true;
        }
    }
}


