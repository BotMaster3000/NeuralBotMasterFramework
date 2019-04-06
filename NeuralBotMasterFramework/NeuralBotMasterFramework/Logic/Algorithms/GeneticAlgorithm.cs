using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;
using NeuralBotMasterFramework.Logic.Networks;
using NeuralBotMasterFramework.Helper;
using NeuralBotMasterFramework.Models;

namespace NeuralBotMasterFramework.Logic.Algorithms
{
    public class GeneticAlgorithm : IGeneticAlgorithm
    {
        public int TotalNetworks { get; }
        public int NetworksToKeep { get; set; }
        public double MutationRate { get; set; }
        public double MutationChance { get; set; }
        public Dictionary<IWeightedNetwork, double> NetworksAndFitness { get; private set; } = new Dictionary<IWeightedNetwork, double>();
        public int InputNodes { get; }
        public int HiddenNodes { get; }
        public int HiddenLayers { get; }
        public int OutputNodes { get; }

        public double[][] CurrentInput { get; private set; }
        public double[][] CurrentExpected { get; private set; }

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

        public void SetupTest(double[][] input, double[][] expected = null)
        {
            CurrentInput = (double[][])input.Clone();
            CurrentExpected = (double[][])expected?.Clone();
        }

        public void PropagateAllNetworks()
        {
            ResetAllFitnesses();
            for (int inputIndex = 0; inputIndex < CurrentInput.Length; inputIndex++)
            {
                foreach (IWeightedNetwork network in NetworksAndFitness.Keys)
                {
                    network.SetInput(CurrentInput[inputIndex]);
                    network.Propagate();
                }
                if (CurrentExpectedIsSet())
                {
                    CalculateFitnesses(CurrentExpected[inputIndex]);
                }
            }
        }

        private bool CurrentExpectedIsSet()
        {
            return CurrentExpected?.Length >= 0;
        }

        private void ResetAllFitnesses()
        {
            Dictionary<IWeightedNetwork, double> tempNetworkAndFitness = new Dictionary<IWeightedNetwork, double>();
            foreach (IWeightedNetwork network in NetworksAndFitness.Keys)
            {
                tempNetworkAndFitness.Add(network, 0);
            }
            NetworksAndFitness = tempNetworkAndFitness;
        }

        private void CalculateFitnesses(double[] expected)
        {
            Dictionary<IWeightedNetwork, double> tempNetworkAndFitness = new Dictionary<IWeightedNetwork, double>();
            foreach (KeyValuePair<IWeightedNetwork, double> networkAndFitness in NetworksAndFitness)
            {
                double fitness = networkAndFitness.Value;
                double[] output = networkAndFitness.Key.GetOutput();
                for (int i = 0; i < output.Length; ++i)
                {
                    fitness += 1 / (Math.Pow(output[i] + expected[i], 2) + 1);
                }
                tempNetworkAndFitness.Add(networkAndFitness.Key, fitness);
            }
            NetworksAndFitness = tempNetworkAndFitness;
        }

        public void SortByFitness()
        {
            NetworksAndFitness = NetworksAndFitness.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public void BreedBestNetworks()
        {
            SortByFitness();
            RemoveUnneccessaryNetworks();
            BreedNewNetworks();
        }

        private void RemoveUnneccessaryNetworks()
        {
            List<KeyValuePair<IWeightedNetwork, double>> tempNetworkAndFitnessToKeep = NetworksAndFitness.Take(NetworksToKeep).ToList();
            NetworksAndFitness = tempNetworkAndFitnessToKeep.ToDictionary(x => x.Key, x => x.Value);
        }

        private void BreedNewNetworks()
        {
            List<IWeightedNetwork> networkAndLikelinesToBreed = GetBreedingPool();
            BreedNetworks(networkAndLikelinesToBreed);
        }

        private List<IWeightedNetwork> GetBreedingPool()
        {
            List<IWeightedNetwork> networkAndLikelinesToBreed = new List<IWeightedNetwork>();
            for (int networkIndex = 0; networkIndex < NetworksToKeep; ++networkIndex)
            {
                IWeightedNetwork currentNetwork = NetworksAndFitness.Keys.ElementAt(networkIndex);
                for (int i = 0; i < NetworksToKeep - networkIndex; ++i)
                {
                    networkAndLikelinesToBreed.Add(currentNetwork);
                }
            }
            return networkAndLikelinesToBreed;
        }

        private void BreedNetworks(List<IWeightedNetwork> networkAndLikelinesToBreed)
        {
            while (NetworksAndFitness.Count < TotalNetworks)
            {
                int networkIndex = RandomNumberGenerator.GetNextNumber(0, networkAndLikelinesToBreed.Count - 1);
                IWeightedNetwork network = BreedNewNetwork(networkAndLikelinesToBreed[networkIndex]);
                NetworksAndFitness.Add(network, 0);
            }
        }

        private IWeightedNetwork BreedNewNetwork(IWeightedNetwork network)
        {
            IWeightedNetwork newNetwork = new WeightedNetwork(InputNodes, HiddenLayers, HiddenNodes, OutputNodes);

            INode[] inputNodes = (INode[])network.InputLayer.Nodes.Clone();
            IWeightedLayer[] hiddenLayers = new IWeightedLayer[HiddenLayers];
            for (int layerIndex = 0; layerIndex < network.HiddenLayers.Length; ++layerIndex)
            {
                hiddenLayers[layerIndex] = new WeightedLayer();
                for (int nodeIndex = 0; nodeIndex < network.HiddenLayers[layerIndex].Nodes.Length; ++nodeIndex)
                {
                    hiddenLayers[layerIndex].Nodes = new IWeightedNode[network.HiddenLayers[layerIndex].Nodes.Length];
                    for (int i = 0; i < hiddenLayers[layerIndex].Nodes.Length; ++i)
                    {
                        hiddenLayers[layerIndex].Nodes[i] = new WeightedNode()
                        {
                            Weights = (double[])network.HiddenLayers[layerIndex].Nodes[i].Weights.Clone()
                        };
                    }
                }
            }
            IWeightedNode[] outputNodes = (IWeightedNode[])network.OutputLayer.Nodes.Clone();
            for (int nodeIndex = 0; nodeIndex < outputNodes.Length; ++nodeIndex)
            {
                outputNodes[nodeIndex] = new WeightedNode()
                {
                    Weights = (double[])network.OutputLayer.Nodes[nodeIndex].Weights.Clone()
                };
            }

            newNetwork.InputLayer.Nodes = inputNodes;
            newNetwork.HiddenLayers = hiddenLayers;
            newNetwork.OutputLayer.Nodes = outputNodes;

            MutateNetwork(newNetwork);

            return newNetwork;
        }

        private void MutateNetwork(IWeightedNetwork network)
        {
            foreach (IWeightedLayer layer in network.HiddenLayers)
            {
                foreach (IWeightedNode node in layer.Nodes)
                {
                    for (int i = 0; i < node.Weights.Length; ++i)
                    {
                        if (RandomNumberGenerator.GetNextDouble() <= MutationChance)
                        {
                            double mutation = MutationRate * RandomNumberGenerator.GetNextDouble();
                            if (RandomNumberGenerator.GetNextDouble() >= 0.5)
                            {
                                mutation *= -1;
                            }
                            node.Weights[i] += mutation;
                        }
                    }
                }
            }
        }

        public double[] GetFitnesses()
        {
            return NetworksAndFitness.Values.ToArray();
        }

        public void SetFitnesses(double[] fitnesses)
        {
            ThrowIfArgumentDoesNotHaveCorrectLength(fitnesses, TotalNetworks);

            Dictionary<IWeightedNetwork, double> tempFitnessAndValues = new Dictionary<IWeightedNetwork, double>();
            for (int i = 0; i < fitnesses.Length; ++i)
            {
                IWeightedNetwork network = NetworksAndFitness.ElementAt(i).Key;
                tempFitnessAndValues.Add(network, fitnesses[i]);
            }
            NetworksAndFitness = tempFitnessAndValues;
        }

        private void ThrowIfArgumentDoesNotHaveCorrectLength(double[] array, int expectedArrayLength)
        {
            if (array.Length != expectedArrayLength)
            {
                throw new ArgumentException($"Argument-Length not valid: Expected: {expectedArrayLength} Actual: {array.Length}");
            }
        }

        public void SetFitness(int networkIndex, double fitness)
        {
            SetFitness(NetworksAndFitness.Keys.ElementAt(networkIndex), fitness);
        }

        public void SetFitness(IWeightedNetwork network, double fitness)
        {
            ThrowIfNetworkNotInDictionary(network);
            NetworksAndFitness[network] = fitness;
        }

        private void ThrowIfNetworkNotInDictionary(IWeightedNetwork network)
        {
            if (!NetworksAndFitness.Keys.Contains(network))
            {
                throw new ArgumentException($"Network not found inside {nameof(NetworksAndFitness)}");
            }
        }
    }
}
