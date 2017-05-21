using System.ComponentModel;
using MazeLib;
using MazeMVVM.ModelLib.Communication;

namespace MazeMVVM.ModelLib.Player
{
    public abstract class PlayerModel : IPlayerModel
    {
        protected IClient Client;
        protected volatile bool Stop;

        private Maze maze;

        public Maze Maze
        {
            get { return maze; }
            set
            {
                maze = value;
                // Update VM
                NotifyPropertyChanged("Maze");
            }
        }

        private Position pos;

        public Position Pos
        {
            get { return pos; }

            set
            {
                pos = value;
                // Update VM
                NotifyPropertyChanged("Pos");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client"> Client object </param>
        public PlayerModel(IClient client)
        {
            this.Client = client;
            this.Stop = false;
            this.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
        }

        /// <summary>
        /// Connect To server.
        /// </summary>
        /// <param name="ip"> ip address</param>
        /// <param name="port"> port number </param>
        public void Connect(string ip, int port)
        {
            this.Client.Connect(ip, port);
        }

        /// <summary>
        /// Disconnect from server.
        /// </summary>
        public void Disconnect()
        {
            this.Client.Disconnect();
            Stop = true;
        }

        /// <summary>
        /// Change Player position according thi direction given.
        /// </summary>
        /// <param name="direction"> Direction of movement </param>
        public virtual void Move(Direction direction)
        {
            Pos = CalcPosition(direction, Pos.Row, Pos.Col);
        }


        protected Position CalcPosition(Direction direction, int currentRow, int currentCol)
        {
            Position position;
            // Right
            if (direction == Direction.Right && currentCol < Maze.Cols - 1 &&
                Maze[currentRow, currentCol + 1] == CellType.Free)
            {
                position = new Position(currentRow, currentCol + 1);
            }
            // Left
            else if (direction == Direction.Left && currentCol > 0 &&
                     Maze[currentRow, currentCol - 1] == CellType.Free)
            {
                position = new Position(currentRow, currentCol - 1);
            }
            // Up
            else if (direction == Direction.Up && currentRow > 0 &&
                     Maze[currentRow - 1, currentCol] == CellType.Free)
            {
                position = new Position(currentRow - 1, currentCol);
            }
            // Down
            else if (direction == Direction.Down && currentRow < Maze.Rows - 1 &&
                     Maze[currentRow + 1, currentCol] == CellType.Free)
            {
                position = new Position(currentRow + 1, currentCol);
            }
            else
            {
                position = new Position(currentRow, currentCol);
            }
            return position;
        }



        /// <summary>
        /// Notify that the proprety with the given name has changed.
        /// </summary>
        /// <param name="propName"> The name of the property that changed </param>
        protected void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}