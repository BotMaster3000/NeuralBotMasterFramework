﻿using System;
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

        const int TOTAL_BITS = 3;
        private static int totalNumberLength = Math.Pow(2, TOTAL_BITS).ToString().Length;

        static void Main(string[] args)
        {
            int totalNetworks = 1000;
            int inputNodes = TOTAL_BITS;
            int hiddenNodes = 10;
            int hiddenLayers = 3;
            int outputNodes = totalNumberLength;

            int networksToKeep = 10;
            double mutationRate = 0.2;
            double mutationChance = 0.4;

            SetupBinaryData();

            GeneticAlgorithm algorithm = new GeneticAlgorithm(totalNetworks, inputNodes, hiddenNodes, hiddenLayers, outputNodes);

            algorithm.SetupTest(inputData, expectedData);

            algorithm.NetworksToKeep = networksToKeep;
            algorithm.MutationRate = mutationRate;
            algorithm.MutationChance = mutationChance;

            Console.WriteLine("Initialization complete");

            while (true)
            {
                Console.WriteLine("Propagating");
                algorithm.PropagateAllNetworks();
                Console.WriteLine("Breeding");
                algorithm.BreedBestNetworks();
                PrintData(algorithm);
                Console.ReadLine();
            }
        }

        private static void PrintData(GeneticAlgorithm algorithm)
        {
            for (int i = 0; i < 10; ++i)
            {
                Console.WriteLine($"Fitness {algorithm.NetworksAndFitness.Values.ElementAt(i)}");
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
