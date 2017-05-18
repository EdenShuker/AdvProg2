﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;

namespace MazeMVVM.ModelLib
{
    public class SinglePlayerModel : PlayerModel
    {
        public SinglePlayerModel(IClient client, string nameOfGame, int rows, int cols) : base(client)
        {
            this.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.Write($"generate {nameOfGame} {rows} {cols}");
            this.Maze = Maze.FromJSON(this.Client.Read());
            this.Pos = this.Maze.InitialPos;
            if (this.Client.Read() == new JObject().ToString())
            {
                this.Disconnect();
            }
        }

        public void RestartGame()
        {
            Pos = this.Maze.InitialPos;
        }

        public string SolveMaze()
        {
            this.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.Write($"solve {this.Maze.Name} {Properties.Settings.Default.SearchAlgorithm}");
            JObject solution = JObject.Parse(this.Client.Read());
            if (this.Client.Read() == new JObject().ToString())
            {
                this.Disconnect();
            }
            return solution["Solution"].ToString();
        }
    }
}