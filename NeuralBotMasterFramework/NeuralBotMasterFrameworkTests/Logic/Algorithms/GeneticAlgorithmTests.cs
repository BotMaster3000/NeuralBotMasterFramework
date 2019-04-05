using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralBotMasterFramework.Logic.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Helper;

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

        [TestMethod]
        public void ConstructorTest()
        {
            GeneticAlgorithm algorithm = new GeneticAlgorithm(TOTAL_NETWORKS, INPUT_NODES, HIDDEN_NODES, HIDDEN_LAYERS, OUTPUT_NODES);
            Assert.AreEqual(TOTAL_NETWORKS, algorithm.TotalNetworks);
            Assert.AreEqual(TOTAL_NETWORKS, algorithm.NetworksAndFitness.Count);
        }

        [TestMethod]
        public void SetInputTest_ShouldCloneInputData()
        {
            double[] input = new double[]
            {
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
                RandomNumberGenerator.GetNextDouble(),
            };
            GeneticAlgorithm algorithm = new GeneticAlgorithm(TOTAL_NETWORKS, INPUT_NODES, HIDDEN_NODES, HIDDEN_LAYERS, OUTPUT_NODES);
            algorithm.SetInput(input);
            for(int i = 0; i < input.Length; ++i)
            {
                Assert.AreEqual(input[i], algorithm.CurrentInput[i]);
            }

            input = new double[]
            {
                RandomNumberGenerator.GetNextDouble(),
            };
            Assert.AreNotEqual(input.Length, algorithm.CurrentInput.Length);
        }

        [TestMethod]
        public void BreedBestNetworksTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CalculateFitnessesTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PropagateAllNetworksTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void SortByFitnessTest()
        {
            Assert.Fail();
        }
    }
}