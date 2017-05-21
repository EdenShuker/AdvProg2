using System;
using System.ComponentModel;
using System.Text;
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

        protected string ProduceStrFromMaze(string mazeStr)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in mazeStr)
            {
                if (c != '\r' && c != '\n')
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
    }
}