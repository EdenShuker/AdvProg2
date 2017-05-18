using System;
using System.ComponentModel;
using MazeMVVM.ModelLib.Player;

namespace MazeMVVM.ViewModelLib
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
