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
        public string MazeStr
        {
            get { return (string) GetValue(MazeStrProperty); }
            set { SetValue(MazeStrProperty, value); }
        }

        public static readonly DependencyProperty MazeStrProperty =
            DependencyProperty.Register("MazeStr", typeof(string), typeof(MazeDisplayer), new PropertyMetadata("..."));


        public string InitPos
        {
            get { return (string) GetValue(InitPosProperty); }
            set { SetValue(InitPosProperty, value); }
        }

        public static readonly DependencyProperty InitPosProperty =
            DependencyProperty.Register("InitPos", typeof(string), typeof(MazeDisplayer), new PropertyMetadata("0"));


        public string GoalPos
        {
            get { return (string) GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(string), typeof(MazeDisplayer), new PropertyMetadata("0"));


        public int Rows
        {
            get { return (int) GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register
            ("Rows", typeof(int), typeof(MazeDisplayer), new PropertyMetadata(0));


        public int Cols
        {
            get { return (int) GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        public static readonly DependencyProperty ColsProperty = DependencyProperty.Register
            ("Cols", typeof(int), typeof(MazeDisplayer), new PropertyMetadata(0));


        public string MazeName
        {
            get { return (string) GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeDisplayer), new PropertyMetadata("maze"));


        public MazeDisplayer()
        {
            InitializeComponent();
            AdjustGrid();
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

        private void DrawBlocks(object sender, RoutedEventArgs e)
        {
            string str = MazeStr;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    // blockDescription will be 0(free block) or 1
                    int index = Cols * i + j;
                    char curr = str[index];
                    TextBlock block = new TextBlock();
                    if (curr == '*' || curr == '#')
                    {
                        block.Background = Brushes.White;
                    }
                    else
                    {
                        int blockDescription = int.Parse(curr.ToString());
                        if (blockDescription == 0)
                        {
                            block.Background = Brushes.White;
                        }
                        else
                        {
                            block.Background = Brushes.Black;
                        }
                    }
                    Grid.SetRow(block, i);
                    Grid.SetColumn(block, j);
                    grid.Children.Add(block);
                }
            }
        }
    }
}