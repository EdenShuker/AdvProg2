namespace MazeMVVM.ModelLib.Communication
{
    /// <summary>
    /// Object that can communicate.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Connect to the given ip and port.
        /// </summary>
        /// <param name="ip">IP address</param>
        /// <param name="port">Port number</param>
        void Connect(string ip, int port);

        /// <summary>
        /// Write the given command to the other side.
        /// </summary>
        /// <param name="command">command to execute</param>
        void Write(string command);

        /// <summary>
        /// Read string from the other side.
        /// </summary>
        /// <returns>string that was read</returns>
        string Read();

        /// <summary>
        /// Disconnect from the IP and port.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Check if the current client is connected.
        /// </summary>
        /// <returns>true if the client is connected, false otherwise</returns>
        bool IsConnected();
    }
}