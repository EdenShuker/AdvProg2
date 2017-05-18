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


        public string MsgWhenGoalReached
        {
            get { return (string) GetValue(MsgWhenGoalReachedProperty); }
            set { SetValue(MsgWhenGoalReachedProperty, value); }
        }

        public static readonly DependencyProperty MsgWhenGoalReachedProperty =
            DependencyProperty.Register("MsgWhenGoalReached", typeof(string), typeof(MazeDisplayer),
                new PropertyMetadata("Message..."));

        public string CurrPosition
        {
            get { return (string) GetValue(CurrPositionProperty); }
            set { SetValue(CurrPositionProperty, value); }
        }

        public static readonly DependencyProperty CurrPositionProperty =
            DependencyProperty.Register("CurrPosition", typeof(string), typeof(MazeDisplayer),
                new PropertyMetadata(PosChanged));

        static void PosChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeDisplayer mazeDisplayer = d as MazeDisplayer;
            mazeDisplayer?.UpdatePlayerLocation((string) e.NewValue);
        }

        private double blockWidth;
        private double blockHeight;

        public void UpdatePlayerLocation(string positionStr)
        {
            if (this.playerImage != null)
            {
                Position position = StringToPosition(positionStr);
                ReplaceObject(this.playerImage, position.Row, position.Col);
            }
            // Check if player reached the goal
            if (this.CurrPosition == this.GoalPos)
            {
                MessageWindow msgWindow = new MessageWindow();
                msgWindow.Msg = this.MsgWhenGoalReached;
                msgWindow.Show();
            }
        }

        private void ReplaceObject(UIElement element, double fromLeft, double fromTop)
        {
            Canvas.SetTop(element, fromLeft * this.blockHeight);
            Canvas.SetLeft(element, fromTop * this.blockWidth);
        }

        private Image playerImage;

        public event EventHandler<PlayerMovedEventArgs> PlayerMoved;

        public MazeDisplayer()
        {
            InitializeComponent();
        }

        private void MazeDisplayer_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.blockWidth = Canvas.Width / Cols;
            this.blockHeight = Canvas.Height / Rows;
            DrawComponents();
            Window window = Window.GetWindow(this);
            window.KeyDown += MazeDisplayer_OnKeyDown;
        }

        private void DrawComponents()
        {
            string str = MazeStr;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = blockWidth;
                    rectangle.Height = blockHeight;
                    int index = Cols * i + j;
                    char curr = str[index];
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
                    ReplaceObject(rectangle, i, j);
                    Canvas.Children.Add(rectangle);
                }
            }
            LoadImageTo(GoalPos, ExitImageFile);
            LoadImageTo(InitPos, PlayerImageFile);
        }

        private void LoadImageTo(string position, string imagePath)
        {
            Image player = new Image();
            player.Width = blockWidth;
            player.Height = blockHeight;
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            // The image path is good only because the minion is in the current project,
            // need to check what happens when the file will be somewhere else.
            logo.UriSource = new Uri("/" + imagePath, UriKind.Relative);
            logo.EndInit();
            player.Source = logo;
            Position currPosition = StringToPosition(position);
            this.playerImage = player;

            ReplaceObject(player, currPosition.Row, currPosition.Col);
            Canvas.Children.Add(player);
        }

        private static Position StringToPosition(string position)
        {
            int index = position.IndexOf(",", StringComparison.Ordinal);
            int row = int.Parse(position.Substring(1, index - 1));
            int col = int.Parse(position.Substring(index + 1, position.Length - index - 2));
            return new Position(row, col);
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
    }
}