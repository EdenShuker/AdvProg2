﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMVVM.ModelLib
{
    public interface IClient
    {
        void connect(string ip, int port);
        void write(string command);
        string read();  // blocking call 
        void disconnect();
    }
}