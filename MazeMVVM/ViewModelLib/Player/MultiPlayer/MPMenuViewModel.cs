using System.Collections.ObjectModel;
using MazeMVVM.ModelLib.Player.MultiPlayer;

namespace MazeMVVM.ViewModelLib.Player.MultiPlayer
{
    class MPMenuViewModel : ViewModel
    {
        private MPMenuModel model;

        public ObservableCollection<string> AvailablesGames
        {
            get
            {
                return this.model.AvailablesGames;
            }
            set
            {
                this.model.AvailablesGames = value;
                NotifyPropertyChanged("AvailablesGames");
            }
        }

        public MPMenuViewModel(MPMenuModel menuModel)
        {
            this.model = menuModel;
        }


        public void RefreshList()
        {
            this.model.RefreshList();
        }
    }
}
