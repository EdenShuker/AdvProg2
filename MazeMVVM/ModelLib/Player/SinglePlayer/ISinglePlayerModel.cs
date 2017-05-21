namespace MazeMVVM.ModelLib.Player.SinglePlayer
{
    public interface ISinglePlayerModel : IPlayerModel
    {
        string SolveMaze();

        void RestartGame();
    }
}