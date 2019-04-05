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

        private readonly GeneticAlgorithm Algorithm = new GeneticAlgorithm(TOTAL_NETWORKS, INPUT_NODES, HIDDEN_NODES, HIDDEN_LAYERS, OUTPUT_NODES);

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
                new double[] {
                RandomNumberGenerator.GetNextDouble(),
                }
            };
            Assert.AreNotEqual(Input.Length, Algorithm.CurrentInput.Length);

            Expected = new double[][]
            {
                new double[] {
                RandomNumberGenerator.GetNextDouble(),
                },
            };
            Assert.AreNotEqual(Expected.Length, Algorithm.CurrentExpected.Length);
        }

        [TestMethod]
        public void PropagateAllNetworksTest()
        {
            Algorithm.SetupTest(Input, Expected);
            Algorithm.PropagateAllNetworks();
            foreach (IWeightedNetwork network in Algorithm.NetworksAndFitness.Keys)
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
                double currentValue = Algorithm.NetworksAndFitness.Values.ElementAt(i);
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
            foreach (IWeightedNetwork network in Algorithm.NetworksAndFitness.Keys)
            {
                Assert.IsNotNull(network);
            }
        }
    }
}