using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Interfaces
{
    public interface IGeneticAlgorithm
    {
        int TotalNetworks { get; }
        int NetworksToKeep { get; }
        int RandomNetworkAmount { get; }
        double MutationRate { get; }
        double MutationChance { get; }

        IBreedingPoolGenerator PoolGenerator { get; }

        int InputNodes { get; }
        int HiddenNodes { get; }
        int HiddenLayers { get; }
        int OutputNodes { get; }

        double[][] CurrentInput { get; }
        double[][] CurrentExpected { get; }

        IList<KeyValuePair<IWeightedNetwork, double>> NetworksAndFitness { get; }
        void SetupTest(double[][] input, double[][] expected);
        void PropagateAllNetworks();
        void SortByFitness();
        void BreedBestNetworks();
        double[] GetFitnesses();
        void SetFitnesses(double[] fitnesses);
        void SetFitness(int networkIndex, double fitness);
        void SetFitness(IWeightedNetwork network, double fitness);
    }
}
