using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Helper;
using NeuralBotMasterFramework.Interfaces;

namespace NeuralBotMasterFramework.Logic.PoolGenerators
{
    public class FitnessBasedPoolGenerator : IBreedingPoolGenerator
    {
        public List<IWeightedNetwork> GenerateBreedingPool(Dictionary<IWeightedNetwork, double> networksAndFitness)
        {
            List<IWeightedNetwork> poolList = new List<IWeightedNetwork>();
            double[] fitnesses = MultiplyUntilBiggerThanOne(networksAndFitness.Values.ToArray());
            int totalFitness = (int)Math.Round(fitnesses.Sum(), 0, MidpointRounding.AwayFromZero);
            for (int networkIndex = 0; networkIndex < networksAndFitness.Count; ++networkIndex)
            {
                int totalNetworksToAdd = (int)Math.Round(fitnesses[networkIndex] / totalFitness * networksAndFitness.Count * 100);
                KeyValuePair<IWeightedNetwork, double> currentNetworkAndFitness = networksAndFitness.ElementAt(networkIndex);
                for (int fitness = 0; fitness < totalNetworksToAdd; ++fitness)
                {
                    poolList.Add(currentNetworkAndFitness.Key);
                }
            }
            return poolList;
        }

        private double[] MultiplyUntilBiggerThanOne(double[] values)
        {
            double[] returnValues = (double[])values.Clone();
            while (returnValues.Min() < 1)
            {
                for (int i = 0; i < returnValues.Length; ++i)
                {
                    returnValues[i] *= 10;
                }
            }
            return returnValues;
        }
    }
}
