using MazeMVVM.ModelLib.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMVVM.ViewModelLib.Player
{
    class MPMenuViewModel : ViewModel
    {
        private MPMenuModel model;

        public string[] AvailablesGames => this.model.AvailablesGames;

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
