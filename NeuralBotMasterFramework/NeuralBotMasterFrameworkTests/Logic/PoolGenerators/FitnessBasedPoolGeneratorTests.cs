using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralBotMasterFramework.Logic.PoolGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Logic.Algorithms;
using NeuralBotMasterFramework.Helper;
using NeuralBotMasterFramework.Interfaces;

namespace NeuralBotMasterFramework.Logic.PoolGenerators.Tests
{
    [TestClass]
    public class FitnessBasedPoolGeneratorTests
    {
        const int TOTAL_NETWORKS = 15;
        const int INPUT_NODES = 5;
        const int HIDDEN_NODES = 5;
        const int HIDDEN_LAYERS = 5;
        const int OUTPUT_NODES = 1;
        const int NETWORKS_TO_KEEP = 10;
        GeneticAlgorithm algorithm = new GeneticAlgorithm(TOTAL_NETWORKS, INPUT_NODES, HIDDEN_NODES, HIDDEN_LAYERS, OUTPUT_NODES);
        [TestMethod]
        public void GenerateBreedingPoolTest()
        {
            double[][] inputData = new double[][] { GetRandomDoubleArray(INPUT_NODES) };
            double[][] expectedData = new double[][] { GetRandomDoubleArray(OUTPUT_NODES) };
            algorithm.SetupTest(inputData, expectedData);
            algorithm.PropagateAllNetworks();
            algorithm.SortByFitness();
            FitnessBasedPoolGenerator generator = new FitnessBasedPoolGenerator();
            List<IWeightedNetwork> networkList = generator.GenerateBreedingPool(algorithm.NetworksAndFitness.Take(NETWORKS_TO_KEEP).ToDictionary(x => x.Key, x => x.Value));
            Assert.IsTrue(networkList.Count > 0);
        }

        [TestMethod]
        [Timeout(500)]
        public void GenerateBreedingPool_ZeroFitnessScore_ShouldIgnoreZeroFitnesses()
        {
            algorithm.SetFitness(0, 100);
            IBreedingPoolGenerator generator = new FitnessBasedPoolGenerator();
            List<IWeightedNetwork> networkList = generator.GenerateBreedingPool(algorithm.NetworksAndFitness.Take(NETWORKS_TO_KEEP).ToDictionary(x => x.Key, x => x.Value));
            Assert.IsTrue(networkList.Count > 0);
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
    }
}