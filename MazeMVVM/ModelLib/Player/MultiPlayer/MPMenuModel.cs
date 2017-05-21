using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MazeMVVM.ModelLib.Communication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MazeMVVM.ModelLib.Player.MultiPlayer
{
    /// <summary>
    /// Multi player menu model.
    /// </summary>
    class MPMenuModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Client for communication.
        /// </summary>
        private IClient Client;

        /// <summary>
        /// List of available games.
        /// </summary>
        private ObservableCollection<string> availablesGames;

        public ObservableCollection<string> AvailablesGames
        {
            get { return this.availablesGames; }
            set
            {
                this.availablesGames = value;
                NotifyPropertyChanged("AvailablesGames");
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client"></param>
        public MPMenuModel(IClient client)
        {
            this.Client = client;
            this.AvailablesGames = new ObservableCollection<string>();
        }

        /// <summary>
        /// Event of property-changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify that the property with the given name changed.
        /// </summary>
        /// <param name="propName"> name of the property that was changed </param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Refresh the list of available games.
        /// </summary>
        public void RefreshList()
        {
            // Get all the available games from the server.
            this.Client.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.Write("list");
            string msg = this.Client.Read();
            if (this.Client.Read() == new JObject().ToString())
            {
                this.Client.Disconnect();
            }
            // Parse the list
            string[] list = JsonConvert.DeserializeObject<string[]>(msg);
            int count = list.Count();
            this.AvailablesGames.Clear();
            for (int i = 0; i < count; i++)
            {
                this.AvailablesGames.Add(list[i]);
            }
        }
    }
}