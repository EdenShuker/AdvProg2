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
        public Image PlayerDisplayer { get; set; }

        private Grid grid;
        private List<ColumnDefinition> gridCols;
        private List<RowDefinition> gridRows;
        private Label[] walls;

        public MazeDisplayer()
        {
            InitializeComponent();
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

