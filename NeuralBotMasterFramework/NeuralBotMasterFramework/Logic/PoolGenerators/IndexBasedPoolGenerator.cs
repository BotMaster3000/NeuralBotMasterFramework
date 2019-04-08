using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;

namespace NeuralBotMasterFramework.Logic.PoolGenerators
{
    public class IndexBasedPoolGenerator : IBreedingPoolGenerator
    {
        public List<IWeightedNetwork> GenerateBreedingPool(Dictionary<IWeightedNetwork, double> networksAndFitness)
        {
            List<IWeightedNetwork> poolList = new List<IWeightedNetwork>();
            for (int networkIndex = 0; networkIndex < networksAndFitness.Count; ++networkIndex)
            {
                IWeightedNetwork currentNetwork = networksAndFitness.Keys.ElementAt(networkIndex);
                for (int i = 0; i < networksAndFitness.Count - networkIndex; ++i)
                {
                    poolList.Add(currentNetwork);
                }
            }
            return poolList;
        }
    }
}
