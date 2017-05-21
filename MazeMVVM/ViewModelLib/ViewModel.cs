using System.ComponentModel;

namespace MazeMVVM.ViewModelLib
{
    /// <summary>
    /// View model
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event of property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify the some property was changed.
        /// </summary>
        /// <param name="propName"> name of the property that was changed </param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}