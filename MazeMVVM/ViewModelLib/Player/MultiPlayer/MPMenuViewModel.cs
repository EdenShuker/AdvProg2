using System.Collections.ObjectModel;
using MazeMVVM.ModelLib.Player.MultiPlayer;

namespace MazeMVVM.ViewModelLib.Player.MultiPlayer
{
    /// <summary>
    /// Multi pplayer menu model.
    /// </summary>
    class MPMenuViewModel : ViewModel
    {
        /// <summary>
        /// Multi player menu model.
        /// </summary>
        private MPMenuModel model;

        /// <summary>
        /// List of available games.
        /// </summary>
        public ObservableCollection<string> AvailablesGames
        {
            get { return this.model.AvailablesGames; }
            set
            {
                this.model.AvailablesGames = value;
                NotifyPropertyChanged("AvailablesGames");
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="menuModel"> menu model </param>
        public MPMenuViewModel(MPMenuModel menuModel)
        {
            this.model = menuModel;
        }

        /// <summary>
        /// Refresh the list of available games.
        /// </summary>
        public void RefreshList()
        {
            this.model.RefreshList();
        }
    }
}