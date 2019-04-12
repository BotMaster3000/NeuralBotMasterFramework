using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralBotMasterFramework.Logic.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Helper;
using NeuralBotMasterFramework.Logic.Networks;
using NeuralBotMasterFramework.Interfaces;

namespace NeuralBotMasterFramework.Logic.Algorithms.Tests
{
    [TestClass]
    public class GeneticAlgorithmTests
    {
        private const int TOTAL_NETWORKS = 100;
        private const int INPUT_NODES = 3;
        private const int HIDDEN_NODES = 5;
        private const int HIDDEN_LAYERS = 3;
        private const int OUTPUT_NODES = 3;

        private readonly GeneticAlgorithm Algorithm = new GeneticAlgorithm(TOTAL_NETWORKS, INPUT_NODES, HIDDEN_NODES, HIDDEN_LAYERS, OUTPUT_NODES);

        private double[][] Input = new double[][]
        {
            new double[]
            {
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
            },
            new double[]
            {
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
            }
        };

        private double[][] Expected = new double[][]
        {
            new double[]
            {
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
            },
            new double[]
            {
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
            },
        };


        [TestMethod]
        public void ConstructorTest()
        {
            Assert.AreEqual(TOTAL_NETWORKS, Algorithm.TotalNetworks);
            Assert.AreEqual(TOTAL_NETWORKS, Algorithm.NetworksAndFitness.Count);
        }

        [TestMethod]
        public void SetupTest_ShouldCloneInputAndExpectedData()
        {
            Algorithm.SetupTest(Input, Expected);
            for (int i = 0; i < Input.Length; ++i)
            {
                for (int j = 0; j < Input[i].Length; ++j)
                {
                    Assert.AreEqual(Input[i][j], Algorithm.CurrentInput[i][j]);
                }
            }

            for (int i = 0; i < Expected.Length; ++i)
            {
                for (int j = 0; j < Expected[i].Length; ++j)
                {
                    Assert.AreEqual(Expected[i][j], Algorithm.CurrentExpected[i][j]);
                }
            }

            Input = new double[][]
            {
                new double[] { RandomNumberGenerator.GetNextDouble(), }
            };
            Assert.AreNotEqual(Input.Length, Algorithm.CurrentInput.Length);

            Expected = new double[][]
            {
                new double[] { RandomNumberGenerator.GetNextDouble() },
            };
            Assert.AreNotEqual(Expected.Length, Algorithm.CurrentExpected.Length);
        }

        [TestMethod]
        public void SetupTest_SecondParameterShouldBeOptional()
        {
            Algorithm.SetupTest(Input);
            Assert.IsNull(Algorithm.CurrentExpected);
        }

        [TestMethod]
        public void PropagateAllNetworksTest()
        {
            Algorithm.SetupTest(Input, Expected);
            Algorithm.PropagateAllNetworks();
            foreach (IWeightedNetwork network in Algorithm.NetworksAndFitness.Select(x => x.Key))
            {
                foreach (IWeightedNode node in network.OutputLayer.Nodes)
                {
                    Assert.AreNotEqual(0, node.Value);
                }
            }
        }

        [TestMethod]
        public void PropagateAllNetworksTest_NoExpectedInput()
        {
            Algorithm.SetupTest(Input);
            Algorithm.PropagateAllNetworks();
            foreach (IWeightedNetwork network in Algorithm.NetworksAndFitness.Select(x => x.Key))
            {
                foreach (IWeightedNode node in network.OutputLayer.Nodes)
                {
                    Assert.AreNotEqual(0, node.Value);
                }
            }
        }

        [TestMethod]
        public void SortByFitnessTest()
        {
            Algorithm.SetupTest(Input, Expected);
            Algorithm.PropagateAllNetworks();
            Algorithm.SortByFitness();

            Assert.AreEqual(TOTAL_NETWORKS, Algorithm.NetworksAndFitness.Count);

            double previousHighestNumber = Algorithm.NetworksAndFitness.FirstOrDefault().Value;
            for (int i = 1; i < Algorithm.NetworksAndFitness.Count; ++i)
            {
                double currentValue = Algorithm.NetworksAndFitness.Select(x => x.Value).ElementAt(i);
                Assert.IsTrue(previousHighestNumber > currentValue);
                previousHighestNumber = currentValue;
            }
        }

        [TestMethod]
        public void BreedBestNetworksTest()
        {
            Algorithm.SetupTest(Input, Expected);
            Algorithm.PropagateAllNetworks();

            Algorithm.NetworksToKeep = 10;
            Algorithm.MutationRate = 0.2;
            Algorithm.MutationChance = 0.1;

            Algorithm.BreedBestNetworks();

            Assert.AreEqual(TOTAL_NETWORKS, Algorithm.NetworksAndFitness.Count);
            foreach (IWeightedNetwork network in Algorithm.NetworksAndFitness.Select(x => x.Key))
            {
                Assert.IsNotNull(network);
            }
        }

        [TestMethod]
        public void SetAndGetFitnesses_CorrectFitnessAmount()
        {
            double[] fitnessValues = GetRandomDoubleArray(TOTAL_NETWORKS);
            Algorithm.SetFitnesses(fitnessValues);
            double[] resultFitnesses = Algorithm.GetFitnesses();

            Assert.AreEqual(fitnessValues.Length, resultFitnesses.Length);
            for (int i = 0; i < fitnessValues.Length; i++)
            {
                Assert.AreEqual(fitnessValues[i], resultFitnesses[i]);
            }
        }

        [TestMethod]
        public void SetFitnesses_MoreFitnessesThanNetworks_ShouldThrowException()
        {
            double[] fitnessValues = GetRandomDoubleArray(TOTAL_NETWORKS + 5);
            Assert.ThrowsException<ArgumentException>(() => Algorithm.SetFitnesses(fitnessValues), "No Exception when more Fitness-Values than Networks");
        }

        [TestMethod]
        public void SetFitnesses_LessFitnessesThanNetworks_ShouldThrowException()
        {
            const int TOTAL_DOUBLE_VALUES = TOTAL_NETWORKS - 5;
            double[] fitnessValues = GetRandomDoubleArray(TOTAL_DOUBLE_VALUES >= 0 ? TOTAL_DOUBLE_VALUES : 0);
            Assert.ThrowsException<ArgumentException>(() => Algorithm.SetFitnesses(fitnessValues), "No Exception when more Fitness-Values than Networks");
        }

        private double[] GetRandomDoubleArray(int totalValues)
        {
            double[] array = new double[totalValues];
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = RandomNumberGenerator.GetNextDouble();
            }
            return array;
        }

        [TestMethod]
        public void SetFitness_IWeightedNetworkAsParameter()
        {
            double newValue = RandomNumberGenerator.GetNextDouble();
            IWeightedNetwork network = Algorithm.NetworksAndFitness.Select(x => x.Key).ElementAt(0);
            Algorithm.SetFitness(network, newValue);
            Assert.AreEqual(newValue, Algorithm.NetworksAndFitness.FirstOrDefault(x => x.Key == network).Value);
        }

        [TestMethod]
        public void SetFitness_IWeightedNetworkAsParameter_NetworkNotFound_ShouldThrowArgumentException()
        {
            double newValue = RandomNumberGenerator.GetNextDouble();
            IWeightedNetwork network = new WeightedNetwork(INPUT_NODES, HIDDEN_LAYERS, HIDDEN_NODES, OUTPUT_NODES);
            Assert.ThrowsException<ArgumentException>(() => Algorithm.SetFitness(network, newValue), "No Exception when Network not found");
        }

        [TestMethod]
        public void SetFitness_NetworkIndexAsParameter()
        {
            int index = RandomNumberGenerator.GetNextNumber(0, Algorithm.NetworksAndFitness.Count);
            double newFitnessValue = RandomNumberGenerator.GetNextDouble();
            Algorithm.SetFitness(index, newFitnessValue);
            KeyValuePair<IWeightedNetwork, double> networkAndFitness = Algorithm.NetworksAndFitness[index];
            Assert.AreEqual(newFitnessValue, networkAndFitness.Value);
        }
    }
}