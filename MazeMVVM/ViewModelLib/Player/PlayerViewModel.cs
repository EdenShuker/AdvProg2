using System;
using System.ComponentModel;
using MazeMVVM.ModelLib.Player;

namespace MazeMVVM.ViewModelLib.Player
{
    public class PlayerViewModel : ViewModel
    {
        protected PlayerViewModel(IPlayerModel model)
        {
            model.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
        }
    }
}