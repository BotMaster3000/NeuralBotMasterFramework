﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;

namespace NeuralBotMasterFramework.Models
{
    public class WeightedNode : IWeightedNode
    {
        public double[] Weights { get; set; }
        public double Value { get; set; }

        public void ApplyWeight()
        {
            throw new NotImplementedException();
        }
    }
}
