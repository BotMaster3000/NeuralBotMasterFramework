﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Interfaces
{
    public interface INetwork
    {
        ILayer InputLayer { get; set; }

        void SetInput();
        void Propagate();
        double[] GetOutput();
    }
}
