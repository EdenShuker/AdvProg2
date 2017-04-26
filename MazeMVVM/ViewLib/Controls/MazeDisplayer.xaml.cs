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
        public string MazeStr { get; set; }
        // the position of the image 
        public string PlayerPosition { get; set; }
        public string ImagePath { get; set; }

        private Grid grid;
        private List<ColumnDefinition> gridCols;
        private List<RowDefinition> gridRows;
        private Label[] walls;
        private Image PlayerDisplayer;

        public MazeDisplayer()
        {
            InitializeComponent();
            this.gridCols = new List<ColumnDefinition>();
            this.gridRows = new List<RowDefinition>();
            DFSMazeGenerator generator = new DFSMazeGenerator();
            Maze demoMaze = generator.Generate(10, 10);
            this.MazeStr = demoMaze.ToJSON();
            if (this.MazeStr != null)
            {
                Maze maze = Maze.FromJSON(this.MazeStr);
                CreateGrid(maze.Rows, maze.Cols);
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
                            this.grid.Children.Add(lb);
                        }
                    }
                }
                this.PlayerPosition = maze.InitialPos.ToString();
                //this.PlayerDisplayer = new Image();
                //ImageSourceConverter converter = new ImageSourceConverter();
                //this.PlayerDisplayer.Source = (ImageSource)converter.ConvertFromString(this.ImagePath);
                //Grid.SetRow(this.PlayerDisplayer, maze.InitialPos.Row);
                //Grid.SetColumn(this.PlayerDisplayer, maze.InitialPos.Col);
                //this.grid.Children.Add(this.PlayerDisplayer);
            }
        }

        private void CreateGrid(int numRows, int numCols)
        {
            this.grid = new Grid();
            this.grid.Width = 400;
            this.grid.Height = 400;
            this.grid.HorizontalAlignment = HorizontalAlignment.Center;
            this.grid.VerticalAlignment = VerticalAlignment.Bottom;
            this.grid.ShowGridLines = true;
            gr.Children.Add(this.grid);

            int i;
            // Create Columns
            for (i = 0; i < numCols; i++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                this.grid.ColumnDefinitions.Add(gridCol);
                this.gridCols.Add(gridCol);
            }

            // Create Rows
            for (i = 0; i < numRows; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                this.grid.RowDefinitions.Add(gridRow);
                this.gridRows.Add(gridRow);
            }
        }


    }
}

