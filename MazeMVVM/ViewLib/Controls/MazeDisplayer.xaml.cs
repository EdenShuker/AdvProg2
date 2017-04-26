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
        // TODO: how to get the maze
        // TODO: setters don't work
        public string MazeStr { get; set; }
        // the position of the image 
        public string PlayerPosition { get; set; }
        public string ImagePath { get; set; }

        public MazeDisplayer()
        {
            InitializeComponent();
            DFSMazeGenerator generator = new DFSMazeGenerator();
            Maze demoMaze = generator.Generate(10, 10);
            this.MazeStr = demoMaze.ToJSON();
            if (this.MazeStr != null)
            {
                Maze maze = Maze.FromJSON(this.MazeStr);
                AdjustGrid(maze.Rows, maze.Cols);
                // Add walls as labels
                for (int i = 0; i < maze.Cols; i++)
                {
                    for (int j = 0; j < maze.Rows; j++)
                    {
                        if (maze[i, j] == CellType.Wall)
                        {
                            Label lb = new Label();
                            lb.Background = new SolidColorBrush(Colors.Black);
                            Grid.SetRow(lb, j);
                            Grid.SetColumn(lb, i);
                            // Add the labels as grid's children
                           grid.Children.Add(lb);
                        }
                    }
                }
                Image player = new Image();
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri("C:/Users/Eden/Source/Repos/AdvProg2/MazeMVVM/minion.gif");
                logo.EndInit();
                player.Source = logo;
                Grid.SetRow(player, maze.InitialPos.Row);
                Grid.SetColumn(player, maze.InitialPos.Col);
                grid.Children.Add(player);

            }
        }

        private void AdjustGrid(int numRows, int numCols)
        {
            int i;
            // Create Columns
            for (i = 0; i < numCols; i++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                grid.ColumnDefinitions.Add(gridCol);
            }

            // Create Rows
            for (i = 0; i < numRows; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                grid.RowDefinitions.Add(gridRow);
            }
        }
    }
}





