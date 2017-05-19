using MazeMVVM.ModelLib.Communication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMVVM.ModelLib.Player
{
    class MPMenuModel : INotifyPropertyChanged
    {
        private IClient Client;

        private string[] availablesGames;
        public string[] AvailablesGames
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
            JObject msg = JObject.Parse(this.Client.Read());
            if (this.Client.Read() == new JObject().ToString())
            {
                this.Client.Disconnect();
            }
            this.AvailablesGames = JsonConvert.DeserializeObject<string[]>(msg.ToString());
        }
    }
}
