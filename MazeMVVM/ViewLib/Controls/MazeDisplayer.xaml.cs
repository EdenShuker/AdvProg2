using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MazeLib;

namespace MazeMVVM.ViewLib.Controls
{
    /// <summary>
    /// Interaction logic for MazeDisplayer.xaml
    /// </summary>
    public partial class MazeDisplayer : UserControl
    {
        /// <summary>
        /// String representing the maze.
        /// </summary>
        public string MazeStr
        {
            get { return (string) GetValue(MazeStrProperty); }
            set { SetValue(MazeStrProperty, value); }
        }

        public static readonly DependencyProperty MazeStrProperty =
            DependencyProperty.Register("MazeStr", typeof(string), typeof(MazeDisplayer), new PropertyMetadata("..."));

        /// <summary>
        /// Initial position of the maze as string.
        /// </summary>
        public string InitPos
        {
            get { return (string) GetValue(InitPosProperty); }
            set { SetValue(InitPosProperty, value); }
        }

        public static readonly DependencyProperty InitPosProperty =
            DependencyProperty.Register("InitPos", typeof(string), typeof(MazeDisplayer), new PropertyMetadata("0"));

        /// <summary>
        /// Goal position of the maze as string.
        /// </summary>
        public string GoalPos
        {
            get { return (string) GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(string), typeof(MazeDisplayer), new PropertyMetadata("0"));

        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows
        {
            get { return (int) GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register
            ("Rows", typeof(int), typeof(MazeDisplayer), new PropertyMetadata(0));


        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Cols
        {
            get { return (int) GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        public static readonly DependencyProperty ColsProperty = DependencyProperty.Register
            ("Cols", typeof(int), typeof(MazeDisplayer), new PropertyMetadata(0));


        /// <summary>
        /// Name of the maze.
        /// </summary>
        public string MazeName
        {
            get { return (string) GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeDisplayer), new PropertyMetadata("maze"));


        /// <summary>
        /// Path to the player image.
        /// </summary>
        public string PlayerImageFile
        {
            get { return (string) GetValue(PlayerImageFileProperty); }
            set { SetValue(PlayerImageFileProperty, value); }
        }

        public static readonly DependencyProperty PlayerImageFileProperty =
            DependencyProperty.Register("PlayerImageFile", typeof(string), typeof(MazeDisplayer),
                new PropertyMetadata("..."));


        /// <summary>
        /// Path to the exit image.
        /// </summary>
        public string ExitImageFile
        {
            get { return (string) GetValue(ExitImageFileProperty); }
            set { SetValue(ExitImageFileProperty, value); }
        }

        public static readonly DependencyProperty ExitImageFileProperty =
            DependencyProperty.Register("ExitImageFile", typeof(string), typeof(MazeDisplayer),
                new PropertyMetadata("..."));

        /// <summary>
        /// Current position of the main player as string.
        /// </summary>
        public string CurrPosition
        {
            get { return (string) GetValue(CurrPositionProperty); }
            set { SetValue(CurrPositionProperty, value); }
        }

        public static readonly DependencyProperty CurrPositionProperty =
            DependencyProperty.Register("CurrPosition", typeof(string), typeof(MazeDisplayer),
                new PropertyMetadata(PosChanged));

        /// <summary>
        /// When the position is changed.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        static void PosChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeDisplayer mazeDisplayer = d as MazeDisplayer;
            mazeDisplayer?.UpdatePlayerLocation((string) e.NewValue);
        }

        /// <summary>
        /// Width of single block in the maze.
        /// </summary>
        private double blockWidth;

        /// <summary>
        /// Height of single block in the maze.
        /// </summary>
        private double blockHeight;

        /// <summary>
        /// Update the player location on the board.
        /// </summary>
        /// <param name="positionStr"></param>
        public void UpdatePlayerLocation(string positionStr)
        {
            if (this.playerImage != null)
            {
                Position position = StringToPosition(positionStr);
                ReplaceObject(this.playerImage, position.Row, position.Col);
            }
        }

        /// <summary>
        /// Set the element position in the canvas.
        /// </summary>
        /// <param name="element"> element to replace </param>
        /// <param name="fromLeft"> cols from left </param>
        /// <param name="fromTop"> rows from top </param>
        private void ReplaceObject(UIElement element, double fromLeft, double fromTop)
        {
            Canvas.SetTop(element, fromLeft * this.blockHeight);
            Canvas.SetLeft(element, fromTop * this.blockWidth);
        }

        /// <summary>
        /// player image.
        /// </summary>
        private Image playerImage;

        /// <summary>
        /// Event of player moved.
        /// </summary>
        public event EventHandler<PlayerMovedEventArgs> PlayerMoved;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MazeDisplayer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On loaded.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void MazeDisplayer_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.blockWidth = Canvas.Width / Cols;
            this.blockHeight = Canvas.Height / Rows;
            DrawComponents();
            Window window = Window.GetWindow(this);
            window.KeyDown += MazeDisplayer_OnKeyDown;
        }

        /// <summary>
        /// Draw the maze components.
        /// </summary>
        private void DrawComponents()
        {
            string str = MazeStr;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    // Create block
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = blockWidth;
                    rectangle.Height = blockHeight;
                    int index = Cols * i + j;
                    char curr = str[index];
                    // Fill the block
                    if (curr == '*' || curr == '#')
                    {
                        rectangle.Fill = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        int blockDescription = int.Parse(curr.ToString());
                        if (blockDescription == 0)
                        {
                            rectangle.Fill = new SolidColorBrush(Colors.White);
                        }
                        else
                        {
                            rectangle.Fill = new SolidColorBrush(Colors.Black);
                        }
                    }
                    // Add it to the maze
                    ReplaceObject(rectangle, i, j);
                    Canvas.Children.Add(rectangle);
                }
            }
            // Load the images.
            LoadImageTo(GoalPos, ExitImageFile);
            LoadImageTo(InitPos, PlayerImageFile);
        }

        /// <summary>
        /// Load an image to the maze.
        /// </summary>
        /// <param name="position"> position as string </param>
        /// <param name="imagePath"> path of the image file </param>
        private void LoadImageTo(string position, string imagePath)
        {
            // Create the image
            Image player = new Image();
            player.Width = blockWidth;
            player.Height = blockHeight;
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("/" + imagePath, UriKind.Relative);
            logo.EndInit();
            player.Source = logo;
            Position currPosition = StringToPosition(position);
            this.playerImage = player;
            // Add it to the maze.
            ReplaceObject(player, currPosition.Row, currPosition.Col);
            Canvas.Children.Add(player);
        }

        /// <summary>
        /// Convert string to position.
        /// </summary>
        /// <param name="position"> position as string </param>
        /// <returns> position as position </returns>
        private static Position StringToPosition(string position)
        {
            int index = position.IndexOf(",", StringComparison.Ordinal);
            int row = int.Parse(position.Substring(1, index - 1));
            int col = int.Parse(position.Substring(index + 1, position.Length - index - 2));
            return new Position(row, col);
        }

        /// <summary>
        /// On key down.
        /// </summary>
        /// <param name="sender"> caller </param>
        /// <param name="e"> args </param>
        private void MazeDisplayer_OnKeyDown(object sender, KeyEventArgs e)
        {
            // Check which direction
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
            // Invoke the event
            PlayerMoved?.Invoke(this, new PlayerMovedEventArgs(direction));
        }
    }
}