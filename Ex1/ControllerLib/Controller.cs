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
    public class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        public ClientHandler view { set; private get; }
        private Dictionary<string, bool> isCommandToSender;

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

        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            // check if it is existing command
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            ICommand command = commands[commandKey];
            string[] args = arr.Skip(1).ToArray();
            // Check if this a valid command
            Checksum checksum = command.Check(args);
            if (!checksum.Valid)
            {
                JObject errorObj = new JObject();
                errorObj["Error"] = checksum.ErrorMsg;
                return errorObj.ToString();
            }
            TcpClient competitor = null;
            // get the competitor player
            if (!this.isCommandToSender[commandKey])
                competitor = model.GetCompetitorOf(client);
            string answer = null;
            // try to execute the command
            try
            {
                answer = command.Execute(args, client);
            }
            catch (Exception e)
            {
                JObject errorObj = new JObject();
                errorObj["Error"] = e.Message;
                return errorObj.ToString();
            }
            // send the message to competitor if necessary.
            if (!this.isCommandToSender[commandKey])
            {
                view.SendMesaageToCompetitor(competitor, answer);
                answer = "do nothing";
            }
            return answer;
        }

        public bool IsClientInGame(TcpClient client)
        {
            return model.IsClientInGame(client);
        }

    }
}