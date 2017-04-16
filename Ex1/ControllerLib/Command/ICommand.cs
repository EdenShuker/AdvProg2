using System.Net.Sockets;

namespace ServerProject.ControllerLib.Command
{
    /// <summary>
    /// Command that can be executed.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute a command with the given arguments.
        /// </summary>
        /// <param name="args">arguments of the command.</param>
        /// <param name="client">The client that sent the command.</param>
        /// <returns></returns>
        ForwardMessageEventArgs Execute(string[] args, TcpClient client = null);

        /// <summary>
        /// Check if the arguments are valid for the command.
        /// </summary>
        /// <param name="args">Arguments to check.</param>
        /// <returns>Checksum object that holds all the data about the checking.</returns>
        Checksum Check(string[] args);
    }

    /// <summary>
    /// Object that holds data about a checking that was made.
    /// </summary>
    public struct Checksum
    {
        /// <summary>
        /// Tells if the checking was successful.
        /// </summary>
        public bool Valid;

        /// <summary>
        /// String, which is the error-message.
        /// </summary>
        public string ErrorMsg;
    }
}
