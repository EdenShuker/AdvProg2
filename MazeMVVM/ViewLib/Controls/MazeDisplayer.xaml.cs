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
using MazeMVVM.ViewModelLib;
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


        public string PlayerImageFile
        {
            get { return (string) GetValue(PlayerImageFileProperty); }
            set { SetValue(PlayerImageFileProperty, value); }
        }

        public static readonly DependencyProperty PlayerImageFileProperty =
            DependencyProperty.Register("PlayerImageFile", typeof(string), typeof(MazeDisplayer),
                new PropertyMetadata("..."));


        public string ExitImageFile
        {
            get { return (string) GetValue(ExitImageFileProperty); }
            set { SetValue(ExitImageFileProperty, value); }
        }

        public static readonly DependencyProperty ExitImageFileProperty =
            DependencyProperty.Register("ExitImageFile", typeof(string), typeof(MazeDisplayer),
                new PropertyMetadata("..."));



        public Position CurrentPosition;
        private Image playerImage;

        public event EventHandler<PlayerMovedEventArgs> PlayerMoved;

        public MazeDisplayer()
        {
            InitializeComponent();
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

        private void DrawBlocks()
        {
            string str = MazeStr;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    // blockDescription will be 0(free block) or 1
                    int index = Cols * i + j;
                    char curr = str[index];
                    Label block = new Label();
                    if (curr == '*' || curr == '#')
                    {
                        block.Background = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        int blockDescription = int.Parse(curr.ToString());
                        if (blockDescription == 0)
                        {
                            block.Background = new SolidColorBrush(Colors.White);
                        }
                        else
                        {
                            block.Background = new SolidColorBrush(Colors.Black);
                        }
                    }
                    Grid.SetRow(block, i);
                    Grid.SetColumn(block, j);
                    grid.Children.Add(block);
                }
            }
            LoadImageTo(GoalPos, ExitImageFile);
            LoadImageTo(InitPos, PlayerImageFile);
        }

        private void LoadImageTo(string position, string imagePath)
        {
            Image player = new Image();
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            // The image path is good only because the minion is in the current project,
            // need to check what happens when the file will be somewhere else.
            logo.UriSource = new Uri("/" + imagePath, UriKind.Relative);
            logo.EndInit();
            player.Source = logo;
            int index = InitPos.IndexOf(",");
            int row = int.Parse(position.Substring(1, index - 1));
            int col = int.Parse(position.Substring(index + 1, position.Length - index - 2));
            this.CurrentPosition = new Position(row, col);
            this.playerImage = player;
            Grid.SetRow(player, row);
            Grid.SetColumn(player, col);
            grid.Children.Add(player);
        }

        private void MazeDisplayer_OnLoaded(object sender, RoutedEventArgs e)
        {
            AdjustGrid();
            DrawBlocks();
            Window window = Window.GetWindow(this);
            window.KeyDown += MazeDisplayer_OnKeyDown;
        }

        private void MazeDisplayer_OnKeyDown(object sender, KeyEventArgs e)
        {
            Direction direction;
            switch (e.Key)
            {
                case Key.Left:
                    direction = Direction.Left;
                    break;
                case Key.Right:
                    direction = Direction.Right;
                    break;
                case Key.Up:
                    direction = Direction.Up;
                    break;
                case Key.Down:
                    direction = Direction.Down;
                    break;
                default:
                    direction = Direction.Unknown;
                    break;
            }
            PlayerMoved?.Invoke(this, new PlayerMovedEventArgs(direction));
        }

        public void UpdatePlayerLocation()
        {
            Grid.SetRow(playerImage, CurrentPosition.Row);
            Grid.SetColumn(playerImage, CurrentPosition.Col);
        }
    }
}