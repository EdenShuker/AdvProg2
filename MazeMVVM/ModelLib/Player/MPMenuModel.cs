using MazeMVVM.ModelLib.Communication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMVVM.ModelLib.Player
{
    class MPMenuModel : INotifyPropertyChanged
    {
        private IClient Client;

        private ObservableCollection<string> availablesGames;
        public ObservableCollection<string> AvailablesGames
        {
            get
            {
                return this.availablesGames;
            }
            set
            {
                this.availablesGames = value;
                NotifyPropertyChanged("AvailablesGames");
            }
        }

        public MPMenuModel(IClient client)
        {
            this.Client = client;
            this.AvailablesGames = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void RefreshList()
        {
            this.Client.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
            this.Client.Write("list");
            string msg = this.Client.Read();
            if (this.Client.Read() == new JObject().ToString())
            {
                this.Client.Disconnect();
            }
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
