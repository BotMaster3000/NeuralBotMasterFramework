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
        double MutationRate { get; }
        double MutationChance { get; }

        int InputNodes { get; }
        int HiddenNodes { get; }
        int HiddenLayers { get; }
        int OutputNodes { get; }

        double[] CurrentInput { get; }
        double[] CurrentExpected { get; }

        Dictionary<IWeightedNetwork, double> NetworksAndFitness { get; }
        void SetupTest(double[] input, double[] expected);
        void PropagateAllNetworks();
        void CalculateFitnesses();
        void SortByFitness();
        void BreedBestNetworks();
    }
}
