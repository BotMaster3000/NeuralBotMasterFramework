using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Logic.Algorithms;

namespace TestConsole
{
    class Program
    {
        private static double[][] inputData;
        private static double[][] expectedData;

        static void Main(string[] args)
        {
            int totalNetworks = 100;
            int inputNodes = 8;
            int hiddenNodes = 10;
            int hiddenLayers = 4;
            int outputNodes = 3;

            int networksToKeep = 10;
            double mutationRate = 0.2;
            double mutationChance = 0.4;

            SetupBinaryData();

            GeneticAlgorithm algorithm = new GeneticAlgorithm(totalNetworks, inputNodes, hiddenNodes, hiddenLayers, outputNodes);

            algorithm.SetupTest(inputData, expectedData);

            algorithm.NetworksToKeep = networksToKeep;
            algorithm.MutationRate = mutationRate;
            algorithm.MutationChance = mutationChance;

            while (true)
            {
                algorithm.PropagateAllNetworks();
                algorithm.BreedBestNetworks();
            }
        }

        private static void SetupBinaryData()
        {
            inputData = new double[256][];
            expectedData = new double[256][];
            for (int i = 0; i <= 255; ++i)
            {
                string binaryString = Convert.ToString(i, 2).PadLeft(8, '0');
                inputData[i] = new double[binaryString.Length];
                for (int j = 0; j < binaryString.Length; ++j)
                {
                    inputData[i][j] = binaryString[j] == '1' ? 1 : 0;
                }
                string numberAsString = Convert.ToString(i).PadLeft(3, '0');
                expectedData[i] = new double[numberAsString.Length];
                for (int j = 0; j < numberAsString.Length; ++j)
                {
                    expectedData[i][j] = double.Parse(numberAsString[j].ToString());
                }
            }
        }
    }
}
