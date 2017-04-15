using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using ServerProject.Command;
using ServerProject.ModelLib;
using ServerProject.ViewLib;
using Newtonsoft.Json.Linq;
using System;

namespace ServerProject.ControllerLib
{
    /// <summary>
    /// 
    /// </summary>
    public class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        public IClientHandler View { set; private get; }
        private Dictionary<string, bool> isCommandToSender;

        public event EventHandler<ForwardMessageEventArgs> ForwardMessageEvent;

        public Controller()
        {
            this.model = new Model();
            // Add Commands
            this.commands = new Dictionary<string, ICommand>();
            this.commands.Add("generate", new GenerateMazeCommand(this.model));
            this.commands.Add("solve", new SolveMazeCommand(this.model));
            this.commands.Add("start", new StartGameCommand(this.model));
            this.commands.Add("list", new ListCommand(this.model));
            this.commands.Add("join", new JoinToGameCommand(this.model));
            this.commands.Add("play", new PlayCommand(this.model));
            this.commands.Add("close", new CloseGameCommand(this.model));

            this.isCommandToSender = new Dictionary<string, bool>();
            this.isCommandToSender.Add("generate", true);
            this.isCommandToSender.Add("solve", true);
            this.isCommandToSender.Add("start", true);
            this.isCommandToSender.Add("list", true);
            this.isCommandToSender.Add("join", true);
            this.isCommandToSender.Add("play", false);
            this.isCommandToSender.Add("close", false);
        }

        public void ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];

            // check if it is existing command
            if (!commands.ContainsKey(commandKey))
            {
                HandleErrorOf(client, "Command not found");
                return;
            }

            ICommand command = commands[commandKey];
            string[] args = arr.Skip(1).ToArray();
            // Check if this a valid command
            Checksum checksum = command.Check(args);
            if (!checksum.Valid)
            {
                HandleErrorOf(client, checksum.ErrorMsg);
                return;
            }

            // try to execute the command
            try
            {
                ForwardMessageEventArgs eventArgs = command.Execute(args, client);
                this.ForwardMessageEvent?.Invoke(this, eventArgs);
            }
            catch (Exception e)
            {
                HandleErrorOf(client, e.Message);
            }
        }

        private void HandleErrorOf(TcpClient client, string errorMsg)
        {
            JObject errorObj = new JObject();
            errorObj["Error"] = errorMsg;
            ForwardMessageEventArgs eventArgs = new ForwardMessageEventArgs(client, errorObj.ToString());
            this.ForwardMessageEvent?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// return if client in game.
        /// </summary>
        /// <param name="client"></param>
        /// <returns> wether the client is in existing game </returns>
        public bool ProceedConnectionWith(TcpClient client)
        {
            return model.IsClientInGame(client);
        }
    }
}