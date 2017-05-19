using MazeMVVM.ViewLib.Controls;

namespace MazeMVVM.ViewModelLib.Player
{
    public interface ISPViewModel : IPlayerVM
    {

        // Methods

        void RestartGame();

        void SolveMaze();
    }
}
