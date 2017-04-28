using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMVVM.ModelLib
{
    public class MultiPlayerModel : PlayerModel
    {
        public Position PosOtherPlayer
        {
            get
            {
                return PosOtherPlayer;
            }
            set
            {
                PosOtherPlayer = value;
                NotifyPropertyChanged("PosOtherPlayer");
            }
        }

        public MultiPlayerModel(IClient client) : base(client) { }

        public override void Start()
        {
            throw new NotImplementedException();
        }
    }
}
