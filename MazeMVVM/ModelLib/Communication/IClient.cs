namespace MazeMVVM.ModelLib.Communication
{
    public interface IClient
    {
        void Connect(string ip, int port);

        void Write(string command);

        string Read();

        void Disconnect();

        bool IsConnected();
    }
}