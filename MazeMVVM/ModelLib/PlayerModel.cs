﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace MazeMVVM.ModelLib
{
    public abstract class PlayerModel : INotifyPropertyChanged
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

        // ViewModel need to subscribe methods to this event
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client"> Client object </param>
        public PlayerModel(IClient client)
        {
            this.Client = client;
            this.Stop = false;
        }

        /// <summary>
        /// Connect To server.
        /// </summary>
        /// <param name="ip"> ip address</param>
        /// <param name="port"> port number </param>
        public void Connect(string ip, int port)
        {
            this.Client.connect(ip, port);
        }

        /// <summary>
        /// Disconnect from server.
        /// </summary>
        public void Disconnect()
        {
            this.Client.disconnect();
            Stop = true;
        }

        /// <summary>
        /// Change Player position according thi direction given.
        /// </summary>
        /// <param name="direction"> Direction of movement </param>
        public virtual void Move(Direction direction)
        {
            // Takes out player current position.
            int currentRow = Pos.Row;
            int currentCol = Pos.Col;

            // Right
            if (direction == Direction.Right && currentCol < Maze.Cols - 1 &&
                Maze[currentRow, currentCol + 1] == CellType.Free)
            {
                Pos = new Position(currentRow, currentCol + 1);
            }
            // Left
            else if (direction == Direction.Left && currentCol > 0 &&
                     Maze[currentRow, currentCol - 1] == CellType.Free)
            {
                Pos = new Position(currentRow, currentCol - 1);
            }
            // Up
            else if (direction == Direction.Up && currentRow > 0 &&
                     Maze[currentRow - 1, currentCol] == CellType.Free)
            {
                Pos = new Position(currentRow - 1, currentCol);
            }
            // Down
            else if (direction == Direction.Down && currentRow < Maze.Rows - 1 &&
                     Maze[currentRow + 1, currentCol] == CellType.Free)
            {
                Pos = new Position(currentRow + 1, currentCol);
            }
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