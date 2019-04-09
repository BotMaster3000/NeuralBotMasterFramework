using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Interfaces
{
    public interface IWeightedNetwork : INetwork
    {
        int ID { get; set; }
        IWeightedLayer[] HiddenLayers { get; set; }
        IWeightedLayer OutputLayer { get; set; }
    }
}
