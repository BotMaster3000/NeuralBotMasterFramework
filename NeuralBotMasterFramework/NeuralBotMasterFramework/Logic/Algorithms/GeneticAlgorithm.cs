using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;
using NeuralBotMasterFramework.Logic.Networks;

namespace NeuralBotMasterFramework.Logic.Algorithms
{
    public class GeneticAlgorithm : IGeneticAlgorithm
    {
        public int TotalNetworks { get; set; }
        public int NetworksToKeep { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double MutationRate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double MutationChance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<IWeightedNetwork, int> NetworksAndFitness { get; set; } = new Dictionary<IWeightedNetwork, int>();
        public int InputNodes { get; set; }
        public int HiddenNodes { get; set; }
        public int HiddenLayers { get; set; }
        public int OutputNodes { get; set; }

        public GeneticAlgorithm(int totalNetworks, int inputNodes, int hiddenNodes, int hiddenLayers, int outputNodes)
        {
            TotalNetworks = totalNetworks;
            InputNodes = inputNodes;
            HiddenNodes = hiddenNodes;
            HiddenLayers = hiddenLayers;
            OutputNodes = outputNodes;

            InitializeNetworks();
        }

        private void InitializeNetworks()
        {
            for (int i = 0; i < TotalNetworks; ++i)
            {
                NetworksAndFitness.Add(new WeightedNetwork(InputNodes, HiddenLayers, HiddenLayers, OutputNodes), 0);
            }
        }

        public void BreedBestNetworks()
        {
            throw new NotImplementedException();
        }

        public void CalculateFitnesses()
        {
            throw new NotImplementedException();
        }

        public void PropagateAllNetworks()
        {
            throw new NotImplementedException();
        }

        public void SetInput(double[] input)
        {
            throw new NotImplementedException();
        }

        public void SortByFitness()
        {
            throw new NotImplementedException();
        }
    }
}
