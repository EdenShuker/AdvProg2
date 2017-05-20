using MazeMVVM.ModelLib.Player;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMVVM.ViewModelLib.Player
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
