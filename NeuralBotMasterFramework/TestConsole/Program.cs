using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;
using NeuralBotMasterFramework.Logic.Algorithms;
using NeuralBotMasterFramework.Logic.PoolGenerators;

namespace TestConsole
{
    class Program
    {
        private static double[][] inputData;
        private static double[][] expectedData;

        private static int dataSetsPerPropagation = 1000;
        private static double[][] currentInputData;
        private static double[][] currentExpectedData;

        const int TOTAL_BITS = 12;
        private static int totalNumberLength = Math.Pow(2, TOTAL_BITS).ToString().Length;

        static void Main(string[] args)
        {
            int totalNetworks = 1000;
            int inputNodes = TOTAL_BITS;
            int hiddenNodes = 10;
            int hiddenLayers = 2;
            int outputNodes = totalNumberLength;

            int networksToKeep = 10;
            int totalRandomNetworks = 10;
            double mutationRate = 0.01;
            double mutationChance = 0.01;

            SetupBinaryData();

            GeneticAlgorithm algorithm = new GeneticAlgorithm(totalNetworks, inputNodes, hiddenNodes, hiddenLayers, outputNodes)
            {
                PoolGenerator = new FitnessBasedPoolGenerator()
            };

            algorithm.NetworksToKeep = networksToKeep;
            algorithm.MutationRate = mutationRate;
            algorithm.MutationChance = mutationChance;
            algorithm.RandomNetworkAmount = totalRandomNetworks;

            Console.WriteLine("Initialization complete");

            int generation = 0;
            while (true)
            {
                Console.WriteLine($"Generation {generation}");
                SetCurrentDataSets();
                algorithm.SetupTest(currentInputData, currentExpectedData);
                Console.WriteLine("Propagating");
                algorithm.PropagateAllNetworks();
                Console.WriteLine("Breeding");
                algorithm.BreedBestNetworks();
                if (generation % 20 == 0)
                {
                    PrintData(algorithm);
                }
                ++generation;
            }
        }

        private static void SetCurrentDataSets()
        {
            int totalDataSets = inputData.Length < dataSetsPerPropagation ? inputData.Length : dataSetsPerPropagation;
            currentInputData = new double[totalDataSets][];
            currentExpectedData = new double[totalDataSets][];

            List<int> indexedDataSets = new List<int>();
            for (int i = 0; i < totalDataSets;)
            {
                int index = NeuralBotMasterFramework.Helper.RandomNumberGenerator.GetNextNumber(0, inputData.Length - 1);
                if (!indexedDataSets.Contains(index))
                {
                    currentInputData[i] = inputData[index];
                    currentExpectedData[i] = expectedData[index];
                    indexedDataSets.Add(index);
                    ++i;
                }
            }
        }

        private static void PrintData(GeneticAlgorithm algorithm)
        {
            for (int i = 0; i < 10; ++i)
            {
                KeyValuePair<IWeightedNetwork, double> networkAndFitness = algorithm.NetworksAndFitness.ElementAt(i);
                Console.WriteLine($"Fitness {networkAndFitness.Value} ID: {networkAndFitness.Key.ID}");
            }
            Console.WriteLine();
        }

        private static void SetupBinaryData()
        {
            int TotalBinaryNumbers = (int)Math.Pow(2, TOTAL_BITS);

            inputData = new double[TotalBinaryNumbers][];
            expectedData = new double[TotalBinaryNumbers][];
            for (int i = 0; i < TotalBinaryNumbers; ++i)
            {
                string binaryString = Convert.ToString(i, 2).PadLeft(TOTAL_BITS, '0');
                inputData[i] = new double[binaryString.Length];
                for (int j = 0; j < binaryString.Length; ++j)
                {
                    inputData[i][j] = binaryString[j] == '1' ? 1 : 0;
                }
                string numberAsString = Convert.ToString(i).PadLeft(totalNumberLength, '0');
                expectedData[i] = new double[numberAsString.Length];
                for (int j = 0; j < numberAsString.Length; ++j)
                {
                    expectedData[i][j] = double.Parse(numberAsString[j].ToString());
                }
            }
        }
    }
}
