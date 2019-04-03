using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;

namespace NeuralBotMasterFramework.Models
{
    public class WeightedLayer : IWeightedLayer
    {
        public IWeightedNode[] Nodes { get; set; }
    }
}
