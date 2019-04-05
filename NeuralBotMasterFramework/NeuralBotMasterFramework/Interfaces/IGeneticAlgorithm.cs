using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Interfaces
{
    public interface IGeneticAlgorithm
    {
        int TotalNetworks { get; set; }
        int NetworksToKeep { get; set; }
        double MutationRate { get; set; }
        double MutationChance { get; set; }

        Dictionary<IWeightedNetwork, int> NetworksAndFitness { get; set; }
        void SetInput(double[] input);
        void PropagateAllNetworks();
        void CalculateFitnesses();
        void SortByFitness();
        void BreedBestNetworks();
    }
}
