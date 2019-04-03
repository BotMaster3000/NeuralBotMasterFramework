using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Interfaces
{
    public interface IWeightedNetwork : INetwork
    {
        new IWeightedLayer[] HiddenLayers { get; set; }
        new IWeightedLayer OutputLayer { get; set; }
    }
}
