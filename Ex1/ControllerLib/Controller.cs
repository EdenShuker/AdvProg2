using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using ServerProject.Command;
using ServerProject.ModelLib;

namespace ServerProject.ControllerLib
{
    public class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
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

        public AnswerInfo ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
            {
                JObject errorObj = new JObject();
                errorObj["Error"] = "Command not found";
                return new AnswerInfo(true, null, errorObj.ToString());
            }

            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            Checksum checksum = command.Check(args);
            if (!checksum.Valid)
            {
                JObject errorObj = new JObject();
                errorObj["Error"] = checksum.ErrorMsg;
                return new AnswerInfo(true, null, errorObj.ToString());
            }

            AnswerInfo answerInfo = null;
            if (isCommandToSender[commandKey])
            {
                answerInfo = new AnswerInfo(true, null);
            }
            else
            {
                answerInfo = new AnswerInfo(false, model.GetCompetitorOf(client));
            }
            string answer = command.Execute(args, client);
            answerInfo.Answer = answer;
            return answerInfo;
        }

        public bool IsClientInGame(TcpClient client)
        {
            return model.IsClientInGame(client);
        }
    }
}