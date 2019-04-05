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
        public int TotalNetworks { get; private set; }
        public int NetworksToKeep { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double MutationRate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double MutationChance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<IWeightedNetwork, double> NetworksAndFitness { get; private set; } = new Dictionary<IWeightedNetwork, double>();
        public int InputNodes { get; private set; }
        public int HiddenNodes { get; private set; }
        public int HiddenLayers { get; private set; }
        public int OutputNodes { get; private set; }

        public double[] CurrentInput { get; private set; }
        public double[] CurrentExpected { get; private set; }

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

        public void SetupTest(double[] input, double[] expected)
        {
            CurrentInput = (double[])input.Clone();
            CurrentExpected = (double[])expected.Clone();
            foreach (IWeightedNetwork network in NetworksAndFitness.Keys)
            {
                network.SetInput(CurrentInput);
            }
        }

        public void PropagateAllNetworks()
        {
            foreach (IWeightedNetwork network in NetworksAndFitness.Keys)
            {
                network.Propagate();
            }
        }

        public void CalculateFitnesses()
        {
            Dictionary<IWeightedNetwork, double> tempNetworkAndFitness = new Dictionary<IWeightedNetwork, double>();
            foreach (IWeightedNetwork network in NetworksAndFitness.Keys)
            {
                double fitness = 0;
                double[] output = network.GetOutput();
                for (int i = 0; i < output.Length; ++i)
                {
                    fitness += 1 / Math.Pow(output[i] + CurrentExpected[i], 2);
                }
                tempNetworkAndFitness.Add(network, fitness);
            }
            NetworksAndFitness = tempNetworkAndFitness;
        }

        public void SortByFitness()
        {
            throw new NotImplementedException();
        }

        public void BreedBestNetworks()
        {
            throw new NotImplementedException();
        }
    }
}
