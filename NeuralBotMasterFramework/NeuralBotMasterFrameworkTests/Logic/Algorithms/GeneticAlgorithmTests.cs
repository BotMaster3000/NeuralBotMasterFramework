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

        private double[] Input = new double[]
        {
            RandomNumberGenerator.GetNextDouble(),
            RandomNumberGenerator.GetNextDouble(),
            RandomNumberGenerator.GetNextDouble(),
        };

        private readonly GeneticAlgorithm Algorithm = new GeneticAlgorithm(TOTAL_NETWORKS, INPUT_NODES, HIDDEN_NODES, HIDDEN_LAYERS, OUTPUT_NODES);

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.AreEqual(TOTAL_NETWORKS, Algorithm.TotalNetworks);
            Assert.AreEqual(TOTAL_NETWORKS, Algorithm.NetworksAndFitness.Count);
        }

        [TestMethod]
        public void SetInputTest_ShouldCloneInputData()
        {
            Algorithm.SetInput(Input);
            for (int i = 0; i < Input.Length; ++i)
            {
                Assert.AreEqual(Input[i], Algorithm.CurrentInput[i]);
            }

            Input = new double[]
            {
                RandomNumberGenerator.GetNextDouble(),
            };
            Assert.AreNotEqual(Input.Length, Algorithm.CurrentInput.Length);
        }

        [TestMethod]
        public void PropagateAllNetworksTest()
        {
            Algorithm.SetInput(Input);
            Algorithm.PropagateAllNetworks();
            foreach (WeightedNetwork network in Algorithm.NetworksAndFitness.Keys)
            {
                foreach (IWeightedNode node in network.OutputLayer.Nodes)
                {
                    Assert.AreNotEqual(0, node.Value);
                }
            }
        }

        [TestMethod]
        public void CalculateFitnessesTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void SortByFitnessTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void BreedBestNetworksTest()
        {
            Assert.Fail();
        }
    }
}