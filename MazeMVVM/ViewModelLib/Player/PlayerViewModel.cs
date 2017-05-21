using System;
using System.ComponentModel;
using System.Text;
using MazeMVVM.ModelLib.Player;

namespace MazeMVVM.ViewModelLib.Player
{
    /// <summary>
    /// Player view model.
    /// </summary>
    public class PlayerViewModel : ViewModel
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model"> player model </param>
        protected PlayerViewModel(IPlayerModel model)
        {
            model.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
        }

        /// <summary>
        /// Create a string representing a maze of the given string.
        /// </summary>
        /// <param name="mazeStr"></param>
        /// <returns></returns>
        protected string ProduceStrFromMaze(string mazeStr)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in mazeStr)
            {
                // Ignore chars of EOL
                if (c != '\r' && c != '\n')
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
    }
}