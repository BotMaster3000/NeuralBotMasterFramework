using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Interfaces
{
    public interface IBreedingPoolGenerator
    {
        List<IWeightedNetwork> GenerateBreedingPool(IList<KeyValuePair<IWeightedNetwork, double>> networksAndFitness);
    }
}
