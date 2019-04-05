using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralBotMasterFramework.Logic.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Logic.Algorithms.Tests
{
    [TestClass]
    public class GeneticAlgorithmTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            const int TOTAL_NETWORKS = 100;
            const int INPUT_NODES = 10;
            const int HIDDEN_NODES = 5;
            const int HIDDEN_LAYERS = 3;
            const int OUTPUT_NODES = 3;
            GeneticAlgorithm algorithm = new GeneticAlgorithm(TOTAL_NETWORKS, INPUT_NODES, HIDDEN_NODES, HIDDEN_LAYERS, OUTPUT_NODES);
            Assert.AreEqual(TOTAL_NETWORKS, algorithm.TotalNetworks);
            Assert.AreEqual(TOTAL_NETWORKS, algorithm.NetworksAndFitness.Count);
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
        public void SetInputTest()
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